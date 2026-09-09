using System;
using System.Net.NetworkInformation;

namespace ZuneUI;

public class InternetConnection : NotifyPropertyChangedImpl
{
	private static InternetConnection _instance;

	private bool _isConnected;

	private bool _initializationSucceeded;

	public static InternetConnection Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new InternetConnection();
			}
			return _instance;
		}
	}

	public bool IsConnected
	{
		get
		{
			if (!_initializationSucceeded)
			{
				IsConnected = Win32InternetConnection.IsConnected;
			}
			return _isConnected;
		}
		private set
		{
			if (_isConnected != value)
			{
				_isConnected = value;
				FirePropertyChanged("IsConnected");
			}
		}
	}

	private InternetConnection()
	{
		Initialize();
	}

	private void Initialize()
	{
		try
		{
			_isConnected = NetworkInterface.GetIsNetworkAvailable();
			NetworkChange.NetworkAvailabilityChanged += OnNetworkAvailabilityChanged;
			_initializationSucceeded = true;
		}
		catch (Exception)
		{
			_initializationSucceeded = false;
		}
	}

	private void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
	{
		IsConnected = e.IsAvailable;
	}
}
