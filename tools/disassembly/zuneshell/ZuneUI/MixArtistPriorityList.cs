using System.Collections;

namespace ZuneUI;

public class MixArtistPriorityList : MixPriorityList
{
	public MixArtistPriorityList(MixResult mixResultSeed)
		: base(mixResultSeed)
	{
	}

	public override void AddList(IList sourceList, string reason, int maxItems)
	{
		AddList(sourceList, reason, maxItems, MixResultArtist.GetItemPriority, MixResultArtist.CreateInstance);
	}
}
