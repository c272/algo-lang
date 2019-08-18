using Antlr4.Runtime;
using ExtendedNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algo.StandardLibrary
{
    //All reflection-based external functions (eg. string -> var).
    public class AlgoStd_Reflection : IFunctionPlugin
    {
        public string Name { get; set; } = "std_reflection";

        public List<AlgoPluginFunction> Functions { get; set; } = new List<AlgoPluginFunction>()
        {
            //gvar(x)
            new AlgoPluginFunction()
            {
                Name = "gvar",
                ParameterCount = 1,
                Function = GetVarByName
            },

            //cvar(x, y)
            new AlgoPluginFunction()
            {
                Name = "cvar",
                ParameterCount = 2,
                Function = CreateVarByName
            },

            //svar(x, y)
            new AlgoPluginFunction()
            {
                Name = "svar",
                ParameterCount = 2,
                Function = SetVarByName
            }
        };

        //Create a variable given a string name.
        public static AlgoValue CreateVarByName(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check first argument is string.
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Name for variable to create must be of type String, not of type " + args[0].Type.ToString() +".");
                return null;
            }

            //Create the variable.
            algoVisitor.Scopes.AddVariable((string)args[0].Value, args[1], context);
            return null;
        }

        //Set a variable given a string name.
        public static AlgoValue SetVarByName(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check first argument is string.
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Name for variable to create must be of type String, not of type " + args[0].Type.ToString() + ".");
                return null;
            }

            //Set the variable.
            algoVisitor.Scopes.SetVariable((string)args[0].Value, args[1], context);
            return null;
        }

        //Get a variable's content by its string name.
        public static AlgoValue GetVarByName(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check variable name is a string.
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Cannot get variable with name of type '" + args[0].Type.ToString() + "', must be of type String.");
                return null;
            }

            //Check it exists.
            if (!algoVisitor.Scopes.VariableExists((string)args[0].Value))
            {
                //It doesn't, return null.
                return AlgoValue.Null;
            }

            //It does, return the variable.
            return algoVisitor.Scopes.GetVariable((string)args[0].Value);
        }
    }
}