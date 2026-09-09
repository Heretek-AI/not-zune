using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class DataObjectStringExtractorList : StringExtractorList
{
	private string _property;

	public string Property
	{
		get
		{
			return _property;
		}
		set
		{
			if (_property != value)
			{
				_property = value;
				Reset();
			}
		}
	}

	public DataObjectStringExtractorList()
	{
		base.CanSearchForString = true;
	}

	public DataObjectStringExtractorList(IList source, string property)
		: this()
	{
		base.Source = source;
		Property = property;
	}

	protected override string ExtractString(object item)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		if (_property != null)
		{
			return ((DataProviderObject)item).GetProperty(_property)?.ToString();
		}
		return base.ExtractString(item);
	}
}
