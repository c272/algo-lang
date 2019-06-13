using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using ExtendedNumerics;

namespace Algo
{
    public partial class storkVisitor : algoBaseVisitor<object>
    {
        //When an expression is parsed in Algo.
        public override object VisitExpr([NotNull] algoParser.ExprContext context)
        {
            //Is it an addition?
            if (context.ADD_OP() != null)
            {
                //Yes. Evaluate the left hand expression.
                AlgoValue left = (AlgoValue)VisitExpr(context.expr());

                //Evaluate the right hand term.
                AlgoValue right = (AlgoValue)VisitTerm(context.term());

                //Perform an add operation, based on type.
                //...
            }
            else if (context.TAKE_OP() != null)
            {
                //Take operation.
                //Evaluate the left hand expression.
                AlgoValue left = (AlgoValue)VisitExpr(context.expr());

                //Evaluate the right hand term.
                AlgoValue right = (AlgoValue)VisitTerm(context.term());

                //Perform a take operation, based on type. Djikstra
                //...
            }
            else
            {
                //No, just a term, evaluate.
                return (AlgoValue)VisitTerm(context.term());
            }

            //Error.
            throw new Exception("Invalid expression type.");
        }

        //When a single term is parsed in Algo.
        public override object VisitTerm([NotNull] algoParser.TermContext context)
        {
            //Is it a multiplication?
            if (context.MUL_OP() != null)
            {
                //Multiplication.
                //Evaluate the left side (term).
                AlgoValue left = (AlgoValue)VisitTerm(context.term());

                //Evaluate the right side.
                AlgoValue right = (AlgoValue)VisitFactor(context.factor());

                //Perform multiplication.
                //...
            }
            else if (context.DIV_OP() != null)
            {
                //Division.
                //Evaluate the left side (term).
                AlgoValue left = (AlgoValue)VisitTerm(context.term());

                //Evaluate the right side.
                AlgoValue right = (AlgoValue)VisitFactor(context.factor());

                //Perform division.
                //...
            }
            else
            {
                //No, just a factor, evaluate.
                return (AlgoValue)VisitFactor(context.factor());
            }

            //Error.
            throw new Exception("Invalid expression type.");
        }

        //When a single factor is parsed in Algo.
        public override object VisitFactor([NotNull] algoParser.FactorContext context)
        {
            //Is there a power?
            if (context.POW_OP() != null)
            {
                //Yes.
                //Evaluate left factor.
                AlgoValue left = (AlgoValue)VisitFactor(context.factor());

                //Evaluate right sub.
                AlgoValue right = (AlgoValue)VisitSub(context.sub());

                //Perform a power.
                return (AlgoValue)AlgoOperators.Pow(left, right);
            } else
            {
                //No, just evaluate the sub.
                return (AlgoValue)VisitSub(context.sub());
            }

            //Error.
            throw new Exception("Invalid expression type.");
        }

        //When a single sub is parsed in Algo.
        public override object VisitSub([NotNull] algoParser.SubContext context)
        {
            //Is this a raw value?
            if (context.value() != null)
            {
                //Yep, return an AlgoValue casted version of that.
                return (AlgoValue)VisitValue(context.value());
            } else
            {
                //No, it's another expression. Return the result of that.
                return (AlgoValue)VisitExpr(context.expr());
            }
        }

        //When a single value is visited in Algo.
        public override object VisitValue([NotNull] algoParser.ValueContext context)
        {
            //Check what type the value is.
            if (context.INTEGER() != null)
            {
                return new AlgoValue()
                {
                    Type = AlgoValueType.Integer,
                    Value = BigInteger.Parse(context.INTEGER().GetText()),
                    IsEnumerable = false
                };
            }
            else if (context.FLOAT() != null)
            {
                return new AlgoValue()
                {
                    Type = AlgoValueType.Float,
                    Value = BigFloat.Parse(context.FLOAT().GetText()),
                    IsEnumerable = false
                };
            }
            else if (context.STRING() != null)
            {
                return new AlgoValue()
                {
                    Type = AlgoValueType.String,
                    Value = context.STRING().GetText().Substring(1, context.STRING().GetText().Length - 2),
                    IsEnumerable = false
                };
            }
            else if (context.RATIONAL() != null)
            {
                //Get the two integer halves from the rational.
                string[] halves = context.RATIONAL().GetText().Split('/');
                string numerator = halves[0];
                string denominator = halves[1];

                return new AlgoValue()
                {
                    Type = AlgoValueType.Rational,
                    Value = new BigRational(BigInteger.Zero, new Fraction(BigInteger.Parse(numerator), BigInteger.Parse(denominator))),
                    IsEnumerable = false
                };
            } else
            {
                //No proper detected value type.
                Error.Fatal(context, "Unknown or invalid type given for value.");
                return null;
            }
        }
    }
}
