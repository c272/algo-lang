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
