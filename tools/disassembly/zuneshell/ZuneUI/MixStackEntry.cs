using System.Collections;

namespace ZuneUI;

public class MixStackEntry
{
	private MixResult _result;

	private object _layout;

	private IList _dataList;

	public MixResult Result => _result;

	public object Layout => _layout;

	public IList DataList => _dataList;

	public MixStackEntry(MixResult result, object layout, IList dataList)
	{
		_result = result;
		_layout = layout;
		_dataList = dataList;
	}
}
