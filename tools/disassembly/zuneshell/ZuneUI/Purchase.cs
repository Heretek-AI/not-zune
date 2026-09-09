using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.ErrorMapperApi;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using ZuneXml;

namespace ZuneUI;

public class Purchase : ModelItem
{
	private AlbumOfferCollection m_albumOfferCollection;

	private TrackOfferCollection m_trackOfferCollection;

	private VideoOfferCollection m_videoOfferCollection;

	private AppOfferCollection m_appOfferCollection;

	private IList m_albumOffers;

	private IList m_trackOffers;

	private IList m_videoOffers;

	private IList m_appOffers;

	private bool m_fPurchaseHD;

	private bool m_fMultipleResolutionOptions;

	private bool m_fMultiplePlaybackOptions;

	private bool m_fRentVideos;

	private bool m_fStreamVideos;

	private bool m_fPurchaseTrials;

	private bool m_fPurchaseSeason;

	private bool m_hasSubscriptionFreeTracks;

	private int m_rentDeviceId = -1;

	private DateTime _videoExpirationDate = DateTime.MaxValue;

	private bool m_fInsufficientPoints;

	private int m_pointsBalance = -1;

	private int m_subscriptionFreeTrackBalance = -1;

	private int m_totalPoints = -1;

	private double m_totalCurrencyPrice = -1.0;

	private int m_totalSubscriptionFreeTracks = -1;

	private string m_displayPointsPrice;

	private string m_displayCurrencyPrice;

	private string m_errorMessage;

	private string m_errorWebHelpUrl;

	private string m_status;

	private string m_additionalStatus;

	private string m_rentDeviceName;

	private string m_rentDeviceEndpointId;

	private bool m_rentDeviceSupportsHD = true;

	private bool m_fRequestingBalances;

	private bool m_fIsBalanceUpdated;

	private bool m_fRequestingPointsOffers;

	private bool m_fPurchaseComplete;

	private bool m_fSubscriptionFreeTracksOnly;

	private bool m_authorizationRequired;

	private ResumePurchaseData m_resumePurchaseData;

	private bool m_fCanPurchase;

	private bool m_fCanDownload;

	private bool m_fPurchasingPoints;

	private BillingOffer m_pointsOffer;

	private BillingOfferCollection m_pointsOffers;

	private BillingOfferHelper m_pointsHelper;

	public int PointsBalance
	{
		get
		{
			if (m_pointsBalance <= 0)
			{
				return 0;
			}
			return m_pointsBalance;
		}
		private set
		{
			if (m_pointsBalance != value)
			{
				m_pointsBalance = value;
				((ModelItem)this).FirePropertyChanged("PointsBalance");
			}
		}
	}

	public bool IsBalanceUpdated
	{
		get
		{
			return m_fIsBalanceUpdated;
		}
		private set
		{
			m_fIsBalanceUpdated = value;
			((ModelItem)this).FirePropertyChanged("IsBalanceUpdated");
		}
	}

	public int SubscriptionFreeTrackBalance
	{
		get
		{
			if (m_subscriptionFreeTrackBalance <= 0)
			{
				return 0;
			}
			return m_subscriptionFreeTrackBalance;
		}
		private set
		{
			if (m_subscriptionFreeTrackBalance != value)
			{
				m_subscriptionFreeTrackBalance = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionFreeTrackBalance");
			}
		}
	}

	public IList AlbumOffers
	{
		get
		{
			return m_albumOffers;
		}
		private set
		{
			if (m_albumOffers != value)
			{
				m_albumOffers = value;
				((ModelItem)this).FirePropertyChanged("AlbumOffers");
			}
		}
	}

	public IList TrackOffers
	{
		get
		{
			return m_trackOffers;
		}
		private set
		{
			if (m_trackOffers != value)
			{
				m_trackOffers = value;
				((ModelItem)this).FirePropertyChanged("TrackOffers");
			}
		}
	}

	public IList VideoOffers
	{
		get
		{
			return m_videoOffers;
		}
		private set
		{
			if (m_videoOffers != value)
			{
				m_videoOffers = value;
				((ModelItem)this).FirePropertyChanged("VideoOffers");
			}
		}
	}

	public IList AppOffers
	{
		get
		{
			return m_appOffers;
		}
		private set
		{
			if (m_appOffers != value)
			{
				m_appOffers = value;
				((ModelItem)this).FirePropertyChanged("AppOffers");
			}
		}
	}

	public bool AuthenticationRequired
	{
		get
		{
			return m_authorizationRequired;
		}
		set
		{
			if (m_authorizationRequired != value)
			{
				m_authorizationRequired = value;
				((ModelItem)this).FirePropertyChanged("AuthenticationRequired");
			}
		}
	}

	public ResumePurchaseData ResumePurchaseData
	{
		get
		{
			return m_resumePurchaseData;
		}
		set
		{
			if (m_resumePurchaseData == null || m_resumePurchaseData.Equals(value))
			{
				m_resumePurchaseData = value;
				((ModelItem)this).FirePropertyChanged("ResumePurchaseData");
			}
		}
	}

	public string AdditionalStatus
	{
		get
		{
			return m_additionalStatus;
		}
		set
		{
			if (m_additionalStatus != value)
			{
				m_additionalStatus = value;
				((ModelItem)this).FirePropertyChanged("AdditionalStatus");
				UpdateState();
			}
		}
	}

