using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal abstract class RatableVideo : Video
{
	internal override bool CanSync => false;

	internal override int PointsPrice => GetPointsPrice();

	internal override bool HasPreview => GetHasPreview();

	internal override bool CanPreview => GetCanPreview();

	internal override bool CanSubscriptionPlay => GetCanSubscriptionPlay();

	internal override bool CanPurchase => GetCanPurchase();

	internal override bool CanPurchaseHD => GetCanPurchaseHD();

	internal override bool CanPurchaseSD => GetCanPurchaseSD();

	internal override bool CanPurchaseSeason => GetCanPurchaseSeason();

	internal override bool CanPurchaseSeasonHD => GetCanPurchaseSeasonHD();

	internal override bool CanPurchaseSeasonSD => GetCanPurchaseSeasonSD();

	internal override bool CanPurchaseAlbumOnly => false;

	internal override bool CanRent => GetCanRent();

	internal override bool CanRentHD => GetCanRentHD();

	internal override bool CanRentSD => GetCanRentSD();

	internal override bool InCollection => GetInCollection();

	internal override bool InCollectionShortcut => GetInCollectionShortcut();

	internal override bool IsDownloading => GetIsDownloading();

	internal override bool IsParentallyBlocked => GetIsParentallyBlocked(Rating);

	internal override MiniArtist PrimaryArtist => null;

	internal override IList Artists => null;

	internal abstract string Description { get; }

	internal abstract string Rating { get; }

	protected RatableVideo(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
