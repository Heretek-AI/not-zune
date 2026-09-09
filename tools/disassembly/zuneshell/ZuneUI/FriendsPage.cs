using Microsoft.Iris;

namespace ZuneUI;

public class FriendsPage : LibraryPage
{
	public static readonly string FriendsPageTemplate = "res://ZuneShellResources!Friends.uix#FriendLibrary";

	private FriendsPanel _friendsPanel;

	private string _selectedZuneTag;

	public FriendsPanel FriendsPanel => _friendsPanel;

	public string SelectedZuneTag
	{
		get
		{
			return _selectedZuneTag;
		}
		set
		{
			if (_selectedZuneTag != value)
			{
				_selectedZuneTag = value;
				((ModelItem)this).FirePropertyChanged("SelectedZuneTag");
			}
		}
	}

	public string ZuneTag
	{
		get
		{
			string result = null;
			if (!base.ShowDeviceContents)
			{
				result = SignIn.Instance.ZuneTag;
			}
			else if (SyncControls.Instance.CurrentDevice.IsValid)
			{
				result = SyncControls.Instance.CurrentDevice.ZuneTag;
			}
			return result;
		}
	}

	public static FriendsPage CreateInstance()
	{
		return new FriendsPage(showDevice: false);
	}

	public static FriendsPage CreateInstance(bool showDevice)
	{
		return new FriendsPage(showDevice);
	}

	public FriendsPage(bool showDevice)
		: base(showDevice, MediaType.UserCard)
	{
		if (showDevice)
		{
			Deviceland.InitDevicePage(this);
			base.PivotPreference = Shell.MainFrame.Device.Friends;
			base.ShowComputerIcon = ComputerIconState.Show;
		}
		else
		{
			base.PivotPreference = Shell.MainFrame.Social.Friends;
		}
		base.TransportControlStyle = TransportControlStyle.Music;
		base.PlaybackContext = PlaybackContext.Music;
		base.IsRootPage = true;
		base.UI = FriendsPageTemplate;
		base.UIPath = "Social\\Friends";
		_friendsPanel = new FriendsPanel(this);
	}

	public override void InvokeSettings()
	{
		if (Shell.MainFrame.Device.IsCurrent)
		{
			((Command)Shell.SettingsFrame.Settings.Device).Invoke();
		}
		else
		{
			((Command)Shell.SettingsFrame.Settings.Account).Invoke();
		}
	}

	public override IPageState SaveAndRelease()
	{
		_friendsPanel.Release();
		return base.SaveAndRelease();
	}
}
