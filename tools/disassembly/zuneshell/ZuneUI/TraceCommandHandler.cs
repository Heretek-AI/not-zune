using System.Collections;

namespace ZuneUI;

public class TraceCommandHandler : ICommandHandler
{
	public void Execute(string command, IDictionary commandArgs)
	{
		Trace(command, commandArgs);
	}

	public static void Trace(string command, IDictionary commandArgs)
	{
		if (commandArgs == null)
		{
			return;
		}
		foreach (object key in commandArgs.Keys)
		{
			_ = commandArgs[key];
		}
	}
}
