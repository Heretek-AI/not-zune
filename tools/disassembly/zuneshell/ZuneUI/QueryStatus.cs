using Microsoft.Iris;

namespace ZuneUI;

public class QueryStatus
{
	private string _name;

	private DataProviderQueryStatus _status;

	public string Title => _name;

	public DataProviderQueryStatus Status => _status;

	public QueryStatus(string name, DataProviderQueryStatus status)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		_name = name;
		_status = status;
	}
}
