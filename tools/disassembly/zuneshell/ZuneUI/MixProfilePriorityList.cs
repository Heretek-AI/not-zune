using System.Collections;

namespace ZuneUI;

public class MixProfilePriorityList : MixPriorityList
{
	public MixProfilePriorityList(MixResult mixResultSeed)
		: base(mixResultSeed)
	{
	}

	public override void AddList(IList sourceList, string reason, int maxItems)
	{
		AddList(sourceList, reason, maxItems, MixResultProfile.GetItemPriority, MixResultProfile.CreateInstance);
	}
}
