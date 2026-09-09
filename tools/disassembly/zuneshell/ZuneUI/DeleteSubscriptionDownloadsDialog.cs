using System;
using Microsoft.Iris;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using UIXControls;

namespace ZuneUI;

public class DeleteSubscriptionDownloadsDialog : DialogHelper
{
	private Command m_delete;

	private string m_title;

	private bool m_enabled;

	public Command Delete => m_delete;

	public bool Enabled
	{
		get
		{
			return m_enabled;
		}
		private set
		{
			if (m_enabled != value)
			{
				Delete.Available = value;
				((DialogHelper)this).Cancel.Available = value;
				m_enabled = value;
				((ModelItem)this).FirePropertyChanged("Enabled");
			}
		}
	}

	public string Title => m_title;

	public static void ShowDialog()
	{
		string subscriptionDirectory = ZuneApplication.Service.GetSubscriptionDirectory();
		if (string.IsNullOrEmpty(subscriptionDirectory))
		{
			MessageBox.Show(Shell.LoadString(StringId.IDS_ACCOUNT_CLEAR_SUB_FAIL_TITLE), Shell.LoadString(StringId.IDS_ACCOUNT_CLEAR_SUB_NO_DIRECTORY), (EventHandler)null);
			return;
		}
		DeleteSubscriptionDownloadsDialog deleteSubscriptionDownloadsDialog = new DeleteSubscriptionDownloadsDialog(subscriptionDirectory);
		((DialogHelper)deleteSubscriptionDownloadsDialog).Show();
	}

	protected DeleteSubscriptionDownloadsDialog(string subscriptionDirectory)
		: base("res://ZuneShellResources!ManagementAccount.uix#DeleteSubscriptionDownloadsDialogContentUI")
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		m_enabled = true;
		m_title = Shell.LoadString(StringId.IDS_ACCOUNT_CLEAR_SUBSCRIPTION_TITLE);
		((ModelItem)this).Description = string.Format(Shell.LoadString(StringId.IDS_ACCOUNT_CLEAR_SUBSCRIPTION_CONFIRM), subscriptionDirectory);
		m_delete = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_DIALOG_OK), (EventHandler)OnDeleteInvoked);
		((DialogHelper)this).Cancel.Invoked += OnCancel;
	}

	private void OnCancel(object sender, EventArgs args)
	{
		((DialogHelper)this).Hide();
	}

	private void OnDeleteInvoked(object sender, EventArgs args)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (Enabled)
		{
			Enabled = false;
			ZuneApplication.Service.DeleteSubscriptionDownloads(new AsyncCompleteHandler(OnDeleteComplete));
		}
	}

	private void OnDeleteComplete(HRESULT hr)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredDeleteCompleteEvent), (object)hr);
	}

	private void DeferredDeleteCompleteEvent(object arg)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		Enabled = true;
		HRESULT val = (HRESULT)arg;
		((DialogHelper)this).Hide();
		if (((HRESULT)(ref val)).IsError)
		{
			Shell.ShowErrorDialog(((HRESULT)(ref val)).Int, StringId.IDS_ACCOUNT_CLEAR_SUB_FAIL_TITLE, StringId.IDS_ACCOUNT_CLEAR_SUB_FAIL_MESSAGE);
		}
	}
}
