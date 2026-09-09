using System.Collections;

namespace ZuneUI;

public class DeviceSetupHashtable : Hashtable
{
	public override object this[object key]
	{
		get
		{
			return base[key];
		}
		set
		{
			base[key] = value;
			DeviceManagement.HandleSetupQueue();
		}
	}
}
