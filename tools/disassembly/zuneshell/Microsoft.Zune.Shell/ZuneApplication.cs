using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Win32;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Messaging;
using Microsoft.Zune.PerfTrace;
using Microsoft.Zune.QuickMix;
using Microsoft.Zune.Service;
using Microsoft.Zune.Subscription;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;
using UIXControls;
using ZuneUI;
using ZuneXml;

namespace Microsoft.Zune.Shell;

public class ZuneApplication
{
	private const string ResourceDllName = "ZuneShellResources.dll";

	private const int ApplicationIconResourceId = 1;

	private static ZuneLibrary _zuneLibrary;

	private static bool _desktopLocked = false;

	private static bool _phase2InitComplete;

	private static List<Hashtable> _unprocessedAppArgs;

	private static ManualResetEvent _transientTableCleanupComplete = new ManualResetEvent(initialState: false);

	private static LaunchFromShellHelper _currentShellCommand;

	private static InteropNotifications _interopNotifications;

	private static IntPtr _hWndSplashScreen;

	private static bool _dbRebuilt;

	private static AppInitializationSequencer _appInitializationSequencer;

	private static InitializationFailsafe _initializationFailsafe;

	private static QuickMixProgress _quickMixProgress;

	public static double ZuneCurrentSettingsVersion => 2.0;

	public static bool IsDesktopLocked => _desktopLocked;

	public static string DefaultCommandLineParameterSwitch => "PlayMedia";

	public static ZuneLibrary ZuneLibrary => _zuneLibrary;

	public static Service Service => Service.Instance;

