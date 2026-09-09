using Microsoft.Iris;

namespace ZuneUI;

public class WirelessSyncWizard : Wizard
{
	private string _progressText;

	public string ProgressText
	{
		get
		{
			return _progressText;
		}
		set
		{
			if (_progressText != value)
			{
				_progressText = value;
				((ModelItem)this).FirePropertyChanged("ProgressText");
			}
		}
	}

	public WirelessSyncWizard()
	{
		AddPage(new WirelessSyncUseExistingPage(this));
		AddPage(new WirelessSyncChooseNetworkPage(this));
		AddPage(new WirelessSyncSummaryPage(this));
		AddPage(new WirelessSyncErrorPage(this));
	}
}
