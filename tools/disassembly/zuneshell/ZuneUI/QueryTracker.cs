using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using Microsoft.Iris;
using ZuneXml;

namespace ZuneUI;

public class QueryTracker : ModelItem
{
	private struct QueryReference(string name, DataProviderQuery value, string busyString)
	{
		private WeakReference _value = new WeakReference(value);

		private string _name = name;

		private string _busyString = busyString;

		public string Name => _name;

		public DataProviderQuery Value
		{
			get
			{
				object? target = _value.Target;
				return (DataProviderQuery)((target is DataProviderQuery) ? target : null);
			}
		}

		public string BusyString => _busyString;
	}

	private List<QueryStatus> _listStatus;

	private DataProviderQueryStatus _status;

	private string _busyString;

	private List<QueryReference> _queries;

	private Dictionary<DataProviderQueryStatus, int> _statusPriority;

	private Dictionary<DataProviderQueryStatus, int> _statusCount;

	private List<object> _errorCodes;

	private PropertyChangedEventHandler _handler;

	private bool _deferredStatusCheck;

	public DataProviderQueryStatus Status
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _status;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (_status != value)
			{
				_status = value;
				((ModelItem)this).FirePropertyChanged("Status");
			}
		}
	}

	public string BusyString
	{
		get
		{
			return _busyString;
		}
		private set
		{
			if (_busyString != value)
			{
				_busyString = value;
				((ModelItem)this).FirePropertyChanged("BusyString");
			}
		}
	}

	public List<QueryStatus> ListStatus
	{
		get
		{
			return _listStatus;
		}
		private set
		{
			if (_listStatus != value)
			{
				_listStatus = value;
				((ModelItem)this).FirePropertyChanged("ListStatus");
			}
		}
	}

	public List<object> ErrorCodes
	{
		get
		{
			return _errorCodes;
		}
		private set
		{
			if (_errorCodes != value)
			{
				_errorCodes = value;
				((ModelItem)this).FirePropertyChanged("ErrorCodes");
			}
		}
	}

	public int QueryCount => _queries.Count;

	public int ErrorCount
	{
		get
		{
			_statusCount.TryGetValue((DataProviderQueryStatus)4, out var value);
			return value;
		}
	}

	public int CompleteCount
	{
		get
		{
			_statusCount.TryGetValue((DataProviderQueryStatus)3, out var value);
			return value;
		}
	}

	public int IdleCount
	{
		get
		{
			_statusCount.TryGetValue((DataProviderQueryStatus)0, out var value);
			return value;
		}
	}

	public QueryTracker()
	{
		_queries = new List<QueryReference>();
		_handler = QueryPropertyChanged;
		_statusPriority = new Dictionary<DataProviderQueryStatus, int>();
		_statusPriority[(DataProviderQueryStatus)4] = 0;
		_statusPriority[(DataProviderQueryStatus)1] = 1;
		_statusPriority[(DataProviderQueryStatus)2] = 2;
		_statusPriority[(DataProviderQueryStatus)3] = 3;
		_statusPriority[(DataProviderQueryStatus)0] = 4;
		_statusCount = new Dictionary<DataProviderQueryStatus, int>();
	}

	public void Register(string name, DataProviderQuery query)
	{
		Register(name, query, "");
	}

	public void Register(string name, DataProviderQuery query, string busyString)
	{
		if (query != null)
		{
			((INotifyPropertyChanged)query).PropertyChanged += _handler;
		}
		_queries.Add(new QueryReference(name, query, busyString));
		((ModelItem)this).FirePropertyChanged("QueryCount");
		EnqueueStatusCheck();
	}

	private void QueryPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "Status")
		{
			EnqueueStatusCheck();
		}
	}

	private void EnqueueStatusCheck()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		if (!_deferredStatusCheck)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(CheckStatus), (object)null);
			_deferredStatusCheck = true;
		}
	}

	private void CheckStatus(object args)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		_deferredStatusCheck = false;
		DataProviderQueryStatus val = (DataProviderQueryStatus)0;
		int num = _statusPriority[val];
		_statusCount = new Dictionary<DataProviderQueryStatus, int>();
		List<QueryStatus> list = new List<QueryStatus>();
		List<QueryReference> list2 = new List<QueryReference>();
		List<object> list3 = null;
		string busyString = null;
		foreach (QueryReference query in _queries)
		{
			DataProviderQuery value = query.Value;
			if (value == null)
			{
				list2.Add(query);
				continue;
			}
			if (value.IsDisposed)
			{
				list2.Add(query);
				continue;
			}
			object errorCode = GetErrorCode(value);
			if (errorCode != null)
			{
				if (list3 == null)
				{
					list3 = new List<object>();
				}
				list3.Add(errorCode);
			}
			DataProviderQueryStatus val2 = value.Status;
			if (value == null)
			{
				val2 = (DataProviderQueryStatus)0;
			}
			int num2 = _statusPriority[val2];
			if (num2 < num)
			{
				val = val2;
				num = num2;
			}
			list.Add(new QueryStatus(query.Name, val2));
			int value2 = 0;
			_statusCount.TryGetValue(val2, out value2);
			_statusCount[val2] = value2 + 1;
			if (QueryHelper.IsBusy(value.Status))
			{
				busyString = query.BusyString;
			}
		}
		foreach (QueryReference item in list2)
		{
			_queries.Remove(item);
			((ModelItem)this).FirePropertyChanged("QueryCount");
		}
		Status = val;
		ListStatus = list;
		ErrorCodes = list3;
		BusyString = busyString;
		((ModelItem)this).FirePropertyChanged("ErrorCount");
		((ModelItem)this).FirePropertyChanged("CompleteCount");
		((ModelItem)this).FirePropertyChanged("IdleCount");
	}

	public static object GetErrorCode(DataProviderQuery query)
	{
		if (query is XmlDataProviderQuery xmlDataProviderQuery)
		{
			return xmlDataProviderQuery.ErrorCode;
		}
		return null;
	}

	public static bool Is404(object errorCode)
	{
		if (errorCode is HttpStatusCode)
		{
			return (HttpStatusCode)errorCode == HttpStatusCode.NotFound;
		}
		return false;
	}

	public static bool Is410(object errorCode)
	{
		if (errorCode is HttpStatusCode)
		{
			return (HttpStatusCode)errorCode == HttpStatusCode.Gone;
		}
		return false;
	}

	public static bool Is407(object errorCode)
	{
		if (errorCode is HttpStatusCode)
		{
			return (HttpStatusCode)errorCode == HttpStatusCode.ProxyAuthenticationRequired;
		}
		return false;
	}
}
