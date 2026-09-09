using System.Collections;

namespace ZuneUI;

public class DeviceComparerForRental : IComparer
{
	public int Compare(object x, object y)
	{
		int result = 0;
		UIDevice uIDevice = x as UIDevice;
		UIDevice uIDevice2 = y as UIDevice;
		if (uIDevice != null && uIDevice2 != null)
		{
			bool supportsRental = uIDevice.SupportsRental;
			bool supportsRental2 = uIDevice2.SupportsRental;
			result = ((supportsRental && !supportsRental2) ? (-1) : ((supportsRental2 && !supportsRental) ? 1 : uIDevice.Name.CompareTo(uIDevice2.Name)));
		}
		return result;
	}
}