	public bool MultiplePlaybackOptions
	{
		get
		{
			return m_fMultiplePlaybackOptions;
		}
		private set
		{
			if (m_fMultiplePlaybackOptions != value)
			{
				m_fMultiplePlaybackOptions = value;
				((ModelItem)this).FirePropertyChanged("MultiplePlaybackOptions");
			}
		}
	}

	public bool MultipleResolutionOptions
	{
		get
		{
			return m_fMultipleResolutionOptions;
		}
		private set
		{
			if (m_fMultipleResolutionOptions != value)
			{
				m_fMultipleResolutionOptions = value;
				((ModelItem)this).FirePropertyChanged("MultipleResolutionOptions");
			}
		}
	}

	public bool PurchaseHD
	{
		get
		{
			return m_fPurchaseHD;
		}
		set
		{
			if (m_fPurchaseHD != value)
			{
				m_fPurchaseHD = value;
				((ModelItem)this).FirePropertyChanged("PurchaseHD");
				UpdateVideoOffers();
				CalculateTotal();
				UpdateState();
			}
		}
	}

	public bool RentVideos
	{
		get
		{
			return m_fRentVideos;
		}
		set
		{
			if (m_fRentVideos != value)
			{
				m_fRentVideos = value;
				((ModelItem)this).FirePropertyChanged("RentVideos");
				UpdateVideoOffers();
				CalculateTotal();
				UpdateState();
			}
		}
	}

	public bool StreamVideos
	{
		get
		{
			return m_fStreamVideos;
		}
		set
		{
			if (m_fStreamVideos != value)
			{
				m_fStreamVideos = value;
				((ModelItem)this).FirePropertyChanged("StreamVideos");
				UpdateVideoOffers();
				CalculateTotal();
				UpdateState();
			}
		}
	}

	public bool PurchaseTrials
	{
		get
		{
			return m_fPurchaseTrials;
		}
		set
		{
			if (m_fPurchaseTrials != value)
			{
				m_fPurchaseTrials = value;
				((ModelItem)this).FirePropertyChanged("PurchaseTrials");
				UpdateAppOffers();
				CalculateTotal();
				UpdateState();
			}
		}
	}

	public bool PurchaseSeason
	{
		get
		{
			return m_fPurchaseSeason;
		}
		set
		{
			if (m_fPurchaseSeason != value)
			{
				m_fPurchaseSeason = value;
				((ModelItem)this).FirePropertyChanged("PurchaseSeason");
				UpdateVideoOffers();
				CalculateTotal();
				UpdateState();
			}
		}
	}

	public int RentDeviceId
	{
		get
		{
			return m_rentDeviceId;
		}
		set
		{
			if (m_rentDeviceId != value)
			{
				m_rentDeviceId = value;
				((ModelItem)this).FirePropertyChanged("RentDeviceId");
				UpdateState();
			}
		}
	}

	public string RentDeviceName
	{
		get
		{
			return m_rentDeviceName ?? string.Empty;
		}
		set
		{
			if (m_rentDeviceName != value)
			{
				m_rentDeviceName = value;
				((ModelItem)this).FirePropertyChanged("RentDeviceName");
			}
		}
	}

	public string RentDeviceEndpointId
	{
		get
		{
			return m_rentDeviceEndpointId ?? string.Empty;
		}
		set
		{
			if (m_rentDeviceEndpointId != value)
			{
				m_rentDeviceEndpointId = value;
				((ModelItem)this).FirePropertyChanged("RentDeviceEndpointId");
			}
		}
	}

	public bool RentDeviceSupportsHD
	{
		get
		{
			return m_rentDeviceSupportsHD;
		}
		set
		{
			if (m_rentDeviceSupportsHD != value)
			{
				m_rentDeviceSupportsHD = value;
				((ModelItem)this).FirePropertyChanged("RentDeviceSupportsHD");
			}
		}
	}

	public string DisplayPointsPrice
	{
		get
		{
			return m_displayPointsPrice;
		}
		private set
		{
			if (value != m_displayPointsPrice)
			{
				m_displayPointsPrice = value;
				((ModelItem)this).FirePropertyChanged("DisplayPointsPrice");
			}
		}
	}

	public string DisplayCurrencyPrice
	{
		get
		{
			return m_displayCurrencyPrice;
		}
		private set
		{
			if (value != m_displayCurrencyPrice)
			{
				m_displayCurrencyPrice = value;
				((ModelItem)this).FirePropertyChanged("DisplayCurrencyPrice");
			}
		}
	}

	public bool IsFree
	{
		get
		{
			if (HasPrice && !HasSubscriptionFreeTracks && TotalPoints == 0)
			{
				return TotalCurrencyPrice == 0.0;
			}
			return false;
		}
	}

	private bool HasSubscriptionFreeTracks
	{
		get
		{
			return m_hasSubscriptionFreeTracks;
		}
		set
		{
			if (m_hasSubscriptionFreeTracks != value)
			{
				m_hasSubscriptionFreeTracks = value;
				((ModelItem)this).FirePropertyChanged("HasSubscriptionFreeTracks");
				((ModelItem)this).FirePropertyChanged("IsFree");
			}
		}
	}

	private bool HasPrice
	{
		get
		{
			if (m_totalPoints < 0)
			{
				return m_totalCurrencyPrice >= 0.0;
			}
			return true;
		}
	}

