using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class AccountCreationFinishStep(Wizard owner, AccountManagementWizardState state) : AccountManagementFinishStep(owner, state, Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_FINISH))
{
	private AccountCreationNextSteps _nextSteps = AccountCreationNextSteps.EditTile;

	public override string UI => "res://ZuneShellResources!AccountCreation.uix#AccountCreationFinishStep";

	internal AccountCreationNextSteps NextSteps
	{
		get
		{
			return _nextSteps;
		}
		set
		{
			if (_nextSteps != value)
			{
				_nextSteps = value;
				((ModelItem)this).FirePropertyChanged("NextSteps");
				FireNextStepChanges();
			}
		}
	}

	public bool ShowPurchaseZunePass
	{
		get
		{
			if (base.CommittSucceeded && !ShowPurchaseZunePassTrial && FeatureEnablement.IsFeatureEnabled((Features)14))
			{
				return (NextSteps & AccountCreationNextSteps.PurchaseZunePass) == AccountCreationNextSteps.PurchaseZunePass;
			}
			return false;
		}
	}

	public bool ShowPurchaseZunePassTrial
	{
		get
		{
			if (base.CommittSucceeded && FeatureEnablement.IsFeatureEnabled((Features)17))
			{
				return (NextSteps & AccountCreationNextSteps.PurchaseZunePassTrial) == AccountCreationNextSteps.PurchaseZunePassTrial;
			}
			return false;
		}
	}

	public bool ShowPurchaseEditTile
	{
		get
		{
			if (base.CommittSucceeded && FeatureEnablement.IsFeatureEnabled((Features)5))
			{
				return (NextSteps & AccountCreationNextSteps.EditTile) == AccountCreationNextSteps.EditTile;
			}
			return false;
		}
	}

	private void FireNextStepChanges()
	{
		((ModelItem)this).FirePropertyChanged("ShowPurchaseZunePassTrial");
		((ModelItem)this).FirePropertyChanged("ShowPurchaseZunePass");
		((ModelItem)this).FirePropertyChanged("ShowPurchaseEditTile");
	}

	protected override void OnActivate()
	{
		if (base.State.PassportPasswordStep.CanCreateAccount || base.State.PassportPasswordStep.IsUpgradeNeeded)
		{
			base.LoadStatus = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_STATUS_CREATION);
		}
		else
		{
			base.LoadStatus = null;
		}
		base.OnActivate();
	}

	protected override bool OnCommitChanges()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		bool flag = false;
		flag = (base.State.PassportPasswordStep.CanCreateAccount ? base.State.CreateZuneAccount() : ((!base.State.PassportPasswordStep.IsUpgradeNeeded) ? base.State.PassportPasswordStep.IsZuneAccount : base.State.UpgradeZuneAccount(includeAccountSettings: true)));
		if (flag)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredFireNextStepChanges), (object)null);
		}
		return flag;
	}

	private void DeferredFireNextStepChanges(object unused)
	{
		FireNextStepChanges();
	}
}
