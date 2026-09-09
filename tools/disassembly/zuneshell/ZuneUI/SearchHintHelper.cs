using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class SearchHintHelper : AutoCompleteHelper
{
	private int _resultsQueueCount;

	private Hashtable _resultsCache = new Hashtable();

	private DataProviderTitleList _mergedTitles;

	private Timer _updateTimer;

	private bool _blockResults = true;

	private bool _filterForKeyword;

	public bool BlockResults
	{
		get
		{
			return _blockResults;
		}
		set
		{
			if (_blockResults != value)
			{
				_blockResults = value;
			}
		}
	}

	public bool FilterForKeyword
	{
		get
		{
			return _filterForKeyword;
		}
		set
		{
			if (_filterForKeyword != value)
			{
				_filterForKeyword = value;
			}
		}
	}

	public SearchHintHelper()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		base.UseEntrySeparator = false;
		base.Applyfilter = false;
		_updateTimer = new Timer();
		_updateTimer.AutoRepeat = false;
		_updateTimer.Interval = 150;
		_updateTimer.Tick += OnUpdateTimerTick;
	}

	public void ClearResultsByType(SearchHintResultType resultType)
	{
		_resultsCache[resultType] = new List<string>();
	}

	public virtual void MergeResults(IList queryResults, SearchHintResultType resultType)
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		if (queryResults == null)
		{
			return;
		}
		DataProviderTitleList dataProviderTitleList = new DataProviderTitleList();
		for (int i = 0; i < queryResults.Count; i++)
		{
			object? obj = queryResults[i];
			DataProviderObject val = (DataProviderObject)((obj is DataProviderObject) ? obj : null);
			string text = (string)val.GetProperty("Title");
			if (!string.IsNullOrEmpty(text) && !dataProviderTitleList.Contains(text) && (!FilterForKeyword || ContainsKeyword(text)))
			{
				dataProviderTitleList.DataProviders.Add(val);
			}
		}
		_resultsCache[resultType] = dataProviderTitleList;
		_resultsQueueCount++;
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			HandleResultsQueue();
		}, (object)null);
	}

	private bool ContainsKeyword(string title)
	{
		title = title.ToLower();
		string[] array = base.Entry.Split(new char[1] { ' ' });
		bool result = false;
		for (int i = 0; i < array.Length; i++)
		{
			string value = array[i].ToLower();
			if (!string.IsNullOrEmpty(value) && title.Contains(value))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public object MergeSearchResultDataDelegate(object item1, object item2)
	{
		if (item1 == null || item2 is LibraryDataProviderListItem)
		{
			return item2;
		}
		return item1;
	}

	private void HandleResultsQueue()
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		if (_resultsQueueCount == 0)
		{
			return;
		}
		_resultsQueueCount--;
		_mergedTitles = new DataProviderTitleList();
		foreach (DictionaryEntry item in _resultsCache)
		{
			DataProviderTitleList dataProviderTitleList = item.Value as DataProviderTitleList;
			_mergedTitles.InitializeDataProviders(ListHelper.Merge(_mergedTitles.DataProviders, dataProviderTitleList?.DataProviders, matchesOnly: false, new SearchResultDataComparer(), MergeSearchResultDataDelegate));
		}
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			HandleResultsQueue();
		}, (object)null);
		_updateTimer.Enabled = true;
	}

	private void OnUpdateTimerTick(object sender, EventArgs args)
	{
		base.Options = _mergedTitles;
	}
}
