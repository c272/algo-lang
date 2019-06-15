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
        //When a variable is first defined.
        public override object VisitStat_define([NotNull] algoParser.Stat_defineContext context)
        {
            //Check if the variable already exists.
            if (Scopes.VariableExists(context.IDENTIFIER().GetText()))
            {
                Error.Fatal(context, "A variable with the name '" + context.IDENTIFIER().GetText() + "' already exists, cannot create a duplicate.");
                return null;
            }

            //Evaluate the expression on the right side of the define.
            AlgoValue value = (AlgoValue)VisitExpr(context.expr());

            //Create the variable at the current scope.
            Scopes.AddVariable(context.IDENTIFIER().GetText(), value);
            return null;
        }

        //When a variable's value is changed.
        public override object VisitStat_setvar([NotNull] algoParser.Stat_setvarContext context)
        {
            //Check if the variable already exists.
            if (!Scopes.VariableExists(context.IDENTIFIER().GetText()))
            {
                Error.Fatal(context, "A variable with the name '" + context.IDENTIFIER().GetText() + "' does not exist, cannot set value.");
                return null;
            }

            //Does, evaluate the expression to set the value.
            AlgoValue value = (AlgoValue)VisitExpr(context.expr());

            //Set variable.
            Scopes.SetVariable(context.IDENTIFIER().GetText(), value);
            return null;
        }
    }
}
