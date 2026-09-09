namespace ZuneXml;

internal class VideoCatalogServiceQueryHelper : SubRepresentationCatalogServiceQueryHelper
{
	protected override bool RequireId => false;

	protected override bool RequireResource => false;

	protected override bool RequireRepresentation => false;

	internal static ZuneServiceQueryHelper ConstructVideoCatalogQuery(ZuneServiceQuery query)
	{
		return new VideoCatalogServiceQueryHelper(query);
	}

	internal VideoCatalogServiceQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
	}
}
