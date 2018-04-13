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
    public class NugetPackageManager
    {
        IPackageRepository _packageRepository;

        private void InstanciateNugetRepository()
        {
            if (_packageRepository == null)
                _packageRepository = PackageRepositoryFactory.Default.CreateRepository("http://localhost:9000/nuget");
        }

        private void DownLoadPackage(string packageId, string version)
        {
            InstanciateNugetRepository();
            var packages = _packageRepository.FindPackagesById(packageId);
            if (packages.Any())
            {
                var path = Environment.CurrentDirectory + "/Temp";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                PackageManager packageManager = new PackageManager(_packageRepository, path);

                //Download and unzip the package
                packageManager.InstallPackage(packageId, SemanticVersion.Parse(version));
            }
        }

        public void AddAssemblie(string packagePath, string packageName, SemanticVersion semanticVersion)
        {
            string fileName = Path.Combine(packagePath);
            var file = new PackageReferenceFile(fileName);

            var currentTargetFw = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(TargetFrameworkAttribute), false);
            var targetFrameworkAttribute = ((TargetFrameworkAttribute[])currentTargetFw).FirstOrDefault();

            file.AddEntry(packageName, semanticVersion, false, new FrameworkName(targetFrameworkAttribute.FrameworkName));
        }
    }
}
