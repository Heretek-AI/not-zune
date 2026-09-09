using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Iris;

namespace ZuneXml;

public class XmlDataVirtualList : VirtualList, IXmlDataProviderObject
{
	private class GetNextChunkArgs
	{
		public readonly XmlDataProviderQuery Query;

		public readonly IPageInfo PageInfo;

		public readonly int StartIndex;

		public GetNextChunkArgs(XmlDataProviderQuery query, IPageInfo pageInfo, int startIndex)
		{
			Query = query;
			PageInfo = pageInfo;
			StartIndex = startIndex;
		}
	}

	private DataProviderQuery _owner;

	private int _currentIndex;

	private object _itemTypeCookie;

	private ConstructObject _itemConstructor;

	private int _chunkStartIndex;

	private List<XmlDataProviderObject> _chunkItems;

	private string _encodedSortBy;

	private string[] _sortBy;

	private bool[] _sortAscending;

	private IPageInfo _nextPage;

	private bool _pageToEnd;

	internal IPageInfo NextPage
	{
		get
		{
			return _nextPage;
		}
		set
		{
			_nextPage = value;
		}
	}

	public bool PageToEnd
	{
		get
		{
			return _pageToEnd;
		}
		set
		{
			if (_pageToEnd != value)
			{
				_pageToEnd = value;
				if (_pageToEnd)
				{
					GetNextChunk();
				}
			}
		}
	}

	public string SortBy
	{
		get
		{
			return _encodedSortBy;
		}
		set
		{
			if (!(_encodedSortBy != value))
			{
				return;
			}
			string[] array = null;
			bool[] array2 = null;
			if (!string.IsNullOrEmpty(value))
			{
				string[] array3 = value.Split(new char[1] { ',' });
				array = new string[array3.Length];
				array2 = new bool[array3.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					ExtractSortData(array3[i], out array[i], out array2[i]);
				}
			}
			_sortBy = array;
			_sortAscending = array2;
			Sort();
		}
	}

	public XmlDataVirtualList(DataProviderQuery owner, object itemTypeCookie)
		: base(true)
	{
		_owner = owner;
		_itemTypeCookie = itemTypeCookie;
		if (itemTypeCookie != null)
		{
			_itemConstructor = XmlDataProviderObjectFactory.GetConstructor(itemTypeCookie);
		}
		else
		{
			_itemConstructor = null;
		}
		_currentIndex = -1;
		_chunkStartIndex = 0;
		_encodedSortBy = null;
		_sortAscending = null;
		_sortBy = null;
	}

	protected override object OnRequestItem(int index)
	{
		return ((ModelItem)this).Data[index];
	}

	protected override void OnRequestSlowData(int index)
	{
		((VirtualList)this).NotifySlowDataAcquireComplete(index);
		if (index == ((VirtualList)this).Count - 1 && _chunkStartIndex != ((VirtualList)this).Count && NextPage != null && !PageToEnd)
		{
			GetNextChunk();
		}
	}

	private void GetNextChunk()
	{
		if (NextPage != null)
		{
			_chunkStartIndex = ((VirtualList)this).Count;
			_currentIndex = -1;
			if (_owner is XmlDataProviderQuery query)
			{
				ThreadPool.QueueUserWorkItem(GetNextChunkThread, new GetNextChunkArgs(query, NextPage, _chunkStartIndex));
			}
		}
	}

	private static void GetNextChunkThread(object obj)
	{
		GetNextChunkArgs getNextChunkArgs = (GetNextChunkArgs)obj;
		string pageUrl = getNextChunkArgs.PageInfo.GetPageUrl(getNextChunkArgs.StartIndex);
		string pagePostBody = getNextChunkArgs.PageInfo.GetPagePostBody(getNextChunkArgs.StartIndex);
		if (!string.IsNullOrEmpty(pageUrl))
		{
			getNextChunkArgs.Query.GetDataFromResource(pageUrl, pagePostBody, fNewGeneration: false);
		}
	}

