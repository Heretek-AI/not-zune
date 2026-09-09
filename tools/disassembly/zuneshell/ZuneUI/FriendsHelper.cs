using System;
using Microsoft.Iris;
using Microsoft.Zune.Subscription;

namespace ZuneUI;

public class FriendsHelper : ModelItem
{
	private bool m_refreshCompleted;

	public bool RefreshCompleted
	{
		get
		{
			return m_refreshCompleted;
		}
		private set
		{
			if (m_refreshCompleted != value)
			{
				m_refreshCompleted = value;
				((ModelItem)this).FirePropertyChanged("RefreshCompleted");
			}
		}
	}

	public FriendsHelper()
	{
		StartListening();
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing)
		{
			StopListening();
		}
		((ModelItem)this).OnDispose(disposing);
	}

	public void Refresh(Guid userGuid, string feedUri)
	{
		if (userGuid != Guid.Empty && SignIn.Instance.LastSignedInUserGuid == userGuid)
		{
			HRESULT val = default(HRESULT);
			((HRESULT)(ref val))._002Ector(SubscriptionManager.Instance.Refresh(SignIn.Instance.LastSignedInUserId, (EMediaTypes)97, false));
			RefreshCompleted = ((HRESULT)(ref val)).IsError;
		}
		else
		{
			RefreshCompleted = true;
		}
	}

	private void StartListening()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		SubscriptionManager.Instance.OnForegroundSubscriptionChanged += new SubscriptionEventHandler(OnForegroundSubscriptionChanged);
	}

	private void StopListening()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		SubscriptionManager.Instance.OnForegroundSubscriptionChanged -= new SubscriptionEventHandler(OnForegroundSubscriptionChanged);
	}

	private void OnForegroundSubscriptionChanged(SubscriptonEventArguments args)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredOnForegroundSubscriptionChanged), (object)args, (DeferredInvokePriority)0);
	}

	private void DeferredOnForegroundSubscriptionChanged(object args)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Invalid comparison between Unknown and I4
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Invalid comparison between Unknown and I4
		SubscriptonEventArguments val = (SubscriptonEventArguments)args;
		if ((int)val.Action == 2 && (int)val.MediaType == 97 && !val.UserInitiated)
		{
			RefreshCompleted = true;
		}
	}
}
