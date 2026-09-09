using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class ProfilePanel : LibraryPanel
{
	private string _zuneTag;

	private UserRelationship _relationshipToSignedInUser;

	private DataProviderObject _profileData;

	private PropertyEditProfile _propertyEditProfile;

	private IList _friends;

	private IList _pivotData;

	private Category _selectedPivot;

	private Guid _selectedPlaylistTrack;

	private int _chosenIndexSortFriends;

	private string _selectedFriendTag;

	private bool _canChangeSelectedItem;

	private bool _loaded;

	private bool _forceLoadStatus;

	public string ZuneTag
	{
		get
		{
			if (string.IsNullOrEmpty(_zuneTag))
			{
				return SignIn.Instance.ZuneTag;
			}
			return _zuneTag;
		}
	}

	public Category SelectedPivot
	{
		get
		{
			return _selectedPivot;
		}
		set
		{
			if (_selectedPivot != value)
			{
				_selectedPivot = value;
				((ModelItem)this).FirePropertyChanged("SelectedPivot");
				SelectedPlaylistTrack = Guid.Empty;
			}
		}
	}

	public Guid SelectedPlaylistTrack
	{
		get
		{
			return _selectedPlaylistTrack;
		}
		set
		{
			if (_selectedPlaylistTrack != value)
			{
				_selectedPlaylistTrack = value;
				((ModelItem)this).FirePropertyChanged("SelectedPlaylistTrack");
			}
		}
	}

	public bool CanChangeSelectedItem
	{
		get
		{
			return _canChangeSelectedItem;
		}
		set
		{
			if (_canChangeSelectedItem != value)
			{
				_canChangeSelectedItem = value;
				((ModelItem)this).FirePropertyChanged("CanChangeSelectedItem");
			}
		}
	}

	public string SelectedFriendTag
	{
		get
		{
			return _selectedFriendTag;
		}
		set
		{
			if (_selectedFriendTag != value)
			{
				_selectedFriendTag = value;
				((ModelItem)this).FirePropertyChanged("SelectedFriendTag");
			}
		}
	}

	public int ChosenIndexSortFriends
	{
		get
		{
			return _chosenIndexSortFriends;
		}
		set
		{
			if (_chosenIndexSortFriends != value)
			{
				_chosenIndexSortFriends = value;
				((ModelItem)this).FirePropertyChanged("ChosenIndexSortFriends");
			}
		}
	}

	public bool IsSignedInUser => RelationshipToSignedInUser == UserRelationship.Self;

	public UserRelationship RelationshipToSignedInUser
	{
		get
		{
			if (_relationshipToSignedInUser == UserRelationship.Unknown && SignIn.Instance.IsSignedInUser(ZuneTag))
			{
				RelationshipToSignedInUser = UserRelationship.Self;
			}
			return _relationshipToSignedInUser;
		}
		set
		{
			if (_relationshipToSignedInUser != value)
			{
				_relationshipToSignedInUser = value;
				((ModelItem)this).FirePropertyChanged("RelationshipToSignedInUser");
				((ModelItem)this).FirePropertyChanged("IsSignedInUser");
			}
		}
	}

	public DataProviderObject ProfileData
	{
		get
		{
			return _profileData;
		}
		set
		{
			if (_profileData != value)
			{
				_profileData = value;
				_propertyEditProfile = null;
				((ModelItem)this).FirePropertyChanged("ProfileData");
				((ModelItem)this).FirePropertyChanged("PropertyEditProfile");
			}
		}
	}

	public PropertyEditProfile PropertyEditProfile
	{
		get
		{
			if (_propertyEditProfile == null)
			{
				_propertyEditProfile = new PropertyEditProfile(_profileData);
			}
			return _propertyEditProfile;
		}
	}

	public IList Friends
	{
		get
		{
			return _friends;
		}
		set
		{
			if (_friends != value)
			{
				_friends = value;
				((ModelItem)this).FirePropertyChanged("Friends");
			}
		}
	}

	public IList PivotData
	{
		get
		{
			return _pivotData;
		}
		set
		{
			if (_pivotData != value)
			{
				_pivotData = value;
				((ModelItem)this).FirePropertyChanged("PivotData");
			}
		}
	}

	public bool Loaded
	{
		get
		{
			return _loaded;
		}
		set
		{
			if (_loaded != value)
			{
				_loaded = value;
				((ModelItem)this).FirePropertyChanged("Loaded");
			}
		}
	}

	public bool ForceLoadStatus
	{
		get
		{
			return _forceLoadStatus;
		}
		set
		{
			if (_forceLoadStatus != value)
			{
				_forceLoadStatus = value;
				((ModelItem)this).FirePropertyChanged("ForceLoadStatus");
			}
		}
	}

	internal ProfilePanel(ProfilePage page, string zuneTag, Category profilePivot, Guid playlistTrack, int chosenSortFriends, string selectedFriendTag)
		: base((IModelItemOwner)(object)page)
	{
		_zuneTag = zuneTag;
		_selectedPivot = profilePivot;
		_relationshipToSignedInUser = UserRelationship.Unknown;
		_chosenIndexSortFriends = chosenSortFriends;
		_selectedFriendTag = selectedFriendTag;
		_selectedPlaylistTrack = playlistTrack;
		_canChangeSelectedItem = true;
	}

	public int GetFriendIndexFromZuneTag(string tagToFind)
	{
		int result = -1;
		int num = 0;
		if (_friends != null && !string.IsNullOrEmpty(tagToFind))
		{
			foreach (object friend in _friends)
			{
				if (friend is ProfileCardData profileCardData && SignIn.TagsMatch(profileCardData.ZuneTag, tagToFind))
				{
					result = num;
					break;
				}
				num++;
			}
		}
		return result;
	}
}
