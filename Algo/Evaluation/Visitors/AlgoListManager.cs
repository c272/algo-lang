using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Algo
{
    /// <summary>
    /// Contains all the functions related to manipulating an Algo List.
    /// </summary>
    public partial class algoVisitor : algoBaseVisitor<object>
    {
        //Adding an item to a list.
        public override object VisitStat_list_add([NotNull] algoParser.Stat_list_addContext context)
        {
            //Evaluate the value to be added to the list.
            AlgoValue toAdd = (AlgoValue)VisitExpr(context.expr()[0]);

            //Check whether the given variable exists.
            string varname = "";
            if (context.IDENTIFIER() != null) { varname = context.IDENTIFIER().GetText(); }
            if (context.obj_access() != null) { varname = context.obj_access().GetText(); }

            if (!Scopes.VariableExists(varname))
            {
                Error.Fatal(context, "A variable with the name '" + varname + "' does not exist.");
                return null;
            }

            //Variable exists, so get the list value from it.
            AlgoValue listVar = Scopes.GetVariable(varname);
            if (listVar.Type != AlgoValueType.List)
            {
                Error.Fatal(context, "Variable given is not list, so can't add an item to it.");
                return null;
            }

            //Add a value to the list, either at a specific index or to the end.
            List<AlgoValue> toReturn = (List<AlgoValue>)listVar.Value;
            if (context.AT_SYM() != null)
            {
                //Evaluate the index.
                AlgoValue index = (AlgoValue)VisitExpr(context.expr()[1]);

                //Is it an integer?
                if (index.Type == AlgoValueType.Integer)
                {
                    Error.Fatal(context, "The index supplied to insert at was not an integer.");
                    return null;
                }

                //Is it 0 or above?
                if ((BigInteger)index.Value < 0)
                {
                    Error.Fatal(context, "The index supplied is below zero, out of range.");
                    return null;
                }

                //Is the index greater than the length of the list?
                if ((BigInteger)index.Value > ((List<AlgoValue>)listVar.Value).Count || (BigInteger)index.Value > int.MaxValue)
                {
                    Error.Fatal(context, "The index supplied was out of range (too large).");
                    return null;
                }

                //Insert.
                toReturn.Insert(int.Parse(((BigInteger)index.Value).ToString()), toAdd);
            }
            else
            {
                toReturn.Add(toAdd);
            }

            //Set the variable.
            Scopes.SetVariable(varname, new AlgoValue()
            {
                Type = AlgoValueType.List,
                Value = toReturn
            });

            return null;
        }

        //Remove an item from a given list.
        public override object VisitStat_list_remove([NotNull] algoParser.Stat_list_removeContext context)
        {
            //Get the value that needs to be removed.
            AlgoValue toRemove = (AlgoValue)VisitExpr(context.expr());

            //Get the variable.
            string varname = "";
            if (context.IDENTIFIER() != null) { varname = context.IDENTIFIER().GetText(); }
            if (context.obj_access() != null) { varname = context.obj_access().GetText(); }

            if (!Scopes.VariableExists(varname))
            {
                Error.Fatal(context, "A variable with the name '" + varname + "' does not exist.");
                return null;
            }

            //Variable exists, so get the list value from it.
            AlgoValue listVar = Scopes.GetVariable(varname);
            if (listVar.Type != AlgoValueType.List)
            {
                Error.Fatal(context, "Variable given is not list, so can't remove an item from it.");
                return null;
            }

            //Get the value of the list.
            List<AlgoValue> toSet = (List<AlgoValue>)listVar.Value;
            if (context.FROM_SYM() != null)
            {
                //Remove a given value from the list.
                //Check if the list contains the value.
                if (!toSet.Any( x => x.Value.Equals(toRemove.Value) ))
                {
                    Error.Fatal(context, "The list selected does not contain an item with value given to remove.");
                    return null;
                }

                //Remove the value.
                int index = toSet.FindIndex(x => x.Value.Equals(toRemove.Value));
                toSet.RemoveAt(index);
            }
            else
            {
                //Is the index valid?
                if (toRemove.Type != AlgoValueType.Integer)
                {
                    Error.Fatal(context, "The index given to remove at is not an integer.");
                    return null;
                }

                if ((BigInteger)toRemove.Value < 0 || (BigInteger)toRemove.Value > toSet.Count)
                {
                    Error.Fatal(context, "Index to remove out of range for the given list.");
                }

                //Yes, remove there.
                toSet.RemoveAt(int.Parse(((BigInteger)toRemove.Value).ToString()));
            }

            return null;
        }
    }
}
