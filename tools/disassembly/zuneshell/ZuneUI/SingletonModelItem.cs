using Microsoft.Iris;

namespace ZuneUI;

public abstract class SingletonModelItem<T> : ModelItem where T : SingletonModelItem<T>, new()
{
	private static T _singletonInstance;

	public static T Instance
	{
		get
		{
			if (_singletonInstance == null)
			{
				_singletonInstance = new T();
			}
			return _singletonInstance;
		}
	}

	protected SingletonModelItem()
		: this((IModelItemOwner)(object)ZuneShell.DefaultInstance)
	{
	}

	protected SingletonModelItem(IModelItemOwner parent)
		: base(parent)
	{
	}
}
