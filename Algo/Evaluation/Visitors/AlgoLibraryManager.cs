using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

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
    }
}
