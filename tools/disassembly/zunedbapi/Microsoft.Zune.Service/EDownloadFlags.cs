namespace Microsoft.Zune.Service;

public enum EDownloadFlags
{
	Stream = 64,
	None = 0,
	Subscription = 16,
	HD = 128,
	Rental = 32,
	DeviceLicensed = 8,
	CanBeOffline = 4,
	UserCard = 2,
	Channel = 1
}
