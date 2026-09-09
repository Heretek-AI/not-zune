using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class PageStack : ModelItem
{
	private IPage _currentPage;

	private ArrayList _pageStack;

	private NavigationDirection _navDirection;

	private uint _maxStackSize;

	public IPage CurrentPage => _currentPage;

	public bool CanNavigateBack => _pageStack.Count > 0;

	public uint MaximumStackSize
	{
		get
		{
			return _maxStackSize;
		}
		set
		{
			if (_maxStackSize != value)
			{
				_maxStackSize = value;
				((ModelItem)this).FirePropertyChanged("MaximumStackSize");
				TrimStackSize();
			}
		}
	}

	public NavigationDirection LastNavigationDirection => _navDirection;

	public PageStack()
		: this(null)
	{
	}

	public PageStack(IModelItemOwner owner)
		: base(owner)
	{
		_pageStack = new ArrayList();
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing)
		{
			if (_currentPage != null)
			{
				_currentPage.OnNavigatedAway(null);
				_currentPage.Release();
				_currentPage = null;
			}
			if (_pageStack != null)
			{
				foreach (IPageState item in _pageStack)
				{
					item.Release();
				}
				_pageStack = null;
			}
		}
		((ModelItem)this).OnDispose(disposing);
	}

	private void SetLastNavigationDirection(NavigationDirection value)
	{
		if (_navDirection != value)
		{
			_navDirection = value;
			((ModelItem)this).FirePropertyChanged("LastNavigationDirection");
		}
	}

	public void NavigateToPage(IPage page)
	{
		if (page == null)
		{
			throw new ArgumentNullException("page");
		}
		if (_currentPage != null)
		{
			_currentPage.OnNavigatedAway(page);
			PushToStack(_currentPage);
		}
		SetCurrentPage(page);
		SetLastNavigationDirection(NavigationDirection.Forward);
		TrimStackSize();
	}

	public void NavigateBack()
	{
		IPage page = null;
		while (CanNavigateBack && page == null)
		{
			int index = _pageStack.Count - 1;
			IPageState pageState = (IPageState)_pageStack[index];
			_pageStack.RemoveAt(index);
			if (pageState != null)
			{
				page = pageState.RestoreAndRelease();
			}
			if (pageState == null)
			{
			}
		}
		if (!CanNavigateBack)
		{
			((ModelItem)this).FirePropertyChanged("CanNavigateBack");
		}
		if (page != null)
		{
			_currentPage.OnNavigatedAway(page);
			_currentPage.Release();
			SetCurrentPage(page);
			SetLastNavigationDirection(NavigationDirection.Back);
		}
	}

	public void PushToStack(IPage page)
	{
		if (page == null)
		{
			throw new ArgumentNullException("page");
		}
		IPageState pageState = page.SaveAndRelease();
		if (pageState != null)
		{
			_pageStack.Add(pageState);
			if (_pageStack.Count == 1)
			{
				((ModelItem)this).FirePropertyChanged("CanNavigateBack");
			}
		}
	}

	private void SetCurrentPage(IPage page)
	{
		_currentPage = page;
		((ModelItem)this).FirePropertyChanged("CurrentPage");
		_currentPage.OnNavigatedTo();
	}

	private void TrimStackSize()
	{
		if (_maxStackSize == 0)
		{
			return;
		}
		int num = _pageStack.Count - (int)_maxStackSize;
		if (num <= 0)
		{
			return;
		}
		int num2 = 0;
		while (num > 0 && num2 < _pageStack.Count)
		{
			IPageState pageState = (IPageState)_pageStack[num2];
			if (pageState.CanBeTrimmed)
			{
				_pageStack.RemoveAt(num2);
				pageState.Release();
				num--;
			}
			else
			{
				num2++;
			}
		}
	}
}
