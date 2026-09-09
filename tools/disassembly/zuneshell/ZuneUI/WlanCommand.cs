using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class WlanCommand : Command
{
	private WlanProfile _profile;

	public WlanProfile Profile => _profile;

	public bool NeedsKey
	{
		get
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			if (Profile != null && (int)Profile.Cipher != 0)
			{
				if ((!Profile.Encrypted && string.IsNullOrEmpty(Profile.Key)) || (Profile.Encrypted && (Profile.EncryptedKey == null || Profile.EncryptedKey.Length == 0)))
				{
					return true;
				}
				return false;
			}
			return false;
		}
	}

	public WlanCommand(WlanProfile profile)
	{
		_profile = profile;
		((ModelItem)this).Description = profile.SSID;
	}

	public override string ToString()
	{
		return ((ModelItem)this).Description;
	}
}
