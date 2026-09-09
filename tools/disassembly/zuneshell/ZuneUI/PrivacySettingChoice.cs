using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class PrivacySettingChoice : Choice
{
	private string m_detailedDescription;

	private PrivacySettingId m_settingId;

	private PrivacyInfoSettings m_infoSettings;

	private static IList s_allowDenyChoices;

	private static IList s_allowFriendsDenyChoices;

	public PrivacySettingId SettingId => m_settingId;

	public PrivacyInfoSettings InfoSettings => m_infoSettings;

	public string DetailedDescription => m_detailedDescription;

	public PrivacySettingValue SettingValue
	{
		get
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			if (((Choice)this).ChosenValue is PrivacySettingValue)
			{
				return (PrivacySettingValue)((Choice)this).ChosenValue;
			}
			return (PrivacySettingValue)(-1);
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Invalid comparison between Unknown and I4
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			if (((Choice)this).Options.Contains(value))
			{
				((Choice)this).ChosenValue = value;
			}
			else if ((int)value == -1)
			{
				((Choice)this).Clear();
			}
		}
	}

	public string LinkDescription { get; private set; }

	public string LinkUrl { get; private set; }

	internal static IList AllowDenyChoices
	{
		get
		{
			if (s_allowDenyChoices == null)
			{
				s_allowDenyChoices = new ArrayList(2);
				s_allowDenyChoices.Add((object)(PrivacySettingValue)2);
				s_allowDenyChoices.Add((object)(PrivacySettingValue)0);
			}
			return s_allowDenyChoices;
		}
	}

	internal static IList AllowFriendDenyChoices
	{
		get
		{
			if (s_allowFriendsDenyChoices == null)
			{
				s_allowFriendsDenyChoices = new ArrayList(3);
				s_allowFriendsDenyChoices.Add((object)(PrivacySettingValue)2);
				s_allowFriendsDenyChoices.Add((object)(PrivacySettingValue)1);
				s_allowFriendsDenyChoices.Add((object)(PrivacySettingValue)0);
			}
			return s_allowFriendsDenyChoices;
		}
	}

	public PrivacySettingChoice(IModelItemOwner owner, string detailedDescription, string description, IList choices, PrivacySettingId settingId, PrivacyInfoSettings infoSettings, string linkDescription, string linkUrl)
		: this(owner, detailedDescription, description, choices, settingId, infoSettings)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		LinkDescription = linkDescription;
		LinkUrl = linkUrl;
	}

	public PrivacySettingChoice(IModelItemOwner owner, string detailedDescription, string description, IList choices, PrivacySettingId settingId, PrivacyInfoSettings infoSettings)
		: base(owner, description, choices)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		m_detailedDescription = detailedDescription;
		m_settingId = settingId;
		m_infoSettings = infoSettings;
	}
}
