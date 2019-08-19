using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    [Serializable]
    public class AlgoScopeCollection
    {
        //The list of scopes.
        public List<Dictionary<string, AlgoValue>> Scopes = new List<Dictionary<string, AlgoValue>>();
        public Dictionary<string, AlgoScopeCollection> Libraries = new Dictionary<string, AlgoScopeCollection>();

        public AlgoScopeCollection()
        {
            //Create base scope.
            Scopes.Add(new Dictionary<string, AlgoValue>());
        }

        //Add a scope.
        public void AddScope(Dictionary<string, AlgoValue> scope = null)
        {
            if (scope == null)
            {
                Scopes.Add(new Dictionary<string, AlgoValue>());
            }
            else
            {
                Scopes.Add(scope);
            }
        }

        //Remove a scope.
        public void RemoveScope()
        {
            if (Scopes.Count == 1)
            {
                throw new Exception("Cannot remove scope, have already reached the top of the scope.");
            }

            Scopes.Remove(Scopes.Last());
        }

        public Dictionary<string, AlgoValue> GetDeepestScope()
        {
            return Scopes.Last();
        }

        //Reset all scopes.
        public void Reset()
        {
            Scopes = new List<Dictionary<string, AlgoValue>>();
            Scopes.Add(new Dictionary<string, AlgoValue>());
        }

        //Add a library.
        public void AddLibrary(string name, AlgoScopeCollection library)
        {
            //Checking if library is a dupe.
            if (Libraries.ContainsKey(name))
            {
                Error.FatalNoContext("A library with the name '" + name + "' already exists in this scope, can't define it again.");
                return;
            }

            //Not a dupe, add it.
            Libraries.Add(name, library);
        }

        //Remove a library.
        public void RemoveLibrary(string name)
        {
            //Does the library exist?
            if (!Libraries.ContainsKey(name))
            {
                Error.FatalNoContext("A library with the name '" + name + "' does not exist to remove.");
                return;
            }

            //Remove it.
            Libraries.Remove(name);
        }

        //Returns the libraries in this collection.
        public Dictionary<string, AlgoScopeCollection> GetLibraries()
        {
            return Libraries;
        }

        //Get a library.
        public AlgoScopeCollection GetLibrary(string name)
        {
            if (!LibraryExists(name))
            {
                Error.FatalNoContext("A library with the name '" + name + "' does not exist.");
                return null;
            }

            //Does exist, return it.
            return Libraries[name];
        }

        //Check if a library exists.
        public bool LibraryExists(string name)
        {
            return Libraries.ContainsKey(name);
        }

        //Gets the correct scope from library access.
        public AlgoScopeCollection GetScopeFromLibAccess(algoParser.Obj_accessContext lib_accessContext)
        {
            AlgoScopeCollection toReturn = this;
            for (int i = 0; i < lib_accessContext.IDENTIFIER().Length - 1; i++)
            {
                //Library?
                string vname = lib_accessContext.IDENTIFIER()[i].GetText();
                if (toReturn.LibraryExists(vname))
                {
                    toReturn = toReturn.GetLibrary(vname);
                }
                else
                {
                    //Object?
                    if (toReturn.VariableExists(vname) && toReturn.GetVariable(vname).Type == AlgoValueType.Object)
                    {
                        toReturn = ((AlgoObject)toReturn.GetVariable(vname).Value).ObjectScopes;
                    }
                    else
                    {
                        Error.Fatal(lib_accessContext, "Invalid library or object name '" + vname + "' given to access.");
                        return null;
                    }
                }
            }

            return toReturn;
        }

        //Returns the value of a referenced object within an object string.
        private AlgoValue GetValueFromObjectString(string objString)
        {
            //Splitting the string by "." to get individual variable parts.
            string[] objParts = objString.Split('.');
            if (objParts.Length == 1)
            {
                if (!VariableExists(objString))
                {
                    return null;
                }

                return GetVariable(objString);
            }

            //Loop and get value.
            if (!VariableExists(objParts[0]))
            {
                return null;
            }

            //Get the first parent object.
            AlgoValue currentObjValue = GetVariable(objParts[0]);
            if (currentObjValue.Type != AlgoValueType.Object)
            {
                return null;
            }

            //Getting the deepest scope.
            AlgoObject currentObj = (AlgoObject)currentObjValue.Value;

            for (int i = 1; i < objParts.Length - 1; i++)
            {
                //Attempt to get the child.
                if (!currentObj.ObjectScopes.VariableExists(objParts[i]))
                {
                    return null;
                }
                currentObjValue = currentObj.ObjectScopes.GetVariable(objParts[i]);

                //Check if it's an object.
                if (currentObjValue.Type != AlgoValueType.Object)
                {
                    return null;
                }

                //Set current object.
                currentObj = (AlgoObject)currentObjValue.Value;
            }

            //Getting the variable referenced at the deepest scope.
            if (!currentObj.ObjectScopes.VariableExists(objParts[objParts.Length - 1]))
            {
                return null;
            }

            //Returning it.
            return currentObj.ObjectScopes.GetVariable(objParts[objParts.Length - 1]);
        }

        //Get a variable within the scopes.
        //Start from the deepest depth, to ensure local scope is evaluated first.
        public AlgoValue GetVariable(string varname)
        {
            if (varname.Contains("."))
            {
                //Contains object reference, return object grabbed.
                return GetValueFromObjectString(varname);
            }
            else
            {
                for (int i = Scopes.Count - 1; i >= 0; i--)
                {
                    if (Scopes[i].Keys.Contains(varname))
                    {
                        return Scopes[i][varname];
                    }
                }
            }

            return null;
        }

        //Get all scopes.
        public List<Dictionary<string, AlgoValue>> GetScopes()
        {
            return Scopes;
        }

        //Set a variable within the scopes.
        //Start from deepest depth.
        public void SetVariable(string varname, AlgoValue value, ParserRuleContext context=null)
        {
            //Is it a normal variable or an object?
            if (!varname.Contains('.'))
            {
                //Finding and setting.
                for (int i = Scopes.Count - 1; i >= 0; i--)
                {
                    if (Scopes[i].Keys.Contains(varname))
                    {
                        Scopes[i][varname] = value;
                        return;
                    }
                }

                //Uncaught missing variable.
                Error.Fatal(context, "No variable found with name '" + varname + "' to set.");
            }
            else
            {
                string[] objParts = varname.Split('.');

                //Loop and get value.
                if (!VariableExists(objParts[0]))
                {
                    Error.Fatal(context, "No parent variable '" + objParts[0] + "' exists.");
                }

                AlgoValue currentObjValue = GetVariable(objParts[0]);
                if (currentObjValue.Type != AlgoValueType.Object)
                {
                    Error.Fatal(context, "Parent variable is not an object, so cannot access children.");
                }

                //Getting the deepest scope.
                AlgoObject currentObj = (AlgoObject)currentObjValue.Value;
                for (int i = 1; i < objParts.Length - 1; i++)
                {
                    //Attempt to get the child.
                    if (!currentObj.ObjectScopes.VariableExists(objParts[i]))
                    {
                        Error.Fatal(context, "A child property '" + objParts[i] + "' does not exist.");
                    }
                    currentObjValue = currentObj.ObjectScopes.GetVariable(objParts[i]);

                    //Check if it's an object.
                    if (currentObjValue.Type != AlgoValueType.Object)
                    {
                        Error.Fatal(context, "A child property '" + objParts[i] + "' is not an object, so can't access further children.");
                    }

                    //Set current object.
                    currentObj = (AlgoObject)currentObjValue.Value;
                }

                //Getting the variable referenced at the deepest scope.
                if (!currentObj.ObjectScopes.VariableExists(objParts[objParts.Length - 1]))
                {
                    Error.Fatal(context, "No variable exists at the deepest scope with name '" + objParts[objParts.Length - 1] + "'.");
                }

                //Set value of variable.
                currentObj.ObjectScopes.SetVariable(objParts[objParts.Length - 1], value);
            }
        }

        //Get a list value.
        public AlgoValue GetListValue(ParserRuleContext context, string objString, algoParser.Array_accessContext array_access)
        {
            //Grab list member array and index array.
            var memIndexes = GetArrayAccessMembers(context, objString, array_access);
            var listMemberArray = memIndexes.Item2;
            var indexes = memIndexes.Item1;

            //Check if the final member is a list.
            if (listMemberArray.Last().Type != AlgoValueType.List)
            {
                Error.Fatal(context, "Cannot index into a non-list value.");
            }

            //Get the final index, return it.
            return ((List<AlgoValue>)listMemberArray.Last().Value)[indexes.Last()];
        }

        //Set list value.
        public void SetListValue(ParserRuleContext context, string objString, algoParser.Array_accessContext array_access, AlgoValue value)
        {
            //Get list member array and index array.
            var memIndexes = GetArrayAccessMembers(context, objString, array_access);
            var listMemberArray = memIndexes.Item2;
            var indexes = memIndexes.Item1;

            //Check if the final member is a list.
            if (listMemberArray.Last().Type != AlgoValueType.List)
            {
                Error.Fatal(context, "Cannot index into a non-list value.");
            }

            //Change the index.
            ((List<AlgoValue>)listMemberArray.Last().Value)[indexes.Last()] = value;

            //Propogate the change back up the list tree.
            if (indexes.Count > 1)
            {
                for (int i = indexes.Count - 2; i < 0; i--)
                {
                    //Set the value to the next one along, until finish.
                    List<AlgoValue> val = (List<AlgoValue>)listMemberArray[i].Value;
                    val[indexes[i]] = listMemberArray[i + 1];

                    listMemberArray[i].Value = val;
                }
            }

            //Set the value.
            SetVariable(objString, listMemberArray.First());
        }

        //Gets the indexes and list member tree for a given array access.
        public Tuple<List<int>, List<AlgoValue>> GetArrayAccessMembers(ParserRuleContext context, string objString, algoParser.Array_accessContext array_access)
        {
            //Array access, so get the variable, and then set the list element, then set.
            AlgoValue list = GetVariable(objString);
            if (list.Type != AlgoValueType.List)
            {
                Error.Fatal(context, "The value to enumerate into is not a list.");
                return null;
            }

            //Get array indexes.
            List<int> indexes = new List<int>();
            foreach (var index in array_access.literal_params().expr())
            {
                var indexVal = (AlgoValue)Program.visitor.VisitExpr(index);
                if (indexVal.Type != AlgoValueType.Integer)
                {
                    Error.Fatal(context, "Invalid type for indexing value.");
                }

                if ((BigInteger)indexVal.Value > int.MaxValue)
                {
                    Error.Fatal(context, "Indexing value is too large for array index.");
                }

                indexes.Add(int.Parse(((BigInteger)indexVal.Value).ToString()));
            }

            //Access indexes.
            List<AlgoValue> listMemberArray = new List<AlgoValue>();
            listMemberArray.Add(list);
            for (int i = 0; i < indexes.Count - 1; i++)
            {
                //Get AlgoValue for this index, check it's a list.
                if (listMemberArray.Last().Type != AlgoValueType.List)
                {
                    Error.Fatal(context, "Cannot index into a non-list value.");
                }

                //Index into it, add to list member array.
                listMemberArray.Add(((List<AlgoValue>)listMemberArray.Last().Value)[indexes[i]]);
            }

            return new Tuple<List<int>, List<AlgoValue>>(indexes, listMemberArray);
        }

        //Checks whether a variable exists.
        public bool VariableExists(string varname)
        {
            if (varname.Contains('.'))
            {
                //Validate it by fake value grabbing.
                List<string> parts = varname.Split('.').ToList();
                AlgoValue partParent = GetVariable(parts[0]);
                if (partParent == null || (partParent.Type != AlgoValueType.Object && parts.Count > 1))
                {
                    return false;
                }

                //If the variable is known to exist and no points, this is just a sanity check to make sure nothing slips through.
                if (parts.Count == 1) { return true; }

                //Iterate through children recursively to check they all exist.
                AlgoObject currentObj = (AlgoObject)partParent.Value;
                parts.RemoveAt(0);
                string childString = string.Join(".", parts.ToArray());

                //Recursive return.
                return currentObj.ObjectScopes.VariableExists(childString);
            }
            return (GetVariable(varname) != null);
        }

        //Checks whether a variable exists at the lowest level.
        public bool VariableExistsLowest(string name)
        {
            return Scopes[Scopes.Count - 1].ContainsKey(name);
        }

        //Add a variable to the scope.
        public void AddVariable(string name, AlgoValue value, ParserRuleContext context=null)
        {
            //Is it an object member?
            if (!name.Contains("."))
            {
                //Check if a variable with this name already exists.
                if (VariableExistsLowest(name))
                {
                    Error.Fatal(context, "A variable with this name already exists, cannot create one.");
                    return;
                }

                //Nope, add it to the lowest scope.
                Scopes[Scopes.Count - 1].Add(name, value);
            }
            else
            {
                //Object member, a more complicated procedure.
                //Get the parent object that the member is being assigned to.
                string objTxt = name;
                List<string> objParts = objTxt.Split('.').ToList();
                string final = objParts.Last();
                objParts.RemoveAt(objParts.Count - 1);
                objTxt = string.Join(".", objParts.ToArray());

                if (!VariableExists(objTxt))
                {
                    Error.Fatal(context, "Cannot create an object member for nonexistant object '" + objTxt + "'.");
                }

                //Check if the variable is an object.
                AlgoValue objVal = GetVariable(objTxt);
                if (objVal.Type != AlgoValueType.Object)
                {
                    Error.Fatal(context, "Cannot create an object member of non-object value '" + objTxt + "'.");
                }
                AlgoObject obj = (AlgoObject)objVal.Value;

                //Check if the variable has the new member already defined.
                if (obj.ObjectScopes.VariableExists(final))
                {
                    Error.Fatal(context, "An object member with that name already exists.");
                }

                //Create the variable.
                obj.ObjectScopes.AddVariable(final, value);

                //Set the object parent.
                SetVariable(objTxt, new AlgoValue()
                {
                    Type = AlgoValueType.Object,
                    Value = obj
                });
            }
        }

        //Remove a variable from the scope.
        public void RemoveVariable(string name)
        {
            //Check if a variable with this name exists.
            if (!VariableExists(name))
            {
                Error.FatalNoContext("No variable with this name exists, so can't destroy it.");
                return;
            }

            //If the name contains a '.', got to go to deepest scope recursively.
            if (name.Contains('.'))
            {
                //Get the parent.
                List<string> parts = name.Split('.').ToList();
                AlgoValue partParent = GetVariable(parts[0]);

                //Checking if it's an object.
                if (partParent.Type != AlgoValueType.Object)
                {
                    Error.FatalNoContext("The value given is not an object, so cannot remove children.");
                    return;
                }

                //Remove variable from this scope recursively.
                parts.RemoveAt(0);
                string newPartString = string.Join(".", parts.ToList());
                AlgoObject partObj = (AlgoObject)partParent.Value;
                partObj.ObjectScopes.RemoveVariable(newPartString);
            }
            else
            {
                //Literal variable in this scope, check through all scopes (from deepest) and delete.
                for (int i = Scopes.Count - 1; i >= 0; i--)
                {
                    if (Scopes[i].ContainsKey(name))
                    {
                        Scopes[i].Remove(name);
                        return;
                    }
                }
            }

            //Could not find.
            Error.FatalNoContext("Could not find variable to delete, despite the fact it exists.");
        }
    }
}
