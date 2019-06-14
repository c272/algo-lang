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
        
        //Checks whether a variable exists.
        public bool VariableExists(string varname)
        {
            return (GetVariable(varname) != null);
        }

        //Add a variable to the scope.
        public void AddVariable(string name, AlgoValue varname)
        {
            if (VariableExists(name))
            {
                //todo
            }
        }
    }
}
