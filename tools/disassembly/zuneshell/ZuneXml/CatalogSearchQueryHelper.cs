using System;
using System.Text;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneXml;

internal class CatalogSearchQueryHelper : CatalogServiceQueryHelper
{
	internal static ZuneServiceQueryHelper ConstructSearchQueryHelper(ZuneServiceQuery query)
	{
		return new CatalogSearchQueryHelper(query);
	}

	internal CatalogSearchQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
	}

	internal override string GetResourceUri()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		string value = (string)((DataProviderQuery)base.Query).GetProperty("ResourceType");
		string text = (string)((DataProviderQuery)base.Query).GetProperty("Keywords");
		if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(text))
		{
			return null;
		}
		string endPointUri = Service.GetEndPointUri(_endPoint);
		StringBuilder stringBuilder = new StringBuilder(128);
		stringBuilder.Append(endPointUri);
		stringBuilder.Append("/");
		stringBuilder.Append(value);
		stringBuilder.Append("?q=");
		stringBuilder.Append(Uri.EscapeDataString(text));
		string value2 = (string)((DataProviderQuery)base.Query).GetProperty("ClientType");
		if (!string.IsNullOrEmpty(value2))
		{
			stringBuilder.Append("&clientType=");
			stringBuilder.Append(value2);
		}
		string value3 = (string)((DataProviderQuery)base.Query).GetProperty("Store");
		if (!string.IsNullOrEmpty(value3))
		{
			stringBuilder.Append("&store=");
			stringBuilder.Append(value3);
		}
		string timeTravel = ClientConfiguration.Service.TimeTravel;
		if (!string.IsNullOrEmpty(timeTravel) && ZuneApplication.Service.IsSignedIn())
		{
			stringBuilder.Append("&instant=");
			stringBuilder.Append(Uri.EscapeDataString(timeTravel));
		}
		return stringBuilder.ToString();
	}
}
