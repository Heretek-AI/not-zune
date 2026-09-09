using System;
using System.Collections;

namespace ZuneUI;

public class CurrentPageCommandHandler : ICommandHandler
{
	public void Execute(string command, IDictionary commandArgs)
	{
		ZuneShell defaultInstance = ZuneShell.DefaultInstance;
		if (defaultInstance == null)
		{
			throw new InvalidOperationException("No Shell instance has been registered.  Unable to perform navigation.");
		}
		ZunePage currentPage = defaultInstance.CurrentPage;
		currentPage.CommandHandler?.Execute(command, commandArgs);
	}
}
