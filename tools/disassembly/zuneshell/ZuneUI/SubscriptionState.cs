using Microsoft.Iris;

namespace ZuneUI;

public class SubscriptionState : ModelItem
{
	private bool _isSubscribed;

	private bool _seriesFound;

	private int _seriesId;

	public bool IsSubscribed
	{
		get
		{
			return _isSubscribed;
		}
		set
		{
			if (_isSubscribed != value)
			{
				_isSubscribed = value;
				((ModelItem)this).FirePropertyChanged("IsSubscribed");
			}
		}
	}

	public bool SeriesFound
	{
		get
		{
			return _seriesFound;
		}
		set
		{
			if (_seriesFound != value)
			{
				_seriesFound = value;
				((ModelItem)this).FirePropertyChanged("SeriesFound");
			}
		}
	}

	public int SeriesId
	{
		get
		{
			return _seriesId;
		}
		set
		{
			if (_seriesId != value)
			{
				_seriesId = value;
				((ModelItem)this).FirePropertyChanged("SeriesId");
			}
		}
	}

	public SubscriptionState(bool isSubscribed, bool seriesFound, int seriesId)
	{
		_seriesFound = seriesFound;
		_isSubscribed = isSubscribed;
		_seriesId = seriesId;
	}

	public SubscriptionState()
	{
		_seriesFound = false;
		_isSubscribed = false;
		_seriesId = -1;
	}
}
