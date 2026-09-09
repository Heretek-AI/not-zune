using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Win32;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class SoftwareUpdates : ModelItem
{
	private const string m_PID = "92510-320-9256355-04773";

	private bool m_checkingForUpdates;

	private bool m_backgroundCheck;

	private bool m_userInitiatedUpdate;

	private static SoftwareUpdates m_instance;

	private UpdateCheckEventArguments _lastUpdateCheckResult;

	public static SoftwareUpdates Instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = new SoftwareUpdates();
			}
			return m_instance;
		}
	}

	public static string PID => "92510-320-9256355-04773";

	public bool CheckingForUpdates
	{
		get
		{
			return m_checkingForUpdates;
		}
		private set
		{
			if (m_checkingForUpdates != value)
			{
				m_checkingForUpdates = value;
				((ModelItem)this).FirePropertyChanged("CheckingForUpdates");
			}
		}
	}

	public UpdateCheckEventArguments LastUpdateCheckResult
	{
		get
		{
			return _lastUpdateCheckResult;
		}
		set
		{
			if (_lastUpdateCheckResult != value)
			{
				_lastUpdateCheckResult = value;
				((ModelItem)this).FirePropertyChanged("LastUpdateCheckResult");
			}
		}
	}

	public event EventHandler InstallInitiated;

	public void StartUp()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredStartUp), (DeferredInvokePriority)1);
	}

	public void CheckForUpdates(bool isUserInitiated)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		if (!CheckingForUpdates)
		{
			_lastUpdateCheckResult = null;
			CheckingForUpdates = true;
			m_userInitiatedUpdate = isUserInitiated;
			UpdateProgressHandler val = new UpdateProgressHandler(UpdateCheckCallback);
			UpdateManager.Instance.BeginUpdateCheck(val);
			if (m_userInitiatedUpdate)
			{
				SQMLog.Log((SQMDataId)35, 1);
			}
		}
		else
		{
			m_backgroundCheck = false;
		}
	}

	public void CancelCheckForUpdates()
	{
		UpdateManager.Instance.CancelUpdateCheck();
		Instance.CheckingForUpdates = false;
	}

	public void InstallUpdates()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		OnInstallInitiated();
		UpdateProgressHandler val = new UpdateProgressHandler(UpdateCheckCallback);
		UpdateManager.Instance.InstallUpdate(val);
	}

	public void ShowUpdateAvailabilityDialog()
	{
		if (!LastUpdateCheckResult.UpdateFound && m_backgroundCheck)
		{
			return;
		}
		if (IsWUClientUpToDate())
		{
			if (LastUpdateCheckResult.HR >= 0)
			{
				UpdateDialogInfo.Show(LastUpdateCheckResult.UpdateFound, LastUpdateCheckResult.CriticalUpdateFound, m_userInitiatedUpdate);
			}
			else
			{
				Shell.ShowErrorDialog(LastUpdateCheckResult.HR, StringId.IDS_CHECK_FOR_UPDATES_FAILED);
			}
		}
		else
		{
			Shell.ShowErrorDialog(-1072885299, StringId.IDS_CHECK_FOR_UPDATES_FAILED, StringId.IDS_CHECK_FOR_UPDATES_FAILED_DESCRIPTION);
		}
	}

	private bool IsWUClientUpToDate()
	{
		string text = Environment.ExpandEnvironmentVariables("%windir%\\System32\\wuaueng.dll");
		bool result = false;
		if (text != null && File.Exists(text))
		{
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(text);
			Version version = new Version(versionInfo.ProductVersion);
			Version value = new Version("7.2.6001.784");
			if (version.CompareTo(value) >= 0)
			{
				result = true;
			}
		}
		return result;
	}

	private void OnInstallInitiated()
	{
		if (this.InstallInitiated != null)
		{
			this.InstallInitiated(this, null);
		}
		((ModelItem)this).FirePropertyChanged("InstallInitiated");
	}

	private void DeferredStartUp(object state)
	{
		CheckOsUpgrade();
		DateTime lastUpdateCheck = ClientConfiguration.Shell.LastUpdateCheck;
		uint num = Math.Min((uint)MachineConfiguration.Setup.UpdateCheckFrequency, 14u);
		if (Math.Abs((DateTime.UtcNow - lastUpdateCheck).Days) >= num && !Fue.Instance.IsFirstLaunch && !Shell.SettingsFrame.Wizard.IsCurrent)
		{
			m_backgroundCheck = true;
			CheckForUpdates(isUserInitiated: false);
		}
		CheckCodecAccounting();
	}

	private void DeferredUpdateCheckCallback(object args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		LastUpdateCheckResult = (UpdateCheckEventArguments)args;
		if (LastUpdateCheckResult.HR >= 0)
		{
			try
			{
				ClientConfiguration.Shell.LastUpdateCheck = DateTime.UtcNow;
			}
			catch (ApplicationException)
			{
			}
		}
		if (m_backgroundCheck)
		{
			ShowUpdateAvailabilityDialog();
		}
		m_userInitiatedUpdate = false;
		m_backgroundCheck = false;
		CheckingForUpdates = false;
	}

	private void UpdateCheckCallback(UpdateCheckEventArguments args)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredUpdateCheckCallback), (object)args, (DeferredInvokePriority)0);
	}

	private void CheckOsUpgrade()
	{
		int major = Environment.OSVersion.Version.Major;
		int minor = Environment.OSVersion.Version.Minor;
		int num = major * 100 + minor;
		int oSVersion = MachineConfiguration.Setup.OSVersion;
		if (num > oSVersion)
		{
			if (oSVersion < 600 && MachineConfiguration.HME.CurrentSharingUID != 0)
			{
				ThreadPool.QueueUserWorkItem(RepairNetworkSharingService, num);
			}
			if (oSVersion < 601 && num >= 601)
			{
				ThreadPool.QueueUserWorkItem(CreatePodcastLibraryTemplate, num);
			}
		}
	}

	private void RepairNetworkSharingService(object state)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		int oSVersion = (int)state;
		HMESettings val = new HMESettings();
		HRESULT val2 = HRESULT.op_Implicit(val.Init());
		if (((HRESULT)(ref val2)).IsSuccess)
		{
			val2 = HRESULT.op_Implicit(val.RepairSharing());
		}
		if (((HRESULT)(ref val2)).IsSuccess)
		{
			MachineConfiguration.Setup.OSVersion = oSVersion;
		}
	}

	private void CreatePodcastLibraryTemplate(object state)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		int oSVersion = (int)state;
		HRESULT val = HRESULT.op_Implicit(Win7ShellManager.Instance.CreatePodcastLibraryTemplate());
		if (((HRESULT)(ref val)).IsSuccess)
		{
			MachineConfiguration.Setup.OSVersion = oSVersion;
		}
		Win7ShellManager.Instance.SyncLibraryFolders();
	}

	private void CheckCodecAccounting()
	{
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Expected O, but got Unknown
		if (MachineConfiguration.Setup.CodecInfoSent)
		{
			return;
		}
		RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Zune");
		DateTime utcNow = DateTime.UtcNow;
		DateTime time = utcNow.ToLocalTime();
		long num = (long)time.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
		string text = "ZuneSetup.exe";
		string wTProfile = MachineConfiguration.Setup.WTProfile;
		string text2 = num.ToString();
		string text3 = time.ToString("yyyy/MM/dd");
		string text4 = TimeZone.CurrentTimeZone.GetUtcOffset(time).Hours.ToString();
		string text5 = time.Hour.ToString();
		string ietfLanguageTag = CultureInfo.CurrentUICulture.IetfLanguageTag;
		string installationSource = MachineConfiguration.Setup.InstallationSource;
		string pID = PID;
		string text6 = (string)registryKey.GetValue("CurrentVersion");
		string oldVersion = MachineConfiguration.Setup.OldVersion;
		string text7 = string.Empty;
		string text8 = "New";
		if (!string.IsNullOrEmpty(text6))
		{
			try
			{
				Version version = new Version(text6);
				_ = version.Major;
				_ = version.Minor;
				if (!string.IsNullOrEmpty(oldVersion))
				{
					Version version2 = new Version(oldVersion);
					text8 = ((version == version2) ? "ReInstall" : ((version.Major > version2.Major) ? "MajorUpgrade" : ((version.Major != version2.Major) ? "ReInstall" : "Upgrade")));
				}
			}
			catch (ArgumentException)
			{
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
		}
		try
		{
			using CryptoHelper cryptoHelper = new CryptoHelper(pID);
			string data = utcNow.ToString("yyyy-MM-dd-hh-mm-ss");
			text7 = Uri.EscapeDataString(cryptoHelper.Encrypt(data));
		}
		catch (Win32Exception)
		{
		}
		string[] array = new string[27]
		{
			"http://m.webtrends.com/dcs8fe5yk00000s538qdxmbst_2t2k/dcs.gif", "?dcsdat=", text2, "&dcsuri=", text, "&dcssip=", wTProfile, "&wt.tz=", text4, "&wt.bh=",
			text5, "&wt.date=", text3, "&wt.ul=", ietfLanguageTag, "&zune_cver=", text6, "&zune_isource=", installationSource, "&zune_itype=",
			text8, "&zune_over=", oldVersion, "&zune_pid=", pID, "&zune_gmthash=", text7
		};
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.Append(array[i]);
		}
		try
		{
			Uri uri = new Uri(stringBuilder.ToString());
			HttpWebRequest val = HttpWebRequest.Create(uri);
			val.CancelOnShutdown = true;
			val.GetResponseAsync(new AsyncRequestComplete(OnRequestComplete), (object)null);
		}
		catch (Exception)
		{
		}
	}

	private void OnRequestComplete(HttpWebResponse response, object requestArgs)
	{
		if (response.StatusCode == HttpStatusCode.OK)
		{
			MachineConfiguration.Setup.CodecInfoSent = true;
			return;
		}
		_ = response.StatusCode;
		_ = 407;
	}
}
