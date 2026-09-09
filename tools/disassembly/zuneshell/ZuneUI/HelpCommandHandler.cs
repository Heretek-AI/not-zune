using System.Collections;
using Microsoft.Win32;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneUI;

public class HelpCommandHandler : ICommandHandler
{
	public void Execute(string command, IDictionary commandArgs)
	{
		RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Zune");
		string text = (string)registryKey.GetValue("Installation Directory");
		ZuneApplication.Service.LaunchBrowserForExternalUrl(text + command, (EPassportPolicyId)0);
	}
}
