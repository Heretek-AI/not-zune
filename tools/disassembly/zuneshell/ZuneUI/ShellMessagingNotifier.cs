using System.Collections;
using Microsoft.Iris;
using Microsoft.Win32;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Messaging;

namespace ZuneUI;

public class ShellMessagingNotifier : ModelItem
{
	private static ShellMessagingNotifier _instance;

	private MessagingNotifier _messagingNotifier;

	private int _newDeviceCartItemsCount;

	private int _newDeviceMessageCount;

	private IList _liveMessageItems;

	public static ShellMessagingNotifier Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ShellMessagingNotifier();
			}
			return _instance;
		}
	}

	public static bool HasInstance => _instance != null;

	public int NewDeviceCartItemsCount => _newDeviceCartItemsCount;

	public int NewDeviceMessageCount => _newDeviceMessageCount;

	public static int PersistedCartItemsCount
	{
		get
		{
			string text = $"{ClientConfiguration.Service.LastSignedInUserGuid}";
			return ((CConfigurationManagedBase)ClientConfiguration.Service).GetIntProperty(text, 0);
		}
		set
		{
			string text = $"{ClientConfiguration.Service.LastSignedInUserGuid}";
			((CConfigurationManagedBase)ClientConfiguration.Service).SetIntProperty(text, value);
		}
	}

	public static int CartItemsToUploadCount
	{
		get
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			string text = $"{ClientConfiguration.Service.LastSignedInUserGuid}";
			MessagingUserGuidConfiguration val = new MessagingUserGuidConfiguration(RegistryHive.CurrentUser, ((CConfigurationManagedBase)ClientConfiguration.Messaging).ConfigurationPath, text);
			return val.CartItemsToUploadCount;
		}
	}

	public IList LiveMessageItems
	{
		get
		{
			return _liveMessageItems;
		}
		set
		{
			if (_liveMessageItems != value)
			{
				_liveMessageItems = value;
				((ModelItem)this).FirePropertyChanged("LiveMessageItems");
			}
		}
	}

	private ShellMessagingNotifier()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		_messagingNotifier = new MessagingNotifier();
		_messagingNotifier.OnDeviceCartItemsPosted += new DeviceItemsPostedHandler(OnDeviceCartItemsPostedAsync);
		_messagingNotifier.OnDeviceMessagesPosted += new DeviceItemsPostedHandler(OnDeviceMessagesPostedAsync);
	}

	protected override void OnDispose(bool disposing)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		if (disposing)
		{
			_messagingNotifier.OnDeviceMessagesPosted -= new DeviceItemsPostedHandler(OnDeviceMessagesPostedAsync);
			_messagingNotifier.OnDeviceCartItemsPosted -= new DeviceItemsPostedHandler(OnDeviceCartItemsPostedAsync);
			_messagingNotifier.Dispose();
			_messagingNotifier = null;
		}
		((ModelItem)this).OnDispose(disposing);
	}

	private void OnDeviceCartItemsPostedAsync(int iNewDeviceCartItems)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(OnDeviceCartItemsPosted), (object)iNewDeviceCartItems);
	}

	private void OnDeviceMessagesPostedAsync(int iNewDeviceMessages)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(OnDeviceMessagesPosted), (object)iNewDeviceMessages);
	}

	private void OnDeviceCartItemsPosted(object args)
	{
		_newDeviceCartItemsCount = (int)args;
		((ModelItem)this).FirePropertyChanged("NewDeviceCartItemsCount");
	}

	private void OnDeviceMessagesPosted(object args)
	{
		_newDeviceMessageCount = (int)args;
		((ModelItem)this).FirePropertyChanged("NewDeviceMessageCount");
	}
}
