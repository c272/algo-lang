using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algo.CLI;
using Newtonsoft.Json;

namespace Algo.PacMan
{
    public partial class Sharpie
    {
        //The major and minor numbers of the package manager.
        public static int MajorVersion = 0;
        public static int MinorVersion = 1;

        //The main global class for the Sharp2e package manager.
        public Sharpie(PackageManagerCLIOptions opts)
        {
            //If the packages and sources files don't exist, initialize them.
            if (!File.Exists(PackagesFile))
            {
                File.WriteAllText(PackagesFile, JsonConvert.SerializeObject(new SharpiePackages()));
            }

            if (!File.Exists(SourcesFile))
            {
                File.WriteAllText(SourcesFile, JsonConvert.SerializeObject(new SharpieSources()));
            }

            //Get help.
            if (opts.Help)
            {
                Console.Write("Use 'algo help' without the 'pkg' to see help information for Sharpie.");
                return;
            }

            //Listing?
            else if (opts.ListPackages)
            {
                ListPackages();
                return;
            }
            
            //Managing sources?
            else if (opts.ListSources)
            {
                ListSources();
                return;
            }

            //Managing packages?
            if (opts.AddPackages != null)
            {
                AddPackage(opts.AddPackages.ToArray());
                return;
            }
            else if (opts.RemovePackages != null)
            {
                RemovePackage(opts.RemovePackages.ToArray());
                return;
            }
            else if (opts.UpdatePackages != null)
            {
                UpdatePackage(opts.UpdatePackages.ToArray());
                return;
            } else
            {
                //Just list the version info.
                Console.WriteLine("Sharpie Package Manager v" + MajorVersion + "." + MinorVersion + ", build " + typeof(Program).Assembly.GetName().Version.ToString().Split('.')[2] + ".");
                Console.WriteLine("(c) Larry Tang 2018 - " + DateTime.Now.Year);
            }
        }
    }
}
