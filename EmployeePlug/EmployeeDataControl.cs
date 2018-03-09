using System;
using System.Drawing;
using System.Windows.Forms;
using HostCommon;

public class EmployeeControl : PlugDataEditControl
{
	
	private Label _lblFirstName;
	private TextBox _txtFirstName;
	
	private Label _lblLastName;
	private TextBox _txtLastName;
		
	internal EmployeeControl(EmployeeData employee) : base(employee)
	{
		
		
		_lblFirstName = new Label();
		_lblFirstName.Text = "First Name";
		_lblFirstName.Size = new Size(100,15);
		_lblFirstName.Location = new Point(10,20);
		
		_txtFirstName = new TextBox();
		_txtFirstName.Size = new Size(150,25);
		_txtFirstName.Location = new Point(10,40);
		_txtFirstName.Text = ((EmployeeData)_data).FirstName;
		_txtFirstName.TextChanged += new EventHandler(this._text_TextChanged);
		
		_lblLastName = new Label();
		_lblLastName.Text = "Last Name";
		_lblLastName.Size = new Size(100,15);
		_lblLastName.Location = new Point(10,75);

		_txtLastName = new TextBox();
		_txtLastName.Size = new Size(150,25);
		_txtLastName.Location = new Point(10,95);
		_txtLastName.Text = ((EmployeeData)_data).LastName;
		_txtLastName.TextChanged += new EventHandler(this._text_TextChanged);
		
		this.Controls.Add(_lblFirstName);
		this.Controls.Add(_txtFirstName);
		this.Controls.Add(_lblLastName);
		this.Controls.Add(_txtLastName);
		
	
		return;
	}
	
	private void _text_TextChanged(object sender, EventArgs e)
	{
		if(sender==_txtFirstName)
			((EmployeeData)_data).FirstName=_txtFirstName.Text;
		else if(sender==_txtLastName)
			((EmployeeData)_data).LastName=_txtLastName.Text;
			
	}

}