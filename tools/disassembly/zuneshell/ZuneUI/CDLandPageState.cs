using Microsoft.Iris;

namespace ZuneUI;

public class CDLandPageState : IPageState
{
	private CDLand _page;

	public bool CanBeTrimmed => true;

	public CDLandPageState(IPage page)
	{
		_page = (CDLand)page;
	}

	public IPage RestoreAndRelease()
	{
		if (_page.Album != Shell.MainFrame.Disc.BurnList && _page.Album != Shell.MainFrame.Disc.NoCD && !_page.Album.IsMediaLoaded)
		{
			((ModelItem)_page).Dispose();
			return null;
		}
		if (_page.Album == Shell.MainFrame.Disc.NoCD && Shell.MainFrame.Disc.HasCD)
		{
			((ModelItem)_page).Dispose();
			return null;
		}
		return _page;
	}

	public void Release()
	{
		_page.Release();
	}
}
