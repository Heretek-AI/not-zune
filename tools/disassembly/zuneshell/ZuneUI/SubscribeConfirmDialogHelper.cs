using System;
using Microsoft.Zune.Service;
using Microsoft.Zune.Subscription;
using UIXControls;
using ZuneXml;

namespace ZuneUI;

internal class SubscribeConfirmDialogHelper
{
	private string m_feedTitle;

	private string m_feedUrl;

	private SubscribeConfirmDialogHelper(string feedTitle, string feedUrl)
	{
		m_feedUrl = feedUrl;
		if (!string.IsNullOrEmpty(feedTitle))
		{
			m_feedTitle = feedTitle;
		}
		else
		{
			m_feedTitle = feedUrl;
		}
	}

	private void ShowDialog()
	{
		MessageBox.Show(Shell.LoadString(StringId.IDS_PODCAST_CONFIRM_DIALOG_TITLE), string.Format(Shell.LoadString(StringId.IDS_PODCAST_CONFIRM_DIALOG_MESSAGE), m_feedTitle), (EventHandler)OnConfirm);
	}

	private void OnConfirm(object sender, EventArgs args)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		SubscriptionManager instance = SubscriptionManager.Instance;
		int seriesId = -1;
		bool flag = default(bool);
		if (!instance.FindByUrl(m_feedUrl, (EMediaTypes)18, ref seriesId, ref flag))
		{
			HRESULT val = HRESULT.op_Implicit(instance.Subscribe(m_feedUrl, m_feedTitle, Guid.Empty, false, (EMediaTypes)18, (ESubscriptionSource)1, ref seriesId));
			if (((HRESULT)(ref val)).IsSuccess)
			{
				string endPointUri = Service.GetEndPointUri((EServiceEndpointId)16);
				if (!string.IsNullOrEmpty(endPointUri) && !string.IsNullOrEmpty(m_feedUrl) && m_feedUrl.Length < 1024)
				{
					string requestUri = endPointUri + "/podcast";
					string requestBody = "URL=" + m_feedUrl;
					HttpWebRequest val2 = WebRequestHelper.ConstructWebPostRequest(requestUri, requestBody, (EPassportPolicyId)0, (HttpRequestCachePolicy)1, fKeepAlive: true, acceptGZipEncoding: false);
					val2.GetResponseAsync(new AsyncRequestComplete(OnRequestComplete), (object)null);
				}
			}
			else
			{
				ErrorDialogInfo.Show(((HRESULT)(ref val)).Int, Shell.LoadString(StringId.IDS_PODCAST_SUBSCRIPTION_ERROR));
			}
		}
		PodcastLibraryPage.FindInCollection(seriesId);
	}

	private void OnRequestComplete(HttpWebResponse response, object requestArgs)
	{
		_ = response.StatusCode;
		_ = 200;
	}

	public static void Show(string feedTitle, string feedUrl)
	{
		SubscribeConfirmDialogHelper subscribeConfirmDialogHelper = new SubscribeConfirmDialogHelper(feedTitle, feedUrl);
		subscribeConfirmDialogHelper.ShowDialog();
	}
}
