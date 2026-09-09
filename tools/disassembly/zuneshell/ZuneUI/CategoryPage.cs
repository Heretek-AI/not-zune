using System;
using Microsoft.Iris;
using UIXControls;

namespace ZuneUI;

public class CategoryPage : DialogPage
{
	private int _currentMenuItemIndex;

	private bool _isWizard;

	private bool _allowAdvance = true;

	private bool _allowCancel = true;

	private bool _requireSecurityIcon;

	private bool _isMenuDisabled;

	private Choice _menu;

	private CategoryPageNode _node;

	private Category _currentCategory;

	private static bool _inSettings = false;

	private static CategoryPage _entryPage;

	private bool _hideDeviceOnCancel;

	internal static CategoryPage EntryPage => _entryPage;

	public Choice Menu => _menu;

	public Category CurrentCategory
	{
		get
		{
			return _currentCategory;
		}
		set
		{
			if (ZuneShell.DefaultInstance.NavigationLocked)
			{
				ZuneShell.DefaultInstance.DeferredNavigateCategory = value;
				ZuneShell.DefaultInstance.BlockedByNavigationLock = true;
			}
			else if (_currentCategory != value)
			{
				_currentCategory = value;
				int num = _menu.Options.IndexOf(value);
				if (num != -1)
				{
					_menu.ChosenIndex = num;
					CurrentMenuItemIndex = num;
				}
				((ModelItem)this).FirePropertyChanged("CurrentCategory");
			}
		}
	}

	public bool InFUE => _node == Shell.SettingsFrame.Wizard.FUE;

	public bool InDeviceSettings => _node == Shell.SettingsFrame.Settings.Device;

	public bool MenuDisabled
	{
		get
		{
			return _isMenuDisabled;
		}
		set
		{
			if (value != _isMenuDisabled)
			{
				_isMenuDisabled = value;
				((ModelItem)this).FirePropertyChanged("MenuDisabled");
			}
		}
	}

	public override bool IsWizard => _isWizard;

	public bool MenuItemsAvailable => _menu != null;

	private int CurrentMenuItemIndex
	{
		get
		{
			return _currentMenuItemIndex;
		}
		set
		{
			if (_currentMenuItemIndex != value)
			{
				_currentMenuItemIndex = value;
				AllowAdvance = true;
			}
		}
	}

	public override bool AllowAdvance
	{
		get
		{
			return _allowAdvance;
		}
		set
		{
			if (_allowAdvance != value)
			{
				_allowAdvance = value;
				((ModelItem)this).FirePropertyChanged("AllowAdvance");
			}
		}
	}

	public override bool AllowCancel
	{
		get
		{
			return _allowCancel;
		}
		set
		{
			if (_allowCancel != value)
			{
				_allowCancel = value;
				((ModelItem)this).FirePropertyChanged("AllowCancel");
			}
		}
	}

	public override bool RequireSecurityIcon
	{
		get
		{
			return _requireSecurityIcon;
		}
		set
		{
			if (_requireSecurityIcon != value)
			{
				_requireSecurityIcon = value;
				((ModelItem)this).FirePropertyChanged("RequireSecurityIcon");
			}
		}
	}

