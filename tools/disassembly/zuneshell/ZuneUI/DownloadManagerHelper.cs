using System;
using System.Collections;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;

namespace ZuneUI;

public static class DownloadManagerHelper
{
	public static void RetryDownloading(IList items)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Invalid comparison between Unknown and I4
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Invalid comparison between Unknown and I4
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Invalid comparison between Unknown and I4
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Invalid comparison between Unknown and I4
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Invalid comparison between Unknown and I4
		if (items == null || items.Count == 0)
		{
			return;
		}
		ArrayList arrayList = new ArrayList(items.Count);
		ArrayList arrayList2 = new ArrayList(items.Count);
		EDownloadFlags val = (EDownloadFlags)0;
		string text = null;
		for (int i = 0; i < items.Count; i++)
		{
			object? obj = items[i];
			DownloadTask val2 = (DownloadTask)((obj is DownloadTask) ? obj : null);
			if (val2 == null)
			{
				continue;
			}
			EDownloadTaskState state = val2.GetState();
			if ((int)state != 4 && (int)state != 5)
			{
				continue;
			}
			EContentType contentType = Service.Instance.GetContentType(val2.GetProperty("Type"));
			if ((int)contentType == 0 || (int)contentType == 3 || (int)contentType == 7)
			{
				EDownloadFlags val3 = (EDownloadFlags)val2.GetPropertyInt("DownloadFlags");
				string property = val2.GetProperty("DeviceEndpointId");
				bool flag = i == 0;
				bool flag2 = val == val3;
				bool flag3 = string.Equals(text, property, StringComparison.InvariantCultureIgnoreCase);
				if (flag || (flag2 && flag3))
				{
					val = val3;
					text = property;
					arrayList2.Add(val2);
					DownloadManager.Instance.RemoveFailed(val2);
				}
				else
				{
					arrayList.Add(val2);
				}
			}
			else if ((int)contentType == 5)
			{
				int propertyInt = val2.GetPropertyInt("SubscriptionId");
				int propertyInt2 = val2.GetPropertyInt("MediaId");
				EpisodeDownloadCommand.DownloadEpisode(propertyInt, propertyInt2);
				DownloadManager.Instance.RemoveFailed(val2);
			}
		}
		if (arrayList2.Count > 0)
		{
			Download.Instance.DownloadContent(arrayList2, val, text);
		}
		RetryDownloading(arrayList);
	}
}
