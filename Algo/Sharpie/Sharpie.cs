using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Algo.PacMan
{
    public partial class Sharpie
    {
        //The major and minor numbers of the package manager.
        public static int MajorVersion = 0;
        public static int MinorVersion = 1;

        //The main global class for the Sharp2e package manager.
        public Sharpie(string[] args)
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

            //What's the user trying to get (eg. packages, build info, source management, help)
            if (args.Length < 1)
            {
                Console.WriteLine("Sharpie Package Manager v" + MajorVersion + "." + MinorVersion + ", build " + typeof(Program).Assembly.GetName().Version.ToString().Split('.')[2] + ".");
                Console.WriteLine("(c) Larry Tang 2019 - " + DateTime.Now.Year);
                return;
            }

            //Get help.
            if (args[0] == "help")
            {
                //todo
            }
            
            //Managing sources?
            if (args[0] == "sources")
            {
                ManageSources(args.Slice(1, -1));
                return;
            }

            //All commands past here require two parameters.
            if (args.Length < 2)
            {
                Error.FatalNoContext("Invalid amount of arguments for the package manager.");
                return;
            }

            //Managing packages?
            if (args[0] == "add")
            {
                AddPackage(args.Slice(1, -1));
            }
            else if (args[0] == "remove")
            {
                RemovePackage(args.Slice(1, -1));
            }
            else if (args[0] == "update")
            {
                UpdatePackage(args.Slice(1, -1));
            } else
            {
                //unknown command
            }
        }
    }
}
