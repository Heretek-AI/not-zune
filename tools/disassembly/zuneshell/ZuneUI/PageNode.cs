using System;
using System.Collections;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class PageNode : Node
{
	private GetPageCallback _handler;

	public PageNode(Experience owner, StringId id, GetPageCallback handler, SQMDataId sqmDataID)
		: base(owner, id, null, sqmDataID)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		_handler = handler;
	}

	protected override void Execute(Shell shell)
	{
		Invoke(null);
	}

	public void Invoke(IDictionary commandArgs)
	{
		ZuneShell defaultInstance = ZuneShell.DefaultInstance;
		if (defaultInstance == null)
		{
			throw new InvalidOperationException("No Shell instance has been registered.  Unable to perform navigation.");
		}
		ZunePage zunePage = _handler();
		if (commandArgs != null)
		{
			zunePage.NavigationArguments = commandArgs;
		}
		defaultInstance.NavigateToPage(zunePage);
	}
}
