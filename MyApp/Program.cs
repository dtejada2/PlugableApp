using Squirrel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var mgr = new UpdateManager("http://localhost:9000/nuget/Packages")) // "MyApp", "C:\\Users\\Daniel-PC2\\AppData\\Local"(Id='CustomerPlug')
            {
                var res = mgr.CurrentlyInstalledVersion();
                MessageBox.Show(res?.Version?.Revision.ToString());
                mgr.UpdateApp((percent)=>
                {
                    Console.WriteLine(percent);
                });
                mgr.CreateShortcutsForExecutable("MyApp.exe", ShortcutLocation.Desktop, false);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
