using System;
using Microsoft.Iris;

namespace ZuneUI;

public class Page : ModelItem, IPage
{
	private bool _isCurrentPage;

	public bool IsCurrentPage
	{
		get
		{
			return _isCurrentPage;
		}
		private set
		{
			if (_isCurrentPage != value)
			{
				_isCurrentPage = value;
				((ModelItem)this).FirePropertyChanged("IsCurrentPage");
			}
		}
	}

	public event EventHandler NavigatedTo;

	public event EventHandler NavigatedAway;

	public void OnNavigatedTo()
	{
		OnNavigatedToWorker();
	}

	public void OnNavigatedAway(IPage destination)
	{
		OnNavigatedAwayWorker(destination);
	}

	public virtual IPageState SaveAndRelease()
	{
		return new InstancePageState(this);
	}

	public virtual void Release()
	{
		((ModelItem)this).Dispose();
	}

	protected virtual void OnNavigatedToWorker()
	{
		IsCurrentPage = true;
		if (this.NavigatedTo != null)
		{
			this.NavigatedTo(this, EventArgs.Empty);
		}
	}

	protected virtual void OnNavigatedAwayWorker(IPage destination)
	{
		IsCurrentPage = false;
		if (this.NavigatedAway != null)
		{
			this.NavigatedAway(this, EventArgs.Empty);
		}
	}
}
