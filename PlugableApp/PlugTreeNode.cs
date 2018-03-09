using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using HostCommon;

namespace PlugableApp
{
	class PlugTreeNode : System.Windows.Forms.TreeNode
	{
		private System.Type _type;
		private IPlug _instance;

		public PlugTreeNode(System.Type type) : base()
		{
			_type=type;
			_instance = (IPlug)Activator.CreateInstance(_type);

			IPlugData[] data = _instance.GetData();
			foreach(IPlugData d in data)
			{
				this.Nodes.Add(new DataTreeNode(d));	
			}

			this.Text = this.DisplayName;
			return;
		}


		public string DisplayName
		{
			get{return _type.GetCustomAttributes(typeof(PlugDisplayNameAttribute),false)[0].ToString();}
		}

		public string Description
		{
			get{return _type.GetCustomAttributes(typeof(PlugDescriptionAttribute),false)[0].ToString();}
		}


		public IPlug Instance
		{
			get
			{
				if(_instance==null)
					_instance = (IPlug)Activator.CreateInstance(_type);

				return _instance;
			}
		}
	}
}