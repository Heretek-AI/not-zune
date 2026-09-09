namespace ZuneUI;

public class ApplicationPageState : DevicePivotManagingPageState
{
	public ApplicationPageState(ApplicationLibraryPage page)
		: base(page)
	{
	}

	public override IPage RestoreAndRelease()
	{
		IPage result = null;
		if (!base.Page.ShowDeviceContents || SyncControls.Instance.CurrentDevice.SupportsSyncApplications)
		{
			result = base.RestoreAndRelease();
		}
		return result;
	}
}
