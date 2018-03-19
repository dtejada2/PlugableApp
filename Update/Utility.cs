using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update
{
    public static class Utility
    {
        public static string GetConfig(string confKey)
        {
            var config = ConfigurationManager.AppSettings[confKey];
            if (config == null)
                throw new Exception("confKey not found!");

            return config.ToString();
        }

        //static string getAppNameFromDirectory(string path = null)
        //{
        //    path = path ?? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //    return (new DirectoryInfo(path)).Name;
        //}

        public static IEnumerable<FileInfo> GetAllFilesRecursively(this DirectoryInfo rootPath)
        {
            Contract.Requires(rootPath != null);

            return rootPath.EnumerateFiles("*", SearchOption.AllDirectories);
        }

        public static IEnumerable<string> GetAllFilePathsRecursively(string rootPath)
        {
            Contract.Requires(rootPath != null);

            return Directory.EnumerateFiles(rootPath, "*", SearchOption.AllDirectories);
        }

        public static async Task CopyToAsync(string from, string to)
        {
            Contract.Requires(!String.IsNullOrEmpty(from) && File.Exists(from));
            Contract.Requires(!String.IsNullOrEmpty(to));

            if (!File.Exists(from))
            { 
                return;
            }

            // XXX: SafeCopy
            await Task.Run(() => File.Copy(from, to, true));
        }
    }
}
