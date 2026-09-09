using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Playlist;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class ExistingSyncGroup : SyncGroup
{
	private SyncRuleDetails _details;

	private SyncRulesView _view;

	private int _index;

	private bool _isExpanded;

	private string _title;

	private AutoPlaylistBuilder _originalComplexRuleSetup;

	private string _originalComplexRuleName;

	private SyncCategory _complexType;

	private bool _isEdited;

	public override string Title => _title;

	public override int ID => _details.mediaId;

	public override SyncCategory Type
	{
		get
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected I4, but got Unknown
			if (IsComplex)
			{
				return _complexType;
			}
			return (SyncCategory)_details.syncCategory;
		}
	}

	public override long Size
	{
		get
		{
			if (!_details.calculated || _isEdited)
			{
				return -1L;
			}
			return _details.totalSize;
		}
	}

	public override int Count
	{
		get
		{
			if (!_details.calculated || _isEdited)
			{
				return -1;
			}
			return (int)_details.totalItems;
		}
	}

	public override SyncGroupState State
	{
		get
		{
			if (!_isEdited)
			{
				if (!_details.calculated)
				{
					return SyncGroupState.Uncalculated;
				}
				return SyncGroupState.Calculated;
			}
			return SyncGroupState.Pending;
		}
	}

	public override bool IsActive
	{
		get
		{
			return _details.included;
		}
		set
		{
			_view.UpdateItem(_index, value);
			DataUpdated();
		}
	}

	public override bool IsVisible => !_details.ignore;

	public override bool IsComplex => _details.complex;

	public ExistingSyncGroup(SyncGroupList parentList, SyncRulesView view, int index)
		: this(parentList, view, index, isExpandedEntry: false)
	{
	}

	public ExistingSyncGroup(SyncGroupList parentList, SyncRulesView view, int index, bool isExpandedEntry)
		: base(parentList)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		_details = view.GetItem(index);
		_view = view;
		_index = index;
		_isExpanded = isExpandedEntry;
		UpdateTitle();
		if (IsComplex)
		{
			UpdateComplexType();
			_originalComplexRuleName = Title;
			_originalComplexRuleSetup = new AutoPlaylistBuilder(ID);
		}
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing && _originalComplexRuleSetup != null)
		{
			_originalComplexRuleSetup.Dispose();
		}
		((ModelItem)this).OnDispose(disposing);
	}

	public override void CommitChanges()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected I4, but got Unknown
		if (!IsVisible || IsActive)
		{
			return;
		}
		MediaIdAndType[] items = new MediaIdAndType[1]
		{
			new MediaIdAndType(ID, (MediaType)_details.mediaType)
		};
		if (_isExpanded)
		{
			base.ParentList.Device.DeleteAndExclude(items);
			return;
		}
		base.ParentList.Device.RemoveSyncRule(items);
		if (IsComplex)
		{
			List<MediaIdAndType> list = new List<MediaIdAndType>(1);
			list.Add(new MediaIdAndType(ID, MediaType.Playlist));
			Shell.DeleteMedia(list, deleteFileOnDisk: false);
		}
	}

	public override void CancelChanges()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (IsComplex && _isEdited)
		{
			if (Title != _originalComplexRuleName)
			{
				PlaylistManager.Instance.RenamePlaylist(ID, _originalComplexRuleName);
			}
			_originalComplexRuleSetup.SetRules(ID);
		}
	}

	public override void DataUpdated()
	{
		_details = _view.GetItem(_index);
		UpdateTitle();
		((ModelItem)this).FirePropertyChanged("Size");
		((ModelItem)this).FirePropertyChanged("Count");
		((ModelItem)this).FirePropertyChanged("State");
		((ModelItem)this).FirePropertyChanged("IsActive");
		((ModelItem)this).FirePropertyChanged("IsVisible");
		if (base.ParentList.GetGroupForSchema(Type) is DetailsBackedSchemaSyncGroup detailsBackedSchemaSyncGroup)
		{
			detailsBackedSchemaSyncGroup.UpdateActiveState();
		}
	}

	public override void DataEdited()
	{
		_isEdited = true;
		UpdateComplexType();
		DataUpdated();
	}

	private void UpdateTitle()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected I4, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Invalid comparison between Unknown and I4
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected I4, but got Unknown
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		if (IsComplex)
		{
			_title = PlaylistManager.GetPlaylistName(ID);
		}
		else
		{
			EListType val = (EListType)11;
			SchemaMap val2 = (SchemaMap)344;
			string format = "{0}";
			switch ((MediaType)(int)_details.mediaType)
			{
			case MediaType.Track:
				val = (EListType)2;
				format = Shell.LoadString(StringId.IDS_TRACK_RULE_BASE);
				break;
			case MediaType.Album:
				val = (EListType)1;
				val2 = (SchemaMap)382;
				format = Shell.LoadString(StringId.IDS_ALBUM_RULE_BASE);
				break;
			case MediaType.Artist:
				val = (EListType)0;
				val2 = (SchemaMap)380;
				format = Shell.LoadString(StringId.IDS_ARTIST_RULE_BASE);
				break;
			case MediaType.Genre:
				val = (EListType)15;
				val2 = (SchemaMap)398;
				format = Shell.LoadString(StringId.IDS_GENRE_RULE_BASE);
				break;
			case MediaType.Playlist:
				val = (EListType)12;
				format = Shell.LoadString(StringId.IDS_PLAYLIST_RULE_BASE);
				break;
			case MediaType.Video:
				val = (EListType)4;
				format = Shell.LoadString(StringId.IDS_VIDEO_RULE_BASE);
				break;
			case MediaType.Photo:
				val = (EListType)3;
				format = Shell.LoadString(StringId.IDS_PHOTO_RULE_BASE);
				break;
			case MediaType.MediaFolder:
				val = (EListType)10;
				val2 = (SchemaMap)317;
				format = Shell.LoadString(StringId.IDS_FOLDER_RULE_BASE);
				break;
			case MediaType.PodcastEpisode:
				val = (EListType)7;
				format = Shell.LoadString(StringId.IDS_PODCAST_EPISODE_RULE_BASE);
				break;
			case MediaType.Podcast:
				val = (EListType)6;
				format = Shell.LoadString(StringId.IDS_PODCAST_SERIES_RULE_BASE);
				break;
			case MediaType.UserCard:
				val = (EListType)18;
				format = Shell.LoadString(StringId.IDS_FRIEND_RULE_BASE);
				break;
			case MediaType.Application:
				val = (EListType)20;
				format = Shell.LoadString(StringId.IDS_APPLICATION_RULE_BASE);
				break;
			case MediaType.PlaylistChannel:
				val = (EListType)12;
				format = "{0}";
				break;
			default:
				_title = Shell.LoadString(StringId.IDS_GENERIC_ERROR);
				break;
			}
			if ((int)val != 11)
			{
				_title = string.Format(format, PlaylistManager.GetFieldValue(ID, val, (int)val2, Shell.LoadString(StringId.IDS_GENERIC_ERROR)));
			}
		}
		((ModelItem)this).FirePropertyChanged("Title");
	}

	private void UpdateComplexType()
	{
		_complexType = UIDeviceList.MapMediaTypeToSyncCategory(PlaylistManager.GetAutoPlaylistSchema(ID));
		((ModelItem)this).FirePropertyChanged("Type");
	}
}
