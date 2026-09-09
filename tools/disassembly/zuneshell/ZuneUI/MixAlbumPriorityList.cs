using System.Collections;

namespace ZuneUI;

public class MixAlbumPriorityList : MixPriorityList
{
	public MixAlbumPriorityList(MixResult mixResultSeed)
		: base(mixResultSeed)
	{
	}

	public override void AddList(IList sourceList, string reason, int maxItems)
	{
		AddList(sourceList, reason, maxItems, MixResultAlbum.GetItemPriority, MixResultAlbum.CreateInstance);
	}
}
