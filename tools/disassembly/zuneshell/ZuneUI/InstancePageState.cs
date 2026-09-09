namespace ZuneUI;

public class InstancePageState : IPageState
{
	private IPage _page;

	public bool CanBeTrimmed => true;

	public InstancePageState(IPage page)
	{
		_page = page;
	}

	public IPage RestoreAndRelease()
	{
		return _page;
	}

	public void Release()
	{
		_page.Release();
	}
}
