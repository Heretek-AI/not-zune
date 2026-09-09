namespace ZuneUI;

public class MixTileSaveData
{
	private MixResult _result;

	private object _size;

	private object _position;

	private object _seedSize;

	public MixResult Result => _result;

	public object Size => _size;

	public object Position => _position;

	public object SeedSize => _seedSize;

	public MixTileSaveData(MixResult result, object size, object position, object seedSize)
	{
		_result = result;
		_size = size;
		_position = position;
		_seedSize = seedSize;
	}
}
