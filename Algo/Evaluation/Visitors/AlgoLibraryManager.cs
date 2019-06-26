using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using System.IO;
using Antlr4.Runtime;

namespace Algo
{
    public partial class algoVisitor : algoBaseVisitor<object>
    {
        //When a library is defined.
        public override object VisitStat_library([NotNull] algoParser.Stat_libraryContext context)
        {
            //Check if a library with this name already exists.
            if (Scopes.LibraryExists(context.IDENTIFIER().GetText()))
            {
                Error.Warning(context, "A library with the name '" + context.IDENTIFIER().GetText() + "' has already been loaded, so loading was ignored.");
            }

            //Switch the current scope out for a new "library" scope.
            AlgoScopeCollection oldScope = Scopes;
            Scopes = new AlgoScopeCollection();

            //Evaluate the contents of the library.
            foreach (var statement in context.statement())
            {
                VisitStatement(statement);
            }

            //Save the new scope, and switch back to the old one.
            AlgoScopeCollection libScope = Scopes;
            Scopes = oldScope;

            //Add the library scope to the library list.
            Scopes.AddLibrary(context.IDENTIFIER().GetText(), libScope);
            return null;
        }

        //When a file is imported.
        public override object VisitStat_import([NotNull] algoParser.Stat_importContext context)
        {
            //The following are checked for the parent library, in order:
            //1. Executing directory of the script + whatever referenced folder path.
            //2. Packages directory for Algo.
            //3. Standard libraries.

            //Getting directory tree text.
            string importLoc = "";
            
            //Evaluating the statement to get dir text.
            AlgoValue locVal = (AlgoValue)VisitExpr(context.expr());
            if (locVal.Type != AlgoValueType.String) 
            {
                Error.Fatal(context, "Given file path to import was not a string.");
                return null;
            }
            importLoc = (string)locVal.Value;

            List<string> fileParts = importLoc.Split('/').ToList();

            //Append the extension to the end (imports don't require an extension).
            if (!fileParts[fileParts.Count - 1].EndsWith(".ag"))
            {
                fileParts[fileParts.Count - 1] = fileParts.Last() + ".ag";
            }

            //Is the import being placed into a different scope?
            string importScope = "";
            if (context.AS_SYM() != null) 
            {
                //Yes, get the name of the scope.
                importScope = context.IDENTIFIER().GetText();
            }

            //Test 1: Executing directory of the script.
            string[] dirParts = new string[] { Environment.CurrentDirectory }.Concat(fileParts).ToArray();
            string dirToCheck = CPFilePath.GetPlatformFilePath(dirParts);

            //Is it there?
            if (File.Exists(dirToCheck))
            {
                //Yes! Run the load function.
                RunAlgoScript(dirToCheck, importScope);
                return null;
            }

            //Nope.
            //Test 2: Packages directory for Algo.
            dirParts = new string[] { AppDomain.CurrentDomain.BaseDirectory, "packages" }.Concat(fileParts).ToArray();
            dirToCheck = CPFilePath.GetPlatformFilePath(dirParts);

            //Is it there?
            if (File.Exists(dirToCheck))
            {
                //Yep, load it.
                RunAlgoScript(dirToCheck, importScope);
                return null;
            }

            //Nope.
            //Test 3: Standard libraries.
            dirParts = new string[] { AppDomain.CurrentDomain.BaseDirectory, "std" }.Concat(fileParts).ToArray();
            dirToCheck = CPFilePath.GetPlatformFilePath(dirParts);

            //Is it there?
            if (File.Exists(dirToCheck))
            {
                //Yep, load it.
                RunAlgoScript(dirToCheck, importScope);
                return null;
            }

            //No, nowhere else to check from, so throw a linking warning.
            Error.Warning(context, "Failed to link the Algo script '" + importLoc + "'. It has not been loaded.");
            return null;
        }

        //Runs an Algo script, given a file path.
        public void RunAlgoScript(string path, string newScopeName="")
        {
            //Read the entire text file into a lexer and tokens.
            string input = File.ReadAllText(path);
            var chars = new AntlrInputStream(input);
            var lexer = new algoLexer(chars);
            var tokens = new CommonTokenStream(lexer);

            //Parse the file.
            var parser = new algoParser(tokens);
            parser.BuildParseTree = true;
            var tree = parser.compileUnit();

            //Set the currently loaded file.
            FileInfo fi = new FileInfo(path);
            string oldFile = AlgoRuntimeInformation.FileLoaded;
            AlgoRuntimeInformation.FileLoaded = fi.Name;

            //If this is being placed in a separate scope, switch out now.
            AlgoScopeCollection oldScope = null;
            if (newScopeName != "") 
            {
                oldScope = Scopes;
                Scopes = new AlgoScopeCollection();
            }

            //Visit this tree, and fully execute.
            VisitCompileUnit(tree);

            //Set the currently loaded file back.
            AlgoRuntimeInformation.FileLoaded = oldFile;

            //If it was executed in a separate scope, save as a library with this name.
            if (newScopeName != "")
            {
                AlgoScopeCollection importScope = Scopes;
                Scopes = oldScope;
                Scopes.AddLibrary(newScopeName, importScope);
            }
        }

        //When an external or internal plugin library function is loaded.
        public override object VisitStat_loadFuncExt([NotNull] algoParser.Stat_loadFuncExtContext context)
        {
            //Get the value of the function.
            AlgoValue func = Plugins.GetEmulatedFuncValue(context);

            //Check if a variable with the supplied name already exists in scope.
            if (Scopes.VariableExists(context.IDENTIFIER().GetText()))
            {
                Error.Fatal(context, "A variable with the name '" + context.IDENTIFIER().GetText() + "' already exists, can't redefine it.");
                return null;
            }

            //Add to scope.
            Scopes.AddVariable(context.IDENTIFIER().GetText(), func);
            return null;
        }
    }
}
