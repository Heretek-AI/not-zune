using System;
using System.Text;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneXml;

internal class CatalogServiceQueryHelper : ZuneServiceQueryHelper
{
	protected EServiceEndpointId _endPoint;

	protected virtual bool ResourceOnly
	{
		get
		{
			object property = ((DataProviderQuery)base.Query).GetProperty("ResourceOnly");
			if (property != null)
			{
				return !(bool)property;
			}
			return true;
		}
	}

	protected virtual bool RequireId => ResourceOnly;

	protected virtual bool RequireResource => true;

	protected virtual bool RequireRepresentation => ResourceOnly;

	internal static ZuneServiceQueryHelper ConstructMusicCatalogQueryHelper(ZuneServiceQuery query)
	{
		return new CatalogServiceQueryHelper(query);
	}

	internal CatalogServiceQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		_endPoint = (EServiceEndpointId)16;
		string timeTravel = ClientConfiguration.Service.TimeTravel;
		if (!string.IsNullOrEmpty(timeTravel) && ZuneApplication.Service.IsSignedIn())
		{
			query.PassportTicketType = (EPassportPolicyId)3;
		}
	}

	internal override string GetResourceUri()
	{
		return BuildServiceUri(0, 0);
	}

	private string BuildServiceUri(int chunkStart, int chunkSize)
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		StringBuilder stringBuilder = new StringBuilder(128);
		string value = (string)((DataProviderQuery)base.Query).GetProperty("Url");
		if (!string.IsNullOrEmpty(value))
		{
			stringBuilder.Append(value);
		}
		else
		{
			string text = (string)((DataProviderQuery)base.Query).GetProperty("Id");
			if (RequireId && text == string.Empty)
			{
				return null;
			}
			string text2 = (string)((DataProviderQuery)base.Query).GetProperty("ResourceType");
			string text3 = (string)((DataProviderQuery)base.Query).GetProperty("Representation");
			if ((RequireResource && text2 == null) || (RequireRepresentation && text3 == null))
			{
				return null;
			}
			string endPointUri = Service.GetEndPointUri(_endPoint);
			stringBuilder.Append(endPointUri);
			stringBuilder.Append("/");
			if (!string.IsNullOrEmpty(text2))
			{
				stringBuilder.Append(text2);
				stringBuilder.Append("/");
			}
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.Append(text);
				stringBuilder.Append("/");
			}
			if (!string.IsNullOrEmpty(text3))
			{
				stringBuilder.Append(text3);
				stringBuilder.Append("/");
			}
		}
		bool fFirst = true;
		AppendStuffAfterRepresentation(stringBuilder, ref fFirst);
		string value2 = (string)((DataProviderQuery)base.Query).GetProperty("ClientType");
		if (!string.IsNullOrEmpty(value2))
		{
			ZuneServiceQueryHelper.AppendParam(stringBuilder, "clientType", value2, ref fFirst);
		}
		string value3 = (string)((DataProviderQuery)base.Query).GetProperty("Cost");
		if (!string.IsNullOrEmpty(value3))
		{
			ZuneServiceQueryHelper.AppendParam(stringBuilder, "cost", value3, ref fFirst);
		}
		string value4 = (string)((DataProviderQuery)base.Query).GetProperty("Tag");
		if (!string.IsNullOrEmpty(value4))
		{
			ZuneServiceQueryHelper.AppendParam(stringBuilder, "tag", value4, ref fFirst);
		}
		string value5 = (string)((DataProviderQuery)base.Query).GetProperty("Store");
		if (!string.IsNullOrEmpty(value5))
		{
			ZuneServiceQueryHelper.AppendParam(stringBuilder, "store", value5, ref fFirst);
		}
		object property = ((DataProviderQuery)base.Query).GetProperty("ChunkSize");
		if (property != null && (int)property > 0)
		{
			ZuneServiceQueryHelper.AppendParam(stringBuilder, "chunkSize", property.ToString(), ref fFirst);
		}
		object property2 = ((DataProviderQuery)base.Query).GetProperty("IsActionable");
		if (property2 != null)
		{
			bool flag = (bool)property2;
			if (flag)
			{
				ZuneServiceQueryHelper.AppendParam(stringBuilder, "isActionable", flag.ToString(), ref fFirst);
			}
		}
		if (chunkSize > 0)
		{
			ZuneServiceQueryHelper.AppendParam(stringBuilder, "count", chunkSize.ToString(), ref fFirst);
			ZuneServiceQueryHelper.AppendParam(stringBuilder, "startIndex", chunkStart.ToString(), ref fFirst);
		}
		string value6 = (string)((DataProviderQuery)base.Query).GetProperty("RequestSortBy");
		if (!string.IsNullOrEmpty(value6))
		{
			ZuneServiceQueryHelper.AppendParam(stringBuilder, "orderby", value6, ref fFirst);
		}
		string timeTravel = ClientConfiguration.Service.TimeTravel;
		if (!string.IsNullOrEmpty(timeTravel) && ZuneApplication.Service.IsSignedIn())
		{
			ZuneServiceQueryHelper.AppendParam(stringBuilder, "instant", Uri.EscapeDataString(timeTravel), ref fFirst);
		}
		return stringBuilder.ToString();
	}

	protected virtual void AppendStuffAfterRepresentation(StringBuilder requestUri, ref bool fFirst)
	{
		string value = (string)((DataProviderQuery)base.Query).GetProperty("StartsWith");
		if (!string.IsNullOrEmpty(value))
		{
			ZuneServiceQueryHelper.AppendParam(requestUri, "startsWith", value, ref fFirst);
		}
		string value2 = (string)((DataProviderQuery)base.Query).GetProperty("StartDate");
		if (!string.IsNullOrEmpty(value2))
		{
			ZuneServiceQueryHelper.AppendParam(requestUri, "startDate", value2, ref fFirst);
		}
		string value3 = (string)((DataProviderQuery)base.Query).GetProperty("EndDate");
		if (!string.IsNullOrEmpty(value3))
		{
			ZuneServiceQueryHelper.AppendParam(requestUri, "endDate", value3, ref fFirst);
		}
		object property = ((DataProviderQuery)base.Query).GetProperty("MinWidth");
		if (property != null && (int)property > 0)
		{
			ZuneServiceQueryHelper.AppendParam(requestUri, "minWidth", property.ToString(), ref fFirst);
		}
		object property2 = ((DataProviderQuery)base.Query).GetProperty("MinHeight");
		if (property2 != null && (int)property2 > 0)
		{
			ZuneServiceQueryHelper.AppendParam(requestUri, "minHeight", property2.ToString(), ref fFirst);
		}
	}
}
