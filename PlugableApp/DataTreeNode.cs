using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using HostCommon;

namespace PlugableApp
{
	class DataTreeNode : System.Windows.Forms.TreeNode
	{
		private IPlugData _data;
		public IPlugData Data{get{return _data;}}

		public DataTreeNode(IPlugData Data) : base()
		{
			_data=Data;
			this.Text = _data.ToString();
			_data.DataChanged += new EventHandler(this._data_DataChanged);
			return;
		}

		private void _data_DataChanged(object sender, EventArgs e)
		{
			this.Text = _data.ToString();
			return;
		}
	}
}