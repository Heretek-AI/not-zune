using System.Text;
using Microsoft.Iris;

namespace ZuneXml;

internal class PodcastCatalogServiceQueryHelper : CatalogServiceQueryHelper
{
	protected override bool RequireId => false;

	protected override bool RequireResource => false;

	protected override bool RequireRepresentation => false;

	internal static ZuneServiceQueryHelper ConstructPodcastCatalogQueryHelper(ZuneServiceQuery query)
	{
		return new PodcastCatalogServiceQueryHelper(query);
	}

	internal PodcastCatalogServiceQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
	}

	protected override void AppendStuffAfterRepresentation(StringBuilder requestUri, ref bool fFirst)
	{
		base.AppendStuffAfterRepresentation(requestUri, ref fFirst);
		string value = (string)((DataProviderQuery)base.Query).GetProperty("PodcastType");
		if (!string.IsNullOrEmpty(value))
		{
			ZuneServiceQueryHelper.AppendParam(requestUri, "type", value, ref fFirst);
		}
		string value2 = (string)((DataProviderQuery)base.Query).GetProperty("PodcastUrl");
		if (!string.IsNullOrEmpty(value2))
		{
			ZuneServiceQueryHelper.AppendParam(requestUri, "url", value2, ref fFirst);
		}
	}

	internal override string GetQueryPostBody()
	{
		string text = (string)((DataProviderQuery)base.Query).GetProperty("PostUrl");
		if (!string.IsNullOrEmpty(text))
		{
			return "URL=" + text;
		}
		return null;
	}
}
