using System;
using System.IO;
using System.Linq;

namespace Poseidon.AppPages.Tools
{
    public static class Extension
    {
        public static bool IsXaml(this string fileName)
        {
            return !fileName.IsNullOrEmpty() && Path.GetExtension(fileName) == ".xaml";
        }

        internal static bool IsNullOrEmpty(this string source)
        {
            return source == null || string.IsNullOrEmpty(source.Trim());
        }
    }
}
