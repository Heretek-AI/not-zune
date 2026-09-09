using Microsoft.Iris;

namespace ZuneUI;

public abstract class SchemaSyncGroup : SyncGroup
{
	private SyncCategory _type;

	private bool _expanded;

	public override string Title => Type switch
	{
		SyncCategory.Music => Shell.LoadString(StringId.IDS_ALL_MUSIC_SYNC_GROUP), 
		SyncCategory.Video => Shell.LoadString(StringId.IDS_ALL_VIDEO_SYNC_GROUP), 
		SyncCategory.Photo => Shell.LoadString(StringId.IDS_ALL_PHOTOS_SYNC_GROUP), 
		SyncCategory.Podcast => Shell.LoadString(StringId.IDS_ALL_PODCASTS_SYNC_GROUP), 
		SyncCategory.Friend => Shell.LoadString(StringId.IDS_ALL_FRIENDS_SYNC_GROUP), 
		SyncCategory.Channel => Shell.LoadString(StringId.IDS_ALL_CHANNELS_SYNC_GROUP), 
		SyncCategory.Application => Shell.LoadString(StringId.IDS_ALL_APPLICATIONS_SYNC_GROUP), 
		SyncCategory.Audiobook => Shell.LoadString(StringId.IDS_ALL_AUDIOBOOKS_SYNC_GROUP), 
		SyncCategory.Guest => Shell.LoadString(StringId.IDS_ALL_GUEST_SYNC_GROUP), 
		_ => Shell.LoadString(StringId.IDS_GENERIC_ERROR), 
	};

	public override int ID => -1;

	public override SyncCategory Type => _type;

	public string TypeName => Type switch
	{
		SyncCategory.Music => Shell.LoadString(StringId.IDS_MUSIC), 
		SyncCategory.Video => Shell.LoadString(StringId.IDS_VIDEOS), 
		SyncCategory.Photo => Shell.LoadString(StringId.IDS_PICTURES), 
		SyncCategory.Podcast => Shell.LoadString(StringId.IDS_PODCASTS), 
		SyncCategory.Friend => Shell.LoadString(StringId.IDS_FRIENDS), 
		SyncCategory.Channel => Shell.LoadString(StringId.IDS_CHANNELS), 
		SyncCategory.Application => Shell.LoadString(StringId.IDS_APPLICATIONS), 
		_ => Shell.LoadString(StringId.IDS_GENERIC_ERROR), 
	};

	public override bool IsComplex => false;

	public bool IsExpanded
	{
		get
		{
			return _expanded;
		}
		set
		{
			if (_expanded != value)
			{
				_expanded = value;
				((ModelItem)this).FirePropertyChanged("IsExpanded");
			}
		}
	}

	public SchemaSyncGroup(SyncGroupList list, SyncCategory type)
		: base(list)
	{
		_type = type;
	}

	public override void DataEdited()
	{
	}
}
