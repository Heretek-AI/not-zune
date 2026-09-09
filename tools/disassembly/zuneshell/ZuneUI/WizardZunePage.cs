using System;
using Microsoft.Iris;

namespace ZuneUI;

public class WizardZunePage : ZunePage, IWizardNavigation
{
	private Wizard _wizard;

	private Command _backCommand;

	private Command _nextCommand;

	private Command _finishCommand;

	private Command _cancelCommand;

	public Wizard Wizard => _wizard;

	public Command Back
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_backCommand == null)
			{
				_backCommand = new Command((IModelItemOwner)(object)this);
				((ModelItem)_backCommand).Description = Shell.LoadString(StringId.IDS_BACK_BUTTON);
			}
			return _backCommand;
		}
	}

	public Command Next
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_nextCommand == null)
			{
				_nextCommand = new Command((IModelItemOwner)(object)this);
				((ModelItem)_nextCommand).Description = Shell.LoadString(StringId.IDS_NEXT_BUTTON);
			}
			return _nextCommand;
		}
	}

	public Command Finish
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_finishCommand == null)
			{
				_finishCommand = new Command((IModelItemOwner)(object)this);
				((ModelItem)_finishCommand).Description = Shell.LoadString(StringId.IDS_FINISH_BUTTON);
			}
			return _finishCommand;
		}
	}

	public Command Cancel
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_cancelCommand == null)
			{
				_cancelCommand = new Command((IModelItemOwner)(object)this);
				((ModelItem)_cancelCommand).Description = Shell.LoadString(StringId.IDS_CANCEL_BUTTON);
			}
			return _cancelCommand;
		}
	}

	internal WizardZunePage(Node node, Wizard wizard)
	{
		_wizard = wizard;
		base.UI = "res://ZuneShellResources!WizardControls.uix#WizardZunePage";
		base.BackgroundUI = "res://ZuneShellResources!WizardControls.uix#WizardBackground";
		base.TransportControlStyle = TransportControlStyle.None;
		base.PivotPreference = node;
		base.ShowCDIcon = false;
		base.ShowDeviceIcon = false;
		base.ShowPlaylistIcon = false;
		base.ShowSettings = false;
		base.ShowSearch = false;
		base.ShowNowPlayingBackgroundOnIdle = false;
		base.CanEnterCompactMode = false;
		base.NotificationAreaVisible = false;
		base.TransportControlsVisible = false;
		Cancel.Invoked += OnCancelInvoked;
		Finish.Invoked += OnFinishInvoked;
	}

	public override bool HandleBack()
	{
		return false;
	}

	public override IPageState SaveAndRelease()
	{
		return null;
	}

	private void LeaveWizard()
	{
		ZuneShell.DefaultInstance.NavigateBack();
	}

	private void OnCancelInvoked(object sender, EventArgs e)
	{
		LeaveWizard();
	}

	private void OnFinishInvoked(object sender, EventArgs e)
	{
		LeaveWizard();
	}
}
