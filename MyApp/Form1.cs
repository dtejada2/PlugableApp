using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

            Assembly assembly = Assembly.GetExecutingAssembly();

			locationTextBox.Text = assembly.Location;
            versionTextBox.Text = assembly.GetName().Version.ToString(3);
		}
	}
}
