using Microsoft.Iris;

namespace ZuneUI;

public class TestPageState : ModelItem, IPageState
{
	private bool _isInvalid;

	private bool _canBeTrimmed;

	private string _mainUI;

	private string _backgroundUI;

	public bool CanBeTrimmed
	{
		get
		{
			return _canBeTrimmed;
		}
		set
		{
			_canBeTrimmed = value;
		}
	}

	public TestPageState(string description, string mainUI, string backgroundUI, bool canBeTrimmed)
	{
		((ModelItem)this).Description = description;
		_mainUI = mainUI;
		_backgroundUI = backgroundUI;
		_canBeTrimmed = canBeTrimmed;
	}

	public IPage RestoreAndRelease()
	{
		TestPage testPage = new TestPage();
		((ModelItem)testPage).Description = ((ModelItem)this).Description;
		testPage.UI = _mainUI;
		testPage.BackgroundUI = _backgroundUI;
		Release();
		return testPage;
	}

	public void Release()
	{
		_isInvalid = true;
	}
}
