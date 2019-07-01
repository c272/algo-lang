using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            SharpieSources sources = JsonConvert.DeserializeObject<SharpieSources>(File.ReadAllText(SourcesFile));

            //No arguments? List all current sources.
            if (args.Length < 1)
            {
                Console.WriteLine("Sharpie Sources\n-----------");
                foreach (var source in sources.Sources)
                {
                    Console.WriteLine(source.SourceName + " | " + source.Link);
                }
                if (sources.Sources.Count == 0)
                {
                    Console.WriteLine("No sources installed.");
                }
                Console.WriteLine();
            }
            else if (args[0] == "add")
            {
                //Add a source.
                AddSource(args.Slice(1, -1));
            }
            else if (args[0] == "remove")
            {
                //Remove a source.
            }
            else
            {
                Error.FatalNoContext("Invalid command for sources.");
                return;
            }

        }

        //Add a source to the source list.
        public void AddSource(string[] args)
        {
            //Check the argument length.
            if (args.Length < 1)
            {
                Error.FatalNoContext("No source links supplied to add.");
            }

            //Deserialize source list and package list.
            SharpieSources sources = JsonConvert.DeserializeObject<SharpieSources>(File.ReadAllText(SourcesFile));
            SharpiePackages packages = JsonConvert.DeserializeObject<SharpiePackages>(File.ReadAllText(PackagesFile));

            //Has the warning been read already? If not, read the warning.
            if (!sources.SourceWarningRead) { DisplayWarning(); sources.SourceWarningRead = true; }

            //Loop the sources, and add them.
            foreach (var source in args)
            {
                //Try and download the source.
                string srcString = "";
                using (var client = new WebClient())
                {
                    try
                    {
                        srcString = client.DownloadString(source);
                    }
                    catch (Exception e)
                    {
                        Error.WarningNoContext("An unknown error occured when downloading the source '" + source + "' (" + e.Message + "), source skipped.");
                        continue;
                    }
                }

                //Attempt to parse the source into a source object.
                Tuple<SharpieSource, List<SharpiePackage>> parsed = SharpieSourceParser.Parse(srcString, source);

                //Check if a source with this name already exists.
                if (sources.SourceExists(parsed.Item1.SourceName))
                {
                    Error.WarningNoContext("A source with the name '" + parsed.Item1.SourceName + "' is already installed, so skipping.");
                    continue;
                }

                //No, so add the packages and source.
                Console.WriteLine("Attempting to add source packages for source '" + parsed.Item1.SourceName + "'...");
                foreach (var pkg in parsed.Item2)
                {
                    if (packages.PackageExists(pkg.PackageName))
                    {
                        Error.WarningNoContext("A package with the name '" + pkg.PackageName + "' already exists, so skipping.");
                        continue;
                    }

                    packages.Packages.Add(pkg);
                    Console.WriteLine("Added package '" + pkg.PackageName + "'.");
                }

                sources.Sources.Add(parsed.Item1);

                //Serializing and saving.
                File.WriteAllText(SourcesFile, JsonConvert.SerializeObject(sources));
                File.WriteAllText(PackagesFile, JsonConvert.SerializeObject(packages));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully added source '" + parsed.Item1.SourceName + "'.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //Displays a warning to the user about installing untrusted sources.
        private void DisplayWarning()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("WARNING: Don't install sources from untrusted sites or providers. Packages from untrusted sources can possibly cause damage to your system.");
            Console.WriteLine(" For recommended sources and advice, visit http://github.com/c272/algo-lang/wiki/.");
            Console.WriteLine();
            Console.WriteLine("Algo and its authors do not take responsibility for any damage you receive to your system or files due to installing packages.");
            Console.WriteLine("Do you accept these terms? (Y/N)");
            Console.ForegroundColor = ConsoleColor.White;
            string yesNo = Console.ReadLine();
            if (yesNo == "Y") { return; }
            Environment.Exit(0);
        }
    }
}
