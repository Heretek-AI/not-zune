using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Iris;

namespace ZuneUI;

public abstract class Wizard : ModelItem
{
	private List<WizardPage> _pages;

	private int _currentPageIndex;

	private EventHandler _cancelCommandHandler;

	private HRESULT _error;

	private bool _errorPageIsEnabled;

	protected BreadcrumbFactory _breadcrumbFactory;

	private bool _finished;

	private IDictionary<ProxySettingDelegate, object> _settings;

	public IList Breadcrumbs
	{
		get
		{
			if (_breadcrumbFactory == null)
			{
				return null;
			}
			return _breadcrumbFactory.Breadcrumbs;
		}
	}

	public virtual bool IsValid
	{
		get
		{
			foreach (WizardPage page in _pages)
			{
				if (page.IsEnabled && !page.IsValid)
				{
					return false;
				}
			}
			return true;
		}
	}

	public bool ShowNavigation
	{
		get
		{
			if (CurrentPage != null)
			{
				return CurrentPage.ShowNavigation;
			}
			return false;
		}
	}

	public bool ShowClose
	{
		get
		{
			if (CurrentPage != null)
			{
				return CurrentPage.ShowClose;
			}
			return false;
		}
	}

	public bool CanAdvancePageIndex => GetNextEnabledPageIndex() != -1;

	public virtual bool CanMoveNext
	{
		get
		{
			if (CurrentPage.IsValid)
			{
				return CanAdvancePageIndex;
			}
			return false;
		}
	}

	public virtual bool CanStart => true;

	public IList Pages => _pages.AsReadOnly();

