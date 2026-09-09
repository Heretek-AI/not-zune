namespace ZuneUI;

public struct FallibleEventArgs(HRESULT hr)
{
	public readonly HRESULT HR = hr;
}
