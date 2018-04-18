using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ExtensionUtility
    {
        public static string GetConfig(string confKey)
        {
            var config = ConfigurationManager.AppSettings[confKey];
            if (config == null)
                throw new Exception(Common.Messages.KeyConfigNotFound);

            return config.ToString();
        }

        public static string GetFullPath(this string relativePath)
        {
            return Path.Combine(Environment.CurrentDirectory, relativePath);
        }

        public static Version ToVersion(this string version)
        {
            return new Version(version);
        }

        public static bool VersionGreaterThanActual(this string source, string target)
        {
            var version1 = source.ToVersion();
            var version2 = target.ToVersion();

            var result = version1.CompareTo(version2);
            if (result > 0)
                return true;
            else //if (result < 0)
                return false;
        }

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
            Contract.Requires(!string.IsNullOrEmpty(from) && File.Exists(from));
            Contract.Requires(!string.IsNullOrEmpty(to));

            if (!File.Exists(from))
            { 
                return;
            }

            // XXX: SafeCopy
            await Task.Run(() => File.Copy(from, to, true));
        }
    }
}