	public HRESULT Error
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _error;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (_error != value)
			{
				_error = value;
				((ModelItem)this).FirePropertyChanged("Error");
			}
		}
	}

	public bool ErrorPageIsEnabled
	{
		get
		{
			return _errorPageIsEnabled;
		}
		protected set
		{
			if (_errorPageIsEnabled != value)
			{
				_errorPageIsEnabled = value;
				((ModelItem)this).FirePropertyChanged("ErrorPageIsEnabled");
			}
		}
	}

	public virtual bool CanMoveBack => GetPrevEnabledPageIndex() != -1;

	public virtual bool CanCommitChanges
	{
		get
		{
			if (IsValid)
			{
				return !CanAdvancePageIndex;
			}
			return false;
		}
	}

	public virtual bool CanCancel
	{
		get
		{
			if (HasPages && CurrentPage != null)
			{
				return CurrentPage.CanCancel;
			}
			return true;
		}
	}

	public EventHandler CancelCommandHandler
	{
		get
		{
			if (_cancelCommandHandler == null)
			{
				_cancelCommandHandler = CancelInvokedHandler;
			}
			return _cancelCommandHandler;
		}
	}

	public bool HasPages => _pages.Count > 0;

	public WizardPage CurrentPage
	{
		get
		{
			if (_pages.Count != 0 && _currentPageIndex >= 0)
			{
				return _pages[_currentPageIndex];
			}
			return null;
		}
	}

	public int CurrentPageIndex
	{
		get
		{
			return _currentPageIndex;
		}
		set
		{
			if (_currentPageIndex != value)
			{
				if (CurrentPage != null)
				{
					CurrentPage.Deactivate();
				}
				_currentPageIndex = value;
				if (_currentPageIndex != -1)
				{
					CurrentPage.Activate();
					((ModelItem)this).FirePropertyChanged("CurrentPageIndex");
					((ModelItem)this).FirePropertyChanged("CanAdvancePageIndex");
					((ModelItem)this).FirePropertyChanged("CurrentPage");
					((ModelItem)this).FirePropertyChanged("CanMoveBack");
					((ModelItem)this).FirePropertyChanged("CanMoveNext");
					((ModelItem)this).FirePropertyChanged("CanCommitChanges");
				}
			}
		}
	}

	public bool HasEntries
	{
		get
		{
			if (_settings != null)
			{
				return _settings.Count > 0;
			}
			return false;
		}
	}

	public bool Finished
	{
		get
		{
			return _finished;
		}
		set
		{
			if (_finished != value)
			{
				_finished = value;
				((ModelItem)this).FirePropertyChanged("Finished");
			}
		}
	}

	public event WizardStateChangeHandler StateChanged;

	protected Wizard()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		_settings = null;
		_pages = new List<WizardPage>();
		_currentPageIndex = -1;
		_error = HRESULT._S_OK;
		_finished = false;
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing)
		{
			foreach (WizardPage page in _pages)
			{
				((ModelItem)page).Dispose();
			}
			if (_settings != null)
			{
				foreach (object value in _settings.Values)
				{
					if (value is IDisposable)
					{
						((IDisposable)value).Dispose();
					}
				}
			}
		}
		((ModelItem)this).OnDispose(disposing);
	}

	protected void AddPage(WizardPage page)
	{
		_pages.Add(page);
		if (_currentPageIndex == -1 && page.IsEnabled)
		{
			_currentPageIndex = _pages.Count - 1;
			CurrentPage.Activate();
		}
		((ModelItem)this).FirePropertyChanged("Pages");
	}

	protected void AddPage(WizardPage page, string breadcrumbTitle)
	{
		if (!string.IsNullOrEmpty(breadcrumbTitle) && page.IsEnabled)
		{
			page.BreadcrumbTitle = breadcrumbTitle;
			if (_breadcrumbFactory == null)
			{
				_breadcrumbFactory = new BreadcrumbFactory();
			}
			_breadcrumbFactory.AddCrumb(new Breadcrumb(page));
		}
		AddPage(page);
	}

	public void MarkBreadcrumbsComplete()
	{
		if (Breadcrumbs == null)
		{
			return;
		}
		foreach (Breadcrumb breadcrumb in Breadcrumbs)
		{
			breadcrumb.Complete = true;
		}
	}

	public virtual void NotifyStateChanged()
	{
		if (this.StateChanged != null)
		{
			this.StateChanged();
		}
		((ModelItem)this).FirePropertyChanged("CanAdvancePageIndex");
	}

	public void Start()
	{
		if (CanStart && OnStart() && CurrentPageIndex < 0)
		{
			CurrentPageIndex = GetNextEnabledPageIndex(-1);
		}
	}

	protected virtual bool OnStart()
	{
		return true;
	}

	public virtual bool MoveNext()
	{
		bool result = false;
		WizardPage currentPage = CurrentPage;
		if (!CanMoveNext)
		{
			throw new ApplicationException("cannot move next in this state");
		}
		if (CurrentPage == null || CurrentPage.OnMovingNext())
		{
			CurrentPageIndex = GetNextEnabledPageIndex();
			result = true;
		}
		WizardPage currentPage2 = CurrentPage;
		if (_breadcrumbFactory != null)
		{
			_breadcrumbFactory.UpdateState(currentPage, currentPage2, movingNext: true);
		}
		return result;
	}

	public void ShowErrorPage(int hr)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		ShowErrorPage(new HRESULT(hr));
	}

	public void ShowErrorPage(HRESULT hr)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		SetError(hr, null);
		ErrorPageIsEnabled = true;
		MoveNext();
	}

	private int GetNextEnabledPageIndex()
	{
		return GetNextEnabledPageIndex(CurrentPageIndex);
	}

	private int GetNextEnabledPageIndex(int startIndex)
	{
		int num = startIndex;
		while (++num < _pages.Count)
		{
			if (_pages[num].IsEnabled)
			{
				return num;
			}
		}
		return -1;
	}

	public virtual bool MoveBack()
	{
		bool result = false;
		WizardPage currentPage = CurrentPage;
		if (!CanMoveBack)
		{
			throw new ApplicationException("cannot move back in this state");
		}
		if (CurrentPage == null || CurrentPage.OnMovingBack())
		{
			CurrentPageIndex = GetPrevEnabledPageIndex();
			result = true;
		}
		WizardPage currentPage2 = CurrentPage;
		if (_breadcrumbFactory != null)
		{
			_breadcrumbFactory.UpdateState(currentPage, currentPage2, movingNext: false);
		}
		return result;
	}

	private int GetPrevEnabledPageIndex()
	{
		int num = CurrentPageIndex;
		while (--num >= 0)
		{
			if (_pages[num].IsEnabled)
			{
				return num;
			}
		}
		return -1;
	}

	public bool CommitChanges()
	{
		if (!CanCommitChanges)
		{
			return false;
		}
		foreach (WizardPage page in _pages)
		{
			if (!page.CommitChanges())
			{
				CurrentPageIndex = _pages.IndexOf(page);
				return false;
			}
		}
		if (!OnCommitChanges())
		{
			return false;
		}
		if (_settings != null)
		{
			foreach (ProxySettingDelegate key in _settings.Keys)
			{
				key(_settings[key]);
			}
			ClearSettings();
		}
		return true;
	}

	public bool AsyncCommitChanges()
	{
		if (!CanCommitChanges)
		{
			return false;
		}
		return ThreadPool.QueueUserWorkItem(AsyncCommitChangesPrivate, _currentPageIndex);
	}

	private void AsyncCommitChangesPrivate(object state)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		int currentPageIndex = _currentPageIndex;
		int num = -1;
		foreach (WizardPage page in _pages)
		{
			if (!page.CommitChanges())
			{
				num = _pages.IndexOf(page);
				break;
			}
		}
		if (!OnCommitChanges())
		{
			num = currentPageIndex;
		}
		Application.DeferredInvoke(new DeferredInvokeHandler(OnAsyncCommitCompletedPrivate), (object)num);
	}

	private void OnAsyncCommitCompletedPrivate(object args)
	{
		int num = (int)args;
		bool flag = num < 0;
		if (flag)
		{
			if (_settings != null)
			{
				foreach (ProxySettingDelegate key in _settings.Keys)
				{
					key(_settings[key]);
				}
				ClearSettings();
			}
		}
		else if (CurrentPageIndex == num)
		{
			CurrentPage.Activate();
		}
		else
		{
			CurrentPageIndex = num;
		}
		OnAsyncCommitCompleted(flag);
	}

	protected virtual void OnAsyncCommitCompleted(bool success)
	{
	}

	protected virtual bool OnCommitChanges()
	{
		return true;
	}

	protected virtual void OnSetError(HRESULT hr, object state)
	{
	}

	public virtual void Cancel()
	{
		if (!CanCancel)
		{
			throw new ApplicationException("cannot cancel in this state");
		}
		CancelSettings();
	}

	public virtual void RecordSetting(ProxySettingDelegate proxy, object data)
	{
		if (_settings == null)
		{
			_settings = new Dictionary<ProxySettingDelegate, object>();
		}
		_settings[proxy] = data;
	}

	public virtual void CancelSettings()
	{
		ClearSettings();
	}

	public void ResetError()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		SetError(HRESULT._S_OK, null);
	}

	public void SetError(HRESULT hr, object state)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (Application.IsApplicationThread)
		{
			Error = hr;
			OnSetError(hr, state);
		}
		else
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(AsyncSetError), (object)new object[2] { hr, state });
		}
	}

	private void AsyncSetError(object args)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		object[] array = (object[])args;
		SetError((HRESULT)array[0], array[1]);
	}

	private void ClearSettings()
	{
		if (_settings != null)
		{
			_settings.Clear();
			_settings = null;
		}
	}

	private void CancelInvokedHandler(object sender, EventArgs args)
	{
		Cancel();
	}
}
