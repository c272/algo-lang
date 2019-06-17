using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using MathNet.Numerics;

namespace Algo
{
    public partial class algoVisitor : algoBaseVisitor<object>
    {
        //Scopes collection for this instance.
        public static AlgoScopeCollection Scopes = new AlgoScopeCollection();

        //When the "Statement" node is visited.
        public override object VisitCompileUnit(algoParser.CompileUnitContext context)
        {
            //Visit the block.
            VisitBlock(context.block());
            return null;
        }

        //Visit each statement in turn.
        public override object VisitBlock([NotNull] algoParser.BlockContext context)
        {
            //Enumerate over all statements.
            foreach (var statement in context.statement())
            {
                VisitStatement(statement);
            }

            return null;
        }

        //When evaluating a statement, switch for type.
        public override object VisitStatement([NotNull] algoParser.StatementContext context)
        {
            //What type is it?
            if (context.stat_define() != null)
            {
                //Define statement.
                VisitStat_define(context.stat_define());
            }
            else if (context.stat_setvar() != null)
            {
                //Set a variable after its definition.
                VisitStat_setvar(context.stat_setvar());
            }
            else if (context.stat_deletevar() != null)
            {
                //Delete a variable from scope.
                VisitStat_deletevar(context.stat_deletevar());
            }
            else if (context.stat_if() != null)
            {
                //An "if" statement.
                VisitStat_if(context.stat_if());
            }
            else if (context.stat_forLoop() != null)
            {
                //..
            }
            else if (context.stat_functionCall() != null)
            {
                //A function call.
                VisitStat_functionCall(context.stat_functionCall());
            }
            else if (context.stat_functionDef() != null)
            {
                //A function definition.
                VisitStat_functionDef(context.stat_functionDef());
            }
            else if (context.stat_return() != null)
            {
                //Returning a value from a function call.
                return VisitStat_return(context.stat_return());
            }
            else if (context.stat_print() != null)
            {
                //A print statement.
                VisitStat_print(context.stat_print());
            }
            else if (context.stat_library() != null)
            {
                //Defining a library.
                VisitStat_library(context.stat_library());
            }
            else
            {
                Error.Fatal(context, "Syntax error, unrecognized statement.");
            }

            return null;
        }
    }
}
