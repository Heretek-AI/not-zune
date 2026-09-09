namespace ZuneUI;

public enum DrmState
{
	Unknown = 0,
	NoLicense = 10,
	DeviceLicense = 23,
	Expired = 20,
	Protected = 30,
	Free = 40,
	Expiring = 26
}
