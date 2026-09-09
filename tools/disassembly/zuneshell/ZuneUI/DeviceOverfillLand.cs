using System;
using Microsoft.Iris;

namespace ZuneUI;

public class DeviceOverfillLand : DialogPage
{
	private UIDevice _device;

	private SyncGroupList _list;

	private bool _deviceWasLocked;

	public UIDevice Device => _device;

	public SyncGroupList List => _list;

	public override bool IsWizard => false;

	public override bool AllowAdvance
	{
		get
		{
			return true;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	public override bool AllowCancel
	{
		get
		{
			return true;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	public override bool RequireSecurityIcon
	{
		get
		{
			return false;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	public DeviceOverfillLand(UIDevice device)
	{
		_device = device;
		_list = Device.GenerateSyncGroupList((IModelItemOwner)(object)this, expandSyncAllEntries: true);
		Deviceland.InitDevicePage(this);
		base.ShowBackArrow = false;
		base.ShowComputerIcon = ComputerIconState.Hide;
		base.ShowNowPlayingX = false;
		base.ShowPivots = false;
		base.ShowSearch = false;
		base.ShowSettings = false;
		base.UI = "res://ZuneShellResources!DeviceSyncGroups.uix#SyncGroupsPage";
		base.BottomBarUI = "res://ZuneShellResources!DeviceSyncGroups.uix#GasGauge";
	}

	public override void NavigatePage(bool forward)
	{
		throw new NotSupportedException();
	}

	public override bool NavigationAvailable(bool forward)
	{
		return false;
	}

	public override void Save()
	{
		List.CommitChanges(null);
		if (!_deviceWasLocked)
		{
			Device.IsLockedAgainstSyncing = false;
			if (List.GasGauge.FreeSpace >= 0)
			{
				Device.BeginSync(userInitiated: true, syncOnNextNotify: false);
			}
		}
	}

	public override void Exit()
	{
		ZuneShell.DefaultInstance.NavigateBack();
	}

	public override void SaveAndExit()
	{
		Save();
		Exit();
	}

	public override void CancelAndExit()
	{
		Device.IsLockedAgainstSyncing = _deviceWasLocked;
		Exit();
	}

	protected override void OnNavigatedToWorker()
	{
		_deviceWasLocked = Device.IsLockedAgainstSyncing;
		if (!_deviceWasLocked)
		{
			Device.IsLockedAgainstSyncing = true;
		}
		base.OnNavigatedToWorker();
	}
}
