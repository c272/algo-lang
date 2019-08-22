using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace Algo
{
    /// <summary>
    /// Contains all the function management visitor nodes.
    /// </summary>
    public partial class algoVisitor : algoBaseVisitor<object>
    {
        //When a try/catch block is executed.
        public override object VisitStat_try_catch([NotNull] algoParser.Stat_try_catchContext context)
        {
            //Turn on execution catch mode.
            AlgoRuntimeInformation.CatchExceptions = true;

            //Loop over the statements, execute all.
            bool foundError = false;
            string errorMessage = "";

            Scopes.AddScope();
            foreach (var statement in context.block()[0].statement())
            {
                //Execute statement.
                AlgoValue returned = null;

                try
                {
                    returned = (AlgoValue)VisitStatement(statement);
                }
                catch(Exception e)
                {
                    //C# error somewhere up the heirarchy, 90% means an error has been thrown.
                    foundError = true;
                    errorMessage = "Internal Language Error: " + e.Message;
                }

                //Check if an error has been found.
                if (AlgoRuntimeInformation.ExceptionCaught())
                {
                    //Yep, break out to the catch block.
                    foundError = true;
                    errorMessage = AlgoRuntimeInformation.GetExceptionMessage();
                    AlgoRuntimeInformation.CatchExceptions = false;
                    break;
                }

                //If something was actually returned, return it up.
                if (returned != null)
                {
                    AlgoRuntimeInformation.CatchExceptions = false;
                    Scopes.RemoveScope();
                    return returned;
                }
            }
            Scopes.RemoveScope();

            //Turn off error catching.
            AlgoRuntimeInformation.CatchExceptions = false;

            //Done executing try statements, did an error come back?
            if (!foundError)
            {
                return null;
            }

            //Yep, get the identifier to use for the error message, check it doesn't exist.
            if (Scopes.VariableExists(context.IDENTIFIER().GetText()))
            {
                Error.Fatal(context, "A variable already exists named '" + context.IDENTIFIER().GetText() + "', cannot use for catch parameter.");
                return null;
            }

            //Create it, assign it the caught value.
            Scopes.AddScope();
            Scopes.AddVariable(context.IDENTIFIER().GetText(), new AlgoValue()
            {
                Type = AlgoValueType.String,
                Value = errorMessage
            }, context);

            //Execute what's in the catch scope.
            foreach (var statement in context.block()[1].statement())
            {
                AlgoValue returned = (AlgoValue)VisitStatement(statement);
                if (returned != null)
                {
                    Scopes.RemoveScope();
                    return returned;
                }
            }

            //Finished!
            return null;
        }

        //When an error is manually thrown by a user.
        public override object VisitStat_throw([NotNull] algoParser.Stat_throwContext context)
        {
            //Evaluate the expression.
            AlgoValue throwStr = (AlgoValue)VisitExpr(context.expr());

            //Is it a string?
            if (throwStr.Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Error message to throw must be a string.");
                return null;
            }

            //Throw the error.
            Error.Fatal(context, (string)throwStr.Value);
            return null;
        }
    }
}