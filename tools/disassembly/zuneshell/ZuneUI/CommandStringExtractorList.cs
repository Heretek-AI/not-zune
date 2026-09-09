using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class CommandStringExtractorList : StringExtractorList
{
	public CommandStringExtractorList()
	{
	}

	public CommandStringExtractorList(IList source)
		: this()
	{
		base.Source = source;
	}

	protected override string ExtractString(object item)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return ((ModelItem)(Command)item).Description;
	}
}
