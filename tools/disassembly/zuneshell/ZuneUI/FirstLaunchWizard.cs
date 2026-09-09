using Microsoft.Zune.Util;

namespace ZuneUI;

public class FirstLaunchWizard : Wizard
{
	public override bool CanCommitChanges
	{
		get
		{
			if (IsValid)
			{
				if (base.CanAdvancePageIndex)
				{
					return base.CurrentPage is FirstLaunchWelcomePage;
				}
				return true;
			}
			return false;
		}
	}

	public FirstLaunchWizard()
	{
		AddPage(new FirstLaunchWelcomePage(this));
		AddPage(new FirstLaunchMonitoredFoldersPage(this));
		AddPage(new FirstLaunchDownloadFoldersPage(this));
		AddPage(new FirstLaunchFileTypesPage(this));
		AddPage(new FirstLaunchPrivacyPage(this));
	}

	protected override bool OnCommitChanges()
	{
		if (base.CurrentPage is FirstLaunchWelcomePage)
		{
			ZuneShell.DefaultInstance.Management.MediaInfoChoice.Value = true;
			ZuneShell.DefaultInstance.Management.SqmChoice.Value = true;
			SQMLog.Log((SQMDataId)45, 0);
		}
		else
		{
			SQMLog.Log((SQMDataId)45, 1);
		}
		ZuneShell.DefaultInstance.Management.CommitListSave();
		Fue.Instance.MigrateLegacyConfiguration();
		Fue.Instance.CompleteFUE();
		return base.OnCommitChanges();
	}
}
