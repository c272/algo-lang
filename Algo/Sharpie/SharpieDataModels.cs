using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.PacMan
{
    //A collection of packages within Sharpie.
    public class SharpiePackages
    {
        public List<SharpiePackage> Packages = new List<SharpiePackage>();

        //Checks whether a package exists in the master list.
        public bool PackageExists(string pkg)
        {
            return (Packages.FindIndex(x => x.PackageName == pkg) != -1);
        }

        //Gets a package from the master list.
        public SharpiePackage GetPackage(string pkg)
        {
            return Packages.Find(x => x.PackageName == pkg);
        }

        //Sets a package in the master list.
        public void SetPackage(string pkg, SharpiePackage pkgObj)
        {
            if (!PackageExists(pkg))
            {
                Error.FatalNoContext("Failed to set package data for the package '" + pkg + "', exiting.");
                return;
            }

            //Get index, set package.
            int index = Packages.FindIndex(x => x.PackageName == pkg);

            //Set.
            Packages[index] = pkgObj;
        }
    }

    //A single package within Sharpie.
    public class SharpiePackage
    {
        public SharpiePackage(string name, int ver, string link, string parent)
        {
            PackageName = name;
            PackageVersion = ver;
            Link = link;
            ParentSource = parent;
        }

        public string PackageName;
        public int PackageVersion;
        public int CurrentPackageVersion = -1;
        public bool Installed = false;
        public string Link;
        public string ParentSource;
    }

    //A collection of sources within Sharpie.
    public class SharpieSources
    {
        public List<SharpieSource> Sources = new List<SharpieSource>();
        public bool SourceWarningRead = false;

        //Checks whether a source exists in the current list or not.
        public bool SourceExists(string source)
        {
            return (Sources.FindIndex(x => x.SourceName == source) != -1);
        }
    }

    //A single source in Sharpie.
    public class SharpieSource
    {
        public SharpieSource(string name, string link)
        {
            SourceName = name;
            Link = link;
        }

        public string SourceName;
        public string Link;
    }
}
