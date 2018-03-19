using NuGet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlugableApp
{
    public partial class FrmPlugins : Form
    {
        IPackageRepository _packageRepository;
        public FrmPlugins()
        {
            InitializeComponent();
        }

        private void InstanciateNugetRepository()
        {
            if (_packageRepository == null)
                _packageRepository = PackageRepositoryFactory.Default.CreateRepository("http://localhost:9000/nuget");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciateNugetRepository();

                const bool prerelease = false;

                var packages = _packageRepository.GetPackages().Where(p => prerelease ? p.IsAbsoluteLatestVersion : p.IsLatestVersion);

                //Iterate through the list and print the full name of the pre-release packages to console
                foreach (IPackage p in packages)
                {
                    dgvPlugins.Rows.Add(p.Description, p.GetFullName(), p.Version, p.Id, "Install");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
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

        private void dgvPlugins_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex == 4)
                {
                    DownLoadPackage(dgvPlugins[3, e.RowIndex].Value.ToString(), dgvPlugins[2, e.RowIndex].Value.ToString());
                }
                MessageBox.Show("Plugin installed success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
