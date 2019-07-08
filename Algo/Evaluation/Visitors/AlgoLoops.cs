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

            //Is it a literal list being looped over, or just a range?
            if (context.UP_SYM() != null)
            {
                //Just a range. Evaluate the value to be stretched to.
                AlgoValue limit = (AlgoValue)VisitExpr(context.expr());
                if (limit.Type != AlgoValueType.Integer)
                {
                    Error.Fatal(context, "Range limit must be an integer, cannot be type" + limit.Type.ToString() + ".");
                    return null;
                }
                if ((BigInteger)limit.Value < 0)
                {
                    Error.Fatal(context, "Range cannot be below 0.");
                    return null;
                }

                //Evaluate all statements for an initial value up to the given value.
                BigInteger curIndex = 0;
                BigInteger maxIndex = (BigInteger)limit.Value;
                while (curIndex <= maxIndex)
                {
                    //Create a scope.
                    Scopes.AddScope();

                    //Add the index to the scope.
                    Scopes.AddVariable(indexName, new AlgoValue()
                    {
                        Type = AlgoValueType.Integer,
                        Value = curIndex
                    });

                    //Enumerate all statements.
                    foreach (var statement in context.statement())
                    {
                        AlgoValue returned = (AlgoValue)VisitStatement(statement);
                        if (returned != null)
                        {
                            //Delete scope, we're done with looping.
                            Scopes.RemoveScope();

                            //Check if it's a break.
                            if (returned == AlgoValue.Break) { return null; }

                            //No, return normally.
                            return returned;
                        }
                    }

                    //Remove the scope.
                    Scopes.RemoveScope();

                    //Increment index.
                    curIndex++;
                }

                return null;
            }

            //It's a loop, not a range.
            //Evaluating the value for the for loop body.
            AlgoValue toLoopOver = (AlgoValue)VisitExpr(context.expr());
            if (toLoopOver.Type != AlgoValueType.List && toLoopOver.Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Cannot loop over a value that is not enumerable.");
                return null;
            }

            //Get the length to be enumerated over.
            int loopLimit = -1;
            List<AlgoValue> literalValues = new List<AlgoValue>();
            if (toLoopOver.Type == AlgoValueType.List) 
            {
                List<AlgoValue> loopList = (List<AlgoValue>)toLoopOver.Value;
                loopLimit = loopList.Count;
                literalValues = loopList;
            } else 
            {
                loopLimit = ((string)toLoopOver.Value).Length;

                //If it's a foreach, we need to set literal values for the string,
                //so they can be looped over.
                if (context.FOREACH_SYM() != null)
                {
                    foreach (var char_ in (string)toLoopOver.Value)
                    {
                        literalValues.Add(new AlgoValue()
                        {
                            Type = AlgoValueType.String,
                            Value = new string(new char[] { char_ })
                        });
                    }
                }
            }

            
            //Enumerate all statements for n times, where n is the length of the list.
            for (int i=0; i<loopLimit; i++)
            {
                //Creating a new scope.
                Scopes.AddScope();

                //Setting the index variable in the scope under the given name.
                //If it's a foreach loop, then use the literal value instead of the index.
                if (context.FOREACH_SYM() != null)
                {
                    Scopes.AddVariable(context.IDENTIFIER().GetText(), literalValues[i]);
                }
                else
                {
                    Scopes.AddVariable(context.IDENTIFIER().GetText(), new AlgoValue()
                    {
                        Type = AlgoValueType.Integer,
                        Value = new BigInteger(i),

                    });
                }

                //Executing all statements in loop.
                foreach (var statement in context.statement())
                {
                    //For statements executing other statements, it must be remembered to return results from
                    //those statements.
                    AlgoValue returned = (AlgoValue)VisitStatement(statement);
                    if (returned != null)
                    {
                        //Delete scope, we're done with looping.
                        Scopes.RemoveScope();

                        //Check if it's a break.
                        if (returned == AlgoValue.Break) { return null; }

                        //No, return normally.
                        return returned;
                    }
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

                //Create a scope.
                Scopes.AddScope();

                //Loop over all the statements.
                foreach (var statement in context.statement())
                {
                    AlgoValue returned = (AlgoValue)VisitStatement(statement);
                    if (returned != null)
                    {
                        //Delete scope, we're done with looping.
                        Scopes.RemoveScope();

                        //Check if it's a break.
                        if (returned == AlgoValue.Break) { return null; }

                        //No, return normally.
                        return returned;
                    }
                }

                //Destroy the scope.
                Scopes.RemoveScope();
            }

            return null;
        }

        //A break statement in Algo.
        public override object VisitStat_break([NotNull] algoParser.Stat_breakContext context)
        {
            return AlgoValue.Break;
        }
    }
}