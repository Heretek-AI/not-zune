namespace ZuneUI;

internal class DatapointInfo
{
	private ETelemetryEvent _event;

	private string _typeName;

	private bool _fSession;

	public ETelemetryEvent Event
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _event;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_event = value;
		}
	}

	public string TypeName
	{
		get
		{
			return _typeName;
		}
		set
		{
			_typeName = value;
		}
	}

	public bool IsSession
	{
		get
		{
			return _fSession;
		}
		set
		{
			_fSession = value;
		}
	}

	public DatapointInfo(ETelemetryEvent evt)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		_event = evt;
		_typeName = "";
		_fSession = false;
	}

	public DatapointInfo(ETelemetryEvent evt, string typeName, bool fSess)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		_event = evt;
		_typeName = typeName;
		_fSession = fSess;
	}
}
