using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class ApplicationLibraryPage : LibraryPage
{
	private ApplicationsPanel _applicationsPanel;

	private object _selectedApplication;

	private IList _selectedApplicationIDs;

	public ApplicationsPanel ApplicationsPanel => _applicationsPanel;

	public object SelectedApplication
	{
		get
		{
			return _selectedApplication;
		}
		set
		{
			if (_selectedApplication != value)
			{
				_selectedApplication = value;
				((ModelItem)this).FirePropertyChanged("SelectedApplication");
			}
		}
	}

	public IList SelectedApplicationIDs
	{
		get
		{
			return _selectedApplicationIDs;
		}
		set
		{
			if (_selectedApplicationIDs != value)
			{
				_selectedApplicationIDs = value;
				((ModelItem)this).FirePropertyChanged("SelectedApplicationIDs");
			}
		}
	}

	private static string LibraryTemplate => "res://ZuneShellResources!ApplicationLibrary.uix#ApplicationLibrary";

	public ApplicationLibraryPage()
		: this(showDevice: false)
	{
	}

	protected override void OnDispose(bool disposing)
	{
		base.OnDispose(disposing);
	}

	public ApplicationLibraryPage(bool showDevice)
		: base(showDevice, MediaType.Application)
	{
		base.UI = LibraryTemplate;
		if (showDevice)
		{
			base.UIPath = "Device\\Applications";
			base.PivotPreference = Shell.MainFrame.Device.Applications;
			Deviceland.InitDevicePage(this);
		}
		else
		{
			base.UIPath = "Collection\\Applications";
			base.PivotPreference = Shell.MainFrame.Collection.Applications;
		}
		base.IsRootPage = true;
		base.ShowPlaylistIcon = false;
		base.ShowCDIcon = false;
		base.TransportControlStyle = TransportControlStyle.None;
		_applicationsPanel = new ApplicationsPanel(this);
	}

	protected override void OnNavigatedToWorker()
	{
		if (base.NavigationArguments != null)
		{
			if (base.NavigationArguments.Contains("ApplicationLibraryId"))
			{
				_selectedApplicationIDs = new int[1] { (int)base.NavigationArguments["ApplicationLibraryId"] };
			}
			base.NavigationArguments = null;
		}
		base.OnNavigatedToWorker();
	}

	protected override void OnNavigatedAwayWorker(IPage destination)
	{
		base.OnNavigatedAwayWorker(destination);
		SelectedApplication = null;
	}

	public static void FindInCollection(int applicationId)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("ApplicationLibraryId", applicationId);
		ZuneShell.DefaultInstance.Execute("Collection\\Applications", hashtable);
	}

	public static bool DoesApplicationNeedUpdate(int applicationId, string serviceVersionString)
	{
		if (string.IsNullOrEmpty(serviceVersionString))
		{
			return false;
		}
		Version version = null;
		if (!TryParseVersion(serviceVersionString, out version))
		{
			return false;
		}
		return DoesApplicationNeedUpdate(applicationId, version);
	}

	public static bool DoesApplicationNeedUpdate(int applicationId, Version serviceVersion)
	{
		if (null == serviceVersion)
		{
			return false;
		}
		bool result = false;
		string fieldValue = PlaylistManager.GetFieldValue<string>(applicationId, (EListType)20, 376, null);
		if (!string.IsNullOrEmpty(fieldValue))
		{
			Version version = null;
			if (TryParseVersion(fieldValue, out version) && version < serviceVersion)
			{
				result = true;
			}
		}
		return result;
	}

	private static bool TryParseVersion(string versionString, out Version version)
	{
		bool result = false;
		version = null;
		try
		{
			version = new Version(versionString);
			result = true;
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
		return result;
	}

	public override IPageState SaveAndRelease()
	{
		return new ApplicationPageState(this);
	}
}
