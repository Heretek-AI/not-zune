using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class ProfilePage : LibraryPage
{
	private ProfilePanel _profilePanel;

	public static readonly string ProfilePageTemplate = "res://ZuneShellResources!Profile.uix#ProfileLibrary";

	public static readonly string ProfileBackgroundTemplate = "res://ZuneShellResources!Profile.uix#ProfileBackground";

	public ProfilePanel ProfilePanel
	{
		get
		{
			return _profilePanel;
		}
		set
		{
			if (_profilePanel != value)
			{
				_profilePanel = value;
				((ModelItem)this).FirePropertyChanged("ProfilePanel");
			}
		}
	}

	public static ProfilePage CreateInstance(IDictionary args)
	{
		string text = ((args != null) ? (args["ZuneTag"] as string) : null);
		Node pivotPreference = ((string.IsNullOrEmpty(text) || SignIn.Instance.IsSignedInUser(text)) ? Shell.MainFrame.Social.Me : Shell.MainFrame.Social.Friends);
		return new ProfilePage(pivotPreference);
	}

	public ProfilePage(Node pivotPreference)
	{
		base.PivotPreference = pivotPreference;
		base.IsRootPage = pivotPreference == Shell.MainFrame.Social.Me;
		base.UI = ProfilePageTemplate;
		base.UIPath = "Social\\Profile";
		base.BackgroundUI = ProfileBackgroundTemplate;
		base.TransportControlStyle = TransportControlStyle.Music;
		base.PlaybackContext = PlaybackContext.Music;
		SignIn.Instance.SignInStatusUpdatedEvent += OnSignedInStatusChange;
	}

	protected override void OnDispose(bool fDisposing)
	{
		if (fDisposing)
		{
			SignIn.Instance.SignInStatusUpdatedEvent -= OnSignedInStatusChange;
		}
		base.OnDispose(fDisposing);
	}

	private void OnSignedInStatusChange(object sender, EventArgs args)
	{
		if (SignIn.Instance.SignedIn && base.PivotPreference == Shell.MainFrame.Social.Me)
		{
			CreateProfilePanel(null, ProfileCategories.RecentlyPlayed, Guid.Empty, 0, null);
		}
	}

	protected override void OnNavigatedToWorker()
	{
		string zuneTag = ((base.NavigationArguments != null) ? (base.NavigationArguments["ZuneTag"] as string) : null);
		string selectedFriendTag = ((base.NavigationArguments != null) ? (base.NavigationArguments["FriendTag"] as string) : null);
		Category profilePivot = ProfileCategories.RecentlyPlayed;
		object obj = ((base.NavigationArguments != null) ? base.NavigationArguments["Pivot"] : null);
		if (obj != null)
		{
			profilePivot = (Category)obj;
		}
		int chosenSortFriends = 0;
		obj = ((base.NavigationArguments != null) ? base.NavigationArguments["ChosenIndexSortFriends"] : null);
		if (obj != null)
		{
			chosenSortFriends = (int)obj;
		}
		Guid playlistTrack = Guid.Empty;
		obj = ((base.NavigationArguments != null) ? base.NavigationArguments["PlaylistTrack"] : null);
		if (obj != null)
		{
			playlistTrack = (Guid)obj;
		}
		CreateProfilePanel(zuneTag, profilePivot, playlistTrack, chosenSortFriends, selectedFriendTag);
		base.OnNavigatedToWorker();
	}

	private void CreateProfilePanel(string zuneTag, Category profilePivot, Guid playlistTrack, int chosenSortFriends, string selectedFriendTag)
	{
		if (base.PivotPreference == Shell.MainFrame.Social.Me)
		{
			zuneTag = null;
			if (base.NavigationArguments != null)
			{
				base.NavigationArguments.Remove("ZuneTag");
			}
		}
		ProfilePanel = new ProfilePanel(this, zuneTag, profilePivot, playlistTrack, chosenSortFriends, selectedFriendTag);
	}

	public override void InvokeSettings()
	{
		((Command)Shell.SettingsFrame.Settings.Account).Invoke();
	}

	public override IPageState SaveAndRelease()
	{
		ProfilePageState result = new ProfilePageState(this);
		_profilePanel.Release();
		((ModelItem)this).Dispose();
		return result;
	}

	public static void NavigateTo(string zuneTag, Category profilePivot, Guid track, string serviceContext)
	{
		bool flag = false;
		if (ZuneShell.DefaultInstance.CurrentPage is ProfilePage)
		{
			ProfilePage profilePage = (ProfilePage)ZuneShell.DefaultInstance.CurrentPage;
			if (SignIn.TagsMatch(profilePage.ProfilePanel.ZuneTag, zuneTag))
			{
				profilePage.ProfilePanel.SelectedPivot = profilePivot;
				profilePage.ProfilePanel.SelectedPlaylistTrack = track;
				profilePage.ProfilePanel.CanChangeSelectedItem = true;
				flag = true;
			}
		}
		if (!flag)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("ZuneTag", zuneTag);
			if (profilePivot != null)
			{
				hashtable.Add("Pivot", profilePivot);
			}
			if (!GuidHelper.IsEmpty(track))
			{
				hashtable.Add("PlaylistTrack", track);
			}
			if (!string.IsNullOrEmpty(serviceContext))
			{
				hashtable.Add("ServiceContext", serviceContext);
			}
			ZuneShell.DefaultInstance.Execute("Social\\Profile", hashtable);
		}
	}
}
