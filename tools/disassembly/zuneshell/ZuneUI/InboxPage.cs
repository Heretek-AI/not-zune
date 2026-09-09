using System.Collections;
using Microsoft.Iris;
using ZuneXml;

namespace ZuneUI;

public class InboxPage : LibraryPage
{
	private InboxPanel _inboxPanel;

	private MessageDetailsPanel _messageDetailsPanel;

	private Command _refreshPageCommand;

	public static readonly string InboxPageTemplate = "res://ZuneShellResources!InboxMainPanel.uix#InboxLibrary";

	public InboxPanel MainPanel => _inboxPanel;

	public MessageDetailsPanel Details => _messageDetailsPanel;

	public Command RefreshPageCommand => _refreshPageCommand;

	public InboxPage()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		base.PivotPreference = Shell.MainFrame.Social.Inbox;
		base.IsRootPage = true;
		base.UI = InboxPageTemplate;
		base.UIPath = "Social\\Inbox";
		_refreshPageCommand = new Command((IModelItemOwner)(object)this);
		_inboxPanel = new InboxPanel(this);
		_messageDetailsPanel = new MessageDetailsPanel(this, showHeaderAndFooter: true);
	}

	public override void InvokeSettings()
	{
		((Command)Shell.SettingsFrame.Settings.Account).Invoke();
	}

	public override IPageState SaveAndRelease()
	{
		if (base.NavigationArguments == null)
		{
			base.NavigationArguments = new Hashtable(1);
		}
		if (Details.SelectedItem != null)
		{
			base.NavigationArguments["MessageId"] = ((MessageRoot)(object)Details.SelectedItem).MessagingId;
			Details.SelectedItem = null;
		}
		else
		{
			base.NavigationArguments.Remove("MessageId");
		}
		_inboxPanel.Release();
		_messageDetailsPanel.Release();
		return base.SaveAndRelease();
	}
}
