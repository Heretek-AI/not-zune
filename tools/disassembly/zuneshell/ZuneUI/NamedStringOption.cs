using System;
using Microsoft.Iris;

namespace ZuneUI;

public class NamedStringOption : Command
{
	private string _value;

	public string Value
	{
		get
		{
			return _value;
		}
		set
		{
			if (_value != value)
			{
				_value = value;
				((ModelItem)this).FirePropertyChanged("Value");
			}
		}
	}

	public NamedStringOption()
	{
	}

	public NamedStringOption(string description, string value)
		: base((IModelItemOwner)null, description, (EventHandler)null)
	{
		_value = value;
	}
}
