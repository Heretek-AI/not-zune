using System;
using Microsoft.Iris;
using Microsoft.Zune.QuickMix;

namespace ZuneUI;

public class QuickMixSubTypeCommand : Command
{
	public QuickMixSubTypeCommand(string description, EQuickMixType type, QuickMixSession session)
		: base((IModelItemOwner)null, description, (EventHandler)null)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		((Command)this).Available = session.GetQuickMixTypeAvailable(type);
	}
}
