using System.Net;
using Microsoft.Iris;

namespace ZuneXml;

public class DataProviderQueryHelper
{
	public static bool ResponseIsForbiddenOrUnauthorized(DataProviderQuery failedQuery)
	{
		if (failedQuery is XmlDataProviderQuery && ((XmlDataProviderQuery)(object)failedQuery).ErrorCode is HttpStatusCode httpStatusCode)
		{
			if (httpStatusCode != HttpStatusCode.Forbidden)
			{
				return httpStatusCode == HttpStatusCode.Unauthorized;
			}
			return true;
		}
		return false;
	}

	public static bool ResponseIsNotFound(DataProviderQuery failedQuery)
	{
		if (failedQuery is XmlDataProviderQuery)
		{
			object errorCode = ((XmlDataProviderQuery)(object)failedQuery).ErrorCode;
			if (errorCode is HttpStatusCode)
			{
				return (HttpStatusCode)errorCode == HttpStatusCode.NotFound;
			}
		}
		return false;
	}
}
