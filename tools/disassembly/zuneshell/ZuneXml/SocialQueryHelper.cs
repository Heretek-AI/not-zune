using System;
using System.Text;
using Microsoft.Iris;
using ZuneUI;

namespace ZuneXml;

internal class SocialQueryHelper : ZuneServiceQueryHelper
{
	internal static ZuneServiceQueryHelper ConstructSocialQueryHelper(ZuneServiceQuery query)
	{
		return new SocialQueryHelper(query);
	}

	internal SocialQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
	}

	internal override object GetComputedProperty(string propertyName)
	{
		if (propertyName == "URI")
		{
			return GetResourceUri();
		}
		return base.GetComputedProperty(propertyName);
	}

	internal override string GetResourceUri()
	{
		string text = null;
		Guid? guid = (Guid?)((DataProviderQuery)base.Query).GetProperty("UserGuid");
		if (guid.HasValue && guid.Value != Guid.Empty)
		{
			text = guid.Value.ToString().ToUpper();
		}
		if (string.IsNullOrEmpty(text))
		{
			text = ((DataProviderQuery)base.Query).GetProperty("ZuneTag") as string;
		}
		string operation = ((DataProviderQuery)base.Query).GetProperty("Operation") as string;
		StringBuilder stringBuilder = new StringBuilder(ComposerHelper.CreateOperationUri(operation, text));
		bool first = true;
		int? num = (int?)((DataProviderQuery)base.Query).GetProperty("StartIndex");
		if (num.HasValue)
		{
			UrlHelper.AppendParam(first, stringBuilder, "startIndex", num.ToString());
			first = false;
		}
		int? num2 = (int?)((DataProviderQuery)base.Query).GetProperty("ChunkSize");
		if (num2.HasValue)
		{
			UrlHelper.AppendParam(first, stringBuilder, "chunkSize", num2.ToString());
			first = false;
		}
		return stringBuilder.ToString();
	}
}
