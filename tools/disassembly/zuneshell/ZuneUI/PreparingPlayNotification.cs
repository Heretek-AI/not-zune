namespace ZuneUI;

public class PreparingPlayNotification : Notification
{
	public PreparingPlayNotification()
		: base(NotificationTask.PreparingPlay, NotificationState.Normal, 10000)
	{
	}
}
