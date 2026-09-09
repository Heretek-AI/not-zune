using Microsoft.Zune.Service;

namespace ZuneUI;

internal static class AccountManagementHelper
{
	internal static HRESULT GetPassportIdentity(string username, string password, out PassportIdentity passportIdentity)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		return Service.Instance.AuthenticatePassport(username, password, (EPassportPolicyId)2, ref passportIdentity);
	}
}