	public CategoryPage(CategoryPageNode node)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		base.UI = "res://ZuneShellResources!Management.uix";
		base.BackgroundUI = "res://ZuneShellResources!Management.uix#Background";
		base.TransportControlStyle = TransportControlStyle.None;
		base.PivotPreference = node;
		base.ShowCDIcon = false;
		base.ShowDeviceIcon = false;
		base.ShowPlaylistIcon = false;
		base.ShowSettings = false;
		base.ShowSearch = false;
		base.ShowNowPlayingBackgroundOnIdle = false;
		base.CanEnterCompactMode = false;
		base.NotificationAreaVisible = false;
		base.TransportControlsVisible = false;
		if (_entryPage == null)
		{
			_entryPage = this;
		}
		_node = node;
		if (node.Experience == Shell.SettingsFrame.Wizard)
		{
			_isWizard = true;
		}
		_hideDeviceOnCancel = node.HideDeviceOnCancel;
		_menu = new Choice((IModelItemOwner)(object)this);
		_menu.Options = node.Categories;
		_menu.ChosenChanged += CurrentCategoryChosenChanged;
		base.ShowBackArrow = node.AllowBackNavigation;
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing && _menu != null)
		{
			_menu.ChosenChanged -= CurrentCategoryChosenChanged;
			((ModelItem)_menu).Dispose();
			_menu = null;
		}
		base.OnDispose(disposing);
	}

	private void CurrentCategoryChosenChanged(object sender, EventArgs args)
	{
		CurrentCategory = (Category)Menu.ChosenValue;
	}

	public void ReleaseDeferredNavigation()
	{
		ZuneShell defaultInstance = ZuneShell.DefaultInstance;
		if (defaultInstance.DeferredNavigateCategory != null)
		{
			Category deferredNavigateCategory = defaultInstance.DeferredNavigateCategory;
			defaultInstance.DeferredNavigateCategory = null;
			CurrentCategory = deferredNavigateCategory;
		}
		else if (defaultInstance.DeferredNavigateNode != null)
		{
			Node deferredNavigateNode = defaultInstance.DeferredNavigateNode;
			defaultInstance.DeferredNavigateNode = null;
			((Command)deferredNavigateNode).Invoke();
		}
	}

	protected override void OnNavigatedAwayWorker(IPage destination)
	{
		base.OnNavigatedAwayWorker(destination);
		if (ZuneShell.DefaultInstance != null)
		{
			Management management = ZuneShell.DefaultInstance.Management;
			management.RemoveNSSDeviceListChangeEvent();
			management.CurrentCategoryPage = null;
		}
	}

	protected override void OnNavigatedToWorker()
	{
		if (base.NavigationArguments != null && base.NavigationArguments.Contains("Host"))
		{
			_menu.ChosenValue = (Category)base.NavigationArguments["Host"];
			base.NavigationArguments.Remove("Host");
		}
		ZuneShell.DefaultInstance.Management.CurrentCategoryPage = this;
		if (!_inSettings)
		{
			_inSettings = true;
			PauseSyncIfNecessary();
		}
		base.OnNavigatedToWorker();
	}

	public override IPageState SaveAndRelease()
	{
		return new CategoryPageState(this);
	}

	public override bool HandleBack()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		bool flag = _entryPage == this;
		if (flag && ZuneShell.DefaultInstance.Management.HasPendingCommits)
		{
			Command val = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_DIALOG_YES), (EventHandler)null);
			val.Invoked += delegate
			{
				SaveAndExit();
			};
			Command val2 = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_DIALOG_NO), (EventHandler)null);
			val2.Invoked += delegate
			{
				CancelAndExit();
			};
			MessageBox.Show(Shell.LoadString(StringId.IDS_SAVE_CHANGES_DIALOG_TITLE), Shell.LoadString(StringId.IDS_SAVE_CHANGES_ON_BACK_DIALOG_TEXT), val, val2, (BooleanChoice)null);
			return true;
		}
		if (flag)
		{
			_entryPage = null;
			CancelAndExit();
			return true;
		}
		return false;
	}

	public override bool HandleEscape()
	{
		ZuneShell.DefaultInstance.NavigateBack();
		return true;
	}

	public override void NavigatePage(bool forward)
	{
		if (Menu.Options.Count != 1)
		{
			if (forward)
			{
				CurrentMenuItemIndex++;
			}
			else
			{
				CurrentMenuItemIndex--;
			}
			Menu.ChosenValue = Menu.Options[CurrentMenuItemIndex];
		}
	}

	public override bool NavigationAvailable(bool forward)
	{
		if (!IsWizard)
		{
			return false;
		}
		if (forward)
		{
			return _currentMenuItemIndex < Menu.Options.Count - 1;
		}
		return _currentMenuItemIndex > 0;
	}

	public override void Save()
	{
		ZuneShell.DefaultInstance.Management.CommitListSave();
		ZuneShell.DefaultInstance.Management.DeviceManagement.SetupComplete(navigateToLandingPage: false);
	}

	public override void Exit()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		_entryPage = null;
		if (!InFUE)
		{
			ZuneShell.DefaultInstance.NavigateBack();
		}
		RestartSyncIfNecessary();
		_inSettings = false;
		DeviceManagement.NavigatingToWizard = false;
		if (ZuneShell.DefaultInstance != null)
		{
			ZuneShell.DefaultInstance.DisposeManagement();
		}
		ZuneShell.DefaultInstance.NavigationLocked = false;
		ZuneShell.DefaultInstance.Management.DeviceManagement.SetupComplete(navigateToLandingPage: false);
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			DeviceManagement.HandleSetupQueue();
		}, (DeferredInvokePriority)1);
		CancelAllFirmwareUpdates();
	}

	private void CancelAllFirmwareUpdates()
	{
		foreach (UIDevice item in SingletonModelItem<UIDeviceList>.Instance)
		{
			if (item.UIFirmwareUpdater != null && item.UIFirmwareUpdater.IsCheckingForUpdates)
			{
				item.UIFirmwareUpdater.CancelFirmwareUpdate();
			}
		}
	}

	public override void SaveAndExit()
	{
		Save();
		Exit();
	}

	public override void CancelAndExit()
	{
		Exit();
		if (DeviceManagement.SetupDevice != null && (_hideDeviceOnCancel || DeviceManagement.SetupDevice.RequiresFirmwareUpdate))
		{
			if (SyncControls.Instance.ChangeIntoSetupDevice)
			{
				SyncControls.Instance.ChangeIntoSetupDevice = false;
				DeviceManagement.SetupDevice = null;
			}
			else
			{
				DeviceManagement.HideSetupDevice();
			}
		}
	}

	public void PauseSyncIfNecessary()
	{
		if (!DeviceManagement.NavigatingToWizard)
		{
			UIDevice currentDevice = SyncControls.Instance.CurrentDevice;
			currentDevice.IsLockedAgainstSyncing = true;
		}
	}

	public void RestartSyncIfNecessary()
	{
		UIDevice currentDevice = SyncControls.Instance.CurrentDevice;
		if (!DeviceManagement.NavigatingToWizard && currentDevice.IsLockedAgainstSyncing)
		{
			currentDevice.IsLockedAgainstSyncing = false;
			if (currentDevice.IsReadyForSync)
			{
				currentDevice.BeginSync(userInitiated: true, syncOnNextNotify: false);
			}
		}
	}
}
