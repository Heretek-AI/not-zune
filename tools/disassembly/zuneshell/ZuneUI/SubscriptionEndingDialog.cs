using System;
using Microsoft.Iris;
using UIXControls;

namespace ZuneUI;

public class SubscriptionEndingDialog : DialogHelper
{
	private BooleanChoice m_neverShowAgain;

	private Command m_subscribe;

	private Command m_delete;

	private string m_title;

	public Command Delete => m_delete;

	public Command Subscribe => m_subscribe;

	public BooleanChoice NeverShowAgain => m_neverShowAgain;

	public string Title => m_title;

	public static void Show(DateTime endDate)
	{
		if (!ZuneShell.DefaultInstance.Management.InhibitSubscriptionEndingWarning && (ZuneShell.DefaultInstance.Management.CurrentCategoryPage == null || !ZuneShell.DefaultInstance.Management.CurrentCategoryPage.IsWizard))
		{
			SubscriptionEndingDialog subscriptionEndingDialog = new SubscriptionEndingDialog(endDate);
			((DialogHelper)subscriptionEndingDialog).Show();
		}
	}

	protected SubscriptionEndingDialog(DateTime endDate)
		: base("res://ZuneShellResources!BillingOffer.uix#SubscriptionEndingDialogContentUI")
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		if (endDate >= DateTime.Today)
		{
			m_title = Shell.LoadString(StringId.IDS_BILLING_SUBSCRIPTION_ENDING_TITLE);
			((ModelItem)this).Description = string.Format(Shell.LoadString(StringId.IDS_BILLING_SUBSCRIPTION_ENDING_WARNING), endDate);
		}
		else
		{
			m_title = Shell.LoadString(StringId.IDS_BILLING_SUBSCRIPTION_ENDED_TITLE);
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_BILLING_SUBSCRIPTION_ENDED_WARNING);
		}
		m_subscribe = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_BILLING_RENEW_SUBSCRIPTION), (EventHandler)OnSubscribe);
		m_delete = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_ACCOUNT_CLEAR_SUBSCRIPTION_BUTTON), (EventHandler)OnDeleteInvoked);
		((DialogHelper)this).Cancel.Invoked += OnCancel;
		m_neverShowAgain = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_DONT_SHOW_THIS_MESSAGE_AGAIN));
		m_neverShowAgain.Value = false;
		((Choice)m_neverShowAgain).ChosenChanged += OnNeverShowAgain;
	}

	private void OnCancel(object sender, EventArgs args)
	{
		((DialogHelper)this).Hide();
	}

	private void OnNeverShowAgain(object sender, EventArgs args)
	{
		ZuneShell.DefaultInstance.Management.InhibitSubscriptionEndingWarning = m_neverShowAgain.Value;
	}

	private void OnSubscribe(object sender, EventArgs args)
	{
		ZuneShell.DefaultInstance.Execute("Settings\\Account\\PurchaseSubscription", null);
		((DialogHelper)this).Hide();
	}

	private void OnDeleteInvoked(object sender, EventArgs args)
	{
		DeleteSubscriptionDownloadsDialog.ShowDialog();
		((DialogHelper)this).Hide();
	}
}
