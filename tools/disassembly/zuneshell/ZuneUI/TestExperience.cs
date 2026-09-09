using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class TestExperience : Experience
{
	private ArrayListDataSet _nodes;

	private Node _stringTester;

	private Node _webHostTester;

	private bool _pivotHasBeenShownBefore;

	public override IList NodesList
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_nodes == null)
			{
				_nodes = new ArrayListDataSet((IModelItemOwner)(object)this);
				((ListDataSet)_nodes).Add((object)StringTester);
				((ListDataSet)_nodes).Add((object)WebHost);
			}
			return (IList)_nodes;
		}
	}

	public Node StringTester
	{
		get
		{
			if (_stringTester == null)
			{
				_stringTester = new Node(this, "string tester", "Test\\StringTester\\Home");
			}
			return _stringTester;
		}
	}

	public Node WebHost
	{
		get
		{
			if (_webHostTester == null)
			{
				_webHostTester = new Node(this, "WebHost", "Test\\WebHost\\Home");
			}
			return _webHostTester;
		}
	}

	public TestExperience(Frame frameOwner)
		: base(frameOwner, StringId.IDS_TEST_PIVOT, (SQMDataId)0)
	{
		_pivotHasBeenShownBefore = false;
	}

	protected override void OnIsCurrentChanged()
	{
		UpdateShowTest();
	}

	public void UpdateShowTest()
	{
		bool flag = base.IsCurrent || _pivotHasBeenShownBefore;
		((MainFrame)base.Frame).ShowTest(flag);
		if (flag)
		{
			_pivotHasBeenShownBefore = true;
		}
	}
}
