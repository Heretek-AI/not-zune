namespace ZuneUI;

public class CategoryPageState : IPageState
{
	private IPage _page;

	public bool CanBeTrimmed => true;

	public CategoryPageState(IPage page)
	{
		_page = page;
	}

	public IPage RestoreAndRelease()
	{
		if (CategoryPage.EntryPage == null)
		{
			Release();
			return null;
		}
		return _page;
	}

	public void Release()
	{
		_page.Release();
	}
}
