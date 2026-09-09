using System;

namespace ZuneUI;

public class NewReview : NotifyPropertyChangedImpl
{
	private DateTime _date;

	private string _userName;

	private string _comment;

	private string _title;

	private float _rating;

	public DateTime Date
	{
		get
		{
			return _date;
		}
		set
		{
			if (_date != value)
			{
				_date = value;
				FirePropertyChanged("Date");
			}
		}
	}

	public string UserName
	{
		get
		{
			return _userName;
		}
		set
		{
			if (_userName != value)
			{
				_userName = value;
				FirePropertyChanged("UserName");
			}
		}
	}

	public string Comment
	{
		get
		{
			return _comment;
		}
		set
		{
			if (_comment != value)
			{
				_comment = value;
				FirePropertyChanged("Comment");
			}
		}
	}

	public string Title
	{
		get
		{
			return _title;
		}
		set
		{
			if (_title != value)
			{
				_title = value;
				FirePropertyChanged("Title");
			}
		}
	}

	public float Rating
	{
		get
		{
			return _rating;
		}
		set
		{
			if (_rating != value)
			{
				_rating = value;
				FirePropertyChanged("Rating");
			}
		}
	}
}
