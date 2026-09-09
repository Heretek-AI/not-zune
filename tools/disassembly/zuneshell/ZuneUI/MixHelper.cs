using Microsoft.Zune.Util;

namespace ZuneUI;

public class MixHelper
{
	public static bool MixAvailable
	{
		get
		{
			if (InternetConnection.Instance.IsConnected)
			{
				return FeatureEnablement.IsFeatureEnabled((Features)29);
			}
			return false;
		}
	}
}
