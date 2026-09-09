namespace ZuneUI;

public class SyncabilityHelper
{
	private UIDevice _device;

	private DelegateFuture<bool> _isGuest;

	private DelegateFuture<int> _userId;

	private DelegateFuture<bool> _supportsUserCards;

	private DelegateFuture<bool> _supportsChannels;

	private DelegateFuture<bool> _supportsApplications;

	private DelegateFuture<bool> _supportsRental;

	private DelegateFuture<bool> _supportsHD;

	private DelegateFuture<bool> _isUnlinked;

	private DelegateFuture<bool> _canSyncUserCards;

	private DelegateFuture<bool> _canSyncChannels;

	private DelegateFuture<bool> _canSyncApplications;

	private DelegateFuture<bool> _canSyncRentalVideo;

	private DelegateFuture<bool> _canSyncHDRentalVideo;

	public bool IsGuest => _isGuest.Value;

	public int UserID => _userId.Value;

	public bool SupportsUserCards => _supportsUserCards.Value;

	public bool SupportsChannels => _supportsChannels.Value;

	public bool SupportsSyncApplications => _supportsApplications.Value;

	public bool SupportsRental => _supportsRental.Value;

	public bool SupportsHD => _supportsHD.Value;

	public bool IsUnlinked => _isUnlinked.Value;

	public bool CanSyncUserCards => _canSyncUserCards.Value;

	public bool CanSyncChannels => _canSyncChannels.Value;

	public bool CanSyncApplications => _canSyncApplications.Value;

	public bool CanSyncRentalVideo => _canSyncRentalVideo.Value;

	public bool CanSyncHDRentalVideo => _canSyncHDRentalVideo.Value;

	public SyncabilityHelper(UIDevice device)
	{
		_device = device ?? UIDeviceList.NullDevice;
		_isGuest = new DelegateFuture<bool>(() => _device.IsGuest);
		_userId = new DelegateFuture<int>(() => _device.UserId);
		_supportsUserCards = new DelegateFuture<bool>(() => _device.SupportsUserCards);
		_supportsChannels = new DelegateFuture<bool>(() => _device.SupportsChannels);
		_supportsApplications = new DelegateFuture<bool>(() => _device.SupportsSyncApplications);
		_supportsRental = new DelegateFuture<bool>(() => _device.SupportsRental);
		_supportsHD = new DelegateFuture<bool>(() => _device.SupportsHD);
		_isUnlinked = new DelegateFuture<bool>(() => UserID == 0);
		_canSyncUserCards = new DelegateFuture<bool>(GetCanSyncUserCards);
		_canSyncChannels = new DelegateFuture<bool>(GetCanSyncChannels);
		_canSyncApplications = new DelegateFuture<bool>(GetCanSyncApplications);
		_canSyncRentalVideo = new DelegateFuture<bool>(GetCanSyncRentalVideo);
		_canSyncHDRentalVideo = new DelegateFuture<bool>(GetCanSyncHDRentalVideo);
	}

	private bool GetCanSyncUserCards()
	{
		if (!IsGuest && UserID == SignIn.Instance.LastSignedInUserId)
		{
			return SupportsUserCards;
		}
		return false;
	}

	private bool GetCanSyncChannels()
	{
		if (!IsGuest && UserID == SignIn.Instance.LastSignedInUserId && SignIn.Instance.LastSignedInUserHadActiveSubscription)
		{
			return SupportsChannels;
		}
		return false;
	}

	private bool GetCanSyncApplications()
	{
		return SupportsSyncApplications;
	}

	private bool GetCanSyncRentalVideo()
	{
		return SupportsRental;
	}

	private bool GetCanSyncHDRentalVideo()
	{
		if (CanSyncRentalVideo)
		{
			return SupportsHD;
		}
		return false;
	}
}
