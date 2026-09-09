using System.Collections;
using System.ComponentModel;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class SelectPaymentInstrumentStep : AccountManagementStep
{
	private PaymentInstrumentHelper _helper;

	private bool _loadedCreditCards;

	private CreditCard _committedCreditCard;

	public override string UI => "res://ZuneShellResources!AccountInfo.uix#SelectPaymentInstrumentStep";

	public override bool IsEnabled
	{
		get
		{
			bool flag = base.IsEnabled;
			if (flag)
			{
				flag = ((_owner.CurrentPage == this) ? _loadedCreditCards : (!_loadedCreditCards || (CreditCards != null && CreditCards.Count > 0)));
			}
			return flag;
		}
	}

	public CreditCard CommittedCreditCard
	{
		get
		{
			return _committedCreditCard;
		}
		set
		{
			if (_committedCreditCard != value)
			{
				_committedCreditCard = value;
				ResetNextTextOverride();
				((ModelItem)this).FirePropertyChanged("CommittedCreditCard");
			}
		}
	}

	public IList CreditCards => _helper.CreditCards;

	public SelectPaymentInstrumentStep(Wizard owner, AccountManagementWizardState state, bool parent)
		: base(owner, state, parent)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_BILLING_EDIT_CC_EDIT_HEADER);
		_helper = new PaymentInstrumentHelper();
		((ModelItem)_helper).PropertyChanged += HelperPropertyChanged;
		base.RequireSignIn = true;
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

	protected virtual void ResetNextTextOverride()
	{
		if (_committedCreditCard != null && base.State.SelectBillingOfferStep.SubscriptionsOnly && !base.State.IsPurchaseConfirmationNeeded)
		{
			base.NextTextOverride = Shell.LoadString(StringId.IDS_BILLING_SIGN_UP);
		}
		else if (_committedCreditCard != null && base.State.SelectBillingOfferStep.PointsOffersOnly)
		{
			base.NextTextOverride = Shell.LoadString(StringId.IDS_BILLING_BUY_BTN);
		}
		else
		{
			base.NextTextOverride = null;
		}
	}

	protected override void OnActivate()
	{
		base.OnActivate();
		if (!_loadedCreditCards && (CreditCards == null || CreditCards.Count == 0))
		{
			_helper.GetPaymentInstruments();
		}
	}

	private void HelperPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		if (((ModelItem)this).IsDisposed || _helper == null)
		{
			return;
		}
		if (args.PropertyName == "CreditCards")
		{
			_loadedCreditCards = true;
			((ModelItem)this).FirePropertyChanged("PaymentInstruments");
			((ModelItem)this).FirePropertyChanged("IsEnabled");
			if (_helper.CreditCards == null || _helper.CreditCards.Count == 0)
			{
				_owner.MoveNext();
			}
		}
		else if (args.PropertyName == "Default")
		{
			PaymentInstrument obj = _helper.Default;
			CommittedCreditCard = (CreditCard)(object)((obj is CreditCard) ? obj : null);
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
}
