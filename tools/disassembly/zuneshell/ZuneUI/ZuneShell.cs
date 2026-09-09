using System;
using System.Collections;
using System.ComponentModel;
using Microsoft.Iris;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class ZuneShell : ModelItem
{
	public Category DeferredNavigateCategory;

	public Node DeferredNavigateNode;

	private static ZuneShell s_defaultInstance;

	private PageStack _pageStack;

	private Command _navigateBackCommand;

	private ICommandHandler _commandHandler;

	private Management _management;

	private int _navigationsToPagePending;

	private bool _navigationLocked;

	private object thisLock = new object();

	public Command NavigateBackCommand => _navigateBackCommand;

	public int MaximumStackSize
	{
		get
		{
			return (int)_pageStack.MaximumStackSize;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			_pageStack.MaximumStackSize = (uint)value;
		}
	}

	public ZunePage CurrentPage => (ZunePage)_pageStack.CurrentPage;

	public bool CanNavigateBack => _pageStack.CanNavigateBack;

	public bool NavigationLocked
	{
		get
		{
			return _navigationLocked;
		}
		set
		{
			if (_navigationLocked != value)
			{
				_navigationLocked = value;
				((ModelItem)this).FirePropertyChanged("NavigationLocked");
			}
		}
	}

	public bool BlockedByNavigationLock
	{
		get
		{
			return true;
		}
		set
		{
			((ModelItem)this).FirePropertyChanged("BlockedByNavigationLock");
		}
	}

	public NavigationDirection LastNavigationDirection => _pageStack.LastNavigationDirection;

	public ICommandHandler CommandHandler
	{
		get
		{
			return _commandHandler;
		}
		set
		{
			if (_commandHandler != value)
			{
				_commandHandler = value;
				((ModelItem)this).FirePropertyChanged("CommandHandler");
			}
		}
	}

	public static ZuneShell DefaultInstance
	{
		get
		{
			return s_defaultInstance;
		}
		private set
		{
			if (s_defaultInstance != null && value != null)
			{
				throw new InvalidOperationException("Should only have one static shell instance.");
			}
			s_defaultInstance = value;
		}
	}

	public bool NavigationsPending => _navigationsToPagePending > 0;

	public Management Management
	{
		get
		{
			if (_management == null && DefaultInstance != null)
			{
				_management = new Management();
			}
			return _management;
		}
	}

	public ZuneShell()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		_pageStack = new PageStack((IModelItemOwner)(object)this);
		((ModelItem)_pageStack).PropertyChanged += OnPageStackPropertyChanged;
		_navigateBackCommand = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_NAVIGATE_BACK), (EventHandler)OnClickNavigateBack);
		_pageStack.MaximumStackSize = 1024u;
		ZunePage page = new StartupPage();
		_pageStack.NavigateToPage(page);
		DefaultInstance = this;
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing)
		{
			DisposeManagement();
		}
		((ModelItem)this).OnDispose(disposing);
		if (disposing && DefaultInstance == this)
		{
			DefaultInstance = null;
		}
	}

	private void OnPageStackPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		string propertyName = args.PropertyName;
		switch (propertyName)
		{
		case "CurrentPage":
		case "CanNavigateBack":
		case "LastNavigationDirection":
		case "MaximumStackSize":
			((ModelItem)this).FirePropertyChanged(propertyName);
			if (propertyName == "CurrentPage" || propertyName == "CanNavigateBack")
			{
				bool available = CanNavigateBack && (CurrentPage.ShowBackArrow || CurrentPage.ShowComputerIcon != ComputerIconState.Hide || CurrentPage.ShowNowPlayingX);
				_navigateBackCommand.Available = available;
			}
			break;
		}
	}

	private void OnClickNavigateBack(object sender, EventArgs args)
	{
		SQMLog.Log((SQMDataId)144, 1);
		NavigateBack();
	}

	public void NavigateToPage(ZunePage page)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		lock (thisLock)
		{
			_navigationsToPagePending++;
		}
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredNavigateToPage), (object)page);
	}

	private void DeferredNavigateToPage(object args)
	{
		ZunePage zunePage = (ZunePage)args;
		if (CurrentPage == null || CurrentPage.CanNavigateForwardTo(zunePage))
		{
			_pageStack.NavigateToPage(zunePage);
		}
		else
		{
			zunePage.Release();
		}
		lock (thisLock)
		{
			_navigationsToPagePending--;
		}
	}

	public void NavigateBack()
	{
		NavigateBack(bypassPage: false);
	}

	public void NavigateBack(bool bypassPage)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredNavigateBack), (object)bypassPage);
	}

	private void DeferredNavigateBack(object args)
	{
		if ((bool)args || CurrentPage == null || !CurrentPage.HandleBack())
		{
			_pageStack.NavigateBack();
		}
	}

	public void Execute(string command, IDictionary commandArguments)
	{
		if (_commandHandler == null)
		{
			throw new InvalidOperationException("No CommandHandler has been registered.  Unable to resolve shell command: " + command);
		}
		_commandHandler.Execute(command, commandArguments);
	}

	public void LaunchHelp()
	{
		string command = (InternetConnection.Instance.IsConnected ? ("Web\\" + CultureHelper.GetHelpUrl()) : ("Help\\" + Shell.LoadString(StringId.IDS_ZUNECLIENT_LOCALE) + "\\help.htm"));
		Execute(command, null);
	}

	public void DisposeManagement()
	{
		if (_management != null)
		{
			((ModelItem)_management).Dispose();
			_management = null;
		}
	}

	public static MediaType MapStringToMediaType(string typeName)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected I4, but got Unknown
		return (MediaType)LibraryDataProvider.NameToMediaType(typeName);
	}

	public static MediaType MapIntToMediaType(int mediaType)
	{
		return (MediaType)mediaType;
	}

	public static int MapMediaTypeToInt(MediaType mediaType)
	{
		return (int)mediaType;
	}

	public static EMediaTypes MapMediaTypeToEMediaTypes(MediaType mediaType)
	{
		return (EMediaTypes)mediaType;
	}

	public static MediaType GetMediaTypeFromMedia(object media)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected I4, but got Unknown
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		EMediaTypes val = (EMediaTypes)(-1);
		if (media is IDatabaseMedia)
		{
			int num = default(int);
			((IDatabaseMedia)media).GetMediaIdAndType(ref num, ref val);
		}
		return (MediaType)val;
	}
}
