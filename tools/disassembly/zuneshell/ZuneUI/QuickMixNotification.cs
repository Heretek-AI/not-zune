namespace ZuneUI;

public class QuickMixNotification : Notification
{
	private readonly string _title;

	private readonly string _text;

	private readonly bool _showWebHelpLink;

	public string Title => _title;

	public string Text => _text;

	public bool ShowWebHelpLink => _showWebHelpLink;

	public QuickMixNotification(string title, string text, NotificationState state, bool showWebHelpLink, int timeout)
		: base(NotificationTask.QuickMix, state, timeout)
	{
		_title = title;
		_text = text;
		_showWebHelpLink = showWebHelpLink;
	}
}
