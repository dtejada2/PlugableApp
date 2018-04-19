using System.Collections.Generic;
using NuGet;

namespace Utility
{
    public interface INugetPackageManager
    {
        void AddPackageConfig(string packageName, SemanticVersion semanticVersion);
        void DownLoadPackage(string packageId, string version);
        IEnumerable<PackageReference> GetPackageInstalled();
        IEnumerable<IPackage> GetServerPackageList(bool prerelease = false);
        bool RemovePackageConfig(string packageName, SemanticVersion semanticVersion);
        void UpdatePackageConfig(string packageName, SemanticVersion semanticVersion);
    }
}