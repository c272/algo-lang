using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace Algo
{
    /// <summary>
    /// All loops in Algo are contained within this file.
    /// </summary>
    public partial class algoVisitor : algoBaseVisitor<object>
    {
        //A single for loop in Algo.
        public override object VisitStat_forLoop([NotNull] algoParser.Stat_forLoopContext context)
        {
            //Getting the name of the variable to declare in scope as the index.
            string indexName = context.IDENTIFIER().GetText();

            //Evaluating the value for the for loop body.
            //...
        }
    }
}