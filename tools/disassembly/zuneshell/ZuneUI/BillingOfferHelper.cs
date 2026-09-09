using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.ErrorMapperApi;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class BillingOfferHelper : ModelItem
{
	private BillingOfferCollection m_pointsOffers;

	private BillingOfferCollection m_subscriptionOffers;

	private HRESULT m_errorCode;

	private BillingOffer m_currentSubscription;

	private BillingOffer m_renewalSubscription;

	public IList PointsOffers
	{
		get
		{
			if (m_pointsOffers == null)
			{
				return null;
			}
			return m_pointsOffers.Items;
		}
	}

	public IList Subscriptions
	{
		get
		{
			if (m_subscriptionOffers == null)
			{
				return null;
			}
			return m_subscriptionOffers.Items;
		}
	}

	public BillingOffer CurrentSubscription
	{
		get
		{
			return m_currentSubscription;
		}
		private set
		{
			if (m_currentSubscription != value)
			{
				m_currentSubscription = value;
				((ModelItem)this).FirePropertyChanged("CurrentSubscription");
			}
		}
	}

	public BillingOffer RenewalSubscription
	{
		get
		{
			return m_renewalSubscription;
		}
		private set
		{
			if (m_renewalSubscription != value)
			{
				m_renewalSubscription = value;
				((ModelItem)this).FirePropertyChanged("RenewalSubscription");
			}
		}
	}

	public HRESULT ErrorCode
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return m_errorCode;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (m_errorCode != value)
			{
				m_errorCode = value;
				((ModelItem)this).FirePropertyChanged("ErrorCode");
			}
		}
	}

	public event EventHandler PurchaseComplete;

	public event EventHandler PurchaseFailed;

	public void GetSubscriptionOffers()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002d: Expected O, but got Unknown
		ErrorCode = HRESULT._S_OK;
		ZuneApplication.Service.GetSubscriptionOffers(new GetBillingOffersCompleteCallback(OnGetSubscriptionsComplete), new GetBillingOffersErrorCallback(OnGetSubscriptionsError));
	}

	public void GetPointsOffers()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002d: Expected O, but got Unknown
		ErrorCode = HRESULT._S_OK;
		ZuneApplication.Service.GetPointsOffers(new GetBillingOffersCompleteCallback(OnGetPointsOffersComplete), new GetBillingOffersErrorCallback(OnGetPointsOffersError));
	}

	public void GetCurrentSubscription()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		//IL_0045: Expected O, but got Unknown
		ErrorCode = HRESULT._S_OK;
		if (SignIn.Instance.SignedInWithSubscription)
		{
			ulong subscriptionId = SignIn.Instance.SubscriptionId;
			ZuneApplication.Service.GetSubscriptionDetails(subscriptionId, new GetBillingOffersCompleteCallback(OnGetCurrentSubscriptionComplete), new GetBillingOffersErrorCallback(OnGetCurrentSubscriptionError));
		}
		else
		{
			CurrentSubscription = null;
		}
	}

	public void GetRenewalSubscription()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004a: Expected O, but got Unknown
		ErrorCode = HRESULT._S_OK;
		ulong subscriptionRenewalId = SignIn.Instance.SubscriptionRenewalId;
		if (SignIn.Instance.SignedInWithSubscription && subscriptionRenewalId != 0)
		{
			ZuneApplication.Service.GetSubscriptionDetails(subscriptionRenewalId, new GetBillingOffersCompleteCallback(OnGetRenewalSubscriptionComplete), new GetBillingOffersErrorCallback(OnGetRenewalSubscriptionError));
		}
		else
		{
			RenewalSubscription = null;
		}
	}

	public void Purchase(BillingOffer billingOffer, PaymentInstrument paymentInstrument)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Invalid comparison between Unknown and I4
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		ErrorCode = HRESULT._S_OK;
		if (billingOffer != null)
		{
			AsyncCompleteHandler val = null;
			val = (((int)billingOffer.OfferType != 8) ? new AsyncCompleteHandler(OnPurchaseSubscriptionComplete) : new AsyncCompleteHandler(OnPurchasePointsComplete));
			ZuneApplication.Service.PurchaseBillingOffer(billingOffer, paymentInstrument, val);
		}
	}

	public static bool IsSubscribed(BillingOffer offer)
	{
		bool result = false;
		if (offer != null && ZuneApplication.Service.IsSignedInWithSubscription())
		{
			result = SignIn.Instance.SubscriptionId == offer.Id;
		}
		return result;
	}

	public static bool IsSubscriptionChanging()
	{
		bool result = false;
		if (ZuneApplication.Service.IsSignedInWithSubscription())
		{
			result = SignIn.Instance.SubscriptionRenewalId != SignIn.Instance.SubscriptionId;
		}
		return result;
	}

	public static bool IsLightWeightError(HRESULT hr)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		if (HRESULT._NS_E_BILLING_LIGHTWEIGHT_ACCOUNT == hr)
		{
			return true;
		}
		ErrorMapperResult mappedErrorDescriptionAndUrl = ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref hr)).Int);
		if (((HRESULT)(ref HRESULT._NS_E_BILLING_LIGHTWEIGHT_ACCOUNT)).Int == mappedErrorDescriptionAndUrl.Hr)
		{
			return true;
		}
		return false;
	}

	private void SetError(HRESULT hrError)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		ErrorCode = hrError;
	}

	private void OnGetSubscriptionsComplete(BillingOfferCollection subscriptions)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredGetSubscriptionsComplete), (object)subscriptions);
	}

	private void OnGetSubscriptionsError(HRESULT hrError)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetError), (object)hrError);
	}

	private void OnGetCurrentSubscriptionComplete(BillingOfferCollection subscriptions)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		object[] array = new object[3]
		{
			(object)(EBillingOfferType)1,
			SignIn.Instance.SubscriptionRenewalId,
			subscriptions
		};
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredGetSubsciptionComplete), (object)array);
	}

	private void OnGetCurrentSubscriptionError(HRESULT hrError)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetError), (object)hrError);
		object[] array = new object[3]
		{
			(object)(EBillingOfferType)1,
			SignIn.Instance.SubscriptionRenewalId,
			null
		};
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredGetSubsciptionComplete), (object)array);
	}

	private void OnGetRenewalSubscriptionComplete(BillingOfferCollection subscriptions)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		object[] array = new object[3]
		{
			(object)(EBillingOfferType)4,
			SignIn.Instance.SubscriptionRenewalId,
			subscriptions
		};
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredGetSubsciptionComplete), (object)array);
	}

	private void OnGetRenewalSubscriptionError(HRESULT hrError)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetError), (object)hrError);
		object[] array = new object[3]
		{
			(object)(EBillingOfferType)4,
			SignIn.Instance.SubscriptionRenewalId,
			null
		};
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredGetSubsciptionComplete), (object)array);
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
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetError), (object)hrError);
	}

	private void OnPurchaseSubscriptionComplete(HRESULT hrError)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredPurchaseComplete), (object)new object[2] { hrError, true });
	}

	private void OnPurchasePointsComplete(HRESULT hrError)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredPurchaseComplete), (object)new object[2] { hrError, false });
	}

	private void DeferredGetSubscriptionsComplete(object args)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		BillingOfferCollection val = (BillingOfferCollection)args;
		if (((ModelItem)this).IsDisposed)
		{
			if (val != null)
			{
				val.Dispose();
			}
			return;
		}
		if (m_subscriptionOffers != null)
		{
			m_subscriptionOffers.Dispose();
		}
		m_subscriptionOffers = val;
		((ModelItem)this).FirePropertyChanged("Subscriptions");
	}

	private void DeferredGetSubsciptionComplete(object args)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Invalid comparison between Unknown and I4
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Invalid comparison between Unknown and I4
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		object[] array = (object[])args;
		EBillingOfferType val = (EBillingOfferType)array[0];
		ulong num = (ulong)array[1];
		object obj = array[2];
		BillingOfferCollection val2 = (BillingOfferCollection)((obj is BillingOfferCollection) ? obj : null);
		BillingOffer val3 = null;
		if (val2 != null && val2.Items != null && val2.Items.Count > 0)
		{
			object? obj2 = val2.Items[0];
			val3 = (BillingOffer)((obj2 is BillingOffer) ? obj2 : null);
		}
		if (val3 == null && SignIn.Instance.SignedInWithSubscription)
		{
			val3 = new BillingOffer(num, val, string.Empty);
		}
		if ((int)val == 1)
		{
			CurrentSubscription = val3;
		}
		else if ((int)val == 4)
		{
			RenewalSubscription = val3;
		}
	}

	private void DeferredGetPointsOffersComplete(object args)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		BillingOfferCollection val = (BillingOfferCollection)args;
		if (((ModelItem)this).IsDisposed)
		{
			if (val != null)
			{
				val.Dispose();
			}
			return;
		}
		if (m_pointsOffers != null)
		{
			m_pointsOffers.Dispose();
		}
		m_pointsOffers = val;
		((ModelItem)this).FirePropertyChanged("PointsOffers");
	}

	private void DeferredPurchaseComplete(object args)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		HRESULT error = (HRESULT)((object[])args)[0];
		bool flag = (bool)((object[])args)[1];
		if (((HRESULT)(ref error)).IsError)
		{
			SetError(error);
			if (this.PurchaseFailed != null)
			{
				this.PurchaseFailed(this, null);
			}
			((ModelItem)this).FirePropertyChanged("PurchaseFailed");
			return;
		}
		if (flag)
		{
			SignIn.Instance.RefreshAccount();
		}
		if (this.PurchaseComplete != null)
		{
			this.PurchaseComplete(this, null);
		}
		((ModelItem)this).FirePropertyChanged("PurchaseComplete");
	}

	private void DeferredSetError(object args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetError((HRESULT)args);
	}

	protected override void OnDispose(bool disposing)
	{
		((ModelItem)this).OnDispose(disposing);
		if (disposing)
		{
			if (m_subscriptionOffers != null)
			{
				m_subscriptionOffers.Dispose();
				m_subscriptionOffers = null;
			}
			if (m_pointsOffers != null)
			{
				m_pointsOffers.Dispose();
				m_pointsOffers = null;
			}
		}
	}
}
