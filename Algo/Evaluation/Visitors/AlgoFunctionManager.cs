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
        //When a function is defined.
        public override object VisitStat_functionDef([NotNull] algoParser.Stat_functionDefContext context)
        {
            //Does a function with this name already exist?
        }
    }
}