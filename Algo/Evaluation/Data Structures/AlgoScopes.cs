using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public class AlgoScopeCollection
    {
        //The list of scopes.
        public List<Dictionary<string, AlgoValue>> Scopes = new List<Dictionary<string, AlgoValue>>();
        public static Dictionary<string, AlgoScopeCollection> Libraries = new Dictionary<string, AlgoScopeCollection>();

        public AlgoScopeCollection()
        {
            Scopes.Add(new Dictionary<string, AlgoValue>());
        }

        //Add a scope.
        public void AddScope()
        {
            Scopes.Add(new Dictionary<string, AlgoValue>());
        }

        //Remove a scope.
        public void RemoveScope()
        {
            if (Scopes.Count==1)
            {
                throw new Exception("Cannot remove scope, have already reached the top of the scope.");
            }

            Scopes.Remove(Scopes.Last());
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
            for (int i=0; i<lib_accessContext.IDENTIFIER().Length-1; i++)
            {
                toReturn = toReturn.GetLibrary(lib_accessContext.IDENTIFIER()[i].GetText());
            }

            return toReturn;
        }

        public AlgoValue GetValueFromObjectString(ParserRuleContext context, string objString)
        {
            string[] objParts = objString.Split('.');
            if (objParts.Length == 1)
            {
                if (!VariableExists(objString))
                {
                    Error.Fatal(context, "No variable with name '" + objParts[0] + "' exists.");
                    return null;
                }

                return GetVariable(objString);
            }

            //Loop and get value.
            if (!VariableExists(objParts[0]))
            {
                Error.FatalNoContext("No parent variable '" + objParts[0] + "' exists.");
                return null;
            }

            AlgoValue currentValue = GetVariable(objParts[0]);
            for (int i=1; i<objParts.Length-1; i++)
            {
                //Check if the current value is an object.
                if (currentValue.Type != AlgoValueType.Object)
                {
                    Error.Fatal(context, "The parent value to get children of is not an object, so does not have children.");
                    return null;
                }

                //todo
            }
        }

        //Get a variable within the scopes.
        //Start from the deepest depth, to ensure local scope is evaluated first.
        public AlgoValue GetVariable(string varname)
        {
            for (int i=Scopes.Count-1; i>=0; i--)
            {
                if (Scopes[i].Keys.Contains(varname))
                {
                    return Scopes[i][varname];
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
        public void SetVariable(string varname, AlgoValue value)
        {
            //Finding and setting.
            for (int i=Scopes.Count-1; i>=0; i--)
            {
                if (Scopes[i].Keys.Contains(varname))
                {
                    Scopes[i][varname] = value;
                    return;
                }
            }

            //Uncaught missing variable.
            Error.FatalNoContext("No variable found with name '" + varname + "' to set.");
        }
        
        //Checks whether a variable exists.
        public bool VariableExists(string varname)
        {
            return (GetVariable(varname) != null);
        }

        //Checks whether a variable exists at the lowest level.
        public bool VariableExistsLowest(string name)
        {
            return Scopes[Scopes.Count - 1].ContainsKey(name);
        }

        //Add a variable to the scope.
        public void AddVariable(string name, AlgoValue value)
        {
            //Check if a variable with this name already exists.
            if (VariableExistsLowest(name))
            {
                Error.FatalNoContext("A variable with this name already exists, cannot create one.");
                return;
            }

            //Okay, it doesn't. Add it to the lowest scope.
            Scopes[Scopes.Count - 1].Add(name, value);
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

            //Yes, so check through all scopes (from deepest) and delete.
            for (int i=Scopes.Count-1; i>=0; i--)
            {
                if (Scopes[i].ContainsKey(name))
                {
                    Scopes[i].Remove(name);
                    return;
                }
            }

            //Could not find.
            Error.FatalNoContext("Could not find variable to delete, despite the fact it exists.");
        }
    }
}
