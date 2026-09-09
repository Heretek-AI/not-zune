using Microsoft.Zune.Util;
using UIXControls;

namespace ZuneUI;

public class FeatureMenuItemCommand : MenuItemCommand
{
	private Features _feature;

	private int _hidden = -1;

	public Features Features
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _feature;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			if (_feature != value)
			{
				_hidden = -1;
			}
			_feature = value;
		}
	}

	public override bool ShouldHide()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (_hidden == -1)
		{
			_hidden = ((!FeatureEnablement.IsFeatureEnabled(_feature)) ? 1 : 0);
		}
		if (_hidden != 1)
		{
			return ((MenuItemCommand)this).ShouldHide();
		}
		return true;
	}
}
