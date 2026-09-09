namespace ZuneUI;

public abstract class FutureBase<T>
{
	private T _value;

	private bool _calculationPerformed;

	public T Value
	{
		get
		{
			if (!_calculationPerformed)
			{
				_value = CalculateValue();
				_calculationPerformed = true;
			}
			return _value;
		}
	}

	protected abstract T CalculateValue();
}
