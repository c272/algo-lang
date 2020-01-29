using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    /// <summary>
    /// Extends the System.Reflection.Assembly "Version" class with utilities for naming.
    /// </summary>
    public static class VersionExtensions
    {
        /// <summary>
        /// Default names for the versions.
        /// </summary>
        private static string[] names =
        {
            "fungi",
            "amoeba",
            "protozoa",
            "algae",
            "archaea",
            "protein",
            "atom",
            "nucleus",
            "electron",
            "proton",
            "neutron",
            "vibrio"
        };

        /// <summary>
        /// Returns a version name for a given build of a program.
        /// </summary>
        public static string GetVersionName(this AssemblyName version)
        {
            //Get the build.
            int build = version.Version.Revision;

            //Divide build to get the number of loops, also get the remainder.
            int loops = build / names.Length;
            int rem = build % names.Length;

            //Fashion a version from those.
            if (rem == names.Length) { rem = 0; }
            return names[rem] + "-" + loops;
        }
    }
}
