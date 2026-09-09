using System;

namespace ZuneUI;

public static class ApplicationMarketplaceHelper
{
	public static UIDevice FindAppDevice()
	{
		UIDevice result = UIDeviceList.NullDevice;
		if (SyncControls.Instance.CurrentDevice.SupportsStoreApplications)
		{
			result = SyncControls.Instance.CurrentDevice;
		}
		else
		{
			DateTime minValue = DateTime.MinValue;
			foreach (UIDevice item in SingletonModelItem<UIDeviceList>.Instance)
			{
				if (item.SupportsStoreApplications && item.LastConnectTime > minValue)
				{
					result = item;
				}
			}
		}
		return result;
	}

	public static void ForceAppUpdateOnAll()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		foreach (UIDevice item in SingletonModelItem<UIDeviceList>.Instance)
		{
			if (item.SupportsPaidApplications && item.IsConnectedToClient)
			{
				item.ForceAppUpdate();
			}
		}
	}

	public static UIDevice FindConnectedPaidAppDevice()
	{
		UIDevice result = UIDeviceList.NullDevice;
		if (SyncControls.Instance.CurrentDevice.SupportsPaidApplications && SyncControls.Instance.CurrentDevice.IsConnectedToClient)
		{
			result = SyncControls.Instance.CurrentDevice;
		}
		else
		{
			DateTime minValue = DateTime.MinValue;
			foreach (UIDevice item in SingletonModelItem<UIDeviceList>.Instance)
			{
				if (item.SupportsPaidApplications && item.IsConnectedToClient && item.LastConnectTime > minValue)
				{
					result = item;
				}
			}
		}
		return result;
	}
}
