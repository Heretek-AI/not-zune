using System;
using Microsoft.Iris;

namespace ZuneUI;

public class RichLayoutCommand : Command
{
	private bool _hasRichLayout;

	public bool HasRichLayout => _hasRichLayout;

	public RichLayoutCommand(IModelItemOwner owner, string description, bool hasRichLayout)
		: base(owner, description, (EventHandler)null)
	{
		_hasRichLayout = hasRichLayout;
	}
}
