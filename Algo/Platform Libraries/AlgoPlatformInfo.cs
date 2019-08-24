using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    //Static class containing platform information for .NET and Algo.
    public static class AlgoPlatformInfo
    {
        //This is a rudamentary bugfix while Mono is broken.
        public static bool IsLinux = Directory.Exists("/");
        public static bool IsWindows = !IsLinux;
    }
}
