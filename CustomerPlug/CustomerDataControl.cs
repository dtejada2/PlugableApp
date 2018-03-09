using System;
using System.Drawing;
using System.Windows.Forms;
using HostCommon;

public class CustomerDataControl : PlugDataEditControl
{
	private Label _lblCompanyName;
	private TextBox _txtCompanyName;
	
	internal CustomerDataControl(CustomerData Data)	: base(Data)
	{
		_lblCompanyName = new Label();
		_lblCompanyName.Text = "Company Name";
		_lblCompanyName.Size = new Size(100,15);
		_lblCompanyName.Location = new Point(10,20);

		_txtCompanyName = new TextBox();
		_txtCompanyName.Size = new Size(150,25);
		_txtCompanyName.Location = new Point(10,40);
		_txtCompanyName.Text = ((CustomerData)_data).CompanyName;
		_txtCompanyName.TextChanged += new EventHandler(this._text_TextChanged);
		
		Controls.Add(_lblCompanyName);
		Controls.Add(_txtCompanyName);
		
		return;
	}
	
	private void _text_TextChanged(object sender, EventArgs e)
	{
		if(sender==_txtCompanyName)
			((CustomerData)_data).CompanyName=_txtCompanyName.Text;
	}
}