	public bool ProcessXPath(string currentXPath, Hashtable attributes, List<XmlDataProviderQuery.XPathMatch> matches)
	{
		bool result = false;
		if (string.IsNullOrEmpty(currentXPath))
		{
			if (_chunkItems == null)
			{
				_chunkItems = new List<XmlDataProviderObject>();
			}
			_currentIndex++;
			if (_itemConstructor != null)
			{
				_chunkItems.Add(_itemConstructor(_owner, _itemTypeCookie));
			}
			else
			{
				_chunkItems.Add(new XmlDataProviderObject(_owner, _itemTypeCookie));
			}
		}
		else
		{
			IXmlDataProviderObject xmlDataProviderObject = _chunkItems[_currentIndex];
			if (xmlDataProviderObject != null)
			{
				result = xmlDataProviderObject.ProcessXPath(currentXPath, attributes, matches);
			}
		}
		return result;
	}

	internal void TransferToAppThread()
	{
		if (_chunkItems == null)
		{
			return;
		}
		foreach (XmlDataProviderObject chunkItem in _chunkItems)
		{
			chunkItem.TransferToAppThread();
		}
		int num = 0;
		foreach (XmlDataProviderObject chunkItem2 in _chunkItems)
		{
			if (!(_owner is XmlDataProviderQuery xmlDataProviderQuery) || !xmlDataProviderQuery.FilterDataProviderObject(chunkItem2))
			{
				((ModelItem)this).Data[_chunkStartIndex + num++] = chunkItem2;
			}
		}
		((VirtualList)this).AddRange(num);
		Sort();
		_chunkItems.Clear();
	}

	internal void OnQueryComplete()
	{
		if (NextPage != null && PageToEnd)
		{
			GetNextChunk();
		}
	}

	private void Sort()
	{
		if (_sortBy == null || _sortBy.Length <= 0 || ((VirtualList)this).Count <= 1)
		{
			return;
		}
		_ = NextPage;
		XmlDataProviderObject[] array = new XmlDataProviderObject[((VirtualList)this).Count];
		if (array != null)
		{
			for (int i = 0; i < ((VirtualList)this).Count; i++)
			{
				array[i] = (XmlDataProviderObject)((ModelItem)this).Data[i];
			}
			Array.Sort(array, CompareUsingSortBy);
			for (int j = 0; j < ((VirtualList)this).Count; j++)
			{
				((ModelItem)this).Data[j] = array[j];
				((VirtualList)this).Modified(j);
			}
		}
	}

	private void ExtractSortData(string sort, out string filteredValue, out bool sortAscending)
	{
		filteredValue = sort;
		sortAscending = true;
		if (!string.IsNullOrEmpty(sort))
		{
			if (sort[0] == '-')
			{
				filteredValue = sort.Substring(1);
				sortAscending = false;
			}
			else if (sort[0] == '+')
			{
				filteredValue = sort.Substring(1);
				sortAscending = true;
			}
		}
	}

	private int CompareUsingSortBy(XmlDataProviderObject x, XmlDataProviderObject y)
	{
		int num = 0;
		for (int i = 0; i < _sortBy.Length; i++)
		{
			num = Compare(x, y, _sortBy[i], _sortAscending[i]);
			if (num != 0)
			{
				break;
			}
		}
		return num;
	}

	private static int Compare(XmlDataProviderObject x, XmlDataProviderObject y, string propertyName, bool ascending)
	{
		object property = ((DataProviderObject)x).GetProperty(propertyName);
		object property2 = ((DataProviderObject)y).GetProperty(propertyName);
		IComparable comparable = property as IComparable;
		IComparable comparable2 = property2 as IComparable;
		if (comparable != null && comparable2 != null)
		{
			int num = comparable.CompareTo(comparable2);
			if (ascending)
			{
				return num;
			}
			return -num;
		}
		return 0;
	}
}
