using System;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Subscription;
using Microsoft.Zune.Util;
using UIXControls;

namespace ZuneUI;

public class QuickplayPage : ZunePage
{
	private bool _startupPage;

	private bool _userInteracted;

	private static bool _showFUE;

	private static bool _createdOnce;

	private bool _hasNavigatedAway;

	public static DateTime InvalidDate => DateTime.MinValue;

	public static bool ShowFUE => _showFUE;

	public bool UserInteracted
	{
		get
		{
			return _userInteracted;
		}
		set
		{
			_userInteracted = value;
		}
	}

	private static string LandBackgroundUI => "res://ZuneShellResources!Quickplay.uix#QuickplayBackground";

	public static int PlaylistTypeMask => 163;

	public QuickplayPage()
	{
		base.PivotPreference = Shell.MainFrame.Quickplay.Default;
		base.IsRootPage = true;
		base.UI = "res://ZuneShellResources!Quickplay.uix#Quickplay";
		base.UIPath = "Quickplay\\Default";
		base.BackgroundUI = LandBackgroundUI;
		base.ShowSearch = false;
		if (!_createdOnce)
		{
			_createdOnce = true;
			_showFUE = ClientConfiguration.Quickplay.ShowFUE;
			if (Shell.SessionStartupPath == Shell.MainFrame.Quickplay.DefaultUIPath)
			{
				SQMLog.Log((SQMDataId)162, 1);
				_startupPage = true;
			}
		}
	}

	protected override void OnNavigatedAwayWorker(IPage destination)
	{
		if (!_hasNavigatedAway && _startupPage)
		{
			if (!UserInteracted && !(destination is NowPlayingLand))
			{
				SQMLog.Log((SQMDataId)163, 1);
			}
			if (ClientConfiguration.Quickplay.CheckUseCount)
			{
				if (UserInteracted)
				{
					ClientConfiguration.Quickplay.UnusedCount = 0;
				}
				else
				{
					ZunePage zunePage = destination as ZunePage;
					string text = "";
					if (zunePage != null && zunePage.PivotPreference != null)
					{
						string text2 = "";
						if (zunePage.PivotPreference.Experience is MarketplaceExperience || zunePage.PivotPreference.Experience is SocialExperience || (zunePage.PivotPreference.Experience is CollectionExperience && !(zunePage.PivotPreference.Experience is DeviceExperience)))
						{
							text2 = zunePage.UIPath;
						}
						if (!string.IsNullOrEmpty(text2))
						{
							text = text2;
							int num = text2.IndexOf('\\');
							if (num != -1)
							{
								text = text2.Remove(num);
							}
						}
					}
					if (!string.IsNullOrEmpty(text))
					{
						if (ClientConfiguration.Quickplay.FavoredExperience == text)
						{
							QuickplayConfiguration quickplay = ClientConfiguration.Quickplay;
							quickplay.UnusedCount += 1;
						}
						else
						{
							ClientConfiguration.Quickplay.FavoredExperience = text;
							ClientConfiguration.Quickplay.UnusedCount = 1;
						}
						if (ClientConfiguration.Quickplay.UnusedCount >= ClientConfiguration.Quickplay.MaxUnusedCount)
						{
							ClientConfiguration.Quickplay.CheckUseCount = false;
							Prompt(zunePage.PivotPreference.Experience);
						}
					}
					else
					{
						ClientConfiguration.Quickplay.UnusedCount = 0;
					}
				}
			}
		}
		_hasNavigatedAway = true;
		base.OnNavigatedAwayWorker(destination);
	}

	public void Prompt(Experience destinationExperience)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		Command val = new Command((IModelItemOwner)null, Shell.LoadString(StringId.IDS_DIALOG_YES), (EventHandler)null);
		val.Invoked += delegate
		{
			ClientConfiguration.Shell.StartupPage = destinationExperience.DefaultUIPath;
		};
		string text = null;
		if (destinationExperience is CollectionExperience)
		{
			text = Shell.LoadString(StringId.IDS_QP_CHANGESTARTUPPAGE_COLLECTION);
		}
		else if (destinationExperience is MarketplaceExperience)
		{
			text = Shell.LoadString(StringId.IDS_QP_CHANGESTARTUPPAGE_MARKETPLACE);
		}
		else if (destinationExperience is SocialExperience)
		{
			text = Shell.LoadString(StringId.IDS_QP_CHANGESTARTUPPAGE_SOCIAL);
		}
		if (!string.IsNullOrEmpty(text))
		{
			MessageBox.ShowYesNo(Shell.LoadString(StringId.IDS_QP_CHANGESTARTUPPAGE_TITLE), text, val);
		}
	}

	public static void FUEComplete()
	{
		if (ShowFUE)
		{
			ClientConfiguration.Quickplay.ShowFUE = false;
		}
	}

	public static bool PlayPodcastsNewestFirst(int seriesId)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Invalid comparison between Unknown and I4
		uint podcastDefaultKeepEpisodes = (uint)ClientConfiguration.Series.PodcastDefaultKeepEpisodes;
		ESeriesPlaybackOrder val = (ESeriesPlaybackOrder)ClientConfiguration.Series.PodcastDefaultPlaybackOrder;
		SubscriptionManager.Instance.GetManagementSettings(seriesId, ref podcastDefaultKeepEpisodes, ref val);
		return (int)val == 0;
	}
}
