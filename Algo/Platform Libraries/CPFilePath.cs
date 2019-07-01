using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    //Cross platform file pathing algorithms.
    public class CPFilePath
    {
        //Converts a given path to the correct format for a platform.
        public static string GetPlatformFilePath(string[] pathparts)
        {
            return Path.Combine(pathparts);
        }

        //Creates the default required directories for Algo.
        public static void CreateDefaultDirectories()
        {
            //Packages directory.
            Directory.CreateDirectory(GetPlatformFilePath(DefaultDirectories.PackagesDirectory));

            //Standard library directory.
            Directory.CreateDirectory(GetPlatformFilePath(DefaultDirectories.StandardLibDirectory));
        }
    }

    //A list of required default directories for Algo.
    public static class DefaultDirectories
    {
        public static string[] PackagesDirectory = { AppDomain.CurrentDomain.BaseDirectory, "packages" };
        public static string[] StandardLibDirectory = { AppDomain.CurrentDomain.BaseDirectory, "std" };
        public static string[] WorkingDirectory = { Environment.CurrentDirectory };
    }
}
