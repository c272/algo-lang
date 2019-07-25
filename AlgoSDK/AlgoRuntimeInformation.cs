using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public static class AlgoRuntimeInformation
    {
        //The currently loaded script file.
        public static string FileLoaded = "";

        //Whether the current runtime is in developer mode or not.
        public static bool DeveloperMode = false;

        //Whether the current runtime should ignore fatal errors and continue running.
        public static bool ContinuousMode = false;
    }
}
