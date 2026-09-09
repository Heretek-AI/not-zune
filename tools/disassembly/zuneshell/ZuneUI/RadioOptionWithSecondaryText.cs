using System;
using Microsoft.Iris;

namespace ZuneUI;

public class RadioOptionWithSecondaryText : Command
{
	private string _secondaryText;

	public string SecondaryText => _secondaryText;

	public RadioOptionWithSecondaryText(IModelItemOwner owner, string text, string secondaryText)
		: base(owner, text, (EventHandler)null)
	{
		_secondaryText = secondaryText;
	}
}
