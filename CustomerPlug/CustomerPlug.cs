using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using HostCommon;



[PlugDisplayName("Customers")]
[PlugDescription("This plug is for managing customer relationships")]
public class CustomerPlug : System.Object, IPlug
{

	public CustomerPlug() : base()
	{
		return;
	}
	
	public IPlugData[] GetData()
	{
	
		IPlugData[] data = new CustomerData[]{
						new CustomerData("Laugh Factory")
						,new CustomerData("Improv")
						};
		
		return data;
	
	}
	
	public PlugDataEditControl GetEditControl(IPlugData Data)
	{

		return new CustomerDataControl((CustomerData)Data);
			
	}
	
	public bool Save(string Path)
	{
		
		MessageBox.Show("todo: add SAVE implementation for CustomerPlug here");
		
		return true;
	}
	
	public bool Print(PrintDocument Document)
	{
		
		
		MessageBox.Show("todo: add PRINT implementation for CustomerPlug here");
				
		return true;
		
	}
	
	

}










