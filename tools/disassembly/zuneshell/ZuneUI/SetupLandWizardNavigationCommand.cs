using System;
using Microsoft.Iris;

namespace ZuneUI;

public class SetupLandWizardNavigationCommand : Command
{
	private SetupLandPage _page;

	public SetupLandWizardNavigationCommand(SetupLandPage page)
		: base((IModelItemOwner)null, (string)null, (EventHandler)null)
	{
		_page = page;
	}

	protected override void OnInvoked()
	{
		ZuneShell.DefaultInstance.NavigateToPage(_page);
		((Command)this).OnInvoked();
	}
}
