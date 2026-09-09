using System;
using System.Runtime.InteropServices;
using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class MetadataNotifications : ModelItem
{
	private static MetadataNotifications _singletonInstance;

	private MetadataMgrNotifications _metadataMgr;

	private MessageNotification _libraryNotification;

	private MessageNotification _updateMetadataNotification;

	private bool _updatingMetadata;

	private Timer _timerOnEndUpdates;

	private object _lock = new object();

	private bool _deferredUpdatePending;

	private int _addCount;

	private static string _libraryActiveImportMessage = Shell.LoadString(StringId.IDS_LIBRARY_IMPORTING_MEDIA);

	private static string _libraryAddFileMessage = Shell.LoadString(StringId.IDS_LIBRARY_ADD_FILE);

	private static string _libraryScanCompleteMessage = Shell.LoadString(StringId.IDS_LIBRARY_SCAN_COMPLETE);

	private static string _metadataUpdateMessage = Shell.LoadString(StringId.IDS_LIBRARY_METADATA_UPDATE);

	private static string _metadataUpdateInfoMessage = Shell.LoadString(StringId.IDS_LIBRARY_METADATA_UPDATE_INFO);

	private static string _libraryFileChangeCompleteMessage = Shell.LoadString(StringId.IDS_LIBRARY_FILE_CHANGE_COMPLETE);

	private static string _libraryFileDeleteFailedMessage = Shell.LoadString(StringId.IDS_LIBRARY_FILE_DELETE_FAILED);

	public static MetadataNotifications Instance
	{
		get
		{
			if (_singletonInstance == null)
			{
				_singletonInstance = new MetadataNotifications((IModelItemOwner)(object)ZuneShell.DefaultInstance);
			}
			return _singletonInstance;
		}
	}

	public bool Importing => _libraryNotification != null;

	private MetadataNotifications(IModelItemOwner owner)
		: base(owner)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		_timerOnEndUpdates = new Timer();
		_timerOnEndUpdates.Interval = 15000;
		_timerOnEndUpdates.AutoRepeat = false;
		_timerOnEndUpdates.Tick += OnEndUpdatesTick;
	}

	protected override void OnDispose(bool fDisposing)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		((ModelItem)this).OnDispose(fDisposing);
		if (fDisposing)
		{
			if (_metadataMgr != null)
			{
				_metadataMgr.FileAdded -= new OnFileAddedHandler(OnFileAdded);
				_metadataMgr.Dispose();
				_metadataMgr = null;
			}
			if (_timerOnEndUpdates != null)
			{
				_timerOnEndUpdates.Tick -= OnEndUpdatesTick;
				((ModelItem)_timerOnEndUpdates).Dispose();
				_timerOnEndUpdates = null;
			}
		}
	}

	public void Phase2Init()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		_metadataMgr = new MetadataMgrNotifications();
		if (_metadataMgr != null)
		{
			_metadataMgr.FileAdded += new OnFileAddedHandler(OnFileAdded);
		}
	}

	private void OnFileAdded(IntPtr pszPath, EMediaTypes MediaType)
	{
		NotifyFileAddedStatus();
	}

	private void NotifyFileAddedStatus()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		bool flag = false;
		int milliseconds = 250;
		lock (_lock)
		{
			if (_addCount == 0)
			{
				milliseconds = 0;
			}
			_addCount++;
			if (!_deferredUpdatePending)
			{
				_deferredUpdatePending = true;
				flag = true;
			}
		}
		if (flag)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredFileAddedStatus), (object)null, new TimeSpan(0, 0, 0, 0, milliseconds));
		}
	}

	private void DeferredFileAddedStatus(object obj)
	{
		if (((ModelItem)this).IsDisposed)
		{
			return;
		}
		int addCount;
		lock (_lock)
		{
			_deferredUpdatePending = false;
			addCount = _addCount;
		}
		_timerOnEndUpdates.Stop();
		_timerOnEndUpdates.Start();
		if (Download.Instance.Notification == null)
		{
			if (_libraryNotification == null)
			{
				_libraryNotification = new MessageNotification(_libraryActiveImportMessage, NotificationTask.Library, NotificationState.Normal);
				NotificationArea.Instance.RemoveAll(NotificationTask.Library, NotificationState.Completed);
				NotificationArea.Instance.Add(_libraryNotification);
				((ModelItem)this).FirePropertyChanged("Importing");
			}
			_libraryNotification.SubMessage = string.Format(_libraryAddFileMessage, addCount);
		}
	}

	private void OnEndUpdatesTick(object sender, EventArgs args)
	{
		int addCount;
		lock (_lock)
		{
			addCount = _addCount;
			_addCount = 0;
		}
		NotificationArea.Instance.Replace(_libraryNotification, new MessageNotification(_libraryFileChangeCompleteMessage, string.Format(_libraryAddFileMessage, addCount), NotificationTask.Library, NotificationState.Completed));
		_libraryNotification = null;
		((ModelItem)this).FirePropertyChanged("Importing");
	}

	private void OnBeginMetadataLifecycle()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (!((ModelItem)this).IsDisposed)
			{
				_updatingMetadata = true;
			}
		}, (object)null);
	}

	private void OnEndMetadataLifecycle()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (!((ModelItem)this).IsDisposed)
			{
				if (_updateMetadataNotification != null && _updateMetadataNotification.Type != NotificationState.Completed)
				{
					NotificationArea.Instance.Replace(_updateMetadataNotification, new MessageNotification("Metadata update completed", NotificationTask.Library, NotificationState.Completed));
					_updateMetadataNotification = null;
				}
				_updatingMetadata = false;
			}
		}, (object)null);
	}

	private void OnMetadataUpdate(IntPtr artistPtr, IntPtr albumPtr)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		string artist = Marshal.PtrToStringUni(artistPtr);
		string album = Marshal.PtrToStringUni(albumPtr);
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (!((ModelItem)this).IsDisposed && _updatingMetadata)
			{
				if (_updateMetadataNotification == null)
				{
					_updateMetadataNotification = new MessageNotification(_metadataUpdateMessage, NotificationTask.Library, NotificationState.Normal);
					NotificationArea.Instance.RemoveAll(NotificationTask.Library, NotificationState.Completed);
					NotificationArea.Instance.Add(_updateMetadataNotification);
				}
				_updateMetadataNotification.SubMessage = string.Format(_metadataUpdateInfoMessage, artist, album);
			}
		}, (object)null);
	}

	private void OnFileDeleteFailed(IntPtr fileUrlPtr)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		string strFileUrl = Marshal.PtrToStringUni(fileUrlPtr);
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (!((ModelItem)this).IsDisposed && _updatingMetadata)
			{
				if (_updateMetadataNotification == null)
				{
					_updateMetadataNotification = new MessageNotification(_metadataUpdateMessage, NotificationTask.Library, NotificationState.Normal);
					NotificationArea.Instance.RemoveAll(NotificationTask.Library, NotificationState.Completed);
					NotificationArea.Instance.Add(_updateMetadataNotification);
				}
				_updateMetadataNotification.SubMessage = string.Format(_libraryFileDeleteFailedMessage, strFileUrl);
			}
		}, (object)null);
	}
}
