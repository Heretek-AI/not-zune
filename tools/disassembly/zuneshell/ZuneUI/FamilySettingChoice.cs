using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class FamilySettingChoice : Choice
{
	private string _title;

	private string _settingId;

	private bool _showBlockUnrated;

	private BooleanChoice _blockUnrated;

	private IList _choices;

	public string Title => _title;

	public string SettingId => _settingId;

	public bool ShowBlockUnrated => _showBlockUnrated;

	public BooleanChoice BlockUnrated => _blockUnrated;

	public IList Choices => _choices;

	public FamilySettingValue SettingValue
	{
		get
		{
			return (FamilySettingValue)((Choice)this).ChosenValue;
		}
		set
		{
			if (((Choice)this).Options.Contains(value))
			{
				((Choice)this).ChosenValue = value;
			}
		}
	}

	public FamilySettingChoice(IModelItemOwner owner, string title, string description, string blockText, string settingId, bool showBlockUnrated, IList choices)
		: base(owner, description, choices)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		_title = title;
		_settingId = settingId;
		_showBlockUnrated = showBlockUnrated;
		_blockUnrated = new BooleanChoice(owner, blockText);
		_choices = choices;
		((Choice)this).Clear();
	}

	public FamilySettingValue GetSettingValueById(int value)
	{
		foreach (FamilySettingValue choice in Choices)
		{
			if (choice.Value == value)
			{
				return choice;
			}
		}
		return null;
	}
}
