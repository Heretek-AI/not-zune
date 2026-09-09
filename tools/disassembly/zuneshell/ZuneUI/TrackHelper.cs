using Microsoft.Iris;
using ZuneXml;

namespace ZuneUI;

public static class TrackHelper
{
	public static void SetOrdinal(DataProviderObject item, int ordinal)
	{
		if (item is Track)
		{
			((Track)(object)item).Ordinal = ordinal;
		}
	}
}
