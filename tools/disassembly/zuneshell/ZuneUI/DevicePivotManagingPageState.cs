namespace ZuneUI;

public class DevicePivotManagingPageState : IPageState
{
	private IDeviceContentsPage _page;

	public bool CanBeTrimmed => false;

	protected IDeviceContentsPage Page => _page;

	public DevicePivotManagingPageState(IDeviceContentsPage page)
	{
		_page = page;
	}

	public virtual IPage RestoreAndRelease()
	{
		if (_page.ShowDeviceContents)
		{
			Shell.MainFrame.ShowDevice(show: true);
		}
		return _page;
	}

	public void Release()
	{
		_page.Release();
	}
}
