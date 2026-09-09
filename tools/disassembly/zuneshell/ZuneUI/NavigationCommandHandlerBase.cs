using System;
using System.Collections;

namespace ZuneUI;

public abstract class NavigationCommandHandlerBase : ICommandHandler
{
	protected string _pageUIPath;

	public string UIPath
	{
		get
		{
			return _pageUIPath;
		}
		set
		{
			_pageUIPath = value;
		}
	}

	protected abstract ZunePage GetPage(IDictionary args);

	public void Execute(string command, IDictionary commandArgs)
	{
		ZuneShell defaultInstance = ZuneShell.DefaultInstance;
		if (defaultInstance == null)
		{
			throw new InvalidOperationException("No Shell instance has been registered.  Unable to perform navigation.");
		}
		if (Shell.IsUIPathEnabled(UIPath))
		{
			ZunePage page = GetPage(commandArgs);
			if (commandArgs != null)
			{
				page.NavigationArguments = commandArgs;
			}
			page.NavigationCommand = command;
			if (UIPath != null)
			{
				page.UIPath = UIPath;
			}
			defaultInstance.NavigateToPage(page);
		}
	}
}
