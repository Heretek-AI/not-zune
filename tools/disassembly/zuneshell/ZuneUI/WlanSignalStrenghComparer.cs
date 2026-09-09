using System.Collections.Generic;
using MicrosoftZuneLibrary;

namespace ZuneUI;

internal class WlanSignalStrenghComparer : IComparer<WlanProfile>
{
	public int Compare(WlanProfile x, WlanProfile y)
	{
		if (x != null && y != null)
		{
			return (int)(y.SignalQuality - x.SignalQuality);
		}
		return 0;
	}
}
