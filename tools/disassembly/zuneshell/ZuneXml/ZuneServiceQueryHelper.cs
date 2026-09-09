using System.Text;
using Microsoft.Iris;

namespace ZuneXml;

internal class ZuneServiceQueryHelper
{
	private ZuneServiceQuery _query;

	internal ZuneServiceQuery Query => _query;

	internal static ZuneServiceQueryHelper ConstructZuneServiceQueryHelper(ZuneServiceQuery query)
	{
		return new ZuneServiceQueryHelper(query);
	}

	internal ZuneServiceQueryHelper(ZuneServiceQuery query)
	{
		_query = query;
	}

	internal virtual string GetResourceUri()
	{
		return ((DataProviderQuery)Query).GetProperty("URI") as string;
	}

	internal virtual object GetComputedProperty(string propertyName)
	{
		return null;
	}

	internal virtual string GetQueryPostBody()
	{
		return null;
	}

	internal virtual bool HandleQueryBeginExecute()
	{
		return false;
	}

	internal virtual void OnQueryPropertyChanged(string propertyName)
	{
	}

	internal virtual bool OnQueryFilterDataProviderObject(XmlDataProviderObject dataObject)
	{
		return false;
	}

	protected static void AppendParam(StringBuilder requestUri, string name, string value, ref bool fFirst)
	{
		requestUri.Append(fFirst ? "?" : "&");
		requestUri.Append(name);
		requestUri.Append("=");
		requestUri.Append(value);
		fFirst = false;
	}
}
