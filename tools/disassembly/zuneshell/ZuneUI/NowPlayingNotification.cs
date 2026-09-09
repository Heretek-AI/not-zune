namespace ZuneUI;

public class NowPlayingNotification : Notification
{
	public NowPlayingNotification()
		: base(NotificationTask.NowPlaying, NotificationState.Normal, 10000)
	{
	}
}
