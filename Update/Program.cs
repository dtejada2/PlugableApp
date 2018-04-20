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
            //waitForParentToExit();
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
            INugetPackageManager npm = new NugetPackageManager();
            var installedPackages = npm.GetPackageInstalled();
            var serverpackage = npm.GetServerPackageList();

            foreach (PackageReference package in installedPackages)
            {
                var result = serverpackage.Where(v => v.Id == package.Id).FirstOrDefault();
                if(result != null)
                {
                    var installed = package.Version.CompareTo(result);
                }
            }
        }
    }
}
