using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class QuickplayExperience : Experience
{
	private ArrayListDataSet _nodes;

	private Node _default;

	public override IList NodesList
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_nodes == null)
			{
				_nodes = new ArrayListDataSet((IModelItemOwner)(object)this);
				((ListDataSet)_nodes).Add((object)Default);
			}
			return (IList)_nodes;
		}
	}

	public Node Default
	{
		get
		{
			if (_default == null)
			{
				_default = new Node(this, DefaultUIPath, (SQMDataId)104);
			}
			return _default;
		}
	}

	public override string DefaultUIPath => "Quickplay\\Default";

	public QuickplayExperience(Frame frameOwner)
		: base(frameOwner, StringId.IDS_QUICKPLAY_PIVOT, (SQMDataId)0)
	{
	}
}
