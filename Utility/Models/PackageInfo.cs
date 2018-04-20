using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class PackageInfo
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public Version CurrentVersion { get; set; }
        public Version ServerVersion { get; set; }


        public string CurrentVersionString { get { return CurrentVersion != null ? CurrentVersion.ToString() : ""; } }
        public string ServerVersionString { get { return ServerVersion != null ? ServerVersion.ToString() : ""; } }
        public string VersionAction
        {
            get
            {
                if (CurrentVersion == null || ServerVersion == null)
                    return "";

                var result = CurrentVersion.CompareTo(ServerVersion);

                if (result < 0)
                {
                    return "Update";
                }
                else if (result == 0)
                {
                    return "No action";
                }
                else
                {
                    return "Downgrade";
                }
            }
        }
    }
}