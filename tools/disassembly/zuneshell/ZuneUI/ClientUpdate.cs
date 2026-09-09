using Microsoft.Iris;

namespace ZuneUI;

public class ClientUpdate : ModelItem
{
	private object mylock = new object();

	private static ClientUpdate singletonInstance;

	public static ClientUpdate Instance
	{
		get
		{
			if (singletonInstance == null)
			{
				singletonInstance = new ClientUpdate();
			}
			return singletonInstance;
		}
	}

	private ClientUpdate()
	{
	}

	public void InvokeClientUpdate()
	{
		SoftwareUpdates.Instance.InstallUpdates();
		DeviceManagement.SetupDevice = null;
	}

	public void ClientUpdateSkipped()
	{
		DeviceManagement.HideSetupDevice();
	}
}
