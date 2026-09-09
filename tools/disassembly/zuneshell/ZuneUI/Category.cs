using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class Category : ModelItem
{
	private string _target;

	private bool _allowScrolling;

	private SQMDataId _sqmCountID;

	public string Target => _target;

	public bool AllowScrolling => _allowScrolling;

	public Category(StringId titleID, string target)
		: this(titleID, target, allowScrolling: true, (SQMDataId)0)
	{
	}

	public Category(StringId titleID, string target, bool allowScrolling)
		: this(titleID, target, allowScrolling, (SQMDataId)0)
	{
		_target = target;
		_allowScrolling = allowScrolling;
	}

	public Category(StringId titleID, string target, bool allowScrolling, SQMDataId sqmCountID)
		: base((IModelItemOwner)null, Shell.LoadString(titleID))
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		_target = target;
		_allowScrolling = allowScrolling;
		_sqmCountID = sqmCountID;
	}

	public void LogCategoryView()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		if ((int)_sqmCountID != 0)
		{
			SQMLog.Log(_sqmCountID, 1);
		}
	}
}
