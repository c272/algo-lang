using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace Algo
{
    public partial class storkVisitor : algoBaseVisitor<object>
    {
        //When a variable is first defined.
        public override object VisitStat_define([NotNull] algoParser.Stat_defineContext context)
        {
            //Evaluate the expression on the right side of the define.
            AlgoValue value = (AlgoValue)VisitExpr(context.expr());

            return null;
        }
    }
}
