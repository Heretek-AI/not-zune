using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using UIXControls;

namespace ZuneUI;

public class PhotoLibraryPage : LibraryPage, ISlideShowStateOwner, INotifyPropertyChanged
{
	private string _invalidPathCharacters;

	private PhotosPanel _photosPanel;

	private PhotoFolderPanel _photoFolderPanel;

	private IList _selectedPhotoIds;

	private Dictionary<int, bool> _expandedFolderIds;

	private Dictionary<int, bool> _refreshFolderIds;

	private Dictionary<string, bool> _foldersToMonitor;

	private int _folderId;

	private int _deviceRootFolderId = -1;

	private int _deviceCameraRollFolderId = -1;

	private int _deviceSavedFolderId = -1;

	private int _deviceParentFolderId = -1;

	private int _libraryDefaultSaveFolderId = -1;

	private SlideShowState _slideShowState;

	private Command _deleteCommand;

	private Command _refreshPictures;

	private Command _editCommand;

	private Command _createCommand;

	private Command _deleteFromDeviceCommand;

	private int _actionDepth;

	private int _pendingFolderToExpandToRoot;

	private int _pendingFolderDeleteId;

	private int _pendingFolderId;

	private string _photoSort;

	private bool _hasSelectedFolder;

	public PhotosPanel PhotosPanel => _photosPanel;

	public PhotoFolderPanel PhotoFolderPanel => _photoFolderPanel;

	public SlideShowState SlideShowState => _slideShowState;

	public IList SelectedPhotoIds
	{
		get
		{
			return _selectedPhotoIds;
		}
		set
		{
			if (_selectedPhotoIds != value)
			{
				_selectedPhotoIds = value;
				((ModelItem)this).FirePropertyChanged("SelectedPhotoIds");
			}
		}
	}

	public Command RefreshPictures => _refreshPictures;

	public bool HasSelectedFolder
	{
		get
		{
			return _hasSelectedFolder;
		}
		set
		{
			_hasSelectedFolder = value;
		}
	}

	public int FolderId
	{
		get
		{
			return _folderId;
		}
		set
		{
			if (_folderId != value)
			{
				_folderId = value;
				HasSelectedFolder = true;
				((ModelItem)this).FirePropertyChanged("FolderId");
			}
		}
	}

	public int DeviceCameraRollFolderId
	{
		get
		{
			return _deviceCameraRollFolderId;
		}
		set
		{
			if (_deviceCameraRollFolderId != value)
			{
				_deviceCameraRollFolderId = value;
				((ModelItem)this).FirePropertyChanged("DeviceCameraRollFolderId");
			}
		}
	}

	public int DeviceSavedFolderId
	{
		get
		{
			return _deviceSavedFolderId;
		}
		set
		{
			if (_deviceSavedFolderId != value)
			{
				_deviceSavedFolderId = value;
				((ModelItem)this).FirePropertyChanged("DeviceSavedFolderId");
			}
		}
	}

	public int DeviceParentFolderId
	{
		get
		{
			return _deviceParentFolderId;
		}
		set
		{
			if (_deviceParentFolderId != value)
			{
				_deviceParentFolderId = value;
				((ModelItem)this).FirePropertyChanged("DeviceParentFolderId");
			}
		}
	}

	public int DeviceRootFolderId
	{
		get
		{
			return _deviceRootFolderId;
		}
		set
		{
			if (_deviceRootFolderId != value)
			{
				_deviceRootFolderId = value;
				((ModelItem)this).FirePropertyChanged("DeviceRootFolderId");
			}
		}
	}

	public int LibraryRootFolderId => 0;

	public int LibraryDefaultSaveFolderId
	{
		get
		{
			return _libraryDefaultSaveFolderId;
		}
		set
		{
			if (_libraryDefaultSaveFolderId != value)
			{
				_libraryDefaultSaveFolderId = value;
				((ModelItem)this).FirePropertyChanged("LibraryDefaultSaveFolderId");
			}
		}
	}

