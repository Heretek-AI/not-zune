using Microsoft.Zune.Util;

namespace ZuneUI;

public class DownloadsPageState : IPageState
{
	private DownloadsPage _page;

	public bool CanBeTrimmed => true;

	public DownloadsPageState(IPage page)
	{
		_page = (DownloadsPage)page;
	}

	public IPage RestoreAndRelease()
	{
		if (DownloadManager.Instance.Percentage >= 100f)
		{
			Release();
		}
		return _page;
	}

	public void Release()
	{
		_page.Release();
		_page = null;
	}
}
