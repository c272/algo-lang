using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.PacMan
{
    public partial class Sharpie
    {
        //Get the sources file.
        public static string SourcesFile = CPFilePath.GetPlatformFilePath(DefaultDirectories.PackagesDirectory.Concat(new string[] { "sources.pkg" }).ToArray());

        //Manage sources in Sharpie.
        public void ManageSources(string[] args)
        {
            //Get the current source list from file.
            SharpieSources sources = JsonConvert.DeserializeObject<SharpieSources>(SourcesFile);

            //No arguments? List all current sources.
            if (args.Length < 1)
            {
                Console.WriteLine("Sharpie Sources\n-----------");
                foreach (var source in sources.Sources)
                {
                    Console.WriteLine(source.SourceName + " | " + source.Link);
                }
                Console.WriteLine();
            }
            else if (args[0] == "add")
            {
                //Add a source.
                
            }
            else if (args[0] == "remove")
            {
                //Remove a source.
            }
            else
            {

            }

        }

        //Add a source to the source list.
        public void AddSource(string[] args)
        {
            //Deserialize source list.
            SharpieSources sources = JsonConvert.DeserializeObject<SharpieSources>(SourcesFile);

            //Has the warning been read already? If not, read the warning.
            if (!sources.SourceWarningRead) { DisplayWarning(); sources.SourceWarningRead = true; }

            //Check the argument length.
            if (args.Length < 1)
            {
                Error.FatalNoContext("No source links supplied to add.");
            }

            //Loop the sources, and add them.
            foreach (var source in args)
            {
                //
            }
        }

        //Displays a warning to the user about installing untrusted sources.
        private void DisplayWarning()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("WARNING: Don't install sources from untrusted sites or providers. Packages from untrusted sources can possibly cause damage to your system.");
            Console.WriteLine("For recommended sources and advice, visit http://github.com/c272/algo-lang/wiki/.");
            Console.WriteLine();
            Console.WriteLine("Algo and its authors do not take responsibility for any damage you receive to your system or files due to installing packages.");
            Console.WriteLine("Do you accept these terms? (Y/N)");
            string yesNo = Console.ReadLine();
            if (yesNo == "Y") { return; }
            Environment.Exit(0);
        }
    }
}
