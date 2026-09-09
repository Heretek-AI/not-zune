using Microsoft.Iris;

namespace ZuneUI;

public class CDLand : LibraryPage
{
	private string _title = Shell.LoadString(StringId.IDS_NO_CD);

	private CDAlbumCommand _album;

	private PlaylistContentsPanel _burnListPanel;

	public PlaylistContentsPanel BurnListPanel => _burnListPanel;

	public CDAlbumCommand Album => _album;

	public CDLand(CDAlbumCommand album)
	{
		_album = album;
		base.PivotPreference = album;
		base.IsRootPage = true;
		base.UI = "res://ZuneShellResources!CDView.uix#CDView";
		base.ShowCDIcon = false;
		base.ShowDeviceIcon = false;
		base.ShowPlaylistIcon = false;
		base.ShowNowPlayingBackgroundOnIdle = false;
		base.PlaybackContext = PlaybackContext.Music;
		_burnListPanel = new PlaylistContentsPanel(this);
	}

	public override void InvokeSettings()
	{
		if (Album == Shell.MainFrame.Disc.BurnList)
		{
			Shell.SettingsFrame.Settings.Software.Invoke(SettingCategories.Burn);
		}
		else
		{
			Shell.SettingsFrame.Settings.Software.Invoke(SettingCategories.Rip);
		}
	}

	public override IPageState SaveAndRelease()
	{
		return new CDLandPageState(this);
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing)
		{
			((ModelItem)_burnListPanel).Dispose();
		}
		base.OnDispose(disposing);
	}
}
