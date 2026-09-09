using System;
using Microsoft.Iris;

namespace ZuneUI;

public class DeviceUpdateWizard : DeviceWizard
{
	private UIFirmwareUpdater _updater;

	private bool _isUpdateAvailable;

	private bool _isLiveIdSignInSuccess;

	private bool _chainedUpdate;

	public UIFirmwareUpdater UIFirmwareUpdater
	{
		get
		{
			return _updater;
		}
		set
		{
			if (_updater != value)
			{
				_updater = value;
				((ModelItem)this).FirePropertyChanged("UIFirmwareUpdater");
				IsUpdateAvailable = _updater.IsUpdateAvailable;
			}
		}
	}

	public bool IsUpdateAvailable
	{
		get
		{
			return _isUpdateAvailable;
		}
		set
		{
			if (_isUpdateAvailable != value)
			{
				_isUpdateAvailable = value;
				((ModelItem)this).FirePropertyChanged("IsUpdateAvailable");
			}
		}
	}

	public bool IsChainedUpdate
	{
		get
		{
			return _chainedUpdate;
		}
		set
		{
			if (_chainedUpdate != value)
			{
				_chainedUpdate = value;
				((ModelItem)this).FirePropertyChanged("IsChainedUpdate");
			}
		}
	}

	public bool IsLiveIdSignInSuccess
	{
		get
		{
			return _isLiveIdSignInSuccess;
		}
		set
		{
			_isLiveIdSignInSuccess = value;
			((ModelItem)this).FirePropertyChanged("IsLiveIdSignInSuccess");
		}
	}

	public DeviceUpdateWizard()
	{
		AddPages();
	}

	protected virtual void AddPages()
	{
		AddPage(new DeviceUpdateCheckingPage(this));
		AddPage(new DeviceUpdateEULAPage(this));
		AddPage(new DeviceUpdateBatteryPowerPage(this));
		AddPage(new DeviceUpdateGuestWarningPage(this));
		AddPage(new DeviceUpdateSyncingPage(this));
		AddPage(new DeviceUpdateDiskSpaceErrorPage(this));
		AddPage(new DeviceUpdateProgressPage(this));
		AddPage(new DeviceUpdateSummaryPage(this));
		AddPage(new DeviceUpdateErrorPage(this));
	}

	public override void Cancel()
	{
		if (_updater != null && _updater.UpdateInProgress)
		{
			_updater.CancelFirmwareUpdate();
		}
		if (base.ActiveDevice != null && base.ActiveDevice != UIDeviceList.NullDevice)
		{
			base.ActiveDevice.SequentialUpdatesInstalled = 0;
			base.ActiveDevice.SkipFutureBackupRequests = false;
		}
		if ((base.CurrentPage is DeviceUpdatePage && (base.ActiveDevice == UIDeviceList.NullDevice || !base.ActiveDevice.RequiresFirmwareUpdate)) || base.CurrentPage is DeviceUpdateErrorPage)
		{
			OnCommitChanges();
		}
		else
		{
			base.Cancel();
		}
	}

	public void SignInWithDeviceLiveId(UIDevice device, string password)
	{
		SignIn.Instance.SignOut();
		SignIn.Instance.CancelSignIn();
		string liveId = device.LiveId;
		if (string.IsNullOrEmpty(liveId) || string.IsNullOrEmpty(password))
		{
			IsLiveIdSignInSuccess = false;
			return;
		}
		SignIn.Instance.SignInStatusUpdatedEvent += OnSignInStatusUpdatedEvent;
		SignIn.Instance.SignInUser(device.LiveId, password);
	}

	private void OnSignInStatusUpdatedEvent(object sender, EventArgs e)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		if (!SignIn.Instance.SigningIn)
		{
			HRESULT signInError = SignIn.Instance.SignInError;
			if (((HRESULT)(ref signInError)).IsError || SignIn.Instance.SignedIn)
			{
				SignIn.Instance.SignInStatusUpdatedEvent -= OnSignInStatusUpdatedEvent;
				HRESULT signInError2 = SignIn.Instance.SignInError;
				IsLiveIdSignInSuccess = ((HRESULT)(ref signInError2)).IsSuccess;
			}
		}
	}
}
