using System;
using System.Collections;

namespace ZuneUI;

public class ProfilePageState : IPageState
{
	private string _zuneTag;

	private Category _profilePivot;

	private int _chosenIndexSortFriends;

	private Node _pivotPreference;

	private string _selectedFriendTag;

	private Guid _selectedPlaylistTrack;

	public bool CanBeTrimmed => true;

	public ProfilePageState(ProfilePage page)
	{
		_zuneTag = page.ProfilePanel.ZuneTag;
		_profilePivot = page.ProfilePanel.SelectedPivot;
		_chosenIndexSortFriends = page.ProfilePanel.ChosenIndexSortFriends;
		_pivotPreference = page.PivotPreference;
		_selectedFriendTag = page.ProfilePanel.SelectedFriendTag;
		_selectedPlaylistTrack = page.ProfilePanel.SelectedPlaylistTrack;
	}

	public ProfilePage Restore()
	{
		Hashtable hashtable = new Hashtable();
		hashtable["ZuneTag"] = _zuneTag;
		hashtable["Pivot"] = _profilePivot;
		hashtable["ChosenIndexSortFriends"] = _chosenIndexSortFriends;
		hashtable["FriendTag"] = _selectedFriendTag;
		hashtable["PlaylistTrack"] = _selectedPlaylistTrack;
		ProfilePage profilePage = new ProfilePage(_pivotPreference);
		profilePage.NavigationArguments = hashtable;
		return profilePage;
	}

	public IPage RestoreAndRelease()
	{
		return Restore();
	}

	public void Release()
	{
	}
}
