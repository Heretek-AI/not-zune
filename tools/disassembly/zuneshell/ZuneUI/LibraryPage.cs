using System.Collections;
using System.ComponentModel;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Shell;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class LibraryPage : ZunePage, IDeviceContentsPage, IPage
{
	private Choice _views;

	private Choice _contentTypes;

	private BooleanChoice _showContentTypes;

	private bool _showDeviceContents;

	private MediaType _mediaType;

	private bool _canAddMedia;

	private CanAddMediaArgs _canAddMediaArgs;

	private bool _isEmpty;

	private Command _escapePressed;

	private QueryTracker _tracker;

	private long _drmStateMask;

	public Choice Views
	{
		get
		{
			return _views;
		}
		set
		{
			if (_views != value)
			{
				_views = value;
				((ModelItem)this).FirePropertyChanged("Views");
			}
		}
	}

	public Choice ContentTypes
	{
		get
		{
			return _contentTypes;
		}
		set
		{
			if (_contentTypes != value)
			{
				_contentTypes = value;
				((ModelItem)this).FirePropertyChanged("ContentTypes");
			}
		}
	}

	public BooleanChoice ShowContentTypes => _showContentTypes;

	public long DrmStateMask
	{
		get
		{
			return _drmStateMask;
		}
		set
		{
			if (_drmStateMask != value)
			{
				_drmStateMask = value;
				((ModelItem)this).FirePropertyChanged("DrmStateMask");
			}
		}
	}

	public Command EscapePressed => _escapePressed;

	public bool IsEmpty
	{
		get
		{
			return _isEmpty;
		}
		set
		{
			if (_isEmpty != value)
			{
				_isEmpty = value;
				((ModelItem)this).FirePropertyChanged("IsEmpty");
			}
		}
	}

	public bool ShowDeviceContents
	{
		get
		{
			return _showDeviceContents;
		}
		set
		{
			_showDeviceContents = value;
		}
	}

	public MediaType MediaType => _mediaType;

	public bool CanAddMedia
	{
		get
		{
			return _canAddMedia;
		}
		set
		{
			if (_canAddMedia != value)
			{
				_canAddMedia = value;
				((ModelItem)this).FirePropertyChanged("CanAddMedia");
			}
			StopCheckingCanAddMedia();
		}
	}

	public QueryTracker Tracker => _tracker;

	private static string LibraryTemplate => "res://ZuneShellResources!Library.uix#Library";

	public LibraryPage()
		: this(MediaType.Undefined)
	{
	}

	public LibraryPage(MediaType mediaType)
		: this(showDeviceContents: false, mediaType)
	{
	}

	public LibraryPage(bool showDeviceContents, MediaType mediaType)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		base.UI = LibraryTemplate;
		ShowDeviceContents = showDeviceContents;
		_mediaType = mediaType;
		_escapePressed = new Command((IModelItemOwner)(object)this);
		_tracker = new QueryTracker();
		((ModelItem)_tracker).PropertyChanged += TrackerPropertyChanged;
		_drmStateMask = ZuneUI.DrmStateMask.All();
		_showContentTypes = new BooleanChoice((IModelItemOwner)(object)this);
	}

	private void TrackerPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (args.PropertyName == "Status" && QueryHelper.HasCompletedOrFailed(Tracker.Status))
		{
			ZuneApplication.PageLoadComplete();
		}
	}

	public virtual void CheckCanAddMedia(IList filenames)
	{
		CanAddMedia = false;
		if (ShowDeviceContents)
		{
			return;
		}
		ArrayList tempFilenames = new ArrayList(filenames);
		CanAddMediaArgs args = new CanAddMediaArgs();
		_canAddMediaArgs = args;
		ThreadPool.QueueUserWorkItem(delegate
		{
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Expected O, but got Unknown
			DeferredInvokeHandler val = null;
			bool canAddMedia = ZuneApplication.CanAddMedia(tempFilenames, MediaType, args);
			if (!args.Aborted)
			{
				if (val == null)
				{
					val = (DeferredInvokeHandler)delegate
					{
						if (!args.Aborted)
						{
							CanAddMedia = canAddMedia;
						}
					};
				}
				Application.DeferredInvoke(val, (object)null);
			}
		});
	}

	public void StopCheckingCanAddMedia()
	{
		if (_canAddMediaArgs != null)
		{
			_canAddMediaArgs.Aborted = true;
			_canAddMediaArgs = null;
		}
	}

	public virtual void AddMedia(IList filenames)
	{
		ArrayList tempFilenames = new ArrayList(filenames);
		ThreadPool.QueueUserWorkItem(delegate
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			if (!ZuneApplication.AddMedia(tempFilenames, MediaType))
			{
				Application.DeferredInvoke((DeferredInvokeHandler)delegate
				{
					MessageNotification notification = new MessageNotification(Shell.LoadString(StringId.IDS_LIBRARY_ADD_FILE_FAILED), NotificationTask.Library, NotificationState.OneShot)
					{
						SubMessage = Shell.LoadString(StringId.IDS_LIBRARY_ADD_FILE_FAILED_SUBMESSAGE)
					};
					NotificationArea.Instance.Add(notification);
				}, (object)null);
			}
		});
	}

	public override IPageState SaveAndRelease()
	{
		return new DevicePivotManagingPageState(this);
	}

	public static void BlockUpdatesFromDBList(IList list, BlockListUpdatesReason reason, bool block)
	{
		VirtualDatabaseList val = (VirtualDatabaseList)((list is VirtualDatabaseList) ? list : null);
		if (val != null)
		{
			val.SetBlockChangesFlag((int)reason, block);
		}
	}

	public static void DisableAutoRefresh(IList list)
	{
		LibraryVirtualList val = (LibraryVirtualList)((list is LibraryVirtualList) ? list : null);
		if (val != null)
		{
			val.DisableAutoRefresh();
		}
	}

	public override void InvokeSettings()
	{
		if (ShowDeviceContents)
		{
			((Command)Shell.SettingsFrame.Settings.Device).Invoke();
		}
		else if (MediaType == MediaType.Photo)
		{
			Shell.SettingsFrame.Settings.Software.Invoke(SettingCategories.Photo);
		}
		else if (MediaType == MediaType.PodcastEpisode || MediaType == MediaType.Podcast)
		{
			Shell.SettingsFrame.Settings.Software.Invoke(SettingCategories.Podcast);
		}
		else
		{
			base.InvokeSettings();
		}
	}
}
