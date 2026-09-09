using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class ShellCommand : Command
{
	private string _command;

	private IDictionary _commandArguments;

	public string Command
	{
		get
		{
			return _command;
		}
		set
		{
			if (_command != value)
			{
				_command = value;
				((ModelItem)this).FirePropertyChanged("Command");
			}
		}
	}

	public IDictionary CommandArguments
	{
		get
		{
			if (_commandArguments == null)
			{
				CommandArguments = new Hashtable();
			}
			return _commandArguments;
		}
		set
		{
			if (_commandArguments != value)
			{
				_commandArguments = value;
				((ModelItem)this).FirePropertyChanged("CommandArguments");
			}
		}
	}

	protected override void OnInvoked()
	{
		if (_command != null)
		{
			ZuneShell.DefaultInstance?.Execute(_command, _commandArguments);
		}
		((Command)this).OnInvoked();
	}
}
