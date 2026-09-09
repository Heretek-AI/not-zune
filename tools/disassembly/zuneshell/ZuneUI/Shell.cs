using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Win32;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;
using UIXControls;

namespace ZuneUI;

public class Shell : ZuneShell
{
	private delegate string ToStringer(object value);

	private delegate bool TryParser(string s, out object value);

	private class FeatureEnumUIPathMapping
	{
		public readonly string Path;

		public readonly Features Feature;

		public FeatureEnumUIPathMapping(string path, Features feature)
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			Path = path;
			Feature = feature;
		}
	}

	private delegate bool ConvertArgumentsDelegate(string value, out string convertedValue);

	private delegate void SetClientContextDelegate(string value);

	private class ExternalLinkMapping
	{
		public string ExternalLinkName;

		public string NavigatePath;

		public string ParamName;

		public ConvertArgumentsDelegate ConversionFunction;

		public SetClientContextDelegate ClientContextFunction;

		public Features FeatureRequired;

		public ExternalLinkMapping(string externalLinkName, string navigatePath, string paramName, ConvertArgumentsDelegate conversionFunction, SetClientContextDelegate clientContextFunction, Features featureRequired)
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			ExternalLinkName = externalLinkName;
			NavigatePath = navigatePath;
			ParamName = paramName;
			ConversionFunction = conversionFunction;
			ClientContextFunction = clientContextFunction;
			FeatureRequired = featureRequired;
		}
	}

	private const int c_minimumWindowWidth = 734;

	private const int c_minimumWindowHeight = 500;

	private static FeatureEnumUIPathMapping[] s_FeatureEnumUIPathMap = new FeatureEnumUIPathMapping[32]
	{
		new FeatureEnumUIPathMapping("Quickplay", (Features)0),
		new FeatureEnumUIPathMapping("Social", (Features)5),
		new FeatureEnumUIPathMapping("Marketplace\\Fresh", (Features)3),
		new FeatureEnumUIPathMapping("Marketplace\\Default", (Features)2),
		new FeatureEnumUIPathMapping("Marketplace\\MusicVideos", (Features)6),
		new FeatureEnumUIPathMapping("Marketplace\\Podcasts", (Features)7),
		new FeatureEnumUIPathMapping("Marketplace\\Channels", (Features)8),
		new FeatureEnumUIPathMapping("Marketplace\\Apps", (Features)10),
		new FeatureEnumUIPathMapping("Marketplace\\Apps", (Features)11),
		new FeatureEnumUIPathMapping("Marketplace\\Videos\\Series", (Features)12),
		new FeatureEnumUIPathMapping("Marketplace\\Videos\\TrailersHome", (Features)13),
		new FeatureEnumUIPathMapping("Marketplace", (Features)2),
		new FeatureEnumUIPathMapping("Collection\\Podcasts", (Features)7),
		new FeatureEnumUIPathMapping("Collection\\Channels", (Features)8),
		new FeatureEnumUIPathMapping("", (Features)14),
		new FeatureEnumUIPathMapping("", (Features)15),
		new FeatureEnumUIPathMapping("", (Features)16),
		new FeatureEnumUIPathMapping("", (Features)17),
		new FeatureEnumUIPathMapping("", (Features)18),
		new FeatureEnumUIPathMapping("", (Features)19),
		new FeatureEnumUIPathMapping("", (Features)20),
		new FeatureEnumUIPathMapping("", (Features)21),
		new FeatureEnumUIPathMapping("", (Features)22),
		new FeatureEnumUIPathMapping("", (Features)23),
		new FeatureEnumUIPathMapping("", (Features)24),
		new FeatureEnumUIPathMapping("", (Features)25),
		new FeatureEnumUIPathMapping("", (Features)26),
		new FeatureEnumUIPathMapping("", (Features)27),
		new FeatureEnumUIPathMapping("", (Features)28),
		new FeatureEnumUIPathMapping("", (Features)29),
		new FeatureEnumUIPathMapping("", (Features)30),
		new FeatureEnumUIPathMapping("", (Features)31)
	};

	private static ExternalLinkMapping[] s_externalLinkMap = new ExternalLinkMapping[19]
	{
		new ExternalLinkMapping("trackID", "Marketplace\\Music\\Artist", "TrackId", CanonicalizeGuid, SetGuidClientContext, (Features)28),
		new ExternalLinkMapping("albumID", "Marketplace\\Music\\Artist", "AlbumId", CanonicalizeGuid, SetGuidClientContext, (Features)28),
		new ExternalLinkMapping("videoID", "Marketplace\\Music\\Artist", "VideoId", CanonicalizeGuid, SetGuidClientContext, (Features)28),
		new ExternalLinkMapping("artistID", "Marketplace\\Music\\Artist", "ArtistId", CanonicalizeGuid, SetGuidClientContext, (Features)28),
		new ExternalLinkMapping("playlistID", "Marketplace\\Music\\Playlist", "PlaylistId", CanonicalizeGuid, SetGuidClientContext, (Features)28),
		new ExternalLinkMapping("hubID", "Marketplace\\Music\\FlexHub", "HubId", null, null, (Features)28),
		new ExternalLinkMapping("podcastID", "Marketplace\\Podcasts\\Series", "PodcastId", CanonicalizeGuid, SetGuidClientContext, (Features)7),
		new ExternalLinkMapping("tvSeriesID", "Marketplace\\Videos\\Series", "SeriesId", CanonicalizeGuid, SetGuidClientContext, (Features)12),
		new ExternalLinkMapping("tvEpisodeID", "Marketplace\\Videos\\Series", "EpisodeId", CanonicalizeGuid, SetGuidClientContext, (Features)12),
		new ExternalLinkMapping("tvSpecialID", "Marketplace\\Videos\\Short", "ShortId", CanonicalizeGuid, SetGuidClientContext, (Features)4),
		new ExternalLinkMapping("movieID", "Marketplace\\Videos\\Movie", "MovieId", CanonicalizeGuid, SetGuidClientContext, (Features)4),
		new ExternalLinkMapping("channelID", "Marketplace\\Channels\\Channel", "ChannelId", CanonicalizeGuid, SetGuidClientContext, (Features)8),
		new ExternalLinkMapping("appID", "Marketplace\\Apps\\Details\\ZuneHD", "AppId", CanonicalizeGuid, SetGuidClientContext, (Features)10),
		new ExternalLinkMapping("phoneAppID", "Marketplace\\Apps\\Details\\WindowsPhone", "AppId", CanonicalizeGuid, SetGuidClientContext, (Features)11),
		new ExternalLinkMapping("cartItemID", "Marketplace\\Cart", "MessageId", null, null, (Features)28),
		new ExternalLinkMapping("messageID", "Social\\Inbox", "MessageId", null, null, (Features)5),
		new ExternalLinkMapping("profile", "Social\\Profile", "ZuneTag", IsValidZuneTag, null, (Features)5),
		new ExternalLinkMapping("myProfile", "Social\\Profile", null, null, null, (Features)5),
		new ExternalLinkMapping("purchasePass", "Settings\\Account\\PurchaseSubscription", null, null, null, (Features)14)
	};

	private Node _currentNode;

	private bool _haveDoneInitialNavigation;

	private Command _searchButton;

	private MainFrame _mainFrame;

	private SettingsFrame _settingsFrame;

	private bool _pivotMismatch;

	private string _backgroundImage;

	private bool _playSounds;

	private bool _compactModeAlwaysOnTop;

	private bool _showWhatsNew;

	private Point _compactModeWindowPosition;

	private Point _normalWindowPosition;

	private Size _normalWindowSize;

	private int _showNowPlayingBackgroundOnIdleTimeout;

	private bool _applicationInitializationIsComplete;

	private static string _sessionStartupPath;

	public Frame CurrentFrame => CurrentExperience?.Frame;

	public Experience CurrentExperience
	{
		get
		{
			if (_currentNode == null)
			{
				return null;
			}
			return _currentNode.Experience;
		}
	}

	public bool PivotMismatch
	{
		get
		{
			return _pivotMismatch;
		}
		set
		{
			if (_pivotMismatch != value)
			{
				_pivotMismatch = value;
				((ModelItem)this).FirePropertyChanged("PivotMismatch");
			}
		}
	}

	public Node CurrentNode
	{
		get
		{
			return _currentNode;
		}
		set
		{
			if (_currentNode == value)
			{
				return;
			}
			Frame currentFrame = CurrentFrame;
			Experience currentExperience = CurrentExperience;
			Node currentNode = _currentNode;
			_currentNode = value;
			if (currentNode != null)
			{
				currentNode.IsCurrent = false;
			}
			if (_currentNode != null)
			{
				_currentNode.IsCurrent = true;
				Experience currentExperience2 = CurrentExperience;
				Choice experiences = CurrentFrame.Experiences;
				int num = experiences.Options.IndexOf(currentExperience2);
				if (num != -1)
				{
					PivotMismatch = false;
					experiences.ChosenIndex = num;
					Choice nodes = currentExperience2.Nodes;
					int num2 = nodes.Options.IndexOf(_currentNode);
					if (num2 != -1)
					{
						nodes.ChosenIndex = num2;
					}
				}
				else
				{
					PivotMismatch = true;
				}
			}
			Frame currentFrame2 = CurrentFrame;
			if (currentFrame != currentFrame2)
			{
				if (currentFrame != null)
				{
					currentFrame.IsCurrent = false;
				}
				if (currentFrame2 != null)
				{
					currentFrame2.IsCurrent = true;
				}
				((ModelItem)this).FirePropertyChanged("CurrentFrame");
			}
			Experience currentExperience3 = CurrentExperience;
			if (currentExperience != currentExperience3)
			{
				if (currentExperience != null)
				{
					currentExperience.IsCurrent = false;
				}
				if (currentExperience3 != null)
				{
					currentExperience3.IsCurrent = true;
				}
				((ModelItem)this).FirePropertyChanged("CurrentExperience");
			}
			((ModelItem)this).FirePropertyChanged("CurrentNode");
		}
	}

	public SettingsFrame SettingsFrameImpl
	{
		get
		{
			if (_settingsFrame == null)
			{
				_settingsFrame = new SettingsFrame((IModelItemOwner)(object)this);
			}
			return _settingsFrame;
		}
	}

	public MainFrame MainFrameImpl => _mainFrame;

	public static SettingsFrame SettingsFrame => ((Shell)ZuneShell.DefaultInstance)?.SettingsFrameImpl;

	public static MainFrame MainFrame => ((Shell)ZuneShell.DefaultInstance)?.MainFrameImpl;

	public static bool PreRelease => false;

	public static string SessionStartupPath
	{
		get
		{
			return _sessionStartupPath;
		}
		private set
		{
			_sessionStartupPath = value;
		}
	}

	public Command SearchButton => _searchButton;

	public string BackgroundImage
	{
		get
		{
			if (_backgroundImage == null)
			{
				_backgroundImage = ClientConfiguration.Shell.BackgroundImage;
			}
			return _backgroundImage;
		}
		internal set
		{
			if (_backgroundImage != value)
			{
				_backgroundImage = value;
				((ModelItem)this).FirePropertyChanged("BackgroundImage");
			}
		}
	}

	public bool AmbientAnimations => (int)Application.RenderingQuality == 1;

	public bool PlaySounds
	{
		get
		{
			return _playSounds;
		}
		internal set
		{
			if (_playSounds != value)
			{
				_playSounds = value;
				((ModelItem)this).FirePropertyChanged("PlaySounds");
			}
		}
	}

	public bool CompactModeAlwaysOnTop
	{
		get
		{
			return _compactModeAlwaysOnTop;
		}
		internal set
		{
			if (_compactModeAlwaysOnTop != value)
			{
				_compactModeAlwaysOnTop = value;
				((ModelItem)this).FirePropertyChanged("CompactModeAlwaysOnTop");
			}
		}
	}

	public int ShowNowPlayingBackgroundOnIdleTimeout
	{
		get
		{
			return _showNowPlayingBackgroundOnIdleTimeout;
		}
		set
		{
			if (_showNowPlayingBackgroundOnIdleTimeout != value)
			{
				_showNowPlayingBackgroundOnIdleTimeout = value;
				((ModelItem)this).FirePropertyChanged("ShowNowPlayingBackgroundOnIdleTimeout");
			}
		}
	}

	public bool ShowWhatsNew
	{
		get
		{
			if (_showWhatsNew)
			{
				return FeatureEnablement.IsFeatureEnabled((Features)16);
			}
			return false;
		}
		set
		{
			if (_showWhatsNew != value)
			{
				_showWhatsNew = value;
				((ModelItem)this).FirePropertyChanged("ShowWhatsNew");
			}
		}
	}

	public Point CompactModeWindowPosition
	{
		get
		{
			return _compactModeWindowPosition;
		}
		set
		{
			if (_compactModeWindowPosition != value)
			{
				_compactModeWindowPosition = value;
				((ModelItem)this).FirePropertyChanged("CompactModeWindowPosition");
				SaveCompactModeWindowPosition();
			}
		}
	}

	public Point NormalWindowPosition
	{
		get
		{
			return _normalWindowPosition;
		}
		set
		{
			if (_normalWindowPosition != value)
			{
				_normalWindowPosition = value;
				((ModelItem)this).FirePropertyChanged("NormalWindowPosition");
				SaveNormalWindowPositionAndSize();
			}
		}
	}

	public Size NormalWindowSize
	{
		get
		{
			return _normalWindowSize;
		}
		set
		{
			if (_normalWindowSize != value)
			{
				_normalWindowSize = value;
				((ModelItem)this).FirePropertyChanged("NormalWindowSize");
				SaveNormalWindowPositionAndSize();
			}
		}
	}

	public bool ApplicationInitializationIsComplete
	{
		get
		{
			return _applicationInitializationIsComplete;
		}
		set
		{
			if (value && !_applicationInitializationIsComplete)
			{
				_applicationInitializationIsComplete = value;
				((ModelItem)this).FirePropertyChanged("ApplicationInitializationIsComplete");
			}
		}
	}

	public static string SettingsRegistryPath => "HKEY_CURRENT_USER\\Software\\Microsoft\\Zune\\Shell";

	private static string LibraryTemplate => "res://ZuneShellResources!Library.uix#Library";

	private static string SearchTemplate => "res://ZuneShellResources!Search.uix#Search";

	private static string regNormalWindowPosition => "NormalWindowPosition";

	private static string regCompactModeWindowPosition => "CompactModeWindowPosition";

	public static int MinimumWindowWidth => 734;

	public static int MinimumWindowHeight => 500;

	public static bool IgnoreAppNavigationsArgs { get; set; }

	public Shell()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		_mainFrame = new MainFrame((IModelItemOwner)(object)this);
		_searchButton = new Command((IModelItemOwner)(object)this, (EventHandler)OnSearchButtonClicked);
		_playSounds = ClientConfiguration.Shell.Sounds;
		_compactModeAlwaysOnTop = ClientConfiguration.GeneralSettings.CompactModeAlwaysOnTop;
		_showNowPlayingBackgroundOnIdleTimeout = ClientConfiguration.Shell.ShowNowPlayingBackgroundOnIdleTimeout;
		_showWhatsNew = ClientConfiguration.Shell.ShowWhatsNew;
		ReadNormalWindowPositionAndSize();
		ReadCompactModeWindowPosition();
	}

	public static void InitializeInstance()
	{
		new Shell();
	}

	public static void NavigateToHomePage()
	{
		try
		{
			string text = ClientConfiguration.Shell.StartupPage;
			if (!IsUIPathEnabled(text))
			{
				text = MainFrame.Collection.DefaultUIPath;
			}
			SessionStartupPath = text;
			ZuneShell.DefaultInstance.Execute(SessionStartupPath, null);
		}
		catch (ArgumentException)
		{
			SessionStartupPath = MainFrame.Collection.DefaultUIPath;
			ZuneShell.DefaultInstance.Execute(SessionStartupPath, null);
		}
	}

	public unsafe static SecureString MakeSecureString(string value, bool readOnly)
	{
		SecureString secureString;
		if (string.IsNullOrEmpty(value))
		{
			secureString = new SecureString();
		}
		else
		{
			fixed (char* value2 = value.ToCharArray())
			{
				secureString = new SecureString(value2, value.Length);
			}
		}
		if (readOnly)
		{
			secureString.MakeReadOnly();
		}
		return secureString;
	}

	protected override void OnPropertyChanged(string property)
	{
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		DeferredInvokeHandler val = null;
		if (property == "CurrentPage")
		{
			UpdatePivots();
		}
		else if (property == "CommandHandler" && !_haveDoneInitialNavigation)
		{
			if (ClientConfiguration.FUE.SettingsVersion < ZuneApplication.ZuneCurrentSettingsVersion || Fue.Instance.IsFirstLaunch)
			{
				Command val2 = (Command)(object)((!FeatureEnablement.IsFeatureEnabled((Features)0)) ? ((Experience)MainFrame.Collection) : ((Experience)MainFrame.Quickplay));
				val2.Invoke((InvokePolicy)0);
				ClientConfiguration.Devices.CurrentDeviceID = 0;
				if ((int)Application.RenderingType == 0)
				{
					NavigateToPage(new GDILandPage());
				}
				else
				{
					NavigateToPage(new FirstLaunchLandPage());
				}
			}
			else if ((int)Application.RenderingType != 0 && ClientConfiguration.GeneralSettings.ReevaluateVideoSettings)
			{
				if (val == null)
				{
					val = (DeferredInvokeHandler)delegate
					{
						GdiToD3DRenderPrompt();
					};
				}
				Application.DeferredInvoke(val, new TimeSpan(0, 0, 3));
			}
			_haveDoneInitialNavigation = true;
		}
		((ModelItem)this).OnPropertyChanged(property);
	}

	private void GdiToD3DRenderPrompt()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		Win32MessageBox.Show(LoadString(StringId.IDS_RENDER_PROMPT_AFTER_D3D_SWITCH), LoadString(StringId.IDS_RENDER_PROMPT_CAPTION), (Win32MessageBoxType)36, (DeferredInvokeHandler)delegate(object args)
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Expected O, but got Unknown
			int num = (int)args;
			ClientConfiguration.GeneralSettings.ReevaluateVideoSettings = false;
			if (num == 7)
			{
				ClientConfiguration.GeneralSettings.RenderingType = 0;
				Win32MessageBox.Show(LoadString(StringId.IDS_RENDER_PROMPT_RESTART), LoadString(StringId.IDS_RENDER_PROMPT_CAPTION), Win32MessageBoxType.MB_ICONASTERISK, (DeferredInvokeHandler)delegate
				{
					Application.Window.Close();
				});
			}
		});
	}

	private void UpdatePivots()
	{
		if (_mainFrame != null)
		{
			ZunePage currentPage = base.CurrentPage;
			_ = TraceSwitches.ShellSwitch.TraceVerbose;
			if (currentPage.PivotPreference != null)
			{
				CurrentNode = currentPage.PivotPreference;
			}
			else
			{
				currentPage.PivotPreference = CurrentNode;
			}
		}
	}

	private void OnSearchButtonClicked(object sender, EventArgs e)
	{
		if (base.CurrentPage.UI != SearchTemplate)
		{
			ZunePage zunePage = new ZunePage();
			zunePage.UI = SearchTemplate;
			zunePage.TakeFocusOnNavigate = false;
			Node pivotPreference = base.CurrentPage.PivotPreference;
			SearchResultContextType usersContextType = SearchResultContextType.Undefined;
			if (pivotPreference == MainFrame.Collection.Music || pivotPreference == MainFrame.Marketplace.Music)
			{
				usersContextType = SearchResultContextType.Music;
			}
			else if (pivotPreference == MainFrame.Collection.Videos || pivotPreference == MainFrame.Marketplace.Videos)
			{
				usersContextType = SearchResultContextType.Video;
			}
			else if (pivotPreference == MainFrame.Collection.Podcasts || pivotPreference == MainFrame.Marketplace.Podcasts)
			{
				usersContextType = SearchResultContextType.Podcast;
			}
			else if (pivotPreference == MainFrame.Collection.Channels || pivotPreference == MainFrame.Marketplace.Channels)
			{
				usersContextType = SearchResultContextType.Channel;
			}
			else if (CurrentFrame.Experiences.ChosenValue == MainFrame.Social)
			{
				usersContextType = SearchResultContextType.Social;
			}
			else if (pivotPreference == MainFrame.Marketplace.Apps)
			{
				usersContextType = SearchResultContextType.App;
			}
			Search.Instance.UsersContextType = usersContextType;
			NavigateToPage(zunePage);
		}
	}

	public static WindowColor WindowColorFromRGB(int rgb)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		if (!ClientConfiguration.Shell.StartupPage.ToLower().Contains("quickplay"))
		{
			if (((rgb >> 24) & 0xFF) != 0)
			{
				return new WindowColor((rgb >> 16) & 0xFF, (rgb >> 8) & 0xFF, rgb & 0xFF);
			}
			return new WindowColor(243, 239, 241);
		}
		return new WindowColor(17, 9, 15);
	}

	public static int WindowColorToRGB(WindowColor color)
	{
		return -16777216 | (color.R << 16) | (color.G << 8) | color.B;
	}

	public static void ShowErrorDialog(int hr, string title)
	{
		ErrorDialogInfo.Show(hr, title);
	}

	public static void ShowErrorDialog(int hr, eErrorCondition condition, string title)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		ErrorDialogInfo.Show(hr, condition, title);
	}

	public static void ShowErrorDialog(int hr, StringId stringId)
	{
		ShowErrorDialog(hr, LoadString(stringId));
	}

	public static void ShowErrorDialog(int hr, eErrorCondition condition, StringId stringId)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		ShowErrorDialog(hr, condition, LoadString(stringId));
	}

	public static void ShowErrorDialog(int hr, StringId titleId, StringId descriptionId)
	{
		ErrorDialogInfo.Show(hr, LoadString(titleId), LoadString(descriptionId));
	}

	public static string LoadString(StringId stringId)
	{
		if (PhoneBrandingStringMap.Instance.BrandingEnabled)
		{
			stringId = PhoneBrandingStringMap.Instance.TryGetMappedStringId(stringId);
		}
		else if (KinBrandingStringMap.Instance.BrandingEnabled)
		{
			stringId = KinBrandingStringMap.Instance.TryGetMappedStringId(stringId);
		}
		return ZuneLibrary.LoadStringFromResource((uint)stringId);
	}

	public static string LoadString(string stringIdString)
	{
		string result = string.Empty;
		if (!string.IsNullOrEmpty(stringIdString))
		{
			try
			{
				StringId stringId = (StringId)System.Enum.Parse(typeof(StringId), stringIdString, ignoreCase: true);
				result = LoadString(stringId);
			}
			catch (ArgumentException)
			{
			}
		}
		return result;
	}

	public static string TimeSpanToString(TimeSpan time, bool prefixWithNegative)
	{
		string text = ((!prefixWithNegative) ? "" : CultureInfo.CurrentCulture.NumberFormat.NegativeSign);
		if (time.Hours != 0)
		{
			return string.Format("{4}{1}{0}{2:00}{0}{3:00}", new object[5]
			{
				CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator,
				time.Hours,
				time.Minutes,
				time.Seconds,
				text
			});
		}
		return string.Format("{3}{1:0}{0}{2:00}", new object[4]
		{
			CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator,
			time.Minutes,
			time.Seconds,
			text
		});
	}

	public static string TimeSpanToString(TimeSpan time)
	{
		return TimeSpanToString(time, prefixWithNegative: false);
	}

	public static void DeleteMedia(IList mediaList, bool deleteFileOnDisk)
	{
		ArrayList tempMediaList = new ArrayList(mediaList);
		ThreadPool.QueueUserWorkItem(delegate
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Expected O, but got Unknown
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			DeleteFromLibraryEventArgs e = new DeleteFromLibraryEventArgs();
			e.DeleteFromDisk = deleteFileOnDisk;
			LibraryDataProvider.ActOnItems((IList)tempMediaList, (BulkItemAction)0, (EventArgs)(object)e);
			foreach (IDatabaseMedia item in tempMediaList)
			{
				IDatabaseMedia val = item;
				LibraryDataProviderListItem val2 = (LibraryDataProviderListItem)(object)((val is LibraryDataProviderListItem) ? val : null);
				if (val2 != null && ((DataProviderObject)val2).TypeName == "MediaFolder")
				{
					Management management = ZuneShell.DefaultInstance.Management;
					management.RemoveMonitoredFolder(management.MonitoredPhotoFolders, (string)((DataProviderObject)val2).GetProperty("FolderPath"), commit: true);
				}
			}
		}, null);
	}

	public static void ProcessExternalLink(string link)
	{
		if (string.IsNullOrEmpty(link))
		{
			return;
		}
		if (string.Compare(link, "zune://refreshAccount/", ignoreCase: true) == 0)
		{
			SignIn.Instance.RefreshAccount();
			return;
		}
		Regex regex = new Regex("^zune:(\\/\\/)?(?<action>\\w+)(\\/)?\\?(?<param1>[^=]+)=?(?<value1>.*)$");
		Match match = regex.Match(link);
		if (!match.Success)
		{
			return;
		}
		string value = match.Groups["action"].Value;
		if (string.Compare(value, "subscribe", ignoreCase: true) == 0)
		{
			string value2 = match.Groups["param1"].Value;
			string value3 = match.Groups["value1"].Value;
			if (value3 != null && value3.Length < 32766)
			{
				value3 = Uri.EscapeUriString(value3);
				SubscribeConfirmDialogHelper.Show(value2, value3);
			}
			else
			{
				ErrorDialogInfo.Show(-1072884971, LoadString(StringId.IDS_PODCAST_SUBSCRIPTION_ERROR));
			}
		}
		else if (string.Compare(value, "navigate", ignoreCase: true) == 0)
		{
			string value4 = match.Groups["param1"].Value;
			string value5 = match.Groups["value1"]?.Value;
			ProtocolHandlerNavigate(value4, value5);
		}
	}

	public static void ProtocolHandlerNavigate(string typeId, string value)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Invalid comparison between Unknown and I4
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected I4, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Invalid comparison between Unknown and I4
		bool flag = false;
		ExternalLinkMapping[] array = s_externalLinkMap;
		foreach (ExternalLinkMapping externalLinkMapping in array)
		{
			if (!string.Equals(externalLinkMapping.ExternalLinkName, typeId, StringComparison.OrdinalIgnoreCase))
			{
				continue;
			}
			if ((int)externalLinkMapping.FeatureRequired == 32 || FeatureEnablement.IsFeatureEnabled(externalLinkMapping.FeatureRequired))
			{
				string convertedValue = value;
				if (externalLinkMapping.ConversionFunction != null && !externalLinkMapping.ConversionFunction(value, out convertedValue))
				{
					break;
				}
				Hashtable hashtable = null;
				if (externalLinkMapping.ParamName != null)
				{
					hashtable = new Hashtable();
					hashtable[externalLinkMapping.ParamName] = convertedValue;
					hashtable["IsDeepLink"] = true;
				}
				ZuneShell.DefaultInstance.Execute(externalLinkMapping.NavigatePath, hashtable);
				if (externalLinkMapping.ClientContextFunction != null)
				{
					externalLinkMapping.ClientContextFunction(convertedValue);
				}
			}
			else
			{
				string empty = string.Empty;
				Features featureRequired = externalLinkMapping.FeatureRequired;
				switch (featureRequired - 4)
				{
				default:
					if ((int)featureRequired == 28)
					{
						empty = LoadString(StringId.IDS_EXTERNAL_LINK_BLOCKED_MUSIC);
						break;
					}
					goto case 1;
				case 3:
					empty = LoadString(StringId.IDS_EXTERNAL_LINK_BLOCKED_PODCAST);
					break;
				case 0:
					empty = LoadString(StringId.IDS_EXTERNAL_LINK_BLOCKED_VIDEO);
					break;
				case 8:
					empty = LoadString(StringId.IDS_EXTERNAL_LINK_BLOCKED_TV);
					break;
				case 4:
					empty = LoadString(StringId.IDS_EXTERNAL_LINK_BLOCKED_CHANNEL);
					break;
				case 6:
					empty = LoadString(StringId.IDS_EXTERNAL_LINK_BLOCKED_APPS);
					break;
				case 7:
					empty = LoadString(StringId.IDS_EXTERNAL_LINK_BLOCKED_APPS);
					break;
				case 10:
					empty = LoadString(StringId.IDS_EXTERNAL_LINK_BLOCKED_SUBSCRIPTION);
					break;
				case 1:
				case 2:
				case 5:
				case 9:
					empty = LoadString(StringId.IDS_EXTERNAL_LINK_BLOCKED_GENERAL);
					break;
				}
				MessageBox.Show(LoadString(StringId.IDS_EXTERNAL_LINK_BLOCKED_HEADER), empty, (EventHandler)null);
			}
			flag = true;
			break;
		}
	}

	private static bool CanonicalizeGuid(string value, out string canonicalValue)
	{
		canonicalValue = null;
		bool result = false;
		try
		{
			canonicalValue = new Guid(value).ToString();
			result = true;
		}
		catch (Exception)
		{
		}
		return result;
	}

	private static void SetGuidClientContext(string value)
	{
		Guid empty = Guid.Empty;
		try
		{
			empty = new Guid(value);
		}
		catch (FormatException)
		{
			empty = Guid.Empty;
		}
		Download.Instance.ReportClientContextEvent((EDownloadContextEvent)20, empty);
	}

	private static bool IsValidZuneTag(string value, out string canonicalValue)
	{
		canonicalValue = null;
		bool flag = true;
		foreach (char c in value)
		{
			if (!char.IsLetterOrDigit(c) && c != ' ')
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			canonicalValue = value;
		}
		return flag;
	}

	public static string ReformatFolderPathName(string path)
	{
		try
		{
			FileInfo fileInfo = new FileInfo(path);
			string format = LoadString(StringId.IDS_MEDIASTORE_TRACK_REFORMATTING);
			string name = fileInfo.Name;
			if (string.IsNullOrEmpty(name))
			{
				name = path;
			}
			return string.Format(format, fileInfo.Name, path);
		}
		catch (Exception)
		{
			return path;
		}
	}

	public static void OpenFolderAndSelectItems(string filePath)
	{
		ShellInterop.OpenFolderAndSelectItem(filePath);
	}

	public static void SaveInt(string keyName, int value)
	{
		Registry.SetValue(SettingsRegistryPath, keyName, value);
	}

	public static int GetInt(string keyName, int min, int max, int defaultValue)
	{
		if (string.IsNullOrEmpty(keyName))
		{
			return defaultValue;
		}
		if (!(Registry.GetValue(SettingsRegistryPath, keyName, defaultValue) is int num))
		{
			return defaultValue;
		}
		if (num < min || num > max)
		{
			return defaultValue;
		}
		return num;
	}

	private static void SaveList(string keyName, IList values, ToStringer toString)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (object value in values)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(';');
			}
			stringBuilder.Append(toString(value));
		}
		Registry.SetValue(SettingsRegistryPath, keyName, stringBuilder.ToString());
	}

	public static void SaveIntList(string keyName, IList values)
	{
		SaveList(keyName, values, (object value) => ((int)value).ToString(NumberFormatInfo.InvariantInfo));
	}

	public static void SaveFloatList(string keyName, IList values)
	{
		SaveList(keyName, values, (object value) => ((float)value).ToString(NumberFormatInfo.InvariantInfo));
	}

	private static IList GetList(string keyName, int expectedCount, TryParser tryParse)
	{
		string text = Registry.GetValue(SettingsRegistryPath, keyName, null) as string;
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		string[] array = text.Split(new char[1] { ';' });
		if (array.Length != expectedCount)
		{
			return null;
		}
		ArrayList arrayList = new ArrayList(expectedCount);
		for (int i = 0; i < expectedCount; i++)
		{
			if (!tryParse(array[i], out var value))
			{
				return null;
			}
			arrayList.Add(value);
		}
		return arrayList;
	}

	public static IList GetIntList(string keyName, int expectedCount)
	{
		return GetList(keyName, expectedCount, delegate(string s, out object value)
		{
			int result2;
			bool result = int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result2);
			value = result2;
			return result;
		});
	}

	public static IList GetPositiveIntList(string keyName, int expectedCount)
	{
		IList list = GetIntList(keyName, expectedCount);
		if (list != null)
		{
			foreach (int item in list)
			{
				if (item <= 0)
				{
					list = null;
					break;
				}
			}
		}
		return list;
	}

	public static IList GetReorderedIntList(string keyName, int expectedCount)
	{
		IList list = GetIntList(keyName, expectedCount);
		if (list != null)
		{
			BitArray bitArray = new BitArray(expectedCount);
			foreach (int item in list)
			{
				if (item < 0 || item >= expectedCount || bitArray[item])
				{
					list = null;
					break;
				}
				bitArray[item] = true;
			}
		}
		return list;
	}

	public static IList GetFloatList(string keyName, int expectedCount)
	{
		return GetList(keyName, expectedCount, delegate(string s, out object value)
		{
			float result2;
			bool result = float.TryParse(s, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out result2);
			value = result2;
			return result;
		});
	}

	public static IList GetPositionList(string keyName, int expectedCount)
	{
		IList list = GetFloatList(keyName, expectedCount);
		if (list != null)
		{
			float num = 0f;
			foreach (float item in list)
			{
				if (item < num || item > 1f)
				{
					list = null;
					break;
				}
				num = item;
			}
		}
		return list;
	}

	private void ReadNormalWindowPositionAndSize()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		IList intList = GetIntList(regNormalWindowPosition, 4);
		if (intList != null && intList.Count == 4)
		{
			_normalWindowPosition = new Point((int)intList[0], (int)intList[1]);
			_normalWindowSize = new Size((int)intList[2], (int)intList[3]);
		}
	}

	private void SaveNormalWindowPositionAndSize()
	{
		if (_normalWindowPosition != null && _normalWindowSize != null)
		{
			SaveIntList(regNormalWindowPosition, new int[4] { _normalWindowPosition.X, _normalWindowPosition.Y, _normalWindowSize.Width, _normalWindowSize.Height });
		}
	}

	private void ReadCompactModeWindowPosition()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		IList intList = GetIntList(regCompactModeWindowPosition, 2);
		if (intList != null && intList.Count == 2)
		{
			_compactModeWindowPosition = new Point((int)intList[0], (int)intList[1]);
		}
	}

	private void SaveCompactModeWindowPosition()
	{
		if (_compactModeWindowPosition != null)
		{
			SaveIntList(regCompactModeWindowPosition, new int[2] { _compactModeWindowPosition.X, _compactModeWindowPosition.Y });
		}
	}

	public static bool IsUIPathEnabled(string path)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		bool flag = true;
		int num = 0;
		if (!string.IsNullOrEmpty(path))
		{
			FeatureEnumUIPathMapping[] array = s_FeatureEnumUIPathMap;
			foreach (FeatureEnumUIPathMapping featureEnumUIPathMapping in array)
			{
				if (path.StartsWith(featureEnumUIPathMapping.Path, StringComparison.InvariantCultureIgnoreCase) && (featureEnumUIPathMapping.Path.Length == path.Length || (featureEnumUIPathMapping.Path.Length < path.Length && path[featureEnumUIPathMapping.Path.Length] == '\\')) && featureEnumUIPathMapping.Path.Length >= num)
				{
					num = featureEnumUIPathMapping.Path.Length;
					flag = FeatureEnablement.IsFeatureEnabled(featureEnumUIPathMapping.Feature);
					if (flag)
					{
						break;
					}
				}
			}
		}
		return flag;
	}
}
