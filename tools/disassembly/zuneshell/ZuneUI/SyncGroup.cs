using System;
using Microsoft.Iris;

namespace ZuneUI;

public abstract class SyncGroup : ModelItem, IComparable, IComparable<SyncGroup>
{
	private SyncGroupList _parentList;

	public abstract string Title { get; }

	public abstract int ID { get; }

	public abstract SyncCategory Type { get; }

	public abstract long Size { get; }

	public abstract int Count { get; }

	public abstract SyncGroupState State { get; }

	public abstract bool IsActive { get; set; }

	public abstract bool IsVisible { get; }

	public abstract bool IsComplex { get; }

	public string TypeAsGroupDescription => Type switch
	{
		SyncCategory.Music => Shell.LoadString(StringId.IDS_SONGS), 
		SyncCategory.Video => Shell.LoadString(StringId.IDS_VIDEOS), 
		SyncCategory.Photo => Shell.LoadString(StringId.IDS_PICTURES), 
		SyncCategory.Podcast => Shell.LoadString(StringId.IDS_PODCAST_EPISODES), 
		SyncCategory.Friend => Shell.LoadString(StringId.IDS_FRIENDS), 
		SyncCategory.Channel => Shell.LoadString(StringId.IDS_CHANNELS), 
		SyncCategory.Application => Shell.LoadString(StringId.IDS_APPLICATIONS), 
		_ => Shell.LoadString(StringId.IDS_GENERIC_ERROR), 
	};

	protected SyncGroupList ParentList => _parentList;

	public SyncGroup(SyncGroupList parentList)
		: base((IModelItemOwner)(object)parentList)
	{
		_parentList = parentList;
	}

	public abstract void CommitChanges();

	public abstract void CancelChanges();

	public abstract void DataUpdated();

	public abstract void DataEdited();

	public int CompareTo(object obj)
	{
		if (!(obj is SyncGroup other))
		{
			return 0;
		}
		return CompareTo(other);
	}

	public int CompareTo(SyncGroup other)
	{
		return Size.CompareTo(other.Size) * -1;
	}
}
