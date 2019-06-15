using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace Algo
{
    /// <summary>
    /// Contains all visitor nodes that handle checks or binary evaluation.
    /// </summary>
    public partial class algoVisitor : algoBaseVisitor<object>
    {
        //An "if" statement.
        public override object VisitStat_if([NotNull] algoParser.Stat_ifContext context)
        {
            //Evaluate the main if check.
            bool mainCheck = (bool)VisitCheck(context.check());
            if (mainCheck)
            {
                //Create scope.
                Scopes.AddScope();

                //Evaluate the main statement body.
                foreach (var statement in context.statement())
                {
                    VisitStatement(statement);
                }

                //Delete scope.
                Scopes.RemoveScope();

                //Return.
                return null;
            }

            //Maincheck failed, complete all the "else if" checks if they exist.
            if (context.stat_elif().Length != 0)
            {
                foreach (var elseifblock in context.stat_elif())
                {
                    //Does the check pass?
                    var checkContext = elseifblock.check();
                    bool elseifCheck = (bool)VisitCheck(checkContext);

                    if (elseifCheck)
                    {
                        //Create scope.
                        Scopes.AddScope();

                        //Evaluate statements.
                        foreach (var statement in elseifblock.statement())
                        {
                            VisitStatement(statement);
                        }

                        //Remove scope.
                        Scopes.RemoveScope();

                        //Return.
                        return null;
                    }
                }
            }

            //Is there an else?
            if (context.stat_else() != null)
            {
                //Create a scope.
                Scopes.AddScope();

                //Evaluate statements.
                foreach (var statement in context.stat_else().statement())
                {
                    VisitStatement(statement);
                }

                //Deleting scope.
                Scopes.RemoveScope();
            }

            //Returning.
            return null;
        }

        //Evaluate a check into a raw boolean (no value wrapper).
        public override object VisitCheck([NotNull] algoParser.CheckContext context)
        {
            //Is the check a single expression or multiple?
            if (context.check().Length != 0)
            {
                //Multiple.
                //Evaluate left and right expressions.
                AlgoValue left = (AlgoValue)VisitCheck(context.check()[0]);
                AlgoValue right = (AlgoValue)VisitCheck(context.check()[1]);

                //Switch on the operator, and evaluate.
                var op = context.check_operator();
                if (op.BIN_AND() != null)
                {
                    return AlgoComparators.AND(context, left, right);
                }
                else if (op.BIN_EQUALS() != null)
                {
                    return AlgoComparators._Equals(context, left, right);
                }
                else if (op.BIN_OR() != null)
                {
                    return AlgoComparators.OR(context, left, right);
                }
                else if (op.GRTR_THAN() != null)
                {
                    return AlgoComparators.GreaterThan(context, left, right, false);
                }
                else if (op.GRTR_THAN_ET() != null)
                {
                    return AlgoComparators.GreaterThan(context, left, right, true);
                }
                else if (op.LESS_THAN() != null)
                {
                    return AlgoComparators.LessThan(context, left, right, false);
                }
                else if (op.LESS_THAN_ET() != null)
                {
                    return AlgoComparators.LessThan(context, left, right, true);
                }
                else
                {
                    Error.Fatal(context, "Invalid comparison operator used, implemented in parser but not in visitor.");
                    return null;
                }
            }
            else
            {
                //Single.
                //Evaluate the expression.
                return (AlgoValue)VisitExpr(context.expr());
            }
        }
    }
}
