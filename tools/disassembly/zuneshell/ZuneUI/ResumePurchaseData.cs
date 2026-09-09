using System;

namespace ZuneUI;

public class ResumePurchaseData : IEquatable<ResumePurchaseData>
{
	public string PurchaseHandle { get; private set; }

	public string UserRedirectUrl { get; private set; }

	public ResumePurchaseData(string purchaseHandle, string redirectUrl)
	{
		PurchaseHandle = purchaseHandle;
		UserRedirectUrl = redirectUrl;
	}

	public bool Equals(ResumePurchaseData other)
	{
		if (string.CompareOrdinal(PurchaseHandle, other.PurchaseHandle) == 0)
		{
			return string.CompareOrdinal(UserRedirectUrl, other.UserRedirectUrl) == 0;
		}
		return false;
	}
}
