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
            if (context.FILEPATH() != null)
            {
                importLoc = context.FILEPATH().GetText();
            } else
            {
                importLoc = context.IDENTIFIER()[0].GetText();
            }
            List<string> fileParts = importLoc.Split('/').ToList();

            //Append the extension to the end (imports don't require an extension).
            if (!fileParts[fileParts.Count - 1].EndsWith(".ag"))
            {
                fileParts[fileParts.Count - 1] = fileParts.Last() + ".ag";
            }


            //Test 1: Executing directory of the script.
            string[] dirParts = new string[] { Environment.CurrentDirectory }.Concat(fileParts).ToArray();
            string dirToCheck = CPFilePath.GetPlatformFilePath(dirParts);

            //Is it there?
            if (File.Exists(dirToCheck))
            {
                //Yes! Run the load function.
                RunAlgoScript(dirToCheck);
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
                RunAlgoScript(dirToCheck);
            }

            //Nope.
            //Test 3: Standard libraries.
            dirParts = new string[] { AppDomain.CurrentDomain.BaseDirectory, "std" }.Concat(fileParts).ToArray();
            dirToCheck = CPFilePath.GetPlatformFilePath(dirParts);

            //Is it there?
            if (File.Exists(dirToCheck))
            {
                //Yep, load it.
                RunAlgoScript(dirToCheck);
            }

            //No, nowhere else to check from, so throw a linking warning.
            Error.Warning(context, "Failed to link the Algo script '" + importLoc + "'. It has not been loaded.");
            return null;
        }

        //Runs an Algo script, given a file path.
        public void RunAlgoScript(string path)
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
            string oldFile = Program.FileLoaded;
            Program.FileLoaded = fi.Name;

            //Visit this tree, and fully execute.
            VisitCompileUnit(tree);

            //Set the currently loaded file back.
            Program.FileLoaded = oldFile;
        }
    }
}
