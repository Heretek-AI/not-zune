using Microsoft.Iris;

namespace ZuneUI;

public class DeviceRollbackWizard : Wizard
{
	private UIFirmwareUpdater _updater;

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
			}
		}
	}

	public DeviceRollbackWizard()
	{
		AddPage(new DeviceRollbackProgressPage(this));
		AddPage(new DeviceRollbackSummaryPage(this));
		AddPage(new DeviceRollbackErrorPage(this));
	}
}
