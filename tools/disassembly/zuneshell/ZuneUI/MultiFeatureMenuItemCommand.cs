using System.Collections;
using Microsoft.Zune.Util;
using UIXControls;

namespace ZuneUI;

public class MultiFeatureMenuItemCommand : MenuItemCommand
{
	private IList _features;

	private int _hidden = -1;

	public IList Features
	{
		get
		{
			return _features;
		}
		set
		{
			if (_features != value)
			{
				_hidden = -1;
			}
			_features = value;
		}
	}

	public override bool ShouldHide()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (_hidden == -1)
		{
			_hidden = 0;
			foreach (object feature in _features)
			{
				if (feature is Features && !FeatureEnablement.IsFeatureEnabled((Features)feature))
				{
					_hidden = 1;
					break;
				}
			}
		}
		if (_hidden != 1)
		{
			return ((MenuItemCommand)this).ShouldHide();
		}
		return true;
	}
}
