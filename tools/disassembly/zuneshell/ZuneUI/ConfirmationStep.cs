using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class ConfirmationStep : AccountManagementStep
{
	private ArrayList _bulletItems;

	private EBillingOfferType _offerType;

	private ulong _offerId;

	public override string UI => "res://ZuneShellResources!AccountInfo.uix#ConfirmationStep";

	public EBillingOfferType OfferType
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _offerType;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_offerType = value;
			((ModelItem)this).FirePropertyChanged("OfferType");
		}
	}

	public ArrayList BulletItems
	{
		get
		{
			return _bulletItems;
		}
		set
		{
			_bulletItems = value;
			((ModelItem)this).FirePropertyChanged("BulletItems");
		}
	}

	public override bool IsEnabled
	{
		get
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Invalid comparison between Unknown and I4
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Invalid comparison between Unknown and I4
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Invalid comparison between Unknown and I4
			bool flag = base.IsEnabled;
			if (flag)
			{
				flag = base.State.IsPurchaseConfirmationNeeded && ((int)OfferType == 1 || (int)OfferType == 4 || (int)OfferType == 4096);
			}
			return flag;
		}
	}

	public ConfirmationStep(Wizard owner, AccountManagementWizardState state, bool parent)
		: base(owner, state, parent)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_CONFIRM_SUBSCRIPTION_HEADER);
		base.NextTextOverride = Shell.LoadString(StringId.IDS_BILLING_CONTINUE);
		_bulletItems = null;
		_offerType = (EBillingOfferType)0;
		_offerId = 0uL;
		base.RequireSignIn = true;
	}

	protected override void OnActivate()
	{
		if (_offerId != base.State.SelectBillingOfferStep.SelectedBillingOffer.Id || BulletItems == null)
		{
			base.ServiceActivationRequestsDone = false;
		}
		base.OnActivate();
	}

	protected override void OnStartActivationRequests(object state)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		ArrayList args = default(ArrayList);
		HRESULT subscriptionDetails = base.State.AccountManagement.GetSubscriptionDetails(base.State.SelectBillingOfferStep.SelectedBillingOffer.Id.ToString(), ref args);
		if (((HRESULT)(ref subscriptionDetails)).IsError)
		{
			args = null;
			SetError(subscriptionDetails, null);
		}
		_offerId = base.State.SelectBillingOfferStep.SelectedBillingOffer.Id;
		EndActivationRequests(args);
	}

	protected override void OnEndActivationRequests(object args)
	{
		if (args == null)
		{
			NavigateToErrorHandler();
		}
		else
		{
			BulletItems = (ArrayList)args;
		}
	}
}
