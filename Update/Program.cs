using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Update
{
    class Program
    {
        static string path = Environment.CurrentDirectory;

        static void Main(string[] args)
        {
            path = "C:\\Users\\Daniel-PC2\\Documents\\Visual Studio 2017\\Projects\\PlugableApp\\PlugableApp\\bin\\Debug";
            path = $"{path}/{Utility.GetConfig("dest")}";
            IEnumerable<FileInfo> data = Utility.GetAllFilesRecursively(new System.IO.DirectoryInfo(path));

            Parallel.ForEach(data, (fileInfo) =>
            {

            });
            foreach (var item in data)
            {
                //item.FullName

                Assembly assembly = Assembly.LoadFrom(item.FullName);
                Version ver = assembly.GetName().Version;
            }
        }

        
    }
}
