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
                    IsEnumerable = false
                };
            }
            else if (context.FLOAT() != null)
            {
                //FLOAT
                return new AlgoValue()
                {
                    Type = AlgoValueType.Float,
                    Value = BigFloat.Parse(context.FLOAT().GetText()),
                    IsEnumerable = false
                };
            }
            else if (context.STRING() != null)
            {
                //STRING
                return new AlgoValue()
                {
                    Type = AlgoValueType.String,
                    Value = context.STRING().GetText().Substring(1, context.STRING().GetText().Length - 2).Replace("\\n", "\n"),
                    IsEnumerable = false
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
                    IsEnumerable = false
                };
            }
            else if (context.BOOLEAN() != null)
            {
                //BOOLEAN
                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = (context.BOOLEAN().GetText() == "true"),
                    IsEnumerable = false
                };
            }
            else if (context.NULL() != null)
            {
                //NULL VALUE
                return new AlgoValue()
                {
                    Type = AlgoValueType.Null,
                    Value = null,
                    IsEnumerable = false
                };
            }
            else if (context.IDENTIFIER() != null)
            {
                //VARIABLE
                //Check if the variable exists in scope.
                if (!Scopes.VariableExists(context.IDENTIFIER().GetText()))
                {
                    Error.Fatal(context, "No variable exists named '" + context.IDENTIFIER().GetText() + "'.");
                    return null;
                }

                //It does, return.
                return Scopes.GetVariable(context.IDENTIFIER().GetText());
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
                        IsEnumerable = false
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
                    Value = list,
                    IsEnumerable = true
                };
            }
            else if (context.array_access() != null)
            {
                //ACCESSED ARRAY VALUE

                //Get the root value.
                if (!Scopes.VariableExists(context.array_access().IDENTIFIER().GetText()))
                {
                    Error.Fatal(context, "An enumerable variable with name '" + context.array_access().IDENTIFIER().GetText() + "' does not exist.");
                    return null;
                }

                //Arrays can be more than 1D, and are separated using commas:
                // x[3, 4, 5]
                //So, this steps through till the very bottom value is found.
                var currentValue = Scopes.GetVariable(context.array_access().IDENTIFIER().GetText());
                foreach (var accessIndexExpr in context.array_access().literal_params().expr())
                {
                    //Already found the end, but still going.
                    if (currentValue.IsEnumerable == false)
                    {
                        Error.Fatal(context, "Attempting to index into a value that isn't enumerable.");
                        return null;
                    }

                    //Evaluate the index expression.
                    AlgoValue accessIndex = (AlgoValue)VisitExpr(accessIndexExpr);
                    if (accessIndex.Type != AlgoValueType.Integer)
                    {
                        Error.Fatal(context, "Access index expression did not return an integer.");
                        return null;
                    }

                    if ((BigInteger)accessIndex.Value > int.MaxValue)
                    {
                        Error.Fatal(context, "Cannot access index in array, the index is too large.");
                        return null;
                    }

                    //Getting an integer representation of the index.
                    int accessIndexInt = int.Parse(((BigInteger)accessIndex.Value).ToString());

                    //Is the index too large?
                    if (accessIndexInt > ((List<AlgoValue>)currentValue.Value).Count) 
                    {
                        Error.Fatal(context, "Cannot access index in array, index out of bounds.");
                        return null;
                    }

                    //Get the index of the current value.
                    try
                    {
                        currentValue = ((List<AlgoValue>)currentValue.Value)[accessIndexInt];
                    }
                    catch
                    {
                        //Not a list, can't index into it.
                        Error.Fatal(context, "Cannot index into an item that is not enumerable.");
                    }
                }

                return currentValue;
            }
            else if (context.obj_access() != null)
            {
                //OBJECT VALUE ACCESS
                //Could be a user-defined object or a library.

                //Check for user defined object first.
                var ids = context.obj_access().IDENTIFIER();
                if (Scopes.VariableExists(ids[0].GetText()))
                {
                    //USER DEFINED OBJECT!

                    //Get the parent object, check that the type is actually an object.
                    var objValue = Scopes.GetVariable(ids[0].GetText());
                    if (objValue.Type != AlgoValueType.Object)
                    {
                        Error.Fatal(context, "You attempted to get a child from value '" + ids[0].GetText() + ", but it's not an object.");
                        return null;
                    }

                    //Getting the object.
                    AlgoObject currentObj = (AlgoObject)objValue.Value;

                    //Step through object children until scope found.
                    for (var i=1; i<ids.Length-1; i++)
                    {
                        if (!currentObj.ObjectScopes.VariableExists(ids[i].GetText())) {
                            Error.Fatal(context, "You attempted to get child '" + ids[i].GetText() + " from an object, but it was not found.");
                            return null;
                        }

                        //Get the variable.
                        var childValue = currentObj.ObjectScopes.GetVariable(ids[i].GetText());
                        //Check it's an object.
                        if (childValue.Type != AlgoValueType.Object)
                        {
                            Error.Fatal(context, "You attempted to get a child from value '" + ids[0].GetText() + ", but it's not an object.");
                            return null;
                        }

                        currentObj = (AlgoObject)childValue.Value;
                    }

                    //Get the value from this final object scope.
                    if (!currentObj.ObjectScopes.VariableExists(ids[ids.Length-1].GetText()))
                    {
                        //Value doesn't exist.
                        Error.Fatal(context, "No value with the name '" + ids[ids.Length - 1].GetText() + "' exists in the given object.");
                        return null;
                    }

                    //Return.
                    return currentObj.ObjectScopes.GetVariable(ids[ids.Length - 1].GetText());
                }
                else if (Scopes.LibraryExists(context.obj_access().IDENTIFIER()[0].GetText()))
                {
                    //LIBRARY

                    //Attempt to get the correct scope.
                    AlgoScopeCollection scope = Scopes.GetScopeFromLibAccess(context.obj_access());

                    //Check if the value is in this scope.
                    var identifiers = context.obj_access().IDENTIFIER();
                    if (!scope.VariableExists(identifiers[identifiers.Length - 1].GetText()))
                    {
                        Error.Fatal(context, "A variable with the name '" + identifiers[identifiers.Length - 1].GetText() + "' does not exist in this library.");
                        return null;
                    }

                    //Yes, it is. Returning.
                    return scope.GetVariable(identifiers[identifiers.Length - 1].GetText());
                } else
                {
                    Error.Fatal(context, "No library or object with name '" + context.obj_access().IDENTIFIER()[0].GetText() + "' exists.");
                    return null;
                }
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
                        Value = toReturn,
                        IsEnumerable = false
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
                        IsEnumerable = false
                    };

                    toReturn.ObjectScopes.AddVariable(value.IDENTIFIER().GetText(), funcValue);
                }

                //Return the object.
                return new AlgoValue()
                {
                    Type = AlgoValueType.Object,
                    Value = toReturn,
                    IsEnumerable = false
                };
            }
            else
            {
                //No proper detected value type.
                Error.Fatal(context, "Unknown or invalid type given for value.");
                return new AlgoValue()
                {
                    Type = AlgoValueType.Null,
                    Value = null,
                    IsEnumerable = false
                };
            }
        }
    }
}