	public Command DeleteCommand
	{
		get
		{
			return _deleteCommand;
		}
		set
		{
			if (_deleteCommand != value)
			{
				_deleteCommand = value;
				((ModelItem)this).FirePropertyChanged("DeleteCommand");
			}
		}
	}

	public Command EditCommand
	{
		get
		{
			return _editCommand;
		}
		set
		{
			if (_editCommand != value)
			{
				_editCommand = value;
				((ModelItem)this).FirePropertyChanged("EditCommand");
			}
		}
	}

	public Command CreateCommand
	{
		get
		{
			return _createCommand;
		}
		set
		{
			if (_createCommand != value)
			{
				_createCommand = value;
				((ModelItem)this).FirePropertyChanged("CreateCommand");
			}
		}
	}

	public Command DeleteFromDeviceCommand
	{
		get
		{
			return _deleteFromDeviceCommand;
		}
		set
		{
			if (_deleteFromDeviceCommand != value)
			{
				_deleteFromDeviceCommand = value;
				((ModelItem)this).FirePropertyChanged("DeleteFromDeviceCommand");
			}
		}
	}

	public int ActionDepth
	{
		get
		{
			return _actionDepth;
		}
		set
		{
			if (_actionDepth != value)
			{
				_actionDepth = value;
				((ModelItem)this).FirePropertyChanged("ActionDepth");
			}
		}
	}

	public int PendingFolderToDelete
	{
		get
		{
			return _pendingFolderDeleteId;
		}
		set
		{
			if (_pendingFolderDeleteId != value)
			{
				_pendingFolderDeleteId = value;
				((ModelItem)this).FirePropertyChanged("PendingFolderToDelete");
			}
		}
	}

	public int PendingFolderToEdit
	{
		get
		{
			return _pendingFolderId;
		}
		set
		{
			if (_pendingFolderId != value)
			{
				_pendingFolderId = value;
				((ModelItem)this).FirePropertyChanged("PendingFolderToEdit");
			}
		}
	}

	public int PendingFolderToExpandToRoot
	{
		get
		{
			return _pendingFolderToExpandToRoot;
		}
		set
		{
			if (_pendingFolderToExpandToRoot != value)
			{
				_pendingFolderToExpandToRoot = value;
				((ModelItem)this).FirePropertyChanged("PendingFolderToExpandToRoot");
			}
		}
	}

	public string PhotoSort
	{
		get
		{
			return _photoSort;
		}
		set
		{
			if (_photoSort != value)
			{
				_photoSort = value;
				((ModelItem)this).FirePropertyChanged("PhotoSort");
			}
		}
	}

	private static string LibraryTemplate => "res://ZuneShellResources!PhotoLibrary.uix#PhotoLibrary";

	public event EventHandler ExpandedFolders;

	public event EventHandler RefreshingFolders;

	public event EventHandler ScrollingToFolder;

	public PhotoLibraryPage()
		: this(showDevice: false)
	{
	}

