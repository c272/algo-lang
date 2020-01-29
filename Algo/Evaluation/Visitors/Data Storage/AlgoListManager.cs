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

            //Get the list variable from the identifier and particle.
            AlgoValue listVar = Particles.ParseParticleBlock(this, context, context.IDENTIFIER(), context.particle());
            if (listVar == null)
            {
                Error.Fatal(context, "No list value returned to add to from particle block.");
                return null;
            }

            //Check the type is actually a list.
            if (listVar.Type != AlgoValueType.List)
            {
                Error.Fatal(context, "Variable given is not list, so can't remove an item from it.");
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

            //Set the variable via. reference (TEST ME!).
            listVar.Value = toReturn;
            return null;
        }

        //Remove an item from a given list.
        public override object VisitStat_list_remove([NotNull] algoParser.Stat_list_removeContext context)
        {
            //Get the value that needs to be removed.
            AlgoValue toRemove = (AlgoValue)VisitExpr(context.expr());

            //Get the variable.
            var listVar = Particles.ParseParticleBlock(this, context, context.IDENTIFIER(), context.particle());
            if (listVar == null)
            {
                Error.Fatal(context, "No variable was returned to remove an item from, so can't remove an item from it.");
                return null;
            }

            //Variable exists, so get the list value from it.
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
                int index = toSet.FindIndex(x => x._Equals(toRemove));
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
