using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.PacMan
{
    public class SharpieSourceParser
    {
        //Gets a list of packages and a SharpieSource from the source string.
        //Specification:
        //
        //someSourceTitle
        //packageName | 1 | http://link.to/direct.zip
        //...
        public static Tuple<SharpieSource, List<SharpiePackage>> Parse(string src, string sourcelink)
        {
            //Ignore all "\r, \t" and any spaces.
            src = src.Replace("\r", "").Replace("\t", "").Replace(" ", "");

            //Split by line into source lines.
            string[] srcLines = src.Split('\n');

            //The first line is the name.
            string srcName = srcLines[0];

            //For each line following that, attempt to parse a source.
            List<SharpiePackage> packages = new List<SharpiePackage>();
            for (int i=1; i<srcLines.Length; i++)
            {
                string[] pkgParts = srcLines[i].Split('|');

                //Check the length to verify source.
                if (pkgParts.Length != 3)
                {
                    Error.FatalNoContext("Source is invalid, at line " + (i+1) + ", invalid amount of package parameters.");
                    return null;
                }

                //Formulate the package, add to list.
                try
                {
                    packages.Add(new SharpiePackage(pkgParts[0], int.Parse(pkgParts[1]), pkgParts[2], srcName));
                }
                catch
                {
                    Error.FatalNoContext("Source is invalid at line " + (i + 1) + ", is the version number invalid?");
                    return null;
                }
            }

            //Return the tuple.
            return new Tuple<SharpieSource, List<SharpiePackage>>(new SharpieSource(srcName, sourcelink), packages);
        }
    }
}
