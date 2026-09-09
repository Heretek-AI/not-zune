using Microsoft.Iris;

namespace ZuneUI;

internal class RecordModeOption : NamedIntOption
{
	private Choice _rateChoice;

	public Choice BitRate => _rateChoice;

	public RecordModeOption(IModelItemOwner owner, string description, int value, Choice rateChoice)
		: base(owner, description, value)
	{
		_rateChoice = rateChoice;
	}
}