	public static SetupInstallContext InstallContext
	{
		get
		{
			SetupInstallContext result = SetupInstallContext.Zune;
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Zune\\Setup");
				if (registryKey != null)
				{
					result = (SetupInstallContext)registryKey.GetValue("WindowsPhonePresent");
				}
			}
			catch (Exception)
			{
			}
			return result;
		}
	}

	public static event EventHandler Closing;

	public static void SetDesktopLockState(bool locked)
	{
		_desktopLocked = locked;
	}

	internal static IntPtr GetRenderWindow()
	{
		return Application.Window.Handle;
	}

	public static void PageLoadComplete()
	{
		_initializationFailsafe.Complete();
	}

	private static void CorePhase3Ready(int hr, bool fSuc)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		if (!fSuc)
		{
			ZuneUI.Shell.ShowErrorDialog(hr, StringId.IDS_ZUNELAUNCH_ERRORTITLE, StringId.IDS_ZUNELAUNCH_COMPONENT_ERROR);
			ConfirmCloseDialog.ShowDefault();
			return;
		}
		SingletonModelItem<WindowSnapSimulator>.Instance.Phase3Init();
		Service.Phase3Initialize();
		SignIn.Instance.Phase3Init();
		MetadataNotifications.Instance.Phase2Init();
		SingletonModelItem<UIDeviceList>.Instance.Phase2Init();
		CDAccess.Phase2Catchup();
		SingletonModelItem<TransportControls>.Instance.Phase2Init();
		SubscriptionEventsListener.Instance.StartListening();
		SoftwareUpdates.Instance.StartUp();
		_interopNotifications = new InteropNotifications();
		if (_interopNotifications != null)
		{
			_interopNotifications.ShowErrorDialog += new OnShowErrorDialogHandler(OnShowErrorDialog);
		}
		Download.Instance.Phase3Init();
		SyncControls.Instance.Phase3Init();
		PodcastCredentials.Instance.Phase2Init();
		ProxyCredentials.Instance.Phase2Init();
		Win7ShellManager.Instance.SubprocWindow(Application.Window.Handle);
		PhotoManager.Instance.SetWindowHandle(Application.Window.Handle);
		if (OSVersion.IsWin7())
		{
			SingletonModelItem<ThumbBarButtons>.Instance.Phase3Init();
			SingletonModelItem<JumpListManager>.Instance.JumpListPinUpdateRequested.Invoke();
		}
		if (!QuickMix.Instance.IsReady)
		{
			_quickMixProgress = new QuickMixProgress();
			_quickMixProgress.PropertyChanged += OnQuickMixPropertyChanged;
		}
		Telemetry.Instance.StartUpload();
		FeaturesChanged.Instance.StartUp();
		CultureHelper.CheckValidRegionAndLanguage();
		((ZuneUI.Shell)ZuneShell.DefaultInstance).ApplicationInitializationIsComplete = true;
	}

	private static void Phase2InitializationUIStage(object arg)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		_initializationFailsafe.Initialize((DeferredInvokeHandler)delegate
		{
			_appInitializationSequencer.UIReady();
		});
		ProcessAppArgs();
		Download.Instance.Phase2Init();
		if (!ZuneShell.DefaultInstance.NavigationsPending && ZuneShell.DefaultInstance.CurrentPage is StartupPage)
		{
			ZuneUI.Shell.NavigateToHomePage();
		}
		if (_dbRebuilt)
		{
			string text = ZuneLibrary.LoadStringFromResource(109u);
			string text2 = ZuneLibrary.LoadStringFromResource(110u);
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				Win32MessageBox.Show(text2, text, Win32MessageBoxType.MB_ICONHAND, null);
			}
		}
		_phase2InitComplete = true;
	}

	private static void Phase2InitializationWorker(object arg)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Win32Window.Close(_hWndSplashScreen);
		int num = default(int);
		bool flag = _zuneLibrary.Phase2Initialization(ref num);
		Application.DeferredInvoke(new DeferredInvokeHandler(Phase2InitializationUIStage), (object)new object[2] { num, flag });
		ZuneLibrary.CleanupTransientMedia();
		_transientTableCleanupComplete.Set();
		SQMLog.Log((SQMDataId)229, ((int)Application.RenderingType == 0) ? 1 : 0);
	}

	private static void Phase2Initialization(object arg)
	{
		ThreadPool.QueueUserWorkItem(Phase2InitializationWorker);
	}

	public static void ProcessMessageFromCommandLine(string strArgs)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(ProcessMessageFromCommandLineDeferred), (object)strArgs, (DeferredInvokePriority)1);
	}

	private static void ProcessMessageFromCommandLineDeferred(object args)
	{
		string arguments = (string)args;
		string[] array = SplitCommandLineArguments(arguments);
		if (array != null)
		{
			if (_unprocessedAppArgs == null)
			{
				_unprocessedAppArgs = new List<Hashtable>();
			}
			Hashtable hashtable = new Hashtable();
			CommandLineArgument[] array2 = CommandLineArgument.ParseArgs(array, DefaultCommandLineParameterSwitch);
			CommandLineArgument[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				CommandLineArgument commandLineArgument = array3[i];
				hashtable[commandLineArgument.Name] = commandLineArgument.Value;
			}
			_unprocessedAppArgs.Add(hashtable);
		}
		if (_phase2InitComplete)
		{
			ProcessAppArgs();
		}
	}

	private static void ProcessAppArgs()
	{
		if (_unprocessedAppArgs == null)
		{
			return;
		}
		if (ClientConfiguration.FUE.SettingsVersion < ZuneCurrentSettingsVersion || Fue.Instance.IsFirstLaunch)
		{
			Fue.FUECompleted += ProcessAppArgsAfterFUE;
			return;
		}
		for (int i = 0; i < _unprocessedAppArgs.Count; i++)
		{
			ProcessAppArgs(_unprocessedAppArgs[i]);
		}
		_unprocessedAppArgs = null;
	}

	private static void ProcessAppArgs(Hashtable args)
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		if (args["device"] is string text && !_phase2InitComplete)
		{
			string currentDeviceByCanonicalName = text.Trim(new char[1] { '"' });
			SyncControls.Instance.SetCurrentDeviceByCanonicalName(currentDeviceByCanonicalName);
		}
		if (args["link"] is string link && !ZuneUI.Shell.IgnoreAppNavigationsArgs)
		{
			ZuneUI.Shell.ProcessExternalLink(link);
		}
		if (args["ripcd"] is string path && !ZuneUI.Shell.IgnoreAppNavigationsArgs)
		{
			CDAccess.HandleDiskFromAutoplay(path, CDAction.Rip);
		}
		if (args["playcd"] is string path2 && !ZuneUI.Shell.IgnoreAppNavigationsArgs)
		{
			CDAccess.HandleDiskFromAutoplay(path2, CDAction.Play);
		}
		if (args["playmedia"] is string text2 && !ZuneUI.Shell.IgnoreAppNavigationsArgs)
		{
			RegisterNewFileEnumeration(new LaunchFromShellHelper("play", text2));
		}
		if (args["shellhlp_v2"] is string text3 && !ZuneUI.Shell.IgnoreAppNavigationsArgs)
		{
			string text4 = args["dataobject"] as string;
			string text5 = args["event"] as string;
			if (text4 != null && text5 != null)
			{
				RegisterNewFileEnumeration(new LaunchFromShellHelper(text3, text4, text5));
			}
		}
		if (args.Contains("refreshlicenses"))
		{
			ZuneLibrary.MarkAllDRMFilesAsNeedingLicenseRefresh();
		}
		if (args.Contains("update"))
		{
			SoftwareUpdates.Instance.InstallUpdates();
		}
		if (args.Contains("shuffleall") && !ZuneUI.Shell.IgnoreAppNavigationsArgs)
		{
			SingletonModelItem<TransportControls>.Instance.ShuffleAllRequested = true;
		}
		if (args.Contains("resumenowplaying") && !ZuneUI.Shell.IgnoreAppNavigationsArgs)
		{
			SingletonModelItem<TransportControls>.Instance.ResumeLastNowPlayingHandler();
		}
		if (args.Contains("refreshcontentandexit"))
		{
			if (!_phase2InitComplete)
			{
				HRESULT hr = HRESULT._S_OK;
				hr = ContentRefreshTask.Instance.StartContentRefresh(new AsyncCompleteHandler(OnContentRefreshTaskComplete));
				if (((HRESULT)(ref hr)).IsError)
				{
					OnContentRefreshTaskComplete(hr);
				}
			}
			else
			{
				ZuneLibrary.MarkAllDRMFilesAsNeedingLicenseRefresh();
			}
		}
		if (args["playpin"] is string pinString && !ZuneUI.Shell.IgnoreAppNavigationsArgs)
		{
			JumpListManager.PlayPin(JumpListPin.Parse(pinString));
		}
	}

	private static void OnContentRefreshTaskComplete(HRESULT hr)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredClose), (DeferredInvokePriority)0);
	}

	public static void DeferredClose(object arg)
	{
		Application.Window.Close();
	}

	private static void ProcessAppArgsAfterFUE(object sender, EventArgs unused)
	{
		Fue.FUECompleted -= ProcessAppArgsAfterFUE;
		ProcessAppArgs();
	}

	private static void RegisterNewFileEnumeration(LaunchFromShellHelper helper)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		if (_currentShellCommand != null)
		{
			_currentShellCommand.Cancel();
		}
		_currentShellCommand = helper;
		_currentShellCommand.Go(new DeferredInvokeHandler(DataObjectEnumerationComplete));
	}

	private static void DataObjectEnumerationComplete(object args)
	{
		if (args != _currentShellCommand)
		{
			return;
		}
		string taskName = _currentShellCommand.TaskName;
		if (taskName == "play" || taskName == "playasplaylist")
		{
			List<FileEntry> files = _currentShellCommand.Files;
			MediaType mediaType = FilterFiles(files);
			if (files != null && files.Count > 0 && (mediaType == MediaType.Track || mediaType == MediaType.Video))
			{
				PlaybackContext playbackContext = PlaybackContext.Music;
				if (mediaType == MediaType.Video)
				{
					playbackContext = PlaybackContext.LibraryVideo;
				}
				SingletonModelItem<TransportControls>.Instance.PlayItems(files, playbackContext);
			}
		}
		_currentShellCommand = null;
	}

	private static MediaType FilterFiles(List<FileEntry> enumeratedFiles)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected I4, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Invalid comparison between Unknown and I4
		MediaType mediaType = MediaType.Undefined;
		if (enumeratedFiles != null && enumeratedFiles.Count > 0)
		{
			mediaType = (MediaType)enumeratedFiles[0].MediaType;
			bool flag = false;
			switch (mediaType)
			{
			case MediaType.Track:
				flag = true;
				break;
			case MediaType.Video:
				if (enumeratedFiles.Count > 1)
				{
					enumeratedFiles.RemoveRange(1, enumeratedFiles.Count - 1);
				}
				break;
			default:
				mediaType = MediaType.Undefined;
				enumeratedFiles.Clear();
				break;
			}
			if (flag)
			{
				for (int num = enumeratedFiles.Count - 1; num >= 0; num--)
				{
					if ((int)enumeratedFiles[num].MediaType != (int)mediaType)
					{
						enumeratedFiles.RemoveAt(num);
					}
				}
			}
		}
		return mediaType;
	}

	private static string[] SplitCommandLineArguments(string arguments)
	{
		if (string.IsNullOrEmpty(arguments))
		{
			return null;
		}
		string[] array = null;
		Regex regex = new Regex("(\\S*?(\\\")([^\\\"])+(\\\"))|[^\\s\"]+");
		MatchCollection matchCollection = regex.Matches(arguments);
		if (matchCollection.Count > 0)
		{
			array = new string[matchCollection.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = matchCollection[i].Value;
			}
		}
		return array;
	}

	[STAThread]
	public static int Launch(string strArgs, IntPtr hWndSplashScreen)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Expected O, but got Unknown
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Expected O, but got Unknown
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Expected O, but got Unknown
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Expected O, but got Unknown
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected O, but got Unknown
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Expected O, but got Unknown
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Expected O, but got Unknown
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Expected O, but got Unknown
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Expected O, but got Unknown
		Microsoft.Zune.PerfTrace.PerfTrace.PERFTRACE_LAUNCHEVENT(Microsoft.Zune.PerfTrace.PerfTrace.LAUNCH_EVENT.IN_MANAGED_LAUNCH, 0u);
		int num = 0;
		Application.ErrorReport += new ErrorReportHandler(ErrorReportHandler);
		string[] array = null;
		array = SplitCommandLineArguments(strArgs);
		Hashtable hashtable = StandAlone.Startup(array, DefaultCommandLineParameterSwitch);
		if (hashtable != null)
		{
			_unprocessedAppArgs = new List<Hashtable>();
			_unprocessedAppArgs.Add(hashtable);
		}
		DialogHelper.DialogCancel = ZuneUI.Shell.LoadString(StringId.IDS_DIALOG_CANCEL);
		DialogHelper.DialogYes = ZuneUI.Shell.LoadString(StringId.IDS_DIALOG_YES);
		DialogHelper.DialogNo = ZuneUI.Shell.LoadString(StringId.IDS_DIALOG_NO);
		DialogHelper.DialogOk = ZuneUI.Shell.LoadString(StringId.IDS_DIALOG_OK);
		XmlDataProviders.Register();
		LibraryDataProvider.Register();
		SubscriptionDataProvider.Register();
		StaticLibraryDataProvider.Register();
		AggregateDataProviderQuery.Register();
		ZuneUI.Shell.InitializeInstance();
		Application.Name = "Zune";
		Application.Window.Caption = "Zune";
		Application.Window.SetIcon("ZuneShellResources.dll", 1);
		if (!hashtable.Contains("noshadow"))
		{
			Image[] array2 = (Image[])(object)new Image[4];
			ImageInset val = default(ImageInset);
			((ImageInset)(ref val))._002Ector(26, 0, 30, 0);
			ImageInset val2 = default(ImageInset);
			((ImageInset)(ref val2))._002Ector(0, 10, 0, 0);
			array2[0] = new Image("res://ZuneShellResources.dll!activeshadowLeft.png", val2);
			array2[1] = new Image("res://ZuneShellResources.dll!activeshadowTop.png", val);
			array2[2] = new Image("res://ZuneShellResources.dll!activeshadowRight.png", val2);
			array2[3] = new Image("res://ZuneShellResources.dll!activeshadowBottom.png", val);
			Application.Window.SetShadowEdgeImages(true, array2);
			((ImageInset)(ref val))._002Ector(23, 0, 29, 0);
			((ImageInset)(ref val2))._002Ector(0, 5, 0, 0);
			array2[0] = new Image("res://ZuneShellResources.dll!inactiveshadowLeft.png", val2);
			array2[1] = new Image("res://ZuneShellResources.dll!inactiveshadowTop.png", val);
			array2[2] = new Image("res://ZuneShellResources.dll!inactiveshadowRight.png", val2);
			array2[3] = new Image("res://ZuneShellResources.dll!inactiveshadowBottom.png", val);
			Application.Window.SetShadowEdgeImages(false, array2);
		}
		Application.Window.CloseRequested += new WindowCloseRequestedHandler(CodeDialogManager.Instance.OnWindowCloseRequested);
		CodeDialogManager.Instance.WindowCloseNotBlocked += OnWindowCloseNotBlocked;
		Application.Window.SessionConnected += new SessionConnectedHandler(OnSessionConnected);
		string text = "res://ZuneShellResources!Frame.uix#Frame";
		_hWndSplashScreen = hWndSplashScreen;
		_initializationFailsafe = new InitializationFailsafe();
		Microsoft.Zune.PerfTrace.PerfTrace.PERFTRACE_LAUNCHEVENT(Microsoft.Zune.PerfTrace.PerfTrace.LAUNCH_EVENT.REQUEST_UI_LOAD, 0u);
		Application.Window.RequestLoad(text);
		Microsoft.Zune.PerfTrace.PerfTrace.PERFTRACE_LAUNCHEVENT(Microsoft.Zune.PerfTrace.PerfTrace.LAUNCH_EVENT.REQUEST_UI_LOAD_COMPLETE, 0u);
		new CallbackOnUIThread();
		_appInitializationSequencer = new AppInitializationSequencer(new CorePhase2ReadyCallback(CorePhase3Ready));
		_zuneLibrary = new ZuneLibrary();
		num = _zuneLibrary.Initialize((string)null, ref _dbRebuilt);
		if (num == 0)
		{
			StandAlone.Run(new DeferredInvokeHandler(Phase2Initialization));
			Application.Window.CloseRequested -= new WindowCloseRequestedHandler(CodeDialogManager.Instance.OnWindowCloseRequested);
			CodeDialogManager.Instance.WindowCloseNotBlocked -= OnWindowCloseNotBlocked;
			Application.Window.SessionConnected -= new SessionConnectedHandler(OnSessionConnected);
			if (Download.IsCreated)
			{
				((ModelItem)Download.Instance).Dispose();
			}
			ZuneShell defaultInstance = ZuneShell.DefaultInstance;
			if (defaultInstance != null)
			{
				((ModelItem)defaultInstance).Dispose();
			}
			ViewTimeLogger.Instance.Shutdown();
			if (PodcastCredentials.HasInstance)
			{
				PodcastCredentials.Instance.Dispose();
			}
			if (ProxyCredentials.HasInstance)
			{
				ProxyCredentials.Instance.Dispose();
			}
			StandAlone.Shutdown();
			HttpWebRequest.Shutdown();
			WorkerQueue.ShutdownAll();
			if (ContentRefreshTask.HasInstance)
			{
				ContentRefreshTask.Instance.Dispose();
			}
			if (Service != null)
			{
				Service.Dispose();
			}
			if (ShellMessagingNotifier.HasInstance)
			{
				((ModelItem)ShellMessagingNotifier.Instance).Dispose();
			}
			if (MessagingService.HasInstance)
			{
				MessagingService.Instance.Dispose();
			}
			if (FeaturesChangedApi.HasInstance)
			{
				FeaturesChangedApi.Instance.Dispose();
			}
			((ModelItem)CDAccess.Instance).Dispose();
			((ModelItem)PlaylistManager.Instance).Dispose();
			if (_interopNotifications != null)
			{
				_interopNotifications.ShowErrorDialog -= new OnShowErrorDialogHandler(OnShowErrorDialog);
				_interopNotifications.Dispose();
				_interopNotifications = null;
			}
		}
		_zuneLibrary.Dispose();
		return num;
	}

	private static void ErrorReportHandler(Error[] errors)
	{
		int num = 0;
		string text = string.Empty;
		foreach (Error val in errors)
		{
			if (!val.Warning)
			{
				text = text + ((object)val).ToString() + "\n";
				num++;
			}
		}
		if (num > 0)
		{
			string context = $"Scripting errors encountered (Process ID) = {Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture)}\n\n{text}";
			throw new ZuneShellException("Internal Zune Shell error", context);
		}
	}

	internal static bool CanAddMedia(IList filenames, MediaType mediaType, CanAddMediaArgs args)
	{
		foreach (string filename in filenames)
		{
			if (args.Aborted)
			{
				return false;
			}
			if (CanAddMedia(filename, mediaType, args))
			{
				return true;
			}
		}
		return false;
	}

	private static bool CanAddMedia(string filename, MediaType mediaType, CanAddMediaArgs args)
	{
		try
		{
			if (Directory.Exists(filename))
			{
				return ZuneLibrary.CanAddFromFolder(filename) && (CanAddMedia(Directory.GetFiles(filename), mediaType, args) || CanAddMedia(Directory.GetDirectories(filename), mediaType, args));
			}
			return ZuneLibrary.CanAddMedia(filename, (EMediaTypes)mediaType);
		}
		catch (UnauthorizedAccessException)
		{
			return false;
		}
		catch (IOException)
		{
			return false;
		}
	}

	internal static bool AddMedia(IList filenames, MediaType mediaType)
	{
		bool flag = false;
		foreach (string filename in filenames)
		{
			flag |= AddMedia(filename, mediaType);
		}
		return flag;
	}

	private static bool AddMedia(string filename, MediaType mediaType)
	{
		bool flag = false;
		try
		{
			if (Directory.Exists(filename))
			{
				flag = AddMedia(Directory.GetFiles(filename), mediaType);
				flag |= AddMedia(Directory.GetDirectories(filename), mediaType);
			}
			else if (ZuneLibrary.CanAddMedia(filename, (EMediaTypes)mediaType))
			{
				flag = ZuneLibrary.AddMedia(filename) != -1;
			}
		}
		catch (UnauthorizedAccessException)
		{
		}
		catch (IOException)
		{
		}
		return flag;
	}

	internal static bool AddTransientMedia(string filename, MediaType mediaType, out int libraryID, out bool fFileAlreadyExists)
	{
		_transientTableCleanupComplete.WaitOne();
		return ZuneLibrary.AddTransientMedia(filename, (EMediaTypes)mediaType, ref libraryID, ref fFileAlreadyExists);
	}

	private static void OnWindowCloseNotBlocked(object sender, EventArgs args)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		bool flag = SyncControls.Instance.CurrentDeviceOverride.UIFirmwareUpdater != null && SyncControls.Instance.CurrentDeviceOverride.UIFirmwareUpdater.UpdateInProgress;
		bool flag2 = SyncControls.Instance.CurrentDeviceOverride.UIFirmwareRestorer != null && SyncControls.Instance.CurrentDeviceOverride.UIFirmwareRestorer.RestoreInProgress;
		if (CDAccess.Instance.Notification != null || CDAccess.Instance.BurnNotification != null || (Download.IsCreated && Download.Instance.Notification != null) || flag || flag2)
		{
			if ((int)Application.RenderingType == 0 && SingletonModelItem<TransportControls>.Instance.PlayingVideo)
			{
				SingletonModelItem<TransportControls>.Instance.Stop.Invoke();
			}
			string ui = "res://ZuneShellResources!ConfirmClose.uix#ConfirmCloseContentUI";
			if (flag)
			{
				ui = "res://ZuneShellResources!ConfirmClose.uix#ConfirmFirmwareUpdateCloseContentUI";
			}
			else if (flag2)
			{
				ui = "res://ZuneShellResources!ConfirmClose.uix#ConfirmFirmwareRestoreCloseContentUI";
			}
			ConfirmCloseDialog.Show(ui, delegate
			{
				ForceClose(sender, args);
			});
		}
		else
		{
			ForceClose(sender, args);
		}
	}

	private static void ForceClose(object sender, EventArgs args)
	{
		if (ZuneApplication.Closing != null)
		{
			ZuneApplication.Closing(sender, args);
		}
		Application.Window.ForceClose();
	}

	private static void OnSessionConnected(object sender, bool fIsConnected)
	{
		if (!fIsConnected)
		{
			SingletonModelItem<TransportControls>.Instance.CloseCurrentSession();
		}
	}

	public static void OnShowErrorDialog(int hr, uint uiStringId)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredShowErrorDialog), (object)new object[2] { hr, uiStringId });
	}

	public static void DeferredShowErrorDialog(object arg)
	{
		object[] array = (object[])arg;
		int hr = Convert.ToInt32(array[0]);
		StringId stringId = (StringId)Convert.ToUInt32(array[1]);
		ZuneUI.Shell.ShowErrorDialog(hr, ZuneUI.Shell.LoadString(stringId));
	}

	private static void OnQuickMixPropertyChanged(object Sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "Progress" && (double)_quickMixProgress.Progress >= 100.0)
		{
			NotificationArea.Instance.Add(new QuickMixNotification(ZuneUI.Shell.LoadString(StringId.IDS_QUICKMIX_NOTIFICATION_BOOTSTRAP_READY_TITLE), ZuneUI.Shell.LoadString(StringId.IDS_QUICKMIX_NOTIFICATION_BOOTSTRAP_READY_TEXT), NotificationState.Completed, showWebHelpLink: true, 10000));
			_quickMixProgress.PropertyChanged -= OnQuickMixPropertyChanged;
			_quickMixProgress = null;
		}
	}
}
