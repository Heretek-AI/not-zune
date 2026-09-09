using System.Collections;

namespace ZuneUI;

public class NavigationCommandHandler : NavigationCommandHandlerBase
{
	private string _pageUI;

	private string _backgroundUI;

	private Node _pivotPreference;

	private bool _isRootPage;

	private PlaybackContext _playbackContext;

	public string UI
	{
		get
		{
			return _pageUI;
		}
		set
		{
			_pageUI = value;
		}
	}

	public string BackgroundUI
	{
		get
		{
			return _backgroundUI;
		}
		set
		{
			_backgroundUI = value;
		}
	}

	public Node PivotPreference
	{
		get
		{
			return _pivotPreference;
		}
		set
		{
			_pivotPreference = value;
		}
	}

	public bool IsRootPage
	{
		get
		{
			return _isRootPage;
		}
		set
		{
			_isRootPage = value;
		}
	}

	public PlaybackContext PlaybackContext
	{
		get
		{
			return _playbackContext;
		}
		set
		{
			_playbackContext = value;
		}
	}

	protected override ZunePage GetPage(IDictionary args)
	{
		ZunePage zunePage = new ZunePage();
		zunePage.UI = UI;
		zunePage.UIPath = base.UIPath;
		zunePage.BackgroundUI = BackgroundUI;
		zunePage.PivotPreference = PivotPreference;
		zunePage.IsRootPage = IsRootPage;
		zunePage.PlaybackContext = PlaybackContext;
		return zunePage;
	}
}
