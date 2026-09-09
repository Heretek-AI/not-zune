using Microsoft.Zune.Service;

namespace ZuneUI;

public class RedeemCodeWizard : AccountManagementWizard
{
	private RedeemCodeFinishStep _finishStep;

	private AccountManagementErrorPage _errorStep;

	public RedeemCodeWizard()
	{
		base.State.ContactInfoStep.LightWeightOnly = true;
		_finishStep = new RedeemCodeFinishStep(this, base.State);
		_errorStep = new AccountManagementErrorPage(this, Shell.LoadString(StringId.IDS_BILLING_PREPAID_CODE_ERROR_TITLE), Shell.LoadString(StringId.IDS_BILLING_PREPAID_CODE_ERROR_DESC));
		AddPage(base.State.RedeemCodeStep);
		AddPage(base.State.ContactInfoStep);
		AddPage(_finishStep);
		AddPage(_errorStep);
	}

	protected override void OnAsyncCommitCompleted(bool success)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Invalid comparison between Unknown and I4
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Invalid comparison between Unknown and I4
		base.OnAsyncCommitCompleted(success);
		if (success)
		{
			ETokenType val = (ETokenType)((base.State.RedeemCodeStep.ConfirmedTokenDetails == null) ? (-1) : ((int)base.State.RedeemCodeStep.ConfirmedTokenDetails.TokenType));
			if ((int)val == 1)
			{
				_finishStep.ClosingMessage = string.Format(Shell.LoadString(StringId.IDS_BILLING_PREPAID_CODE_POINTS_SUCCESS), base.State.RedeemCodeStep.ConfirmedTokenDetails.OfferName);
			}
			else if ((int)val == 2)
			{
				_finishStep.ClosingMessage = string.Format(Shell.LoadString(StringId.IDS_BILLING_PREPAID_CODE_PASS_SUCCESS), base.State.RedeemCodeStep.ConfirmedTokenDetails.OfferName);
			}
			else
			{
				_finishStep.ClosingMessage = Shell.LoadString(StringId.IDS_BILLING_PREPAID_CODE_DEFAULT_SUCCESS);
			}
		}
	}
}
