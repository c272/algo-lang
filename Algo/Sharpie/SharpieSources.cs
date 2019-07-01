using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Sharpie
{
    public partial class Sharpie
    {
        //Get the sources file.
        public static string SourcesFile = CPFilePath.GetPlatformFilePath(DefaultDirectories.PackagesDirectory.Concat(new string[] { "sources.pkg" }).ToArray());

        //Manage sources in sharpie.
        public void ManageSources(string[] args)
        {
            
        }
    }
}
