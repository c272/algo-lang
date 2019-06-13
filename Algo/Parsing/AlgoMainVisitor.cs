using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using MathNet.Numerics;

namespace Algo
{
    public partial class storkVisitor : algoBaseVisitor<object>
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
            else if (context.stat_forLoop() != null)
            {

            }
            else if (context.stat_functionCall() != null)
            {

            }
            else if (context.stat_functionDef() != null)
            {

            }
            else
            {

            }

            return null;
        }
    }
}
