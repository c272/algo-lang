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

                //Unzip the file into the packages directory. (/packages/somepkgname)
                try
                {
                    ZipFile.ExtractToDirectory(pkgZipFile, CPFilePath.GetPlatformFilePath(new string[] { PackagesDirectory, pkg }));
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
                    Error.WarningNoContext("A package with the name '" + pkg + "' does not exist in the master list, so has been skipped.");
                    continue;
                }

                //Package does exist, get it and check if it's installed.
                SharpiePackage pkgObj = packages.GetPackage(pkg);
                if (!pkgObj.Installed)
                {
                    Error.WarningNoContext("The package '" + pkg + "' was not installed, so has not been removed. Skipping.");
                    continue;
                }

                //Delete the package directory, mark as uninstalled.
                string pkgDir = CPFilePath.GetPlatformFilePath(new string[] { PackagesDirectory, pkg });
                try
                {
                    Directory.Delete(pkgDir, true);
                } catch(Exception e)
                {
                    Error.WarningNoContext("Failed to delete package directory for the package '" + pkg + "', skipping.");
                    continue;
                }

                //Mark as uninstalled, serialize back.
                pkgObj.Installed = false;
                packages.SetPackage(pkg, pkgObj);
                File.WriteAllText(PackagesFile, JsonConvert.SerializeObject(packages));

                //Confirm success.
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully removed package '" + pkg + "'.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void UpdatePackage(string[] args)
        {
            //Check the argument is there.
            if (args.Length < 1)
            {
                Error.FatalNoContext("No packages given to update.");
            }

            //Deserialize package file.
            SharpiePackages packages = JsonConvert.DeserializeObject<SharpiePackages>(PackagesFile);

            //For each argument, process the package.
            foreach (var pkg in args)
            {
                //Does the package exist?
                if (!packages.PackageExists(pkg))
                {
                    Error.WarningNoContext("No package with the name '" + pkg + "' exists in the master list, skipping.");
                    continue;
                }

                //Is it installed?
                SharpiePackage pkgObj = packages.GetPackage(pkg);
                if (!pkgObj.Installed)
                {
                    Error.WarningNoContext("The package '" + pkg + "' is not installed, so cannot update. Skipping.");
                    continue;
                }

                //Is the current version less than the version?
                if (pkgObj.CurrentPackageVersion >= pkgObj.PackageVersion)
                {
                    Error.WarningNoContext("The package '" + pkg + "' is already up to date, skipping.");
                    continue;
                }

                //It needs updating. Attempt to grab from the source.
                Console.WriteLine("Package '" + pkg + "' requires an update, reinstalling...");
                RemovePackage(new string[] { pkg });
                AddPackage(new string[] { pkg });
                Console.WriteLine("Successfully updated package '" + pkg + "' to the latest version (" + pkgObj.PackageVersion + ").");
            }
        }
    }
}
