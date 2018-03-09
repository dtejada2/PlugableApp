using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace HostCommon
{

	public interface IPlug
	{


		IPlugData[] GetData();
		PlugDataEditControl GetEditControl(IPlugData Data);
		bool Save(string Path);
		bool Print(PrintDocument Document);


	}

}