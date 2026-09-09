using Microsoft.Iris;

namespace ZuneUI;

public abstract class DeviceWizard : Wizard
{
	private bool _disconnected;

	private UIDevice _activeDevice;

	public UIDevice ActiveDevice
	{
		get
		{
			if (_activeDevice == null)
			{
				_activeDevice = ((DeviceManagement.SetupDevice != null) ? DeviceManagement.SetupDevice : SyncControls.Instance.CurrentDeviceOverride);
			}
			return _activeDevice;
		}
	}

	public bool Disconnected
	{
		get
		{
			return _disconnected;
		}
		set
		{
			if (_disconnected != value)
			{
				_disconnected = value;
				((ModelItem)this).FirePropertyChanged("Disconnected");
			}
		}
	}

	public DeviceWizard()
	{
		SingletonModelItem<UIDeviceList>.Instance.DeviceDisconnectedEvent += OnDeviceDisconnected;
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing)
		{
			SingletonModelItem<UIDeviceList>.Instance.DeviceDisconnectedEvent -= OnDeviceDisconnected;
		}
		base.OnDispose(disposing);
	}

	public override void Cancel()
	{
		CancelSettings();
		if (ActiveDevice != UIDeviceList.NullDevice && (ActiveDevice.RequiresFirmwareUpdate || ActiveDevice.Relationship == DeviceRelationship.None || !ActiveDevice.InStandardMode))
		{
			SingletonModelItem<UIDeviceList>.Instance.HideDevice(ActiveDevice);
		}
		else if (ActiveDevice.Relationship == DeviceRelationship.Guest)
		{
			ZuneShell.DefaultInstance.Management.DisposeDeviceManagement(deviceManagementLocked: false);
		}
	}

	private void OnDeviceDisconnected(object sender, DeviceListEventArgs args)
	{
		if (ActiveDevice == args.Device)
		{
			Disconnected = true;
		}
	}
}
