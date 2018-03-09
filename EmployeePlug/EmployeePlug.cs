using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using HostCommon;



[PlugDisplayName("Employees")]
[PlugDescription("This plug is for managing employee data")]
public class EmployeePlug : System.Object, IPlug
{

	
	public EmployeePlug() : base()
	{
		return;
	}
	
	public IPlugData[] GetData()
	{
	
		IPlugData[] data = new EmployeeData[]{
						new EmployeeData("Jerry", "Seinfeld")
						,new EmployeeData("Bill", "Cosby")
						,new EmployeeData("Martin", "Lawrence")
						};
		
		return data;
	
	}
	
	public PlugDataEditControl GetEditControl(IPlugData Data)
	{
		return new EmployeeControl((EmployeeData)Data);
	}
	
	
	public bool Save(string Path)
	{
		
		MessageBox.Show("todo: add SAVE implementation for EmployeePlug here");

		return true;
	}

	public bool Print(PrintDocument Document)
	{

		MessageBox.Show("todo: add PRINT implementation for EmployeePlug here");

		return true;
			
	}
	

}




