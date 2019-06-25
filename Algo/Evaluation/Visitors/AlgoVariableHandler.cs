using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using ExtendedNumerics;

namespace Algo
{
    public partial class algoVisitor : algoBaseVisitor<object>
    {
        //When a variable is first defined.
        public override object VisitStat_define([NotNull] algoParser.Stat_defineContext context)
        {
            //Check if the variable already exists at the local scope.
            if (Scopes.VariableExistsLowest(context.IDENTIFIER().GetText()))
            {
                Error.Fatal(context, "A variable with the name '" + context.IDENTIFIER().GetText() + "' already exists, cannot create a duplicate.");
                return null;
            }

            //Evaluate the expression on the right side of the define.
            AlgoValue value = (AlgoValue)VisitExpr(context.expr());

            //Create the variable at the current scope.
            Scopes.AddVariable(context.IDENTIFIER().GetText(), value);
            return null;
        }

        //When a variable's value is changed.
        public override object VisitStat_setvar([NotNull] algoParser.Stat_setvarContext context)
        {
            //Get the variable/object reference.
            string objString = "";
            if (context.IDENTIFIER() != null)
            {
                objString = context.IDENTIFIER().GetText();
            } else
            {
                objString = context.obj_access().GetText();
            }

            //Validate it.
            if (!Scopes.VariableExists(objString))
            {
                Error.Fatal(context, "An object reference named '" + objString + "' does not exist.");
                return null;
            }

            //Does, evaluate the expression to set the value.
            AlgoValue value = (AlgoValue)VisitExpr(context.expr());

            //Check if there's a rounding expression.
            if (context.rounding_expr() != null)
            {
                //Evaluate rounding number expression.
                AlgoValue roundingNum = (AlgoValue)VisitExpr(context.rounding_expr().expr());
                if (roundingNum.Type != AlgoValueType.Integer)
                {
                    Error.Warning(context, "Rounding expression did not return an integer to round by, so rounding was ignored.");
                }
                else if ((BigInteger)roundingNum.Value > int.MaxValue)
                {
                    Error.Warning(context, "Rounding number too large, so rounding was ignored.");
                }
                else
                {
                    //Getting rounding integer.
                    int roundingInt = int.Parse(((BigInteger)roundingNum.Value).ToString());

                    //Rounding the value, setting.
                    Scopes.SetVariable(context.IDENTIFIER().GetText(), AlgoOperators.Round(context, value, roundingInt));

                    return null;
                }
            }

            //Set variable.
            Scopes.SetVariable(objString, value);
            return null;
        }

        //When a variable value is changed by a self modifying operator.
        public override object VisitStat_setvar_op([NotNull] algoParser.Stat_setvar_opContext context)
        {
            //Get variable name.
            string varname = "";
            if (context.IDENTIFIER() != null)
            {
                varname = context.IDENTIFIER().GetText();
            }
            else
            {
                varname = context.obj_access().GetText();
            }

            //Check if the variable already exists.
            if (!Scopes.VariableExists(varname))
            {
                Error.Fatal(context, "A variable with the name '" + varname + "' does not exist, cannot set value.");
                return null;
            }

            //It does, get the variable.
            AlgoValue oldValue = Scopes.GetVariable(varname);

            //Does, evaluate the expression to set the value.
            AlgoValue value = (AlgoValue)VisitExpr(context.expr());

            //Switching on selfmod value.
            AlgoValue newValue;
            if (context.selfmod_op().ADDFROM_OP() != null)
            {
                //Attempt to add.
                newValue = AlgoOperators.Add(context, oldValue, value);
            }
            else if (context.selfmod_op().DIVFROM_OP() != null)
            {
                //Attempt to divide.
                newValue = AlgoOperators.Div(context, oldValue, value);
            }
            else if (context.selfmod_op().MULFROM_OP() != null)
            {
                //Attempt to multiply.
                newValue = AlgoOperators.Mul(context, oldValue, value);
            }
            else if (context.selfmod_op().TAKEFROM_OP() != null)
            {
                //Attempt to subtract.
                newValue = AlgoOperators.Sub(context, oldValue, value);
            }
            else
            {
                //Invalid operator type, oopsie! Implemented in parser but not here.
                Error.Fatal(context, "Invalid operator given for self modifying variable, implemented in parser but not in interpreter.");
                return null;
            }

            //Set the value of the variable.
            Scopes.SetVariable(varname, newValue);
            return null;
        }

        //When a variable is modified using a postfix operator.
        public override object VisitStat_setvar_postfix([NotNull] algoParser.Stat_setvar_postfixContext context)
        {
            //Get variable name.
            string varname = "";
            if (context.IDENTIFIER() != null)
            {
                varname = context.IDENTIFIER().GetText();
            }
            else
            {
                varname = context.obj_access().GetText();
            }

            //Check if the variable already exists.
            if (!Scopes.VariableExists(varname))
            {
                Error.Fatal(context, "A variable with the name '" + varname + "' does not exist, cannot set value.");
                return null;
            }

            //It does, get the variable.
            AlgoValue oldValue = Scopes.GetVariable(varname);

            //Switch on the operator type.
            int toAdd = 0;
            if (context.postfix_op().ADD_PFOP() != null)
            {
                //Increment.
                toAdd = 1;
            }
            else
            {
                //Decrement.
                toAdd = -1;
            }

            //Apply the operator.
            AlgoValue newValue = null;
            switch (oldValue.Type)
            {
                case AlgoValueType.Integer:
                case AlgoValueType.Float:
                case AlgoValueType.Rational:
                    newValue = AlgoOperators.Add(context, oldValue, new AlgoValue()
                    {
                        Type = AlgoValueType.Integer,
                        Value = new BigInteger(toAdd)
                    });

                    break;

                default:
                    Error.Fatal(context, "Cannot increment a variable of type " + oldValue.Type.ToString() + ".");
                    return null;
            }

            //Set the variable value.
            Scopes.SetVariable(varname, newValue);
            return null;
        }

        //When a variable is deleted.
        public override object VisitStat_deletevar([NotNull] algoParser.Stat_deletevarContext context)
        {
            //Checking if the disregard wants to delete all variables or only one.
            if (context.IDENTIFIER() != null || context.obj_access() != null)
            {
                //Get variable name.
                string varname = "";
                if (context.IDENTIFIER() != null)
                {
                    varname = context.IDENTIFIER().GetText();
                }
                else
                {
                    varname = context.obj_access().GetText();
                }

                //Check if variable exists.
                if (!Scopes.VariableExists(varname))
                {
                    //Maybe it's a library?
                    if (Scopes.LibraryExists(varname)) 
                    {
                        //Okay, just disregard the library.
                        Scopes.RemoveLibrary(varname);
                        return null;
                    }

                    //Not a library, so doesn't exist.
                    Error.Fatal(context, "Invalid variable name given to disregard.");
                    return null;
                }

                //Remove variable.
                Scopes.RemoveVariable(varname);
            }
            else
            {
                //Reset all scopes.
                Scopes.Reset();
            }

            return null;
        }
    }
}
