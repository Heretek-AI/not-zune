using Microsoft.Iris;

namespace ZuneUI;

public class RadioPage : LibraryPage
{
	private const string _UI = "res://ZuneShellResources!RadioPanel.uix#RadioLibrary";

	private const string _UIPath = "Collection\\Radio";

	private CollectionRadioPanel m_radioPanel;

	public CollectionRadioPanel RadioPanel
	{
		get
		{
			return m_radioPanel;
		}
		set
		{
			if (value != m_radioPanel)
			{
				m_radioPanel = value;
				((ModelItem)this).FirePropertyChanged("RadioPanel");
			}
		}
	}

	public RadioPage()
	{
		base.PivotPreference = Shell.MainFrame.Collection.Radio;
		base.IsRootPage = true;
		base.UI = "res://ZuneShellResources!RadioPanel.uix#RadioLibrary";
		base.UIPath = "Collection\\Radio";
		base.TransportControlStyle = TransportControlStyle.Music;
		base.PlaybackContext = PlaybackContext.Music;
		RadioPanel = new CollectionRadioPanel(this);
	}
}
