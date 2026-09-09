using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Subscription;

namespace ZuneUI;

public abstract class SubscriptionLibraryPage : LibraryPage
{
	private SubscriptionSeriesPanel m_seriesPanel;

	private SubscriptionEpisodePanel m_episodePanel;

	private LibraryPanel m_detailsPanel;

	private SubscriptionManager m_subscriptionManager;

	private int m_selectedSeriesId;

	private int m_selectedEpisodeId;

	private Command m_seriesClicked = new Command();

	private Command m_episodeClicked = new Command();

	protected MenuItemCommand m_markAllAsPlayed;

	protected MenuItemCommand m_markAllAsUnplayed;

	public SubscriptionSeriesPanel SeriesPanel
	{
		get
		{
			return m_seriesPanel;
		}
		set
		{
			if (value != m_seriesPanel)
			{
				m_seriesPanel = value;
				((ModelItem)this).FirePropertyChanged("SeriesPanel");
			}
		}
	}

	public SubscriptionEpisodePanel EpisodePanel
	{
		get
		{
			return m_episodePanel;
		}
		set
		{
			if (value != m_episodePanel)
			{
				m_episodePanel = value;
				((ModelItem)this).FirePropertyChanged("EpisodePanel");
			}
		}
	}

	public LibraryPanel DetailsPanel
	{
		get
		{
			return m_detailsPanel;
		}
		set
		{
			if (value != m_detailsPanel)
			{
				m_detailsPanel = value;
				((ModelItem)this).FirePropertyChanged("DetailsPanel");
			}
		}
	}

	public int SeriesState
	{
		get
		{
			int result = 1;
			object selectedItem = m_seriesPanel.SelectedItem;
			DataProviderObject val = (DataProviderObject)((selectedItem is DataProviderObject) ? selectedItem : null);
			if (val != null)
			{
				result = (int)val.GetProperty("SeriesState");
			}
			return result;
		}
		set
		{
			((ModelItem)this).FirePropertyChanged("SeriesState");
		}
	}

	public MenuItemCommand MarkAllAsPlayed => m_markAllAsPlayed;

	public MenuItemCommand MarkAllAsUnplayed => m_markAllAsUnplayed;

	public int LastSelectedSeriesIndex
	{
		get
		{
			return ClientConfiguration.Series.PodcastLastSelectedSeriesIndex;
		}
		set
		{
			ClientConfiguration.Series.PodcastLastSelectedSeriesIndex = value;
		}
	}

	protected abstract string LandUI { get; }

	protected abstract EMediaTypes SeriesMediaType { get; }

	protected abstract EListType SeriesListType { get; }

	protected abstract StringId SubscriptionErrorStringId { get; }

	public int SelectedSeriesId
	{
		get
		{
			return m_selectedSeriesId;
		}
		set
		{
			if (m_selectedSeriesId != value)
			{
				SelectedEpisodeId = -1;
				if (EpisodePanel != null)
				{
					EpisodePanel.SelectedItem = null;
					EpisodePanel.SelectedLibraryIds.Clear();
				}
				m_selectedSeriesId = value;
				((ModelItem)this).FirePropertyChanged("SelectedSeriesId");
			}
		}
	}

	public Command SeriesClicked => m_seriesClicked;

	public int SelectedEpisodeId
	{
		get
		{
			return m_selectedEpisodeId;
		}
		set
		{
			if (m_selectedEpisodeId != value)
			{
				m_selectedEpisodeId = value;
				((ModelItem)this).FirePropertyChanged("SelectedEpisodeId");
			}
		}
	}

	public Command EpisodeClicked => m_episodeClicked;

	public SubscriptionLibraryPage(bool showDevice, MediaType mediaType)
		: base(showDevice, mediaType)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		base.UI = LandUI;
		m_selectedSeriesId = -1;
		m_selectedEpisodeId = -1;
		m_subscriptionManager = SubscriptionManager.Instance;
	}

	protected override void OnNavigatedToWorker()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		if (base.NavigationArguments != null)
		{
			int num = -1;
			int num2 = -1;
			if (base.NavigationArguments.Contains("SeriesLibraryId"))
			{
				num = (int)base.NavigationArguments["SeriesLibraryId"];
			}
			if (base.NavigationArguments.Contains("EpisodeLibraryId"))
			{
				num2 = (int)base.NavigationArguments["EpisodeLibraryId"];
			}
			if (num <= 0 && num2 > 0)
			{
				num = PlaylistManager.GetFieldValue(num2, SeriesListType, 311, -1);
			}
			SelectedSeriesId = num;
			SelectedEpisodeId = num2;
			base.NavigationArguments = null;
		}
		base.OnNavigatedToWorker();
	}

	public override IPageState SaveAndRelease()
	{
		m_seriesPanel.Release();
		m_episodePanel.Release();
		m_seriesPanel.SelectedItem = null;
		m_episodePanel.SelectedItem = null;
		return base.SaveAndRelease();
	}

	public void OpenOfficialWebSite(string link)
	{
		if (string.IsNullOrEmpty(link))
		{
			return;
		}
		try
		{
			Uri.TryCreate(link, UriKind.Absolute, out Uri result);
			if (result != null && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
			{
				Process.Start(link);
			}
		}
		catch (Win32Exception)
		{
		}
	}

	public void Unsubscribe(int seriesId, bool deleteContent)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		HRESULT val = HRESULT.op_Implicit(m_subscriptionManager.Unsubscribe(seriesId, SeriesMediaType, deleteContent));
		if (((HRESULT)(ref val)).IsSuccess)
		{
			SeriesState = 1;
		}
		else
		{
			ErrorDialogInfo.Show(((HRESULT)(ref val)).Int, Shell.LoadString(SubscriptionErrorStringId));
		}
	}

	public void Resubscribe(int seriesId)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		HRESULT val = HRESULT.op_Implicit(m_subscriptionManager.Subscribe(seriesId, SeriesMediaType));
		if (((HRESULT)(ref val)).IsSuccess)
		{
			SeriesState = 0;
		}
		else
		{
			ErrorDialogInfo.Show(((HRESULT)(ref val)).Int, Shell.LoadString(SubscriptionErrorStringId));
		}
	}

	public SeriesSettings GetSeriesSettings(int seriesId)
	{
		return new SeriesSettings(m_subscriptionManager, seriesId);
	}

	public bool IsSubscribed(DataProviderObject series)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		if (series != null)
		{
			string text = (string)series.GetProperty("FeedUrl");
			if (text != null)
			{
				int num = default(int);
				m_subscriptionManager.FindByUrl(text, SeriesMediaType, ref num, ref result);
			}
		}
		return result;
	}

	public bool IsSubscribed()
	{
		object selectedItem = m_seriesPanel.SelectedItem;
		return IsSubscribed((DataProviderObject)((selectedItem is DataProviderObject) ? selectedItem : null));
	}

	public static Guid GetZuneMediaId(int seriesId, EListType listType)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		Guid result = Guid.Empty;
		if (seriesId > 0)
		{
			result = PlaylistManager.GetFieldValue(seriesId, listType, 451, Guid.Empty);
		}
		return result;
	}
}
