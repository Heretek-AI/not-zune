using System.Text;
using Microsoft.Iris;

namespace ZuneXml;

internal class SubRepresentationCatalogServiceQueryHelper : CatalogServiceQueryHelper
{
	internal SubRepresentationCatalogServiceQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
	}

	protected override void AppendStuffAfterRepresentation(StringBuilder requestUri, ref bool fFirst)
	{
		string value = (string)((DataProviderQuery)base.Query).GetProperty("SubId");
		string value2 = (string)((DataProviderQuery)base.Query).GetProperty("SubRepresentation");
		if (!string.IsNullOrEmpty(value))
		{
			requestUri.Append("/");
			requestUri.Append(value);
		}
		if (!string.IsNullOrEmpty(value2))
		{
			requestUri.Append("/");
			requestUri.Append(value2);
		}
		base.AppendStuffAfterRepresentation(requestUri, ref fFirst);
	}
}
