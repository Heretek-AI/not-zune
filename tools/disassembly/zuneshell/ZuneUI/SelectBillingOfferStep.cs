using System.Collections;
using System.ComponentModel;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class SelectBillingOfferStep : AccountManagementStep
{
	private EBillingOfferType _offerTypes;

	private BillingOffer _selectedBillingOffer;

	private BillingOfferHelper _helper;

	public override string UI => "res://ZuneShellResources!AccountInfo.uix#SelectBillingOfferStep";

	public override bool IsEnabled
	{
		get
		{
			bool flag = base.IsEnabled;
			if (flag)
			{
				flag = _owner.CurrentPage != this || (Subscriptions != null && SubscriptionsOnly) || (PointsOffers != null && PointsOffersOnly);
			}
			return flag;
		}
	}

	public bool TrialOnly => (int)_offerTypes == 4096;

	public bool SubscriptionsOnly
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Invalid comparison between Unknown and I4
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Invalid comparison between Unknown and I4
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Invalid comparison between Unknown and I4
			if ((int)_offerTypes != 1 && (int)_offerTypes != 4096)
			{
				return (int)_offerTypes == 4;
			}
			return true;
		}
	}

	public bool PointsOffersOnly => (int)_offerTypes == 8;

	public EBillingOfferType ShowOffers
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _offerTypes;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (_offerTypes != value)
			{
				_offerTypes = value;
				SetDescription();
				((ModelItem)this).FirePropertyChanged("ShowOffers");
				((ModelItem)this).FirePropertyChanged("TrialOnly");
				((ModelItem)this).FirePropertyChanged("SubscriptionsOnly");
				((ModelItem)this).FirePropertyChanged("PointsOffersOnly");
				((ModelItem)this).FirePropertyChanged("IsEnabled");
			}
		}
	}

	public BillingOffer TrialOffer
	{
		get
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Expected O, but got Unknown
			BillingOffer result = null;
			if (_helper.Subscriptions != null)
			{
				foreach (BillingOffer subscription in _helper.Subscriptions)
				{
					BillingOffer val = subscription;
					if (val.Trial)
					{
						result = val;
						break;
					}
				}
			}
			return result;
		}
	}

	public IList Subscriptions => _helper.Subscriptions;

	public IList PointsOffers => _helper.PointsOffers;

	public BillingOffer SelectedBillingOffer
	{
		get
		{
			return _selectedBillingOffer;
		}
		set
		{
			if (_selectedBillingOffer != value)
			{
				_selectedBillingOffer = value;
				((ModelItem)this).FirePropertyChanged("SelectedBillingOffer");
			}
		}
	}

	public SelectBillingOfferStep(Wizard owner, AccountManagementWizardState state)
		: base(owner, state, parentAccount: false)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		_helper = new BillingOfferHelper();
		((ModelItem)_helper).PropertyChanged += HelperPropertyChanged;
		_offerTypes = (EBillingOfferType)0;
		SetDescription();
		base.RequireSignIn = true;
		Initialize(null);
	}

	protected override void OnDispose(bool disposing)
	{
		((ModelItem)this).OnDispose(disposing);
		if (_helper != null && disposing)
		{
			((ModelItem)_helper).PropertyChanged -= HelperPropertyChanged;
			((ModelItem)_helper).Dispose();
			_helper = null;
		}
	}

	protected override void OnActivate()
	{
		base.OnActivate();
		if (Subscriptions == null && SubscriptionsOnly)
		{
			_helper.GetSubscriptionOffers();
		}
		else if (PointsOffers == null && PointsOffersOnly)
		{
			_helper.GetPointsOffers();
		}
	}

	private void HelperPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		if (((ModelItem)this).IsDisposed || _helper == null)
		{
			return;
		}
		if (args.PropertyName == "Subscriptions")
		{
			if (TrialOnly)
			{
				SelectedBillingOffer = TrialOffer;
				if (SelectedBillingOffer == null)
				{
					ShowOffers = (EBillingOfferType)1;
				}
			}
			((ModelItem)this).FirePropertyChanged("TrialOffer");
			((ModelItem)this).FirePropertyChanged("Subscriptions");
			((ModelItem)this).FirePropertyChanged("IsEnabled");
		}
		else if (args.PropertyName == "PointsOffers")
		{
			((ModelItem)this).FirePropertyChanged("PointsOffers");
			((ModelItem)this).FirePropertyChanged("IsEnabled");
		}
		else if (args.PropertyName == "ErrorCode")
		{
			HRESULT errorCode = _helper.ErrorCode;
			if (((HRESULT)(ref errorCode)).IsError)
			{
				SetError(_helper.ErrorCode, null);
			}
		}
	}

	private void SetDescription()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Invalid comparison between Unknown and I4
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Invalid comparison between Unknown and I4
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Invalid comparison between Unknown and I4
		if ((int)_offerTypes == 4096)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_BILLING_TRIAL_TITLE);
		}
		else if ((int)_offerTypes == 1)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_BILLING_ZUNE_PASS_HEADER);
		}
		else if ((int)_offerTypes == 8)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_BILLING_PURCHASE_POINTS_HEADER);
		}
		else
		{
			((ModelItem)this).Description = null;
		}
	}
}
