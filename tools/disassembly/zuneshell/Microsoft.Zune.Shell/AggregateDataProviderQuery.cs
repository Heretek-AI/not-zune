using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Iris;

namespace Microsoft.Zune.Shell;

public class AggregateDataProviderQuery : DataProviderQuery
{
	private List<DataProviderQuery> _currentQueries = new List<DataProviderQuery>();

	internal static void Register()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		Application.RegisterDataProvider("Aggregate", new DataProviderQueryFactory(ConstructAggregateDataProviderQuery));
	}

	internal static DataProviderQuery ConstructAggregateDataProviderQuery(object queryTypeCookie)
	{
		return (DataProviderQuery)(object)new AggregateDataProviderQuery(queryTypeCookie);
	}

	protected AggregateDataProviderQuery(object queryTypeCookie)
		: base(queryTypeCookie)
	{
	}

	protected override void BeginExecute()
	{
		InitializeCurrentQueries();
		RefreshCurrentQueries();
	}

	private void InitializeCurrentQueries()
	{
		((DataProviderQuery)this).Result = null;
		UnsubscribeFromCurrentQueries();
		_currentQueries.Clear();
		if (((DataProviderQuery)this).GetProperty("Queries") is IList list)
		{
			foreach (object item in list)
			{
				DataProviderQuery val = (DataProviderQuery)((item is DataProviderQuery) ? item : null);
				if (val != null)
				{
					_currentQueries.Add(val);
				}
			}
		}
		SubscribeToCurrentQueries();
	}

	private void RefreshCurrentQueries()
	{
		foreach (DataProviderQuery currentQuery in _currentQueries)
		{
			currentQuery.Refresh();
		}
	}

	private void SubscribeToCurrentQueries()
	{
		foreach (DataProviderQuery currentQuery in _currentQueries)
		{
			currentQuery.PropertyChanged += OnQueryPropertyChanged;
		}
	}

	private void UnsubscribeFromCurrentQueries()
	{
		foreach (DataProviderQuery currentQuery in _currentQueries)
		{
			currentQuery.PropertyChanged -= OnQueryPropertyChanged;
		}
	}

	private void OnQueryPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if ("Status" == args.PropertyName)
		{
			UpdateStatusAndResult();
		}
	}

	private void UpdateStatusAndResult()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Invalid comparison between I4 and Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Invalid comparison between I4 and Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Invalid comparison between I4 and Unknown
		int num = 0;
		bool flag = false;
		foreach (DataProviderQuery currentQuery in _currentQueries)
		{
			if (3 == (int)currentQuery.Status || 4 == (int)currentQuery.Status)
			{
				num++;
				if (4 == (int)currentQuery.Status)
				{
					flag = true;
				}
			}
		}
		if (_currentQueries.Count == num)
		{
			((DataProviderQuery)this).Status = (DataProviderQueryStatus)2;
			ArrayList arrayList = new ArrayList();
			foreach (DataProviderQuery currentQuery2 in _currentQueries)
			{
				arrayList.Add(currentQuery2.Result);
			}
			((DataProviderQuery)this).Result = arrayList;
			((DataProviderQuery)this).Status = (DataProviderQueryStatus)(flag ? 4 : 3);
		}
		else
		{
			((DataProviderQuery)this).Status = (DataProviderQueryStatus)1;
		}
	}

	protected override void OnDispose()
	{
		UnsubscribeFromCurrentQueries();
		((DataProviderQuery)this).OnDispose();
	}
}
