using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Utility;
using System.Text.RegularExpressions;
using System.Diagnostics.Contracts;
using NuGet;
using System.Runtime.Versioning;

namespace Update
{
    class Program
    {
        static string path = Environment.CurrentDirectory;
        static readonly Regex entryRegex = new Regex(@"^([0-9a-fA-F]{40})\s+(\S+)\s+(\d+)[\r]*$");
        static readonly Regex commentRegex = new Regex(@"\s*#.*$");
        static readonly Regex stagingRegex = new Regex(@"#\s+(\d{1,3})%$");

        void waitForParentToExit()
        {
            // Grab a handle the parent process
            var parentPid = NativeMethods.GetParentProcessId();
            var handle = default(IntPtr);

            // Wait for our parent to exit
            try
            {
                handle = NativeMethods.OpenProcess(ProcessAccess.Synchronize, false, parentPid);
                if (handle != IntPtr.Zero)
                {
                    NativeMethods.WaitForSingleObject(handle, 0xFFFFFFFF /*INFINITE*/);
                }
                else
                {
                }
            }
            finally
            {
                if (handle != IntPtr.Zero) NativeMethods.CloseHandle(handle);
            }

        }

        public async Task UpdateSelf()
        {
            waitForParentToExit();
            var src = Assembly.GetExecutingAssembly().Location;
            var updateDotExeForOurPackage = Path.Combine(
                Path.GetDirectoryName(src),
                "..", "Update.exe");

            await Task.Run(() => {
                File.Copy(src, updateDotExeForOurPackage, true);
            });
        }


        static void Main(string[] args)
        {
            string fileName = Path.Combine(path, @"packages.cssonfig");

            var file = new PackageReferenceFile(fileName);
            foreach (PackageReference packageReference in file.GetPackageReferences())
            {
                Console.WriteLine("Id={0}, Version={1}", packageReference.Id, packageReference.Version);
            }

            var currentTargetFw = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(TargetFrameworkAttribute), false);
            var targetFrameworkAttribute = ((TargetFrameworkAttribute[])currentTargetFw).FirstOrDefault();

            // Update the packages.config file    
            file.AddEntry("data", new NuGet.SemanticVersion("10.0.0.1"), false, new FrameworkName(targetFrameworkAttribute.FrameworkName));
            file.DeleteEntry("data", new NuGet.SemanticVersion("10.0.0.1"));

            //XmlHandler xml = new XmlHandler();

            /*path = "C:\\Users\\Daniel-PC2\\Documents\\Visual Studio 2017\\Projects\\PlugableApp\\PlugableApp\\bin\\Debug";
            path = Path.Combine(path, ExtensionUtility.GetConfig("dest"));
           // path = $"{path}/{ExtensionUtility.GetConfig("dest")}";
            IEnumerable<FileInfo> data = ExtensionUtility.GetAllFilesRecursively(new System.IO.DirectoryInfo(path));

            Parallel.ForEach(data, (fileInfo) =>
            {

            });
            foreach (var item in data)
            {
                Assembly assembly = Assembly.LoadFrom(item.FullName);
                Version ver = assembly.GetName().Version;
                string assemblyId = assembly.GetName().Name;
            }

            var database = File.ReadLines("RELEASES", Encoding.UTF8);
            foreach (var item in database)
            {
                ParseReleaseEntry(item);
            }
            //------------------------------------------
            */
        }

        public static Version ParseReleaseEntry(string entry)
        {
            //var database = File.ReadAllText("RELEASEs", Encoding.UTF8);

            Contract.Requires(entry != null);

            float? stagingPercentage = null;
            var m = stagingRegex.Match(entry);
            if (m != null && m.Success)
            {
                stagingPercentage = Single.Parse(m.Groups[1].Value) / 100.0f;
            }

            entry = commentRegex.Replace(entry, "");
            if (String.IsNullOrWhiteSpace(entry))
            {
                return null;
            }

            m = entryRegex.Match(entry);
            if (!m.Success)
            {
                throw new Exception("Invalid release entry: " + entry);
            }

            if (m.Groups.Count != 4)
            {
                throw new Exception("Invalid release entry: " + entry);
            }

            string filename = m.Groups[2].Value;

            // Split the base URL and the filename if an URI is provided,
            // throws if a path is provided
            string baseUrl = null;
            string query = null;

            //if (Utility.IsHttpUrl(filename))
            //{
            //    var uri = new Uri(filename);
            //    var path = uri.LocalPath;
            //    var authority = uri.GetLeftPart(UriPartial.Authority);

            //    if (String.IsNullOrEmpty(path) || String.IsNullOrEmpty(authority))
            //    {
            //        throw new Exception("Invalid URL");
            //    }

            //    var indexOfLastPathSeparator = path.LastIndexOf("/") + 1;
            //    baseUrl = authority + path.Substring(0, indexOfLastPathSeparator);
            //    filename = path.Substring(indexOfLastPathSeparator);

            //    if (!String.IsNullOrEmpty(uri.Query))
            //    {
            //        query = uri.Query;
            //    }
            //}

            if (filename.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
            {
                throw new Exception("Filename can either be an absolute HTTP[s] URL, *or* a file name");
            }

            long size = Int64.Parse(m.Groups[3].Value);

            var obj = new
            {
                Groups = m.Groups[1].Value,
                filename,
                size,
                baseUrl,
                query,
                stagingPercentage
            };


            return null; 
        }
    }
}
