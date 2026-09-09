namespace ZuneUI;

public class SyncOverrideNotification : MessageNotification
{
	public SyncOverrideNotification()
		: this(null)
	{
	}

	public SyncOverrideNotification(string message)
		: base(message, NotificationTask.Sync, NotificationState.Normal)
	{
	}
}
