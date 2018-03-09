using System;
using System.Text;
using HostCommon;


internal class EmployeeData : System.Object, IPlugData
{
	
	public event EventHandler DataChanged;
	
	private string _firstName;
	public string FirstName
	{
		get
		{
			return _firstName;
		}
		
		set
		{
			_firstName=value;
			if(DataChanged!=null)
				DataChanged(this, new EventArgs());
			
			return;
		}
	}
	
	private string _lastName;
	public string LastName
	{
		
		get
		{
			return _lastName;
		}
		
		set
		{
			_lastName=value;
			if(DataChanged!=null)
				DataChanged(this, new EventArgs());

			return;
		}
	}
	
	
	internal EmployeeData(string FirstName, string LastName)
	{
		_firstName = FirstName;
		_lastName = LastName;
		return;
	}
	
	public override string ToString()
	{	
		StringBuilder sb = new StringBuilder(_firstName);
		sb.Append(" ");
		sb.Append(_lastName);
		
		return sb.ToString();
	}

}