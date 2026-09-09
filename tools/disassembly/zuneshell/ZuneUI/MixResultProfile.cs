using System;
using Microsoft.Iris;

namespace ZuneUI;

public class MixResultProfile : MixResult
{
	protected MixResultProfile()
	{
	}

	public static MixResultProfile CreateInstance(DataProviderObject dataProviderObject, string reason)
	{
		MixResultProfile mixResultProfile = new MixResultProfile();
		Guid? guid = (Guid?)dataProviderObject.GetProperty("UserGuid");
		mixResultProfile.Initialize(MixResultType.Profile, reason, (string)(dataProviderObject.GetProperty("ZuneTag") ?? ""), "", guid.HasValue ? guid.Value.ToString() : "", (string)(dataProviderObject.GetProperty("TileUrl") ?? ""), Guid.Empty, dataProviderObject);
		return mixResultProfile;
	}

	internal static int GetItemPriority(DataProviderObject item, int startPriority)
	{
		return startPriority;
	}
}
