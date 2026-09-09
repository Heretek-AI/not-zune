using System;
using System.Text;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;
using ZuneUI;

namespace ZuneXml;

internal class CatalogPrefixSearchQueryHelper : CatalogServiceQueryHelper
{
	internal static ZuneServiceQueryHelper ConstructPrefixSearchQueryHelper(ZuneServiceQuery query)
	{
		return new CatalogPrefixSearchQueryHelper(query);
	}

	internal CatalogPrefixSearchQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
	}

	internal override string GetResourceUri()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		string text = (string)((DataProviderQuery)base.Query).GetProperty("Prefix");
		if (!Search.Instance.IsValidKeyword(text))
		{
			return null;
		}
		string endPointUri = Service.GetEndPointUri(_endPoint);
		StringBuilder stringBuilder = new StringBuilder(128);
		stringBuilder.Append(endPointUri);
		stringBuilder.Append("/?prefix=");
		stringBuilder.Append(Uri.EscapeDataString(text));
		if ((bool)((DataProviderQuery)base.Query).GetProperty("OnlyIncludeZuneRadioArtists"))
		{
			if (FeatureEnablement.IsFeatureEnabled((Features)28))
			{
				stringBuilder.Append("&includeZuneRadioArtists=true");
			}
		}
		else
		{
			if (FeatureEnablement.IsFeatureEnabled((Features)28))
			{
				stringBuilder.Append("&includeTracks=true");
				stringBuilder.Append("&includeAlbums=true");
				stringBuilder.Append("&includeArtists=true");
			}
			if (FeatureEnablement.IsFeatureEnabled((Features)4))
			{
				stringBuilder.Append("&includeMovies=true");
				stringBuilder.Append("&includeVideoShorts=true");
			}
			if (FeatureEnablement.IsFeatureEnabled((Features)12))
			{
				stringBuilder.Append("&includeTVSeries=true");
			}
			if (FeatureEnablement.IsFeatureEnabled((Features)6))
			{
				stringBuilder.Append("&includeMusicVideos=true");
			}
			if (FeatureEnablement.IsFeatureEnabled((Features)7))
			{
				stringBuilder.Append("&includePodcasts=true");
			}
			if (FeatureEnablement.IsFeatureEnabled((Features)11))
			{
				stringBuilder.Append("&includeApplications=true");
			}
		}
		string value = (string)((DataProviderQuery)base.Query).GetProperty("ClientType");
		if (!string.IsNullOrEmpty(value))
		{
			stringBuilder.Append("&clientType=");
			stringBuilder.Append(value);
		}
		return stringBuilder.ToString();
	}
}
