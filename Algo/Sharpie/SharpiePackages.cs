using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace Algo.Sharpie
{
    public partial class Sharpie
    {
        //Get the packages directory.
        public static string PackagesDirectory = CPFilePath.GetPlatformFilePath(DefaultDirectories.PackagesDirectory);
        public static string PackagesFile = CPFilePath.GetPlatformFilePath(DefaultDirectories.PackagesDirectory.Concat(new string[] { "packages.pkg" }).ToArray());

        //Add a package to the packages directory.
        public void AddPackage(string[] args)
        {
            //Check the argument is there.
            if (args.Length < 1)
            {
                Error.FatalNoContext("No packages given to install.");
            }

            //Deserialize package file.
            SharpiePackages packages = JsonConvert.DeserializeObject<SharpiePackages>(PackagesFile);

            //For each argument, process the package.
            foreach (var pkg in args)
            {
                //Check if this package exists in the master list.
                if (!packages.PackageExists(pkg))
                {
                    Error.WarningNoContext("No package exists in the master list with name '" + pkg + "' to install, it has been skipped.");
                    continue;
                }

                //It does, so attempt to do a HTTP grab from the link to get the file.
                SharpiePackage pkgObj = packages.GetPackage(pkg);
                if (pkgObj.Installed == true)
                {
                    Error.WarningNoContext("The package '" + pkg + "' is already installed, so it has been skipped.");
                    continue;
                }

                string pkgZipFile = CPFilePath.GetPlatformFilePath(new string[] { PackagesDirectory, pkg + ".zip" });
                using (var client = new WebClient())
                {
                    try
                    {
                        client.DownloadFile(pkgObj.Link, pkgZipFile);
                    }
                    catch(Exception e)
                    {
                        Error.WarningNoContext("An unknown error occured when downloading the given package '" + pkg + "' (" + e.Message + "), package skipped.");
                        continue;
                    }
                }

                //Unzip the file into the packages directory.
                try
                {
                    ZipFile.ExtractToDirectory(pkgZipFile, PackagesDirectory);
                } catch(Exception e)
                {
                    Error.WarningNoContext("An unknown error occured when extracting the given package '" + pkg + "' (" + e.Message + "), package skipped.");
                    continue;
                }

                //Deleting the zip file.
                try
                {
                    File.Delete(pkgZipFile);
                }
                catch(Exception e)
                {
                    Error.FatalNoContext("Failed to delete temporary file '" + pkgZipFile + "', given error: " + e.Message);
                    return;
                }

                //Mark as installed in sharpie packages.
                pkgObj.Installed = true;

                //Save to object, serialize again.
                packages.SetPackage(pkg, pkgObj);
                File.WriteAllText(PackagesFile, JsonConvert.SerializeObject(packages));

                //Notify of successful installation.
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully installed package '" + pkg + "'.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //Remove a package from the packages directory.
        public void RemovePackage(string[] args)
        {
            //Check the argument is there.
            if (args.Length < 1)
            {
                Error.FatalNoContext("No packages given to remove.");
            }

            //Deserialize package file.
            SharpiePackages packages = JsonConvert.DeserializeObject<SharpiePackages>(PackagesFile);

            //For each argument, process the package.
            foreach (var pkg in args)
            {
                //Does the package exist?
                if (!packages.PackageExists(pkg))
                {
                    
                }
            }
        }

        public void UpdatePackage(string[] args)
        {
            //Check the argument is there.
            if (args.Length < 1)
            {
                Error.FatalNoContext("No packages given to update.");
            }

            //For each argument, process the package.
            foreach (var pkg in args)
            {

            }
        }
    }
}
