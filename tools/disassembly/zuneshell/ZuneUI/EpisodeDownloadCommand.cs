using System;
using Microsoft.Iris;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Subscription;

namespace ZuneUI;

public class EpisodeDownloadCommand : DownloadCommand
{
	private DataProviderObject m_episode;

	private EItemDownloadType m_downloadType;

	private bool m_requireSignIn;

	private bool m_explicit;

	public bool RequireSignIn
	{
		get
		{
			return m_requireSignIn;
		}
		set
		{
			m_requireSignIn = value;
		}
	}

	public bool Explicit
	{
		get
		{
			return m_explicit;
		}
		set
		{
			m_explicit = value;
		}
	}

	public EpisodeDownloadCommand(IModelItem owner, DataProviderObject episode)
		: base(owner)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		m_episode = episode;
		m_downloadType = (EItemDownloadType)m_episode.GetProperty("DownloadType");
		base.TaskId = (string)m_episode.GetProperty("EnclosureUrl");
	}

	public static void DownloadEpisode(DataProviderObject episode)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		int num = -1;
		int num2 = -1;
		if (episode is SubscriptionDataProviderItem)
		{
			SubscriptionDataProviderItem val = (SubscriptionDataProviderItem)episode;
			val.SaveToLibrary();
		}
		num = (int)episode.GetProperty("SeriesId");
		num2 = (int)episode.GetProperty("LibraryId");
		DownloadEpisode(num, num2);
	}

	public static void DownloadEpisode(int seriesId, int episodeId)
	{
		try
		{
			SubscriptionManager.Instance.DownloadEpisode(seriesId, episodeId);
		}
		catch (ApplicationException)
		{
		}
	}

	protected override void OnInvoked()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Invalid comparison between Unknown and I4
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Invalid comparison between Unknown and I4
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Invalid comparison between Unknown and I4
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Invalid comparison between Unknown and I4
		EDownloadTaskState downloadState = GetDownloadState();
		if ((int)downloadState == 3 || (int)downloadState == 2 || (int)downloadState == 1 || (int)downloadState == 0)
		{
			ZuneShell.DefaultInstance.Execute("Marketplace\\Downloads\\Home", null);
		}
		else if ((int)downloadState == 6)
		{
			PodcastLibraryPage.FindInCollection((int)m_episode.GetProperty("SeriesId"), (int)m_episode.GetProperty("LibraryId"));
		}
		else if ((!m_requireSignIn || SignIn.Instance.SignedIn) && (!m_explicit || !ZuneApplication.Service.BlockExplicitContent()))
		{
			DownloadEpisode(m_episode);
			Refresh();
		}
	}

	protected override EDownloadTaskState GetDownloadState()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected I4, but got Unknown
		m_downloadType = (EItemDownloadType)m_episode.GetProperty("DownloadType");
		EItemDownloadState val = (EItemDownloadState)m_episode.GetProperty("DownloadState");
		EItemDownloadState val2 = val;
		return (EDownloadTaskState)((int)val2 switch
		{
			0 => 7, 
			2 => 2, 
			3 => 6, 
			_ => 7, 
		});
	}

	protected override string GetDownloadString(EDownloadTaskState downloadState)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Invalid comparison between Unknown and I4
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Invalid comparison between Unknown and I4
		if ((int)downloadState == 6)
		{
			if ((int)m_downloadType == 1)
			{
				return Shell.LoadString(StringId.IDS_AUTOMATIC);
			}
			return Shell.LoadString(StringId.IDS_INCOLLECTION);
		}
		return base.GetDownloadString(downloadState);
	}

	public static int ConvertDownloadStatusToInt(EItemDownloadState type)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Expected I4, but got Unknown
		return (int)type;
	}

	public static int ConvertDownloadTypeToInt(EItemDownloadType type)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Expected I4, but got Unknown
		return (int)type;
	}
}
