using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class CommandHandlerBase : ICommandHandler
{
	private IDictionary _arguments;

	private ICommand _executeCommand;

	public IDictionary Arguments => _arguments;

	public ICommand ExecuteCommand
	{
		get
		{
			return _executeCommand;
		}
		set
		{
			if (_executeCommand != value)
			{
				_executeCommand = value;
			}
		}
	}

	public void Execute(string command, IDictionary commandArgs)
	{
		_arguments = commandArgs;
		if (_executeCommand != null)
		{
			_executeCommand.Invoke();
		}
	}
}
