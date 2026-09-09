using System;
using System.Collections;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneUI;

public class WebUrlCommandHandler : ICommandHandler
{
	public void Execute(string command, IDictionary commandArgs)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		EPassportPolicyId val = (EPassportPolicyId)0;
		if (commandArgs != null && commandArgs.Contains("PassportPolicyId"))
		{
			val = (EPassportPolicyId)commandArgs["PassportPolicyId"];
		}
		else if (SignIn.Instance.SignedIn)
		{
			Uri uri = new Uri(command, UriKind.Absolute);
			if (uri.Host.EndsWith("zune.net", StringComparison.OrdinalIgnoreCase))
			{
				if (uri.Scheme == "http")
				{
					val = (EPassportPolicyId)3;
				}
				else if (uri.Scheme == "https")
				{
					val = (EPassportPolicyId)2;
				}
			}
		}
		ZuneApplication.Service.LaunchBrowserForExternalUrl(command, val);
	}
}
