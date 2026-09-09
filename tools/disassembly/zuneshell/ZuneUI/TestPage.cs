using Microsoft.Iris;

namespace ZuneUI;

public class TestPage : ZunePage
{
	private int _refCount;

	private bool _saveToBackStack;

	private bool _canBeTrimmed;

	private bool _sharedInstance;

	public bool SaveToBackStack
	{
		get
		{
			return _saveToBackStack;
		}
		set
		{
			if (_saveToBackStack != value)
			{
				_saveToBackStack = value;
				((ModelItem)this).FirePropertyChanged("SaveToBackStack");
			}
		}
	}

	public bool SharedInstance
	{
		get
		{
			return _sharedInstance;
		}
		set
		{
			if (_sharedInstance != value)
			{
				_sharedInstance = value;
				((ModelItem)this).FirePropertyChanged("SharedInstance");
			}
		}
	}

	public bool CanBeTrimmed
	{
		get
		{
			return _canBeTrimmed;
		}
		set
		{
			if (_canBeTrimmed != value)
			{
				_canBeTrimmed = value;
				((ModelItem)this).FirePropertyChanged("CanBeTrimmed");
			}
		}
	}

	protected bool IsValid => _refCount > 0;

	public TestPage()
	{
		_saveToBackStack = true;
		_canBeTrimmed = true;
	}

	public override IPageState SaveAndRelease()
	{
		IPageState result = null;
		if (_saveToBackStack)
		{
			if (_sharedInstance)
			{
				result = new InstancePageState(this);
			}
			else
			{
				result = new TestPageState(((ModelItem)this).Description, base.UI, base.BackgroundUI, CanBeTrimmed);
				Release();
			}
		}
		else
		{
			Release();
		}
		return result;
	}

	public override void Release()
	{
		_refCount--;
	}

	protected override void OnNavigatedToWorker()
	{
		if (IsValid)
		{
			_ = _sharedInstance;
		}
		_refCount++;
		base.OnNavigatedToWorker();
	}

	protected override void OnNavigatedAwayWorker(IPage destination)
	{
		base.OnNavigatedAwayWorker(destination);
	}
}
