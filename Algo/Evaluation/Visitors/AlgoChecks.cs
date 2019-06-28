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
            //Evaluate the check.
            AlgoValue checkReturned = (AlgoValue)VisitCheck(context.check());
            bool mainCheck = AlgoComparators.GetBooleanValue(checkReturned, context);
            
            //Did it pass? If so, eval the if.
            if (mainCheck)
            {
                //Create scope.
                Scopes.AddScope();

                //Evaluate the main statement body.
                foreach (var statement in context.statement())
                {
                    AlgoValue returned = (AlgoValue)VisitStatement(statement);
                    if (returned != null)
                    {
                        return returned;
                    }
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
                    AlgoValue elifCheckReturned = (AlgoValue)VisitCheck(checkContext);
                    bool elseifCheck = AlgoComparators.GetBooleanValue(elifCheckReturned, context);

                    if (elseifCheck)
                    {
                        //Create scope.
                        Scopes.AddScope();

                        //Evaluate statements.
                        foreach (var statement in elseifblock.statement())
                        {
                            AlgoValue returned = (AlgoValue)VisitStatement(statement);
                            if (returned != null)
                            {
                                return returned;
                            }
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
                    AlgoValue returned = (AlgoValue)VisitStatement(statement);
                    if (returned != null)
                    {
                        return returned;
                    }
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
            //Switch for the type of check.
            if (context.BIN_AND() != null)
            {
                //Binary AND.
                //Evaluate the left and right values.
                AlgoValue left = (AlgoValue)VisitCheck(context.check());
                AlgoValue right = (AlgoValue)VisitCheckfrag(context.checkfrag());

                //Return the comparison result of these two (as AlgoValue).
                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = AlgoComparators.AND(context, left, right),
                    
                };
            }
            else if (context.BIN_OR() != null)
            {
                //Binary OR.
                //Evaluate the left and right values.
                AlgoValue left = (AlgoValue)VisitCheck(context.check());
                AlgoValue right = (AlgoValue)VisitCheckfrag(context.checkfrag());

                //Return an OR on these values.
                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = AlgoComparators.OR(context, left, right),
                    
                };
            }
            else if (context.LBRACKET() != null)
            {
                //Bracketed check.
                return (AlgoValue)VisitCheck(context.check());
            }
            else if (context.INVERT_SYM() != null)
            {
                //Boolean invert.
                AlgoValue evaled = (AlgoValue)VisitCheck(context.check());

                //Get the boolean value for the evaluated value.
                bool evaledBool = AlgoComparators.GetBooleanValue(evaled, context);

                //Return the inverted bool.
                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = !evaledBool,
                    
                };
            }
            else
            {
                //Just a checkfrag. Evaluate and return.
                return (AlgoValue)VisitCheckfrag(context.checkfrag());
            }
        }

        //Evaluate the check fragment for the full check.
        public override object VisitCheckfrag([NotNull] algoParser.CheckfragContext context)
        {
            //Evaluate the expressions on the left and right, depending on the operator.
            if (context.GRTR_THAN() != null)
            {
                // Binary greater than.
                //Evaluate both the expressions.
                AlgoValue left = (AlgoValue)VisitExpr(context.expr()[0]);
                AlgoValue right = (AlgoValue)VisitExpr(context.expr()[1]);

                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = AlgoComparators.GreaterThan(context, left, right, false),
                    
                };
            }
            else if (context.GRTR_THAN_ET() != null)
            {
                // Binary greater than or equal to.
                //Evaluate both the expressions.
                AlgoValue left = (AlgoValue)VisitExpr(context.expr()[0]);
                AlgoValue right = (AlgoValue)VisitExpr(context.expr()[1]);

                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = AlgoComparators.GreaterThan(context, left, right, true),
                    
                };
            }
            else if (context.LESS_THAN() != null)
            {
                // Binary less than.
                //Evaluate both the expressions.
                AlgoValue left = (AlgoValue)VisitExpr(context.expr()[0]);
                AlgoValue right = (AlgoValue)VisitExpr(context.expr()[1]);

                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = AlgoComparators.LessThan(context, left, right, false),
                    
                };
            }
            else if (context.LESS_THAN_ET() != null)
            {
                // Binary less than or equal to.
                //Evaluate both the expressions.
                AlgoValue left = (AlgoValue)VisitExpr(context.expr()[0]);
                AlgoValue right = (AlgoValue)VisitExpr(context.expr()[1]);

                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = AlgoComparators.LessThan(context, left, right, true),
                    
                };
            }
            else if (context.BIN_EQUALS() != null)
            {
                //Binary EQUALS.
                //Evaluate the left and right values.
                AlgoValue left = (AlgoValue)VisitExpr(context.expr()[0]);
                AlgoValue right = (AlgoValue)VisitExpr(context.expr()[1]);

                //Return whether these two are equal, as AlgoValue.
                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = AlgoComparators._Equals(context, left, right),
                    
                };
            }
            else if (context.BIN_NET() != null)
            {
                //Binary not equal to.
                //Evaluate the left and right values.
                AlgoValue left = (AlgoValue)VisitExpr(context.expr()[0]);
                AlgoValue right = (AlgoValue)VisitExpr(context.expr()[1]);

                //Return whether these two are equal, as AlgoValue.
                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = !AlgoComparators._Equals(context, left, right),
                    
                };
            }
            else
            {
                //Only a single value, so evaluate and return.
                return (AlgoValue)VisitExpr(context.expr()[0]);
            }
        }
    }
}
