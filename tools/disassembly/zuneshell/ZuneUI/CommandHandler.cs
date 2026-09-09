using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class CommandHandler : Command, ICommandHandler
{
	private IDictionary _arguments;

	public IDictionary Arguments => _arguments;

	public void Execute(string command, IDictionary commandArgs)
	{
		_arguments = commandArgs;
		((Command)this).Invoke();
	}
}
