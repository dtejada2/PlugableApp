using NuGet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class NugetPackageManager : INugetPackageManager
    {
        IPackageRepository _packageRepository;
        string nugetServer = string.Empty;
        public NugetPackageManager()
        {
            nugetServer = ExtensionUtility.GetConfig(nugetServer);

            if (string.IsNullOrEmpty(nugetServer))
                throw new Exception(Common.Messages.NugetServerNotFound);

            if (_packageRepository == null)
                _packageRepository = PackageRepositoryFactory.Default.CreateRepository(nugetServer);
        }

        public IEnumerable<IPackage> GetServerPackageList(bool prerelease = false)
        {
            var packages = _packageRepository.GetPackages().Where(p => prerelease ? p.IsAbsoluteLatestVersion : p.IsLatestVersion);

            //Iterate through the list and print the full name of the pre-release packages to console
            return packages.ToList();
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
}
