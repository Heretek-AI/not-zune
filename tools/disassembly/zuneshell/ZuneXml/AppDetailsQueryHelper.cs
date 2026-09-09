using System.Text;
using Microsoft.Iris;

namespace ZuneXml;

internal class AppDetailsQueryHelper : CatalogServiceQueryHelper
{
	internal static AppDetailsQueryHelper ConstructAppDetailsQueryHelper(ZuneServiceQuery query)
	{
		return new AppDetailsQueryHelper(query);
	}

	internal AppDetailsQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
	}

	protected override void AppendStuffAfterRepresentation(StringBuilder requestUri, ref bool fFirst)
	{
		base.AppendStuffAfterRepresentation(requestUri, ref fFirst);
		string value = (string)((DataProviderQuery)base.Query).GetProperty("Version");
		if (!string.IsNullOrEmpty(value))
		{
			ZuneServiceQueryHelper.AppendParam(requestUri, "version", value, ref fFirst);
		}
	}
}
