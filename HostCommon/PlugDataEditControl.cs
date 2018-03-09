using System;
using System.Windows.Forms;

namespace HostCommon
{


	public abstract class PlugDataEditControl : System.Windows.Forms.UserControl
	{

		protected IPlugData _data;

		public PlugDataEditControl(IPlugData Data)
		{
			_data=Data;
		}



	}

}