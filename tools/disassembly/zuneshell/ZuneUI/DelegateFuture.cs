namespace ZuneUI;

public class DelegateFuture<T> : FutureBase<T>
{
	private CalculateValue<T> _delegate;

	public DelegateFuture(CalculateValue<T> calculateMethod)
	{
		_delegate = calculateMethod;
	}

	protected override T CalculateValue()
	{
		return _delegate();
	}
}
