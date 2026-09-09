using System;
using System.ComponentModel;
using Microsoft.Iris;
using Microsoft.Zune.Subscription;

namespace ZuneUI;

public class SubscriptionEventsListener : INotifyPropertyChanged
{
	private static volatile SubscriptionEventsListener m_instance;

	private static MessageNotification _notification;

	private static object myLock = new object();

	public static SubscriptionEventsListener Instance
	{
		get
		{
			if (m_instance == null)
			{
				lock (myLock)
				{
					if (m_instance == null)
					{
						m_instance = new SubscriptionEventsListener();
					}
				}
			}
			return m_instance;
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event EventHandler SubscriptionChanged;

	private SubscriptionEventsListener()
	{
	}

	public void StartListening()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		SubscriptionManager.Instance.OnForegroundSubscriptionChanged += new SubscriptionEventHandler(OnForegroundSubscriptionChanged);
	}

	public void StopListening()
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
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Invalid comparison between Unknown and I4
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Invalid comparison between Unknown and I4
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected I4, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Invalid comparison between I4 and Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Invalid comparison between I4 and Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Invalid comparison between I4 and Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Invalid comparison between I4 and Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Invalid comparison between I4 and Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Invalid comparison between I4 and Unknown
		SubscriptonEventArguments val = (SubscriptonEventArguments)args;
		if ((int)val.Action != 3 && val.UserInitiated && ((int)val.MediaType == 18 || (int)val.MediaType == 9))
		{
			if (_notification == null)
			{
				_notification = new MessageNotification(NotificationTask.Podcast, NotificationState.Normal);
				NotificationArea.Instance.RemoveAll(NotificationTask.Podcast, NotificationState.Completed);
				NotificationArea.Instance.Add(_notification);
			}
			_notification.SubMessage = val.SubscriptionTitle;
			SubscriptionAction action = val.Action;
			switch ((int)action)
			{
			default:
				return;
			case 0:
				if (18 == (int)val.MediaType)
				{
					_notification.Message = Shell.LoadString(StringId.IDS_PODCAST_SUBSCRIBED_NOTIFICATION);
				}
				else if (9 == (int)val.MediaType)
				{
					_notification.Message = Shell.LoadString(StringId.IDS_CHANNEL_SUBSCRIBED_NOTIFICATION);
				}
				_notification.Type = NotificationState.OneShot;
				_notification = null;
				break;
			case 1:
				if (18 == (int)val.MediaType)
				{
					_notification.Message = Shell.LoadString(StringId.IDS_PODCAST_REFRESH_START_NOTIFICATION);
				}
				else if (9 == (int)val.MediaType)
				{
					_notification.Message = Shell.LoadString(StringId.IDS_CHANNEL_REFRESH_START_NOTIFICATION);
				}
				_notification.Type = NotificationState.Normal;
				break;
			case 2:
				if (18 == (int)val.MediaType)
				{
					_notification.Message = Shell.LoadString(StringId.IDS_PODCAST_REFRESH_END_NOTIFICATION);
				}
				else if (9 == (int)val.MediaType)
				{
					_notification.Message = Shell.LoadString(StringId.IDS_CHANNEL_REFRESH_END_NOTIFICATION);
				}
				_notification.Type = NotificationState.Completed;
				_notification = null;
				break;
			}
		}
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs("SubscriptionChanged"));
		}
		if (this.SubscriptionChanged != null)
		{
			this.SubscriptionChanged(this, new EventArgs());
		}
	}
}
