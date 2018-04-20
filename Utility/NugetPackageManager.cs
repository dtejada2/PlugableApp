using NuGet;
using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Versioning;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text;
using System.Diagnostics;

namespace Utility
{
    public class NugetPackageManager : INugetPackageManager
    {
        IPackageRepository _packageRepository;
        string rootAppDirectory;

        public NugetPackageManager()
        {
            string _nugetServer = Common.Config.nugetServer;
            var nugetServer = ExtensionUtility.GetConfig(_nugetServer);

            if (string.IsNullOrEmpty(nugetServer))
                throw new Exception(Common.Messages.NugetServerNotFound);

            if (_packageRepository == null)
                _packageRepository = PackageRepositoryFactory.Default.CreateRepository(nugetServer);
        }
        
        public IEnumerable<IPackage> GetServerPackageList(bool prerelease = false)
        {
            prerelease = false;
            //prerelease ? p.IsAbsoluteLatestVersion: p.IsLatestVersion
            var packages = _packageRepository.GetPackages().Where(p => (prerelease && p.IsAbsoluteLatestVersion) || (!prerelease && p.IsLatestVersion));
            foreach (IPackage p in packages)
            {
                var desc = p.Description;
            }
            //Iterate through the list and print the full name of the pre-release packages to console
            return packages.ToList();
        }

        public void InstallPackage(string packageId, string version)
        {
            DownLoadPackage(packageId, version);

            AddPackageConfig(packageId, SemanticVersion.Parse(version));
        }

        public void UninstallPackage(string packageId, string version)
        {
            KillAllProcessesBelongingToPackage();

            //TODO check if the dll or exe is in use
            if (true)
                RemovePackageConfig(packageId, SemanticVersion.Parse(version));
        }

        public void KillAllProcessesBelongingToPackage()
        {
            var ourExe = Assembly.GetEntryAssembly();
            var ourExePath = ourExe != null ? ourExe.Location : null;

            UnsafeUtility.EnumerateProcesses()
                .Where(x =>
                {
                    // Processes we can't query will have an empty process name, we can't kill them
                    // anyways
                    if (String.IsNullOrWhiteSpace(x.Item1)) return false;

                    // Files that aren't in our root app directory are untouched
                    if (!x.Item1.StartsWith(rootAppDirectory, StringComparison.OrdinalIgnoreCase)) return false;

                    // Never kill our own EXE
                    if (ourExePath != null && x.Item1.Equals(ourExePath, StringComparison.OrdinalIgnoreCase)) return false;

                    var name = Path.GetFileName(x.Item1).ToLowerInvariant();
                    if (name == "Update.exe") return false;

                    return true;
                })
                .ForEach(x =>
                {
                    try
                    {
                        Process.GetProcessById(x.Item2).Kill();
                        //this.WarnIfThrows(() => Process.GetProcessById(x.Item2).Kill());
                    }
                    catch { }
                });
        }

        private void RemoveFile(string packageId)
        {
            var file = LocatePackage(packageId);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        //TODO
        private string LocatePackage(string packageId)
        {
            return "";
        }

        public void DownLoadPackage(string packageId, string version)
        {
            var packages = _packageRepository.FindPackagesById(packageId);
            if (packages.Any())
            {
                var path = Common.Config.TempRelativeDirectory.GetFullPath();
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                PackageManager packageManager = new PackageManager(_packageRepository, path);

                //Download and unzip the package
                packageManager.InstallPackage(packageId, SemanticVersion.Parse(version));
            }
        }

        public void AddPackageConfig(string packageName, SemanticVersion semanticVersion)
        {
            var file = GetPackageReferenceFile();
            var targetFrameworkAttribute = GetTargetFrameworkAttribute();
            file.AddEntry(packageName, semanticVersion, false, new FrameworkName(targetFrameworkAttribute.FrameworkName));
        }

        private PackageReferenceFile GetPackageReferenceFile()
        {
            var configPath = Common.Config.packageConfigRelativePath;
            configPath = ExtensionUtility.GetConfig(configPath);
            return new PackageReferenceFile(configPath.GetFullPath());
        }

        private TargetFrameworkAttribute GetTargetFrameworkAttribute()
        {
            var currentTargetFw = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(TargetFrameworkAttribute), false);
            return ((TargetFrameworkAttribute[])currentTargetFw).FirstOrDefault();
        }

        public void UpdatePackageConfig(string packageName, SemanticVersion semanticVersion)
        {
            var file = GetPackageReferenceFile();
            var targetFrameworkAttribute = GetTargetFrameworkAttribute();
            file.AddEntry(packageName, semanticVersion, false, new FrameworkName(targetFrameworkAttribute.FrameworkName));
        }

        public bool RemovePackageConfig(string packageName, SemanticVersion semanticVersion)
        {
            var file = GetPackageReferenceFile();

            if (file.EntryExists(packageName, semanticVersion))
                return file.DeleteEntry(packageName, semanticVersion);
            return false;
        }
        
        public IEnumerable<PackageReference> GetPackageInstalled()
        {
            var file = GetPackageReferenceFile();
            return file.GetPackageReferences();
        }
    }

    static unsafe class UnsafeUtility
    {
        public static List<Tuple<string, int>> EnumerateProcesses()
        {
            int bytesReturned = 0;
            var pids = new int[2048];

            fixed (int* p = pids)
            {
                if (!NativeMethods.EnumProcesses((IntPtr)p, sizeof(int) * pids.Length, out bytesReturned))
                {
                    throw new Win32Exception("Failed to enumerate processes");
                }

                if (bytesReturned < 1) throw new Exception("Failed to enumerate processes");
            }

            return Enumerable.Range(0, bytesReturned / sizeof(int))
                .Where(i => pids[i] > 0)
                .Select(i => {
                    try
                    {
                        var hProcess = NativeMethods.OpenProcess(ProcessAccess.QueryLimitedInformation, false, pids[i]);
                        if (hProcess == IntPtr.Zero) throw new Win32Exception();

                        var sb = new StringBuilder(256);
                        var capacity = sb.Capacity;
                        if (!NativeMethods.QueryFullProcessImageName(hProcess, 0, sb, ref capacity))
                        {
                            throw new Win32Exception();
                        }

                        NativeMethods.CloseHandle(hProcess);
                        return Tuple.Create(sb.ToString(), pids[i]);
                    }
                    catch (Exception)
                    {
                        return Tuple.Create(default(string), pids[i]);
                    }
                })
                .ToList();
        }
    }
}
