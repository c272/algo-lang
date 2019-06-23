using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            AlgoValue toLoopOver = (AlgoValue)VisitValue(context.value());
            if (!toLoopOver.IsEnumerable)
            {
                Error.Fatal(context, "Cannot loop over a value that is not enumerable.");
                return null;
            }

            //Get the list to be enumerated over.
            List<AlgoValue> loopList = (List<AlgoValue>)toLoopOver.Value;
            
            //Enumerate all statements for n times, where n is the length of the list.
            for (int i=0; i<loopList.Count; i++)
            {
                //Creating a new scope.
                Scopes.AddScope();

                //Setting the index variable in the scope under the given name.
                Scopes.AddVariable(context.IDENTIFIER().GetText(), new AlgoValue()
                {
                    Type = AlgoValueType.Integer,
                    Value = new BigInteger(i),
                    IsEnumerable = false
                });

                //Executing all statements in loop.
                foreach (var statement in context.statement())
                {
                    VisitStatement(statement);
                }

                //Destroying the scope.
                Scopes.RemoveScope();
            }

            //Done evaluating.
            return null;
        }

        //A single "while" loop in Algo.
        public override object VisitStat_whileLoop([NotNull] algoParser.Stat_whileLoopContext context)
        {
            //Forever!
            while (true)
            {
                //Evaluate check, see if it's true. If not, break out the loop.
                AlgoValue whileCheckRes = (AlgoValue)VisitCheck(context.check());
                bool checkPassed = AlgoComparators.GetBooleanValue(whileCheckRes, context);

                //Did it pass? If not, break.
                if (!checkPassed)
                {
                    break;
                }

                //Loop over all the statements.
                foreach (var statement in context.statement())
                {
                    VisitStatement(statement);
                }
            }

            return null;
        }
    }
}