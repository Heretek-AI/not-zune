namespace ZuneUI;

public class PlaylistOverrideNotification : MessageNotification
{
	private static string message = Shell.LoadString(StringId.IDS_PLAYLIST_LAND_NOTIFICATION);

	public PlaylistOverrideNotification()
		: base(message, NotificationTask.EditPlaylist, NotificationState.Normal)
	{
	}
}
