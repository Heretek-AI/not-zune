using System.Collections;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class WizardExperience : Experience
{
	private CategoryPageNode _fue;

	private CategoryPageNode _wirelessSetup;

	public override IList NodesList => null;

	public CategoryPageNode FUE
	{
		get
		{
			if (_fue == null)
			{
				_fue = new CategoryPageNode(this, StringId.IDS_SETTINGS_PIVOT, new Category[3]
				{
					SettingCategories.Collection,
					SettingCategories.Filetype,
					SettingCategories.Privacy
				}, (SQMDataId)0, allowBackNavigation: false, hideDeviceOnCancel: false);
			}
			return _fue;
		}
	}

	public CategoryPageNode WirelessSetup
	{
		get
		{
			if (_wirelessSetup == null)
			{
				_wirelessSetup = new CategoryPageNode(this, StringId.IDS_SET_UP_YOUR_ZUNE, new Category[1] { SettingCategories.WirelessSetup }, (SQMDataId)0, allowBackNavigation: true, hideDeviceOnCancel: false);
			}
			return _wirelessSetup;
		}
	}

	public WizardExperience(Frame frameOwner)
		: base(frameOwner)
	{
	}
}