	public PhotoLibraryPage(bool showDevice)
		: base(showDevice, MediaType.Photo)
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		base.UI = LibraryTemplate;
		base.UIPath = "Collection\\Photos";
		if (showDevice)
		{
			base.PivotPreference = Shell.MainFrame.Device.Photos;
			Deviceland.InitDevicePage(this);
		}
		else
		{
			base.PivotPreference = Shell.MainFrame.Collection.Photos;
		}
		base.IsRootPage = true;
		_photosPanel = new PhotosPanel(this);
		_photoFolderPanel = new PhotoFolderPanel(this);
		base.ShowPlaylistIcon = false;
		base.TransportControlStyle = TransportControlStyle.Photo;
		_slideShowState = new SlideShowState((IModelItemOwner)(object)this);
		_expandedFolderIds = new Dictionary<int, bool>();
		_refreshFolderIds = new Dictionary<int, bool>();
		_refreshPictures = new Command();
		_pendingFolderToExpandToRoot = -1;
		_pendingFolderDeleteId = -1;
		_pendingFolderId = -1;
	}

	public void ScrollToFolder()
	{
		((ModelItem)this).FirePropertyChanged("ScrollingToFolder");
		if (this.ScrollingToFolder != null)
		{
			this.ScrollingToFolder(this, new EventArgs());
		}
	}

	public void ExpandFolder(int id)
	{
		if (!_expandedFolderIds.ContainsKey(id))
		{
			_expandedFolderIds.Add(id, value: true);
			((ModelItem)this).FirePropertyChanged("ExpandedFolders");
			if (this.ExpandedFolders != null)
			{
				this.ExpandedFolders(this, new EventArgs());
			}
		}
	}

	public void CollapseFolder(int id)
	{
		if (_expandedFolderIds.ContainsKey(id))
		{
			_expandedFolderIds.Remove(id);
			((ModelItem)this).FirePropertyChanged("ExpandedFolders");
			if (this.ExpandedFolders != null)
			{
				this.ExpandedFolders(this, new EventArgs());
			}
		}
	}

	public bool FolderIsExpanded(int id)
	{
		return _expandedFolderIds.ContainsKey(id);
	}

	public void ToggleFolder(int id)
	{
		if (FolderIsExpanded(id))
		{
			CollapseFolder(id);
		}
		else
		{
			ExpandFolder(id);
		}
	}

	public void RefreshFolder(int id)
	{
		if (!_refreshFolderIds.ContainsKey(id))
		{
			_refreshFolderIds.Add(id, value: true);
			((ModelItem)this).FirePropertyChanged("RefreshingFolders");
			if (this.RefreshingFolders != null)
			{
				this.RefreshingFolders(this, new EventArgs());
			}
		}
	}

	public bool FolderHasRefreshPending(int id)
	{
		return _refreshFolderIds.ContainsKey(id);
	}

	public void RefreshedFolder(int id)
	{
		if (_refreshFolderIds.ContainsKey(id))
		{
			_refreshFolderIds.Remove(id);
		}
	}

	public string GetNextAvailableAlbumName(IList childList)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		if (childList == null)
		{
			return Shell.LoadString(StringId.IDS_NEW_ALBUM_NAME);
		}
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		foreach (DataProviderObject child in childList)
		{
			DataProviderObject val = child;
			dictionary.Add(((string)val.GetProperty("Title")).ToLowerInvariant(), value: true);
		}
		string result = string.Empty;
		for (int i = 1; i < int.MaxValue; i++)
		{
			string empty = string.Empty;
			empty = ((i != 1) ? string.Format(Shell.LoadString(StringId.IDS_NEW_ALBUM_NAME_MULTIPLE), i) : Shell.LoadString(StringId.IDS_NEW_ALBUM_NAME));
			if (!dictionary.ContainsKey(empty.ToLowerInvariant()))
			{
				result = empty;
				break;
			}
		}
		return result;
	}

	private Dictionary<string, bool> GetFoldersToMonitor(IList paths, ref string fileParentFolder)
	{
		Management management = ZuneShell.DefaultInstance.Management;
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		bool flag = false;
		foreach (object path in paths)
		{
			string text = path as string;
			if (string.IsNullOrEmpty(text) || dictionary.ContainsKey(text))
			{
				continue;
			}
			if (Directory.Exists(text))
			{
				if (!management.IsMonitored(management.MonitoredPhotoFolders, text))
				{
					dictionary.Add(text, value: true);
				}
			}
			else if (!flag)
			{
				try
				{
					FileInfo fileInfo = new FileInfo(text);
					fileParentFolder = fileInfo.DirectoryName;
					flag = true;
				}
				catch
				{
				}
			}
		}
		return dictionary;
	}

	public override void CheckCanAddMedia(IList filenames)
	{
		base.CanAddMedia = false;
		if (base.ShowDeviceContents || filenames == null || filenames.Count == 0)
		{
			return;
		}
		Management management = ZuneShell.DefaultInstance.Management;
		List<string> list = new List<string>();
		string fileParentFolder = string.Empty;
		if (GetFoldersToMonitor(filenames, ref fileParentFolder).Keys.Count > 0)
		{
			base.CanAddMedia = true;
			return;
		}
		if (string.IsNullOrEmpty(fileParentFolder) || management.IsMonitored(management.MonitoredPhotoFolders, fileParentFolder))
		{
			base.CanAddMedia = false;
			return;
		}
		foreach (string filename in filenames)
		{
			list.Add(filename);
		}
		base.CheckCanAddMedia(list);
	}

	public override void AddMedia(IList filenames)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		if (base.ShowDeviceContents || filenames == null || filenames.Count == 0)
		{
			return;
		}
		string fileParentFolder = string.Empty;
		_foldersToMonitor = GetFoldersToMonitor(filenames, ref fileParentFolder);
		if (!string.IsNullOrEmpty(fileParentFolder))
		{
			_foldersToMonitor[fileParentFolder] = true;
		}
		Command val = new Command((IModelItemOwner)(object)this);
		val.Invoked += delegate
		{
			if (_foldersToMonitor == null || _foldersToMonitor.Count == 0)
			{
				return;
			}
			List<string> list = new List<string>(_foldersToMonitor.Keys);
			list.Sort(StringComparer.CurrentCultureIgnoreCase);
			Management management = ZuneShell.DefaultInstance.Management;
			foreach (string item in list)
			{
				if (management.UsingWin7Libraries)
				{
					Win7ShellManager.Instance.AddLocationToLibrary((EWin7LibraryKind)2, false, item);
				}
				else
				{
					management.AddMonitoredFolder(management.MonitoredPhotoFolders, item, commit: true);
				}
			}
		};
		((ModelItem)val).Description = Shell.LoadString(StringId.IDS_PHOTO_ADD_FOLDER_BUTTON);
		MessageBox.Show(Shell.LoadString(StringId.IDS_PHOTO_ADD_FOLDER_TITLE), Shell.LoadString(StringId.IDS_PHOTO_ADD_FOLDER_DESCRIPTION), val, (Command)null, (BooleanChoice)null);
	}

	protected override void OnNavigatedAwayWorker(IPage destination)
	{
		_deviceCameraRollFolderId = -1;
		_deviceSavedFolderId = -1;
		_deviceRootFolderId = -1;
		_libraryDefaultSaveFolderId = -1;
		base.OnNavigatedAwayWorker(destination);
	}

	protected override void OnNavigatedToWorker()
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (base.NavigationArguments != null)
		{
			int num = -1;
			int num2 = -1;
			if (base.NavigationArguments.Contains("PhotoLibraryId"))
			{
				num = (int)base.NavigationArguments["PhotoLibraryId"];
			}
			if (base.NavigationArguments.Contains("FolderId"))
			{
				int num3 = (int)base.NavigationArguments["FolderId"];
				if (num3 > -1)
				{
					num2 = num3;
				}
			}
			_selectedPhotoIds = null;
			if (num > -1)
			{
				_selectedPhotoIds = new int[1] { num };
			}
			if (num > -1 && num2 == -1)
			{
				PhotoManager.Instance.FindPhotoContainer(num, ref num2);
				PendingFolderToExpandToRoot = num2;
			}
			if (num2 > -1)
			{
				PendingFolderToExpandToRoot = num2;
				FolderId = num2;
			}
			base.NavigationArguments = null;
		}
		base.OnNavigatedToWorker();
	}

	public static void FindInCollection(int folderId, int photoId)
	{
		if (photoId >= 0 && folderId < 0)
		{
			folderId = -1;
		}
		Hashtable hashtable = new Hashtable();
		hashtable.Add("FolderId", folderId);
		if (photoId >= 0)
		{
			hashtable.Add("PhotoLibraryId", photoId);
		}
		ZuneShell.DefaultInstance.Execute("Collection\\Photos", hashtable);
	}

	public override IPageState SaveAndRelease()
	{
		_photosPanel.Release();
		_photoFolderPanel.Release();
		return base.SaveAndRelease();
	}

	public void Rename(DataProviderObject source, string folderName)
	{
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		if (string.IsNullOrEmpty(folderName) || source == null || source.TypeName != "MediaFolder")
		{
			return;
		}
		int id = (int)source.GetProperty("LibraryId");
		if (id <= 0)
		{
			return;
		}
		int parentId = (int)source.GetProperty("ParentId");
		if (parentId < 0)
		{
			return;
		}
		string path = (string)source.GetProperty("FolderPath");
		if (!Directory.Exists(path))
		{
			return;
		}
		try
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			if (string.Compare(directoryInfo.Name, folderName, StringComparison.CurrentCulture) == 0)
			{
				return;
			}
		}
		catch (SecurityException)
		{
		}
		catch (ArgumentException)
		{
			return;
		}
		Management management = ZuneShell.DefaultInstance.Management;
		management.RemoveChildMonitoredFolders(path, commit: true);
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			HRESULT val = PhotoManager.Instance.RenameFolder(id, folderName);
			if (((HRESULT)(ref val)).IsSuccess)
			{
				RefreshFolder(parentId);
				RefreshPictures.Invoke();
			}
		}, (object)null);
	}

	public void MoveFolder(DataProviderObject source, DataProviderObject target)
	{
		if (source == null || target == null || source.TypeName != "MediaFolder" || target.TypeName != "MediaFolder")
		{
			return;
		}
		int num = (int)source.GetProperty("LibraryId");
		if (num <= 0)
		{
			return;
		}
		int num2 = (int)source.GetProperty("ParentId");
		if (num2 >= 0)
		{
			int num3 = (int)target.GetProperty("LibraryId");
			if (num3 > 0)
			{
				string sourceFolderPath = (string)source.GetProperty("FolderPath");
				string targetFolderPath = (string)target.GetProperty("FolderPath");
				MoveFolder(num, num2, sourceFolderPath, num3, targetFolderPath);
			}
		}
	}

	private void MoveFolder(int sourceId, int sourceParentId, string sourceFolderPath, int targetId, string targetFolderPath)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		if (sourceId <= 0 || !Directory.Exists(sourceFolderPath))
		{
			return;
		}
		Management management = ZuneShell.DefaultInstance.Management;
		management.RemoveChildMonitoredFolders(sourceFolderPath, commit: true);
		int[] sourceIds = new int[1];
		sourceIds[0] = sourceId;
		string resultantFolderPath = Path.Combine(targetFolderPath, Path.GetFileName(sourceFolderPath));
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			HRESULT val = PhotoManager.Instance.Move(sourceIds, (EMediaTypes)20, targetId);
			if (((HRESULT)(ref val)).IsSuccess)
			{
				ZuneApplication.ZuneLibrary.AddGrovelerScanDirectory(sourceFolderPath, (EMediaTypes)5);
				RefreshFolder(sourceParentId);
				ZuneApplication.ZuneLibrary.AddGrovelerScanDirectory(resultantFolderPath, (EMediaTypes)5);
				RefreshFolder(targetId);
			}
		}, (object)null);
	}

	public void Import(IList shellItems, int destinationFolderId)
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		foreach (object shellItem in shellItems)
		{
			DeferredInvokeHandler val = null;
			DeferredInvokeHandler val2 = null;
			string path = shellItem as string;
			if (string.IsNullOrEmpty(path) || destinationFolderId <= 0)
			{
				continue;
			}
			if (File.Exists(path) && ZuneApplication.ZuneLibrary.CanAddMedia(path, (EMediaTypes)5))
			{
				if (val == null)
				{
					val = (DeferredInvokeHandler)delegate
					{
						//IL_0017: Unknown result type (might be due to invalid IL or missing references)
						PhotoManager.Instance.Import(path, (EMediaTypes)5, destinationFolderId);
						FolderId = destinationFolderId;
						RefreshPictures.Invoke();
					};
				}
				Application.DeferredInvoke(val, (object)null);
			}
			else
			{
				if (!Directory.Exists(path))
				{
					continue;
				}
				int num = FindFolder(path);
				if (num > 0)
				{
					Management management = ZuneShell.DefaultInstance.Management;
					management.RemoveChildMonitoredFolders(path, commit: true);
				}
				if (val2 == null)
				{
					val2 = (DeferredInvokeHandler)delegate
					{
						//IL_0018: Unknown result type (might be due to invalid IL or missing references)
						//IL_001d: Unknown result type (might be due to invalid IL or missing references)
						HRESULT val3 = PhotoManager.Instance.Import(path, (EMediaTypes)20, destinationFolderId);
						if (((HRESULT)(ref val3)).IsSuccess && destinationFolderId > 0)
						{
							ZuneApplication.ZuneLibrary.AddGrovelerScanDirectory(path, (EMediaTypes)5);
							FolderId = destinationFolderId;
							RefreshFolder(destinationFolderId);
							ExpandFolder(destinationFolderId);
						}
					};
				}
				Application.DeferredInvoke(val2, (object)null);
			}
		}
	}

	public static bool DeleteFolder(DataProviderObject folderItem)
	{
		if (folderItem == null || folderItem.TypeName != "MediaFolder")
		{
			return false;
		}
		int num = (int)folderItem.GetProperty("LibraryId");
		if (num <= 0)
		{
			return false;
		}
		string path = (string)folderItem.GetProperty("FolderPath");
		Management management = ZuneShell.DefaultInstance.Management;
		if (management.RemoveChildMonitoredFolders(path, commit: true))
		{
			Thread.Sleep(500);
		}
		return ZuneApplication.ZuneLibrary.DeleteFilesystemFolder(num, (EMediaTypes)5);
	}

	public void MovePhotos(IList sourceDataProviderList, int targetId)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		int[] sourceIds = new int[sourceDataProviderList.Count];
		for (int i = 0; i < sourceDataProviderList.Count; i++)
		{
			DataProviderObject val = (DataProviderObject)sourceDataProviderList[i];
			sourceIds[i] = (int)val.GetProperty("LibraryId");
		}
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			PhotoManager.Instance.Move(sourceIds, (EMediaTypes)5, targetId);
			RefreshPictures.Invoke();
		}, (object)null);
	}

	public int CreateFolder(string name, int targetId)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		int result = 0;
		PhotoManager.Instance.CreateFolder(name, targetId, ref result);
		return result;
	}

	public int FindFolder(string folderName)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		int result = default(int);
		HRESULT val = PhotoManager.Instance.FindFolder(folderName, ref result);
		if (!((HRESULT)(ref val)).IsSuccess)
		{
			return -1;
		}
		return result;
	}

	public bool CanDropShellFolder(string treePath, string dropPath)
	{
		try
		{
			if (!Directory.Exists(treePath) || !Directory.Exists(dropPath))
			{
				return false;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(treePath);
			DirectoryInfo directoryInfo2 = new DirectoryInfo(dropPath);
			string text = directoryInfo.FullName + Path.DirectorySeparatorChar;
			string value = directoryInfo2.FullName + Path.DirectorySeparatorChar;
			if (text.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0)
			{
				return false;
			}
			if (string.Compare(directoryInfo.FullName, directoryInfo2.Parent.FullName, StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				return false;
			}
		}
		catch
		{
			return false;
		}
		return true;
	}

	public string GetInvalidPathCharacters()
	{
		if (string.IsNullOrEmpty(_invalidPathCharacters))
		{
			char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
			if (invalidFileNameChars != null && invalidFileNameChars.Length > 0)
			{
				bool flag = true;
				for (int i = 0; i < invalidFileNameChars.Length; i++)
				{
					if (!char.IsControl(invalidFileNameChars[i]))
					{
						if (flag)
						{
							_invalidPathCharacters = invalidFileNameChars[i].ToString();
							flag = false;
						}
						else
						{
							_invalidPathCharacters += $" {invalidFileNameChars[i]}";
						}
					}
				}
			}
		}
		return _invalidPathCharacters;
	}

	public bool ValidateFilename(string filename)
	{
		if (!string.IsNullOrEmpty(filename))
		{
			return filename.IndexOfAny(Path.GetInvalidFileNameChars()) == -1;
		}
		return false;
	}
}
