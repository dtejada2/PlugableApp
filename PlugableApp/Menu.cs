﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlugableApp
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnPlugins_Click(object sender, EventArgs e)
        {
            FrmPlugins frm = new FrmPlugins();
            frm.ShowDialog();
        }

        private void btnHostForm_Click(object sender, EventArgs e)
        {
            new HostForm();
        }
    }
}
