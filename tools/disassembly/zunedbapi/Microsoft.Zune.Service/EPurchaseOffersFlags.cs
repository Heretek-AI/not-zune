using System;

namespace Microsoft.Zune.Service;

[Flags]
public enum EPurchaseOffersFlags
{
	PurchaseTrials = 8,
	StreamVideos = 4,
	RentVideos = 2,
	PurchaseHD = 1,
	None = 0
}
