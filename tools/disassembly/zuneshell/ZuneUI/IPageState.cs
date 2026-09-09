namespace ZuneUI;

public interface IPageState
{
	bool CanBeTrimmed { get; }

	IPage RestoreAndRelease();

	void Release();
}
