using System.Collections.Generic;
using Microsoft.Iris;

namespace ZuneXml;

internal class ZuneServiceQuery : XmlDataProviderQuery
{
	private delegate ZuneServiceQueryHelper ConstructQueryHelper(ZuneServiceQuery query);

	private static IDictionary<string, ConstructQueryHelper> _queryTypeToHelper;

	private ZuneServiceQueryHelper _helper;

	internal ZuneServiceQueryHelper Helper
	{
		get
		{
			if (_helper == null)
			{
				if (_queryTypeToHelper == null)
				{
					_queryTypeToHelper = new Dictionary<string, ConstructQueryHelper>(17);
					RegisterZuneServiceQueryConstructors(_queryTypeToHelper);
				}
				string key = ((DataProviderQuery)this).GetProperty("QueryType") as string;
				_helper = _queryTypeToHelper[key](this);
			}
			return _helper;
		}
	}

	private static void RegisterZuneServiceQueryConstructors(IDictionary<string, ConstructQueryHelper> queryTypeToHelper)
	{
		queryTypeToHelper.Add("Marketplace", CatalogServiceQueryHelper.ConstructMusicCatalogQueryHelper);
		queryTypeToHelper.Add("MarketplaceSearch", CatalogSearchQueryHelper.ConstructSearchQueryHelper);
		queryTypeToHelper.Add("Messaging", MessagingQueryHelper.ConstructMessagingQueryHelper);
		queryTypeToHelper.Add("PodcastMarketplace", PodcastCatalogServiceQueryHelper.ConstructPodcastCatalogQueryHelper);
		queryTypeToHelper.Add("PodcastMarketplaceSearch", CatalogSearchQueryHelper.ConstructSearchQueryHelper);
		queryTypeToHelper.Add("PrefixSearch", CatalogPrefixSearchQueryHelper.ConstructPrefixSearchQueryHelper);
		queryTypeToHelper.Add("Recommendations", RecommendationsQueryHelper.ConstructRecommendationsQueryHelper);
		queryTypeToHelper.Add("Social", SocialQueryHelper.ConstructSocialQueryHelper);
		queryTypeToHelper.Add("TopListeners", TopListenersQueryHelper.ConstructTopListenersQueryHelper);
		queryTypeToHelper.Add("UriResource", ZuneServiceQueryHelper.ConstructZuneServiceQueryHelper);
		queryTypeToHelper.Add("VideoMarketplace", VideoCatalogServiceQueryHelper.ConstructVideoCatalogQuery);
		queryTypeToHelper.Add("VideoMarketplaceSearch", CatalogSearchQueryHelper.ConstructSearchQueryHelper);
		queryTypeToHelper.Add("AppDetails", AppDetailsQueryHelper.ConstructAppDetailsQueryHelper);
		queryTypeToHelper.Add("AppGenres", AppGenresQueryHelper.ConstructAppGenresQueryHelper);
		queryTypeToHelper.Add("Reviews", ReviewsQueryHelper.ConstructReviewsQueryHelper);
		queryTypeToHelper.Add("SubscriptionHistory", SubscriptionHistoryQueryHelper.ConstructSubscriptionHistoryQueryHelper);
		queryTypeToHelper.Add("PurchaseHistory", PurchaseHistoryQueryHelper.ConstructPurchaseHistoryQueryHelper);
	}

	internal static DataProviderQuery ConstructZuneServiceQuery(object queryTypeCookie)
	{
		return (DataProviderQuery)(object)new ZuneServiceQuery(queryTypeCookie);
	}

	internal ZuneServiceQuery(object queryTypeCookie)
		: base(queryTypeCookie)
	{
		_acceptGZipEncoding = true;
	}

	public override object GetProperty(string propertyName)
	{
		return Helper.GetComputedProperty(propertyName) ?? ((DataProviderQuery)this).GetProperty(propertyName);
	}

	protected override string GetResourceUri()
	{
		return Helper.GetResourceUri();
	}

	protected override string GetPostBody()
	{
		return Helper.GetQueryPostBody();
	}

	protected override void BeginExecute()
	{
		if (!Helper.HandleQueryBeginExecute())
		{
			base.BeginExecute();
		}
	}

	internal override bool FilterDataProviderObject(XmlDataProviderObject dataObject)
	{
		return Helper.OnQueryFilterDataProviderObject(dataObject);
	}

	protected override void OnPropertyChanged(string propertyName)
	{
		if (((DataProviderQuery)this).Initialized)
		{
			base.OnPropertyChanged(propertyName);
			Helper.OnQueryPropertyChanged(propertyName);
		}
	}
}
