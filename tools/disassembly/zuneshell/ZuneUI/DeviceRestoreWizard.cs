using Microsoft.Iris;

namespace ZuneUI;

public class DeviceRestoreWizard : DeviceWizard
{
	private UIFirmwareRestorer _restorer;

	public UIFirmwareRestorer UIFirmwareRestorer
	{
		get
		{
			return _restorer;
		}
		set
		{
			if (_restorer != value)
			{
				_restorer = value;
				((ModelItem)this).FirePropertyChanged("UIFirmwareRestorer");
			}
		}
	}

	public DeviceRestoreWizard()
	{
		AddPage(new DeviceRestoreConfirmationPage(this));
		AddPage(new DeviceRestoreBatteryPowerPage(this));
		AddPage(new DeviceRestoreSyncingPage(this));
		AddPage(new DeviceRestoreProgressPage(this));
		AddPage(new DeviceRestoreSummaryPage(this));
		AddPage(new DeviceRestoreErrorPage(this));
	}

	public override void Cancel()
	{
		if (_restorer != null && _restorer.RestoreInProgress)
		{
			_restorer.CancelFirmwareRestore();
		}
		base.Cancel();
	}
}
