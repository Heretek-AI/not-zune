using System;
using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class WirelessNetworkTypeCommand : Command
{
	private WlanAuthCipherPair _networkType;

	public WlanAuthCipherPair NetworkType => _networkType;

	public WirelessNetworkTypeCommand(IModelItemOwner owner, string description, EventHandler handler, WlanAuthCipherPair pair)
		: base(owner, description, handler)
	{
		_networkType = pair;
	}
}
