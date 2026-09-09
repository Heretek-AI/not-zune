using System;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class Fue : ModelItem
{
	private const int RenderPromptIntervalInitValue = 120000;

	private const int RenderPromptIntervalPostValue = 120000;

	private bool _proxyDefaultPathsComplete;

	private bool _autoFUE;

	private string errorMessage;

	private static Fue singletonInstance;

	private bool fileTypeAssociationsAreSet;

	private int _renderPromptInterval = 120000;

	private Timer _renderPromptTimer;

	public static Fue Instance
	{
		get
		{
			if (singletonInstance == null)
			{
				singletonInstance = new Fue();
			}
			return singletonInstance;
		}
	}

	public bool IsFirstLaunch
	{
		get
		{
			bool result = ClientConfiguration.FUE.ShowFirstLaunchVideo && FeatureEnablement.IsFeatureEnabled((Features)21);
			if (!ClientConfiguration.FUE.ShowFUE)
			{
				return result;
			}
			return true;
		}
	}

	public string ErrorMessage
	{
		get
		{
			return errorMessage;
		}
		set
		{
			errorMessage = value;
			Shell.ShowErrorDialog(0, errorMessage);
		}
	}

	public bool AutoFUE
	{
		get
		{
			return _autoFUE;
		}
		set
		{
			_autoFUE = value;
		}
	}

	public int RenderPromptInterval
	{
		get
		{
			return _renderPromptInterval;
		}
		set
		{
			if (_renderPromptInterval != value)
			{
				_renderPromptInterval = value;
				((ModelItem)this).FirePropertyChanged("RenderPromptInterval");
			}
		}
	}

	public Timer RenderPromptTimer
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_renderPromptTimer == null)
			{
				_renderPromptTimer = new Timer((IModelItemOwner)(object)this);
				_renderPromptTimer.Interval = RenderPromptInterval;
				_renderPromptTimer.AutoRepeat = false;
				_renderPromptTimer.Tick += RenderPromptTimeout;
			}
			return _renderPromptTimer;
		}
	}

	public static event EventHandler FUECompleted;

	private Fue()
	{
	}

	public void StartJobs()
	{
		InitializeDefaultPaths();
		UpdateNSS();
	}

	public void MigrateLegacyConfiguration()
	{
		ClientConfiguration.FUE.SettingsVersion = ZuneApplication.ZuneCurrentSettingsVersion;
	}

	public void SetFileTypeAssociationsAreSet()
	{
		fileTypeAssociationsAreSet = true;
	}

	public void InitializeQuickplayConfig()
	{
		if (FeatureEnablement.IsFeatureEnabled((Features)0))
		{
			ClientConfiguration.Shell.StartupPage = Shell.MainFrame.Quickplay.DefaultUIPath;
			ClientConfiguration.Quickplay.ShowFUE = true;
			ClientConfiguration.Quickplay.CheckUseCount = true;
			ClientConfiguration.Quickplay.UnusedCount = 0;
			ClientConfiguration.Quickplay.FavoredExperience = "";
		}
	}

	public void CompleteFUE()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Invalid comparison between Unknown and I4
		ClientConfiguration.FUE.ShowFUE = false;
		ClientConfiguration.FUE.ShowArtistChooser = FeatureEnablement.IsFeatureEnabled((Features)26) && (FeatureEnablement.IsFeatureEnabled((Features)0) || FeatureEnablement.IsFeatureEnabled((Features)3));
		InitializeQuickplayConfig();
		if (!fileTypeAssociationsAreSet)
		{
			ZuneShell.DefaultInstance.Management.SaveFileTypesAsDefault();
		}
		ZuneShell.DefaultInstance.NavigateBack();
		string[] args = new string[1] { Shell.LoadString(StringId.IDS_ZUNECLIENT_LOCALE) };
		SQMLog.LogToStream((SQMDataId)47, args);
		SQMLog.Log((SQMDataId)46, ((int)Application.RenderingType == 1) ? 1 : 0);
		ClientConfiguration.SQM.SQMLaunchIndex = 0;
		StartJobs();
		if (Fue.FUECompleted != null)
		{
			Fue.FUECompleted(Instance, EventArgs.Empty);
		}
	}

	public void CompleteMigration()
	{
		InitializeQuickplayConfig();
		ZuneShell.DefaultInstance.NavigateBack();
		if (Fue.FUECompleted != null)
		{
			Fue.FUECompleted(Instance, EventArgs.Empty);
		}
	}

	public void ProxyDefaultPaths()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (_proxyDefaultPathsComplete)
		{
			return;
		}
		Management management = ZuneShell.DefaultInstance.Management;
		string[] array = default(string[]);
		string[] array2 = default(string[]);
		string[] array3 = default(string[]);
		string[] array4 = default(string[]);
		string[] array5 = default(string[]);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		HRESULT.op_Implicit(ZuneApplication.ZuneLibrary.GetKnownFolders(ref array, ref array2, ref array3, ref array4, ref array5, ref text, ref text2, ref text3, ref text4, ref text5));
		if (ClientConfiguration.Groveler.MonitoredAudioFolders == null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				management.MonitoredAudioFolders.Add((object)array[i]);
			}
		}
		if (ClientConfiguration.Groveler.MonitoredPhotoFolders == null)
		{
			for (int j = 0; j < array3.Length; j++)
			{
				management.MonitoredPhotoFolders.Add((object)array3[j]);
			}
		}
		if (ClientConfiguration.Groveler.MonitoredVideoFolders == null)
		{
			for (int k = 0; k < array2.Length; k++)
			{
				management.MonitoredVideoFolders.Add((object)array2[k]);
			}
		}
		if (ClientConfiguration.Groveler.MonitoredPodcastFolders == null)
		{
			for (int l = 0; l < array4.Length; l++)
			{
				management.MonitoredPodcastFolders.Add((object)array4[l]);
			}
		}
		management.MediaFolder = (string.IsNullOrEmpty(ClientConfiguration.Groveler.RipDirectory) ? text : ClientConfiguration.Groveler.RipDirectory);
		management.VideoMediaFolder = (string.IsNullOrEmpty(ClientConfiguration.Groveler.VideoMediaFolder) ? text2 : ClientConfiguration.Groveler.VideoMediaFolder);
		management.PhotoMediaFolder = (string.IsNullOrEmpty(ClientConfiguration.Groveler.PhotoMediaFolder) ? text3 : ClientConfiguration.Groveler.PhotoMediaFolder);
		management.PodcastMediaFolder = (string.IsNullOrEmpty(ClientConfiguration.Groveler.PodcastMediaFolder) ? text4 : ClientConfiguration.Groveler.PodcastMediaFolder);
		management.SaveMonitoredFolders(commit: false);
		_proxyDefaultPathsComplete = true;
	}

	public void InitializeDefaultPaths()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		string[] monitoredAudioFolders = default(string[]);
		string[] monitoredVideoFolders = default(string[]);
		string[] monitoredPhotoFolders = default(string[]);
		string[] monitoredPodcastFolders = default(string[]);
		string[] array = default(string[]);
		string text = default(string);
		string text2 = default(string);
		string text3 = default(string);
		string text4 = default(string);
		string text5 = default(string);
		HRESULT.op_Implicit(ZuneApplication.ZuneLibrary.GetKnownFolders(ref monitoredAudioFolders, ref monitoredVideoFolders, ref monitoredPhotoFolders, ref monitoredPodcastFolders, ref array, ref text, ref text2, ref text3, ref text4, ref text5));
		if (ClientConfiguration.Groveler.MonitoredAudioFolders == null)
		{
			ClientConfiguration.Groveler.MonitoredAudioFolders = monitoredAudioFolders;
		}
		if (ClientConfiguration.Groveler.MonitoredPhotoFolders == null)
		{
			ClientConfiguration.Groveler.MonitoredPhotoFolders = monitoredPhotoFolders;
		}
		if (ClientConfiguration.Groveler.MonitoredVideoFolders == null)
		{
			ClientConfiguration.Groveler.MonitoredVideoFolders = monitoredVideoFolders;
		}
		if (ClientConfiguration.Groveler.MonitoredPodcastFolders == null)
		{
			ClientConfiguration.Groveler.MonitoredPodcastFolders = monitoredPodcastFolders;
		}
		if (string.IsNullOrEmpty(ClientConfiguration.Groveler.RipDirectory) && !string.IsNullOrEmpty(text))
		{
			ClientConfiguration.Groveler.RipDirectory = text;
		}
		if (string.IsNullOrEmpty(ClientConfiguration.Groveler.VideoMediaFolder) && !string.IsNullOrEmpty(text2))
		{
			ClientConfiguration.Groveler.VideoMediaFolder = text2;
		}
		if (string.IsNullOrEmpty(ClientConfiguration.Groveler.PhotoMediaFolder) && !string.IsNullOrEmpty(text3))
		{
			ClientConfiguration.Groveler.PhotoMediaFolder = text3;
		}
		if (string.IsNullOrEmpty(ClientConfiguration.Groveler.PodcastMediaFolder) && !string.IsNullOrEmpty(text4))
		{
			ClientConfiguration.Groveler.PodcastMediaFolder = text4;
		}
	}

	public void UpdateNSS()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		HMESettings val = new HMESettings();
		HRESULT val2 = HRESULT.op_Implicit(val.Init());
		if (!((HRESULT)(ref val2)).IsError && val.VelaSharingEnabled)
		{
			val.EnableSharingForUser();
			val.SetSharingEnabledForMediaType((EMediaTypes)3, true);
			val.SetSharingEnabledForMediaType((EMediaTypes)5, false);
			val.SetSharingEnabledForMediaType((EMediaTypes)4, false);
		}
	}

	private void RenderPromptTimeout(object sender, EventArgs e)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		if ((int)Application.RenderingType == 0)
		{
			return;
		}
		Win32MessageBox.Show(Shell.LoadString(StringId.IDS_RENDER_PROMPT), Shell.LoadString(StringId.IDS_RENDER_PROMPT_CAPTION), (Win32MessageBoxType)36, (DeferredInvokeHandler)delegate(object args)
		{
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			switch ((int)args)
			{
			case 6:
				RenderPromptInterval = 120000;
				break;
			case 7:
				ClientConfiguration.GeneralSettings.RenderingType = 0;
				Win32MessageBox.Show(Shell.LoadString(StringId.IDS_RENDER_PROMPT_RESTART), Shell.LoadString(StringId.IDS_RENDER_PROMPT_CAPTION), Win32MessageBoxType.MB_ICONASTERISK, (DeferredInvokeHandler)delegate
				{
					Application.Window.Close();
				});
				break;
			}
		});
	}
}
