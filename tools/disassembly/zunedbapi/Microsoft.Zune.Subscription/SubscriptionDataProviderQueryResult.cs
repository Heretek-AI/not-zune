using System;
using Microsoft.Iris;

namespace Microsoft.Zune.Subscription;

public class SubscriptionDataProviderQueryResult : DataProviderObject
{
	private SubscriptionSeriesInfo m_seriesInfo;

	private VirtualSubscriptionEpisodeList m_episodeList;

	public SubscriptionDataProviderQueryResult(DataProviderQuery owner, object typeCookie, string feedUrl, string serviceId, string sort)
		: base(owner, typeCookie)
	{
		m_episodeList = new VirtualSubscriptionEpisodeList(owner, ((DataProviderObject)this).Mappings["Items"].UnderlyingCollectionTypeCookie, feedUrl, sort);
		m_seriesInfo = new SubscriptionSeriesInfo(owner, ((DataProviderObject)this).Mappings["PodcastSeriesInfo"].PropertyTypeCookie, serviceId);
		if (!string.IsNullOrEmpty(feedUrl))
		{
			m_episodeList.AsyncRetrieveEpisodeList(m_seriesInfo);
		}
	}

	public void OnDispose()
	{
		((IDisposable)m_episodeList)?.Dispose();
		m_episodeList = null;
		SubscriptionSeriesInfo seriesInfo = m_seriesInfo;
		if (seriesInfo != null)
		{
			seriesInfo.OnDispose();
			m_seriesInfo = null;
		}
	}

	public override object GetProperty(string propertyName)
	{
		if ("Items" == propertyName)
		{
			return m_episodeList;
		}
		if ("PodcastSeriesInfo" == propertyName)
		{
			return m_seriesInfo;
		}
		return null;
	}

	public override void SetProperty(string propertyName, object value)
	{
		throw new NotSupportedException();
	}

	public unsafe static object ConvertVariantToType(string typeName, CComPropVariant* varValue)
	{
		object result = null;
		switch (typeName)
		{
		case "Int32":
			result = ((int*)varValue)[2];
			break;
		case "Boolean":
		{
			byte b = ((((short*)varValue)[4] == -1) ? ((byte)1) : ((byte)0));
			result = b != 0;
			break;
		}
		case "String":
			result = new string((char*)(int)((uint*)varValue)[2]);
			break;
		case "TimeSpan":
		{
			TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, ((int*)varValue)[2]);
			result = timeSpan;
			break;
		}
		case "DateTime":
			result = DateTime.FromOADate(((double*)varValue)[1]);
			break;
		}
		return result;
	}
}
