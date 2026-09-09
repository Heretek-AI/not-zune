using System.Collections;
using Microsoft.Iris;
using Microsoft.Win32;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class RadioStationHelper : ModelItem
{
	private const string _rootKeyPath = "Software\\Microsoft\\Zune\\Radio";

	private static RadioStationHelper _instance;

	private ArrayList stationList;

	public static RadioStationHelper Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new RadioStationHelper();
			}
			return _instance;
		}
	}

	public ArrayList StationList
	{
		get
		{
			if (stationList == null)
			{
				RefreshStationList();
			}
			return stationList;
		}
		private set
		{
			if (value != stationList)
			{
				stationList = value;
			}
		}
	}

	private RadioStationHelper()
	{
	}

	public void AddStation(string title, string sourceUrl, string image)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		RadioStationProgressHandler val = new RadioStationProgressHandler(RadioStationAsyncCallback);
		RadioStationManager.Instance.AddStation(title, sourceUrl, image, val);
	}

	public void DeleteStation(string title)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		RadioStationProgressHandler val = new RadioStationProgressHandler(RadioStationAsyncCallback);
		RadioStationManager.Instance.DeleteStation(title, val);
	}

	private void RadioStationAsyncCallback(HRESULT hr)
	{
		if (!((HRESULT)(ref hr)).IsSuccess)
		{
			Shell.ShowErrorDialog(((HRESULT)(ref hr)).Int, StringId.IDS_RADIO_ERROR);
		}
		RefreshStationList();
	}

	private void RefreshStationList()
	{
		stationList = new ArrayList();
		RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Zune\\Radio");
		if (registryKey == null)
		{
			Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Zune\\Radio");
		}
		else
		{
			string[] subKeyNames = registryKey.GetSubKeyNames();
			string[] array = subKeyNames;
			foreach (string text in array)
			{
				string keyName = "HKEY_CURRENT_USER\\Software\\Microsoft\\Zune\\Radio\\" + text;
				string sourceURL = (string)Registry.GetValue(keyName, "SourceURL", "");
				string imagePath = (string)Registry.GetValue(keyName, "Image", "");
				RadioStation value = new RadioStation(text, sourceURL, imagePath);
				stationList.Add(value);
			}
		}
		((ModelItem)this).FirePropertyChanged("StationList");
	}
}
