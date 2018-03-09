using System;
using HostCommon;


internal class CustomerData : System.Object, IPlugData
{
	public event EventHandler DataChanged;
	
	internal CustomerData(string CompanyName)
	{
		_companyName = CompanyName;
		return;
	}
	
	public string _companyName;
	public string CompanyName
	{
		get
		{
			return _companyName;
		}

		set
		{
			_companyName=value;
			if(DataChanged!=null)
				DataChanged(this, new EventArgs());

			return;
		}
	}
	
	public override string ToString()
	{	
		return _companyName;
	}

}