	public int TotalPoints
	{
		get
		{
			if (m_totalPoints <= 0)
			{
				return 0;
			}
			return m_totalPoints;
		}
		private set
		{
			if (m_totalPoints != value)
			{
				m_totalPoints = value;
				((ModelItem)this).FirePropertyChanged("TotalPoints");
				((ModelItem)this).FirePropertyChanged("IsFree");
			}
		}
	}

	public double TotalCurrencyPrice
	{
		get
		{
			if (!(m_totalCurrencyPrice > 0.0))
			{
				return 0.0;
			}
			return m_totalCurrencyPrice;
		}
		private set
		{
			if (m_totalCurrencyPrice != value)
			{
				m_totalCurrencyPrice = value;
				((ModelItem)this).FirePropertyChanged("TotalCurrencyPrice");
				((ModelItem)this).FirePropertyChanged("IsFree");
			}
		}
	}

	public int TotalSubscriptionFreeTracks
	{
		get
		{
			if (m_totalSubscriptionFreeTracks <= 0)
			{
				return 0;
			}
			return m_totalSubscriptionFreeTracks;
		}
		private set
		{
			if (m_totalSubscriptionFreeTracks != value)
			{
				m_totalSubscriptionFreeTracks = value;
				((ModelItem)this).FirePropertyChanged("TotalSubscriptionFreeTracks");
			}
		}
	}

	public bool InsufficientPoints
	{
		get
		{
			return m_fInsufficientPoints;
		}
		private set
		{
			if (m_fInsufficientPoints != value)
			{
				m_fInsufficientPoints = value;
				((ModelItem)this).FirePropertyChanged("InsufficientPoints");
			}
		}
	}

	public bool CanPurchase
	{
		get
		{
			return m_fCanPurchase;
		}
		private set
		{
			if (m_fCanPurchase != value)
			{
				m_fCanPurchase = value;
				((ModelItem)this).FirePropertyChanged("CanPurchase");
			}
		}
	}

	public bool CanDownload
	{
		get
		{
			return m_fCanDownload;
		}
		private set
		{
			if (m_fCanDownload != value)
			{
				m_fCanDownload = value;
				((ModelItem)this).FirePropertyChanged("CanDownload");
			}
		}
	}

	public string ErrorMessage
	{
		get
		{
			return m_errorMessage ?? string.Empty;
		}
		set
		{
			if (m_errorMessage != value)
			{
				m_errorMessage = value;
				((ModelItem)this).FirePropertyChanged("ErrorMessage");
			}
		}
	}

	public string ErrorWebHelpUrl
	{
		get
		{
			return m_errorWebHelpUrl ?? string.Empty;
		}
		set
		{
			if (m_errorWebHelpUrl != value)
			{
				m_errorWebHelpUrl = value;
				((ModelItem)this).FirePropertyChanged("ErrorWebHelpUrl");
			}
		}
	}

	public bool SubscriptionFreeTracksOnly
	{
		get
		{
			return m_fSubscriptionFreeTracksOnly;
		}
		private set
		{
			if (m_fSubscriptionFreeTracksOnly != value)
			{
				m_fSubscriptionFreeTracksOnly = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionFreeTracksOnly");
			}
		}
	}

	public string Status
	{
		get
		{
			return m_status ?? string.Empty;
		}
		set
		{
			if (m_status != value)
			{
				m_status = value;
				((ModelItem)this).FirePropertyChanged("Status");
			}
		}
	}

	public DateTime VideoExpirationDate
	{
		get
		{
			return _videoExpirationDate;
		}
		set
		{
			if (_videoExpirationDate != value)
			{
				_videoExpirationDate = value;
				((ModelItem)this).FirePropertyChanged("ExpirationDate");
			}
		}
	}

	public bool PurchaseComplete
	{
		get
		{
			return m_fPurchaseComplete;
		}
		set
		{
			if (m_fPurchaseComplete != value)
			{
				m_fPurchaseComplete = value;
				((ModelItem)this).FirePropertyChanged("PurchaseComplete");
			}
		}
	}

	public bool PurchasingPoints
	{
		get
		{
			return m_fPurchasingPoints;
		}
		private set
		{
			if (m_fPurchasingPoints != value)
			{
				m_fPurchasingPoints = value;
				((ModelItem)this).FirePropertyChanged("PurchasingPoints");
			}
		}
	}

	public BillingOffer BestPointsOffer
	{
		get
		{
			return m_pointsOffer;
		}
		private set
		{
			if (m_pointsOffer != value)
			{
				m_pointsOffer = value;
				((ModelItem)this).FirePropertyChanged("BestPointsOffer");
			}
		}
	}

	private BillingOfferHelper PointsHelper
	{
		get
		{
			if (m_pointsHelper == null)
			{
				m_pointsHelper = new BillingOfferHelper();
				EventHandler value = OnPointsPurchaseCompletedOrFailed;
				m_pointsHelper.PurchaseComplete += value;
				m_pointsHelper.PurchaseFailed += value;
			}
			return m_pointsHelper;
		}
	}

	private string ConditionsOfPurchase
	{
		get
		{
			string empty = string.Empty;
			if (VideoOffers.Count > 0 && !RentVideos)
			{
				if ((VideoExpirationDate - DateTime.UtcNow).Days > 365)
				{
					empty = Shell.LoadString(StringId.IDS_PURCHASE_NO_REFUNDS_UNKNOWN_BLACKOUT);
				}
				else
				{
					string format = Shell.LoadString(StringId.IDS_PURCHASE_NO_REFUNDS_KNOWN_BLACKOUT);
					empty = string.Format(format, VideoExpirationDate.ToLocalTime().ToShortDateString());
				}
			}
			else
			{
				empty = Shell.LoadString(StringId.IDS_PURCHASE_NO_REFUNDS);
			}
			if (!string.IsNullOrEmpty(AdditionalStatus))
			{
				return $"{empty} {AdditionalStatus}";
			}
			return empty;
		}
	}

