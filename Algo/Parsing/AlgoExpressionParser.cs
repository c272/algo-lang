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
    public partial class algoVisitor : algoBaseVisitor<object>
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
                return AlgoOperators.Add(context, left, right);
            }
            else if (context.TAKE_OP() != null)
            {
                //Take operation.
                //Evaluate the left hand expression.
                AlgoValue left = (AlgoValue)VisitExpr(context.expr());

                //Evaluate the right hand term.
                AlgoValue right = (AlgoValue)VisitTerm(context.term());

                //Perform a take operation, based on type.
                return AlgoOperators.Sub(context, left, right);
            }
            else
            {
                //No, just a term, evaluate.
                return (AlgoValue)VisitTerm(context.term());
            }
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
                return AlgoOperators.Mul(context, left, right);
            }
            else if (context.DIV_OP() != null)
            {
                //Division.
                //Evaluate the left side (term).
                AlgoValue left = (AlgoValue)VisitTerm(context.term());

                //Evaluate the right side.
                AlgoValue right = (AlgoValue)VisitFactor(context.factor());

                //Perform division.
                return AlgoOperators.Div(context, left, right);
            }
            else if (context.MOD_OP() != null)
            {
                //Modulus.
                AlgoValue left = (AlgoValue)VisitTerm(context.term());
                AlgoValue right = (AlgoValue)VisitFactor(context.factor());

                //Perform modulus.
                return AlgoOperators.Mod(context, left, right);
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
                return AlgoOperators.Pow(context, left, right);
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
                //INTEGER
                return new AlgoValue()
                {
                    Type = AlgoValueType.Integer,
                    Value = BigInteger.Parse(context.INTEGER().GetText()),

                };
            }
            else if (context.FLOAT() != null)
            {
                //FLOAT
                return new AlgoValue()
                {
                    Type = AlgoValueType.Float,
                    Value = BigFloat.Parse(context.FLOAT().GetText()),

                };
            }
            else if (context.STRING() != null)
            {
                //STRING
                return new AlgoValue()
                {
                    Type = AlgoValueType.String,
                    Value = context.STRING().GetText().Substring(1, context.STRING().GetText().Length - 2)
                                    //text escape codes
                                   .Replace("\\n", "\n")
                                   .Replace("\\t", "\t")
                                   .Replace("\\r", "\r")
                                   .Replace("\\q", "\"")
                };
            }
            else if (context.RATIONAL() != null)
            {
                //RATIONAL
                //Get the two integer halves from the rational.
                string[] halves = context.RATIONAL().GetText().Split('/');
                string numerator = halves[0];
                string denominator = halves[1];

                return new AlgoValue()
                {
                    Type = AlgoValueType.Rational,
                    Value = new BigRational(BigInteger.Zero, new Fraction(BigInteger.Parse(numerator), BigInteger.Parse(denominator))),
                };
            }
            else if (context.BOOLEAN() != null)
            {
                //BOOLEAN
                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = (context.BOOLEAN().GetText() == "true"),

                };
            }
            else if (context.HEX() != null)
            {
                //BYTES
                return new AlgoValue()
                {
                    Type = AlgoValueType.Bytes,
                    Value = context.HEX().GetText().ToByteArray()
                };
            }
            else if (context.NULL() != null)
            {
                //NULL VALUE
                return new AlgoValue()
                {
                    Type = AlgoValueType.Null,
                    Value = null,

                };
            }
            else if (context.IDENTIFIER() != null)
            {
                //VARIABLE/OBJECT
                return Particles.ParseParticleBlock(this, context, context.IDENTIFIER(), context.particle());
            }
            else if (context.stat_functionCall() != null)
            {
                //FUNCTION CALL
                AlgoValue value = (AlgoValue)VisitStat_functionCall(context.stat_functionCall());
                if (value != null)
                {
                    return value;
                } else
                {
                    //Return a null value.
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Null,
                        Value = null,

                    };
                }
            }
            else if (context.array() != null)
            {
                //LIST.

                //Evaluate all list items as expressions.
                List<AlgoValue> list = new List<AlgoValue>();
                foreach (var item in context.array().value())
                {
                    list.Add((AlgoValue)VisitValue(item));
                }

                //Return as a single value.
                return new AlgoValue()
                {
                    Type = AlgoValueType.List,
                    Value = list
                };
            }
            else if (context.@object() != null)
            {
                //OBJECT DEFINITION

                //Create a new object.
                AlgoObject toReturn = new AlgoObject();

                //Anything to enumerate?
                if (context.@object().obj_child_definitions() == null)
                {
                    //Nope.
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Object,
                        Value = toReturn
                    };
                }

                //Enumerate through all the define statements and evaluate their values.
                foreach (var value in context.@object().obj_child_definitions().obj_vardefine())
                {
                    //Check if the variable already exists.
                    if (toReturn.ObjectScopes.VariableExists(value.IDENTIFIER().GetText()))
                    {
                        Error.Fatal(context, "The variable with name '" + value.IDENTIFIER().GetText() + "' is defined twice or more in an object.");
                        return null;
                    }

                    //Evaluate the value on the right.
                    AlgoValue evaluated = (AlgoValue)VisitExpr(value.expr());

                    //Add the variable to scope.
                    toReturn.ObjectScopes.AddVariable(value.IDENTIFIER().GetText(), evaluated);
                }

                //Enumerate through all functions and define them.
                foreach (var value in context.@object().obj_child_definitions().obj_funcdefine())
                {
                    //Check if the variable already exists.
                    if (toReturn.ObjectScopes.VariableExists(value.IDENTIFIER().GetText()))
                    {
                        Error.Fatal(context, "The variable with name '" + value.IDENTIFIER().GetText() + "' is defined twice or more in an object.");
                        return null;
                    }

                    //Create a list of parameters.
                    List<string> params_ = new List<string>();
                    if (value.abstract_params() != null)
                    {
                        foreach (var param in value.abstract_params().IDENTIFIER())
                        {
                            //Check if param already exists.
                            if (params_.Contains(param.GetText()))
                            {
                                Error.Fatal(context, "The parameter with name '" + param.GetText() + "' is already defined in the function.");
                                return null;
                            }

                            params_.Add(param.GetText());
                        }
                    }

                    //Create a function, push.
                    AlgoFunction func = new AlgoFunction(value.statement().ToList(), params_, value.IDENTIFIER().GetText());
                    AlgoValue funcValue = new AlgoValue()
                    {
                        Type = AlgoValueType.Function,
                        Value = func,

                    };

                    toReturn.ObjectScopes.AddVariable(value.IDENTIFIER().GetText(), funcValue);
                }

                //Enumerate all external functions defined.
                foreach (var ext in context.@object().obj_child_definitions().obj_externdefine())
                {
                    //Get the value of the function.
                    var loadFuncStat = ext.stat_loadFuncExt();
                    AlgoValue func = Plugins.GetEmulatedFuncValue(loadFuncStat);

                    //Check if a variable with this name already exists.
                    if (toReturn.ObjectScopes.VariableExists(loadFuncStat.IDENTIFIER()[0].GetText()))
                    {
                        Error.Fatal(context, "A variable with the name '" + loadFuncStat.IDENTIFIER()[0].GetText() + "' already exists in this object. Cannot duplicate.");
                        return null;
                    }

                    //Add that function to the return object.
                    toReturn.ObjectScopes.AddVariable(loadFuncStat.IDENTIFIER()[0].GetText(), func);
                }

                //Return the object.
                return new AlgoValue()
                {
                    Type = AlgoValueType.Object,
                    Value = toReturn
                };
            }
            else
            {
                //No proper detected value type.
                Error.Fatal(context, "Unknown or invalid type given for value.");
                return new AlgoValue()
                {
                    Type = AlgoValueType.Null,
                    Value = null
                };
            }
        }
    }
}
