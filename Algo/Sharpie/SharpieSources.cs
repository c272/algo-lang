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

        /// <summary>
        /// Lists all sources currently installed on the Sharpie master list.
        /// </summary>
        public void ListSources()
        {
            //Get the current source list from file.
            SharpieSources sources = JsonConvert.DeserializeObject<SharpieSources>(File.ReadAllText(SourcesFile));

            //List all current sources.
            Console.WriteLine("Sharpie Sources\n-----------");
            foreach (var source in sources.Sources)
            {
                Console.WriteLine(source.SourceName + " | " + source.Link);
            }
            if (sources.Sources.Count == 0)
            {
                Console.WriteLine("No sources installed.");
            }
            Console.WriteLine("-----------\n");
            
        }

        /// <summary>
        /// Adds a source to the Sharpie master list.
        /// </summary>
        public void AddSource(string[] args)
        {
            //Check the argument length.
            if (args.Length < 1)
            {
                Error.FatalNoContext("No source link(s) supplied to add.");
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

        /// <summary>
        /// Remove sources from the source list, and all associated packages.
        /// </summary>
        public void RemoveSource(string[] args)
        {
            //Check the argument length.
            if (args.Length < 1)
            {
                Error.FatalNoContext("No source(s) given to remove.");
                return;
            }

            //Get the source and package lists from file.
            SharpieSources sources = JsonConvert.DeserializeObject<SharpieSources>(File.ReadAllText(SourcesFile));
            SharpiePackages packages = JsonConvert.DeserializeObject<SharpiePackages>(File.ReadAllText(PackagesFile));

            foreach (var source in args)
            {
                //Check if the source is actually installed.
                if (!sources.SourceExists(source))
                {
                    Error.WarningNoContext("A source with the name '" + source + "' is not installed, skipping.");
                    continue;
                }

                //Yes, loop through all packages with the source name attached and uninstall them.
                Console.WriteLine("Attempting to remove packages for source '" + source + "'...");
                string[] toRemove = packages.Packages.Where(x => x.ParentSource == source).Where(x => x.Installed==true).Select(x => x.PackageName).ToArray();
                RemovePackage(toRemove);

                //Actually deleting the packages from the master list which have this source as a parent.
                packages.Packages.RemoveAll(x => x.ParentSource == source);

                //Removing the source itself from the source list.
                Console.WriteLine("Deleting source '" + source + "' from master list...");
                int amtRemoved = sources.Sources.RemoveAll(x => x.SourceName == source);
                if (amtRemoved <= 0)
                {
                    Error.WarningNoContext("Failed to remove source from master list, skipping.");
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully removed source '" + source + "'.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            //Done modifying the lists, serialize them back to file.
            File.WriteAllText(PackagesFile, JsonConvert.SerializeObject(packages));
            File.WriteAllText(SourcesFile, JsonConvert.SerializeObject(sources));
        }

        //Update a given source (or all sources) from the master list.
        private void UpdateSources(string[] args)
        {
            Error.Internal("This feature is currently not implemented. Sorry!");
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