	private EPurchaseOffersFlags PurchaseOffersFlags
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			EPurchaseOffersFlags val = (EPurchaseOffersFlags)0;
			if (PurchaseHD)
			{
				val = (EPurchaseOffersFlags)(val | 1);
			}
			if (RentVideos)
			{
				val = (EPurchaseOffersFlags)(val | 2);
			}
			if (StreamVideos)
			{
				val = (EPurchaseOffersFlags)(val | 4);
			}
			if (PurchaseTrials)
			{
				val = (EPurchaseOffersFlags)(val | 8);
			}
			return val;
		}
	}

	internal static event EventHandler PurchaseEvent;

	public Purchase()
	{
		m_fPurchaseHD = ClientConfiguration.Service.PurchaseHD;
	}

	public void GetOffers(IList guidAlbumIds, IList guidTrackIds, IList guidVideoIds, IList guidAppIds)
	{
		GetOffers(guidAlbumIds, guidTrackIds, guidVideoIds, guidAppIds, subscriptionFreeTracksOnly: false, null);
	}

	public void GetOffers(IList guidAlbumIds, IList guidTrackIds, IList guidVideoIds, IList guidAppIds, bool subscriptionFreeTracksOnly)
	{
		GetOffers(guidAlbumIds, guidTrackIds, guidVideoIds, guidAppIds, subscriptionFreeTracksOnly, null);
	}

	public void GetOffers(IList guidAlbumIds, IList guidTrackIds, IList guidVideoIds, IList guidAppIds, bool subscriptionFreeTracksOnly, IDictionary mapIdToContext)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0073: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		SubscriptionFreeTracksOnly = subscriptionFreeTracksOnly;
		string text = string.Empty;
		if (RentVideos)
		{
			text = RentDeviceEndpointId;
		}
		if (mapIdToContext == null)
		{
			mapIdToContext = new Hashtable();
		}
		UpdateContextMap(guidAlbumIds, guidTrackIds, mapIdToContext);
		EGetOffersFlags val = (EGetOffersFlags)0;
		if (PurchaseSeason)
		{
			val = (EGetOffersFlags)(val | 2);
		}
		if (subscriptionFreeTracksOnly)
		{
			val = (EGetOffersFlags)(val | 1);
		}
		ZuneApplication.Service.GetOffers(guidAlbumIds, guidTrackIds, guidVideoIds, guidAppIds, mapIdToContext, val, text, new GetOffersCompleteCallback(OnGetOffersComplete), new GetOffersErrorCallback(OnGetOffersError));
	}

	private void UpdateContextMap(IList guidAlbumIds, IList guidTrackIds, IDictionary mapIdToContext)
	{
		ZunePage currentPage = ZuneShell.DefaultInstance.CurrentPage;
		if (currentPage.NavigationArguments == null || !currentPage.NavigationArguments.Contains("ReferrerContext"))
		{
			return;
		}
		string value = (string)currentPage.NavigationArguments["ReferrerContext"];
		Guid guid = Guid.Empty;
		Guid guid2 = Guid.Empty;
		Guid guid3 = Guid.Empty;
		if (currentPage.NavigationArguments.Contains("ReferrerTrackId"))
		{
			guid = (Guid)currentPage.NavigationArguments["ReferrerTrackId"];
		}
		else if (currentPage.NavigationArguments.Contains("ReferrerAlbumId"))
		{
			guid2 = (Guid)currentPage.NavigationArguments["ReferrerAlbumId"];
		}
		else if (currentPage.NavigationArguments.Contains("ReferrerArtistId"))
		{
			guid3 = (Guid)currentPage.NavigationArguments["ReferrerArtistId"];
		}
		if (guidAlbumIds != null)
		{
			foreach (Guid guidAlbumId in guidAlbumIds)
			{
				if (!mapIdToContext.Contains(guidAlbumId) && (guid3 != Guid.Empty || guid != Guid.Empty || guid2 == guidAlbumId))
				{
					mapIdToContext[guidAlbumId] = value;
				}
			}
		}
		if (guidTrackIds == null)
		{
			return;
		}
		foreach (Guid guidTrackId in guidTrackIds)
		{
			if (!mapIdToContext.Contains(guidTrackId) && (guid3 != Guid.Empty || guid2 != Guid.Empty || guid == guidTrackId))
			{
				mapIdToContext[guidTrackId] = value;
			}
		}
	}

	public void GetBalances()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0031: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006a: Expected O, but got Unknown
		if (!m_fRequestingBalances)
		{
			m_fRequestingBalances = true;
			ZuneApplication.Service.GetBalances(new GetBalancesCompleteCallback(OnGetBalancesComplete), new GetBalancesErrorCallback(OnGetBalancesError));
		}
		if (!m_fRequestingPointsOffers && m_pointsOffers == null)
		{
			m_fRequestingPointsOffers = true;
			ZuneApplication.Service.GetPointsOffers(new GetBillingOffersCompleteCallback(OnGetPointsOffersComplete), new GetBillingOffersErrorCallback(OnGetPointsOffersError));
		}
	}

	public void PurchaseOffers(PaymentInstrument payment)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		ErrorMessage = null;
		ErrorWebHelpUrl = null;
		ZuneApplication.Service.PurchaseOffers(payment, m_albumOfferCollection, m_trackOfferCollection, m_videoOfferCollection, m_appOfferCollection, PurchaseOffersFlags, new PurchaseOffersCompleteHandler(OnPurchaseOffersComplete));
		if (RentVideos)
		{
			Status = Shell.LoadString(StringId.IDS_PURCHASE_RENTAL_IN_PROGRESS);
		}
		else
		{
			Status = Shell.LoadString(StringId.IDS_PURCHASE_IN_PROGRESS);
		}
	}

	public void ResumePurchase(string purchaseHandle, string token)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		ZuneApplication.Service.ResumePurchase(purchaseHandle, token, new AsyncCompleteHandler(OnResumePurchaseComplete));
	}

	public void PurchaseBestPointsOffer(PaymentInstrument paymentInstrument)
	{
		if (m_pointsOffer != null)
		{
			PurchasingPoints = true;
			PointsHelper.Purchase(m_pointsOffer, paymentInstrument);
			UpdateState();
		}
	}

	public void ChangeRentDevice(int deviceId, string deviceEndpointId, string deviceName, bool deviceSupportsHD)
	{
		if (deviceId != RentDeviceId)
		{
			if (m_albumOfferCollection != null && m_albumOfferCollection.Items != null && m_trackOfferCollection != null && m_trackOfferCollection.Items != null && m_videoOfferCollection != null && m_videoOfferCollection.Items != null && m_appOfferCollection != null && m_appOfferCollection.Items != null)
			{
				m_albumOfferCollection.Items.Clear();
				m_trackOfferCollection.Items.Clear();
				m_videoOfferCollection.Items.Clear();
				m_appOfferCollection.Items.Clear();
				UpdateOffers(m_albumOfferCollection, m_trackOfferCollection, m_videoOfferCollection, m_appOfferCollection, 0);
				AlbumOffers = null;
				TrackOffers = null;
				VideoOffers = null;
				AppOffers = null;
			}
			RentDeviceId = deviceId;
			RentDeviceEndpointId = deviceEndpointId;
			RentDeviceName = deviceName;
			RentDeviceSupportsHD = deviceSupportsHD;
		}
	}

	public void RentDeviceCountMaxExceeded()
	{
		ShipAssert.Assert(false);
	}

	private void UpdateState()
	{
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Expected O, but got Unknown
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Expected O, but got Unknown
		InsufficientPoints = m_totalPoints >= 0 && m_pointsBalance >= 0 && m_totalPoints > m_pointsBalance;
		if (AlbumOffers != null && TrackOffers != null && VideoOffers != null && AppOffers != null)
		{
			if (AlbumOffers.Count == 0 && TrackOffers.Count == 0 && VideoOffers.Count == 0 && AppOffers.Count == 0)
			{
				Status = Shell.LoadString(StringId.IDS_PURCHASE_NO_ITEMS);
			}
			else if (PurchasingPoints)
			{
				Status = Shell.LoadString(StringId.IDS_PURCHASE_POINTS_IN_PROGRESS);
			}
			else if (InsufficientPoints)
			{
				CalculateBestPointsOffer();
				Status = Shell.LoadString(StringId.IDS_PURCHASE_INSUFFICIENT_POINTS);
			}
			else if (TotalSubscriptionFreeTracks > 0)
			{
				if (SubscriptionFreeTracksOnly)
				{
					Status = Shell.LoadString(StringId.IDS_PURCHASE_SUGGESTED_SONGS_NOTICE);
				}
				else
				{
					Status = string.Format(Shell.LoadString(StringId.IDS_PURCHASE_FREE_TRACKS_AND_NO_REFUNDS), TotalSubscriptionFreeTracks);
				}
			}
			else if (MultipleResolutionOptions)
			{
				string format = Shell.LoadString(PurchaseHD ? StringId.IDS_PURCHASE_HD_DESC_AND_CONDITIONS : StringId.IDS_PURCHASE_SD_DESC_AND_CONDITIONS);
				Status = string.Format(format, ConditionsOfPurchase);
			}
			else
			{
				Status = ConditionsOfPurchase;
			}
		}
		else
		{
			Status = Shell.LoadString(StringId.IDS_PURCHASE_CALC_TOTAL);
		}
		bool flag = false;
		bool flag2 = false;
		if (AlbumOffers != null)
		{
			foreach (AlbumOffer albumOffer in AlbumOffers)
			{
				AlbumOffer val = albumOffer;
				if (!((Offer)val).InCollection)
				{
					if (!((Offer)val).PreviouslyPurchased)
					{
						flag2 = true;
					}
					else
					{
						flag = true;
					}
				}
			}
		}
		if (TrackOffers != null)
		{
			foreach (TrackOffer trackOffer in TrackOffers)
			{
				TrackOffer val2 = trackOffer;
				if (!((Offer)val2).InCollection)
				{
					if (!((Offer)val2).PreviouslyPurchased)
					{
						flag2 = true;
					}
					else
					{
						flag = true;
					}
				}
			}
		}
		if (VideoOffers != null)
		{
			foreach (VideoOffer videoOffer in VideoOffers)
			{
				VideoOffer val3 = videoOffer;
				if (!((Offer)val3).InCollection)
				{
					if (!((Offer)val3).PreviouslyPurchased)
					{
						flag2 = true;
					}
					else
					{
						flag = true;
					}
				}
			}
		}
		if (AppOffers != null)
		{
			foreach (AppOffer appOffer in AppOffers)
			{
				AppOffer val4 = appOffer;
				if (!((Offer)val4).InCollection)
				{
					if (!((Offer)val4).PreviouslyPurchased)
					{
						flag2 = true;
					}
					else
					{
						flag = true;
					}
				}
			}
		}
		CanPurchase = flag2 && !InsufficientPoints;
		CanDownload = flag && !flag2;
	}

	private void CalculateBestPointsOffer()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		BillingOffer val = null;
		if (m_pointsOffers != null)
		{
			int num = TotalPoints - PointsBalance;
			foreach (BillingOffer item in m_pointsOffers.Items)
			{
				BillingOffer val2 = item;
				if (val2.Points >= num && (val == null || val.Points > val2.Points))
				{
					val = val2;
				}
			}
		}
		BestPointsOffer = val;
	}

	private void SetError(HRESULT hrError)
	{
		ErrorMapperResult mappedErrorDescriptionAndUrl = ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref hrError)).Int, (eErrorCondition)2);
		ErrorMessage = mappedErrorDescriptionAndUrl.Description;
		ErrorWebHelpUrl = mappedErrorDescriptionAndUrl.WebHelpUrl;
		CanPurchase = false;
		CanDownload = false;
		Status = null;
	}

	private void OnGetBalancesComplete(int pointsBalance, int freeTrackBalance)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredGetBalancesComplete), (object)new int[2] { pointsBalance, freeTrackBalance });
	}

	private void OnGetBalancesError(HRESULT hrError)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredGetBalancesError), (object)hrError);
	}

	private void OnGetPointsOffersComplete(BillingOfferCollection pointsOffers)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredGetPointsOffersComplete), (object)pointsOffers);
	}

	private void OnGetPointsOffersError(HRESULT hrError)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredGetPointsOffersError), (object)hrError);
	}

	private void OnGetOffersComplete(AlbumOfferCollection albumOffers, TrackOfferCollection trackOffers, VideoOfferCollection videoOffers, AppOfferCollection appOffers, int subscriptionFreeTracks)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredGetOffersComplete), (object)new object[5] { albumOffers, trackOffers, videoOffers, appOffers, subscriptionFreeTracks });
	}

	private void OnGetOffersError(HRESULT hrError)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetError), (object)hrError);
	}

	private void OnResumePurchaseComplete(HRESULT hr)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		OnPurchaseOffersComplete(hr, null, null);
	}

	private void OnPurchaseOffersComplete(HRESULT hr, string redirectUrl, string handle)
	{
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Expected O, but got Unknown
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		if (AlbumOffers == null || TrackOffers == null || VideoOffers == null || AppOffers == null)
		{
			return;
		}
		if (((HRESULT)(ref hr)).IsSuccess)
		{
			ArrayList arrayList = new ArrayList(AlbumOffers.Count + TrackOffers.Count + VideoOffers.Count + AppOffers.Count);
			VideoPlaybackTrack videoPlaybackTrack = null;
			foreach (AlbumOffer albumOffer in AlbumOffers)
			{
				AlbumOffer value = albumOffer;
				arrayList.Add(value);
			}
			foreach (TrackOffer trackOffer in TrackOffers)
			{
				TrackOffer value2 = trackOffer;
				arrayList.Add(value2);
			}
			foreach (VideoOffer videoOffer in VideoOffers)
			{
				VideoOffer val = videoOffer;
				arrayList.Add(val);
				if (val.IsStream && videoPlaybackTrack == null)
				{
					videoPlaybackTrack = new VideoPlaybackTrack(((Offer)val).Id, ((Offer)val).Title, null, null, isDownloading: false, isStreaming: true, ignoreCollection: false, fallbackToPreview: false, forcePreview: false, val.IsHD ? VideoDefinitionEnum.HD : VideoDefinitionEnum.SD);
				}
			}
			foreach (AppOffer appOffer in AppOffers)
			{
				AppOffer value3 = appOffer;
				arrayList.Add(value3);
			}
			if (StreamVideos && videoPlaybackTrack != null)
			{
				Application.DeferredInvoke(new DeferredInvokeHandler(DeferredPlayPurchasedStreams), (object)videoPlaybackTrack);
			}
			EDownloadFlags val2 = (EDownloadFlags)0;
			if (RentVideos)
			{
				val2 = (EDownloadFlags)(val2 | 0x20);
				if (RentDeviceId > 0)
				{
					val2 = (EDownloadFlags)(val2 | 8);
				}
			}
			if (StreamVideos)
			{
				val2 = (EDownloadFlags)(val2 | 0x40);
			}
			if (PurchaseHD)
			{
				val2 = (EDownloadFlags)(val2 | 0x80);
			}
			Download.Instance.DownloadContent(arrayList, val2, RentDeviceEndpointId, OnDownloadsAllPending);
		}
		else
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetError), (object)hr);
		}
	}

	private void OnDownloadsAllPending(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredDownloadsAllPending), (object)null);
	}

	private void OnPointsPurchaseCompletedOrFailed(object sender, EventArgs args)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		PurchasingPoints = false;
		HRESULT errorCode = PointsHelper.ErrorCode;
		if (((HRESULT)(ref errorCode)).IsError)
		{
			SetError(PointsHelper.ErrorCode);
			UpdateState();
			return;
		}
		if (m_pointsOffer != null)
		{
			int num = m_pointsBalance + (int)m_pointsOffer.Points;
			InsufficientPoints = m_totalPoints >= 0 && num >= 0 && m_totalPoints > num;
		}
		GetBalances();
	}

	private void DeferredGetOffersComplete(object args)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Expected O, but got Unknown
		object[] array = (object[])args;
		UpdateOffers((AlbumOfferCollection)array[0], (TrackOfferCollection)array[1], (VideoOfferCollection)array[2], (AppOfferCollection)array[3], (int)array[4]);
	}

	public void UpdateOffers(AlbumOfferCollection albums, TrackOfferCollection tracks, VideoOfferCollection videos, AppOfferCollection apps, int totalSubscriptionFreeTracks)
	{
		m_albumOfferCollection = albums;
		m_trackOfferCollection = tracks;
		m_videoOfferCollection = videos;
		m_appOfferCollection = apps;
		TotalSubscriptionFreeTracks = totalSubscriptionFreeTracks;
		AlbumOffers = ((m_albumOfferCollection != null) ? m_albumOfferCollection.Items : null);
		TrackOffers = ((m_trackOfferCollection != null) ? m_trackOfferCollection.Items : null);
		UpdateAppOffers();
		UpdateVideoOffers();
		CalculateTotal();
		UpdateState();
	}

	private void UpdateAppOffers()
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		IList list = null;
		if (m_appOfferCollection != null && m_appOfferCollection.Items != null)
		{
			list = new ArrayList(m_appOfferCollection.Items.Count);
			if (PurchaseTrials)
			{
				foreach (AppOffer item in m_appOfferCollection.Items)
				{
					AppOffer val = item;
					if (val.IsTrialPurchase)
					{
						list.Add(val);
					}
				}
			}
			else
			{
				foreach (AppOffer item2 in m_appOfferCollection.Items)
				{
					AppOffer val2 = item2;
					if (!val2.IsTrialPurchase)
					{
						list.Add(val2);
					}
				}
			}
		}
		AppOffers = list;
	}

	private void UpdateVideoOffers()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Expected O, but got Unknown
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Expected O, but got Unknown
		bool flag = false;
		IList list = ((m_videoOfferCollection != null) ? m_videoOfferCollection.Items : null);
		if (list != null)
		{
			foreach (VideoOffer item in list)
			{
				VideoOffer val = item;
				if (((Offer)val).PreviouslyPurchased && !val.IsRental)
				{
					flag = true;
				}
			}
		}
		int num = 0;
		int num2 = 0;
		if (list != null)
		{
			foreach (VideoOffer item2 in list)
			{
				VideoOffer val2 = item2;
				if (val2.IsRental == RentVideos && (!RentVideos || !flag))
				{
					if (val2.IsStream)
					{
						num++;
					}
					else
					{
						num2++;
					}
				}
			}
		}
		MultiplePlaybackOptions = num > 0 && num2 > 0;
		if (!MultiplePlaybackOptions)
		{
			StreamVideos = num > 0;
		}
		int num3 = 0;
		int num4 = 0;
		if (list != null)
		{
			foreach (VideoOffer item3 in list)
			{
				VideoOffer val3 = item3;
				if (val3.IsRental == RentVideos && val3.IsStream == StreamVideos && (!RentVideos || !flag))
				{
					if (val3.IsHD)
					{
						num3++;
					}
					else
					{
						num4++;
					}
				}
			}
		}
		MultipleResolutionOptions = num3 > 0 && num4 > 0;
		if (!MultipleResolutionOptions && list != null)
		{
			PurchaseHD = num3 > 0;
		}
		IList list2 = null;
		if (list != null)
		{
			list2 = new ArrayList(PurchaseHD ? num3 : num4);
			foreach (VideoOffer item4 in list)
			{
				VideoOffer val4 = item4;
				if (val4.IsRental == RentVideos && val4.IsStream == StreamVideos && val4.IsHD == PurchaseHD && (!RentVideos || !flag))
				{
					list2.Add(val4);
				}
			}
		}
		VideoExpirationDate = DateTime.MaxValue;
		if (list2 != null)
		{
			foreach (VideoOffer item5 in list2)
			{
				VideoOffer val5 = item5;
				DateTime? expirationDate = val5.ExpirationDate;
				if (expirationDate < VideoExpirationDate)
				{
					VideoExpirationDate = expirationDate.Value;
				}
			}
		}
		VideoOffers = list2;
	}

	public int CalculateTotalPoints(bool isRental, bool isStream, bool isHD)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		int num = 0;
		if (m_videoOfferCollection != null)
		{
			foreach (VideoOffer item in m_videoOfferCollection.Items)
			{
				VideoOffer val = item;
				if (val.IsRental == isRental && val.IsStream == isStream && val.IsHD == isHD && !((Offer)val).InCollection && !((Offer)val).PreviouslyPurchased)
				{
					num += ((Offer)val).PriceInfo.PointsPrice;
				}
			}
		}
		return num;
	}

	private void CalculateTotal()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		int num = -1;
		bool flag = false;
		double num2 = -1.0;
		string displayPointsPrice = null;
		string text = null;
		string text2 = null;
		if (AlbumOffers != null)
		{
			foreach (AlbumOffer albumOffer in AlbumOffers)
			{
				AlbumOffer val = albumOffer;
				if (!((Offer)val).InCollection && !((Offer)val).PreviouslyPurchased)
				{
					if (num < 0)
					{
						num = 0;
					}
					num += ((Offer)val).PriceInfo.PointsPrice;
				}
			}
		}
		if (TrackOffers != null)
		{
			foreach (TrackOffer trackOffer in TrackOffers)
			{
				TrackOffer val2 = trackOffer;
				if (!((Offer)val2).InCollection && !((Offer)val2).PreviouslyPurchased)
				{
					if (num < 0)
					{
						num = 0;
					}
					num += ((Offer)val2).PriceInfo.PointsPrice;
					flag |= val2.SubscriptionFree;
				}
			}
		}
		if (VideoOffers != null)
		{
			foreach (VideoOffer videoOffer in VideoOffers)
			{
				VideoOffer val3 = videoOffer;
				if (!((Offer)val3).InCollection && !((Offer)val3).PreviouslyPurchased)
				{
					if (num < 0)
					{
						num = 0;
					}
					num += ((Offer)val3).PriceInfo.PointsPrice;
				}
			}
		}
		if (AppOffers != null)
		{
			foreach (AppOffer appOffer in AppOffers)
			{
				AppOffer val4 = appOffer;
				if (!((Offer)val4).InCollection && !((Offer)val4).PreviouslyPurchased)
				{
					if (num2 < 0.0)
					{
						text = ((Offer)val4).PriceInfo.DisplayPrice;
						num2 = 0.0;
					}
					else
					{
						text = null;
					}
					num2 += ((Offer)val4).PriceInfo.CurrencyPrice;
					if (text2 == null || text2.Equals(((Offer)val4).PriceInfo.CurrencyCode, StringComparison.InvariantCultureIgnoreCase))
					{
						text2 = ((Offer)val4).PriceInfo.CurrencyCode;
					}
				}
			}
		}
		if (num > -1)
		{
			displayPointsPrice = string.Format(Shell.LoadString(StringId.IDS_POINTS_TOTAL_FORMAT), num);
		}
		if (num2 > -1.0 && text == null)
		{
			text = StringFormatHelper.FormatPrice(num2, text2);
		}
		if (num2 > 0.0 && !string.IsNullOrEmpty(text))
		{
			string taxString = FeatureEnablement.GetTaxString();
			if (!string.IsNullOrEmpty(taxString))
			{
				text = string.Format(Shell.LoadString(StringId.IDS_CURRENCY_WITH_TAX), text, taxString);
			}
		}
		TotalPoints = num;
		TotalCurrencyPrice = num2;
		DisplayPointsPrice = displayPointsPrice;
		DisplayCurrencyPrice = text;
		HasSubscriptionFreeTracks = flag;
	}

	private void DeferredSetError(object args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetError((HRESULT)args);
	}

	private void DeferredGetBalancesComplete(object arg)
	{
		int[] array = (int[])arg;
		m_fRequestingBalances = false;
		PointsBalance = array[0];
		SubscriptionFreeTrackBalance = array[1];
		IsBalanceUpdated = true;
		UpdateState();
	}

	private void DeferredGetBalancesError(object args)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		m_fRequestingBalances = false;
		IsBalanceUpdated = false;
		SetError((HRESULT)args);
	}

	private void DeferredGetPointsOffersComplete(object args)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		m_fRequestingPointsOffers = false;
		m_pointsOffers = (BillingOfferCollection)args;
		UpdateState();
	}

	private void DeferredGetPointsOffersError(object args)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		m_fRequestingPointsOffers = false;
		SetError((HRESULT)args);
	}

	private void DeferredDownloadsAllPending(object args)
	{
		PurchaseComplete = true;
		if (SubscriptionFreeTrackBalance > 0 && ((TrackOffers != null && TrackOffers.Count > 0) || (AlbumOffers != null && AlbumOffers.Count > 0)))
		{
			SignIn.Instance.UpdateSubscriptionFreeTrackBalance();
		}
		ClientConfiguration.Service.PurchaseHD = PurchaseHD;
		if (Purchase.PurchaseEvent != null)
		{
			Purchase.PurchaseEvent(this, EventArgs.Empty);
		}
	}

	private void DeferredPlayPurchasedStreams(object args)
	{
		SingletonModelItem<TransportControls>.Instance.PlayItem((VideoPlaybackTrack)args);
	}

	protected override void OnDispose(bool disposing)
	{
		((ModelItem)this).OnDispose(disposing);
		if (disposing)
		{
			if (m_albumOfferCollection != null)
			{
				m_albumOfferCollection.Dispose();
				m_albumOfferCollection = null;
			}
			if (m_trackOfferCollection != null)
			{
				m_trackOfferCollection.Dispose();
				m_trackOfferCollection = null;
			}
			if (m_videoOfferCollection != null)
			{
				m_videoOfferCollection.Dispose();
				m_videoOfferCollection = null;
			}
			if (m_appOfferCollection != null)
			{
				m_appOfferCollection.Dispose();
				m_appOfferCollection = null;
			}
			if (m_pointsHelper != null)
			{
				((ModelItem)m_pointsHelper).Dispose();
				m_pointsHelper = null;
			}
			if (m_pointsOffers != null)
			{
				m_pointsOffers.Dispose();
				m_pointsOffers = null;
			}
		}
	}
}
