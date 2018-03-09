using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using HostCommon;

namespace PlugableApp
{

	class HostForm : System.Windows.Forms.Form
	{
		private MenuItem _file;
		private MenuItem _save;
		private MenuItem _print;
		private MenuItem _sep;
		private MenuItem _exit;

		private TreeView _tree;
		private Splitter _split;
		private Panel _panel;
		private StatusBar _status;


		public HostForm () : base ()
		{

			this.Menu = new MainMenu();
			_file = new MenuItem("File", new EventHandler(this._menuItem_Clicked));
			_print = new MenuItem("Print", new EventHandler(this._menuItem_Clicked));
			_save = new MenuItem("Save", new EventHandler(this._menuItem_Clicked));
			_sep = new MenuItem("-");
			_exit = new MenuItem("Exit",new EventHandler(this._menuItem_Clicked));
			_file.MenuItems.AddRange(new MenuItem[]{_print, _save,_sep,_exit});
			this.Menu.MenuItems.Add(_file);

			_tree = new TreeView();
			_tree.Dock = DockStyle.Left;
			_tree.AfterSelect += new TreeViewEventHandler(this._tree_AfterSelect);

			_split = new Splitter();
			_split.Dock = DockStyle.Left;

			_panel = new Panel();
			_panel.Dock = DockStyle.Fill;
			_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

			_status = new StatusBar();
			_status.ShowPanels=true;
			_status.Panels.Add("Description");

			this.Controls.Add(_panel);
			this.Controls.Add(_split);
			this.Controls.Add(_tree);
			this.Controls.Add(_status);


			this.LoadPlugs();


			this.Size = new Size(400,350);
			this.Text = "Host Application";
			_status.Panels[0].Width=_status.Width;
			this.Show();



			return;
		}

		private PlugTreeNode GetSelectedPlug()
		{

			TreeNode node = _tree.SelectedNode as PlugTreeNode;
			if(node!=null)
				return (PlugTreeNode)node;
			else
			{
				node = _tree.SelectedNode.Parent as PlugTreeNode;
				if(node!=null)
					return (PlugTreeNode)node;
			}

			return null;

		}

		private void _menuItem_Clicked(object sender, EventArgs e)
		{
			if(sender==_print)
			{

				PrintDocument doc = new PrintDocument();
				PrintDialog pd = new PrintDialog();
				pd.Document = doc;

				if (pd.ShowDialog()==DialogResult.OK)
				{
					PlugTreeNode node = GetSelectedPlug();
					node.Instance.Print(doc);
				}


			}
			else if(sender==_save)
			{


				SaveFileDialog sfd = new SaveFileDialog();
				if (sfd.ShowDialog()==DialogResult.OK)
				{
					PlugTreeNode node = GetSelectedPlug();
					node.Instance.Save(sfd.FileName);
				}


			}
			else if(sender==_exit)
				this.Close();
		}

		private void _tree_AfterSelect(object sender, TreeViewEventArgs e)
		{

			foreach(Control c in _panel.Controls)
				c.Dispose();
			_panel.Controls.Clear();


			TreeNode node=null;
			node = e.Node as PlugTreeNode;

			if(node!=null)
				_status.Panels[0].Text = ((PlugTreeNode)node).Description;
			else
			{
				node = e.Node as DataTreeNode;
				if(node!=null)
				{
					_status.Panels[0].Text = ((PlugTreeNode)node.Parent).Description;
					_panel.Controls.Add( ((PlugTreeNode)node.Parent).Instance.GetEditControl(((DataTreeNode)node).Data));	
				}

			}	

			return;
		}

		private void LoadPlugs()
		{

			string[] files = Directory.GetFiles("Plugs", "*.dll");

			foreach(string f in files)
			{
				try
				{
					Assembly a = Assembly.LoadFrom(f);

					System.Type[] types = a.GetTypes();
					foreach(System.Type type in types)
					{
						if(type.GetInterface("IPlug")!=null)
						{
							if(type.GetCustomAttributes(typeof(PlugDisplayNameAttribute),false).Length!=1)
								throw new PlugNotValidException(type, "PlugDisplayNameAttribute is not supported");
							if(type.GetCustomAttributes(typeof(PlugDescriptionAttribute),false).Length!=1)
								throw new PlugNotValidException(type, "PlugDescriptionAttribute is not supported");

							_tree.Nodes.Add(new PlugTreeNode(type));
						}
					}
				}
				catch(Exception e)
				{
					MessageBox.Show(e.Message);
				}
			}
			return;
		}
        
		protected override void Dispose(bool disposing)
		{
			GC.SuppressFinalize(this);
			base.Dispose(disposing);
			return;
		}
	}
}





















