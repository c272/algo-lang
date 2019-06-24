using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    //Static class containing platform information for .NET and Algo.
    public static class AlgoPlatformInfo
    {
        public static bool IsLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        public static bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }
}
