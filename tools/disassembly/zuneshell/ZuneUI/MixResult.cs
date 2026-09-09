using System;
using System.Collections;
using System.ComponentModel;
using Microsoft.Iris;

namespace ZuneUI;

public class MixResult : INotifyPropertyChanged
{
	private MixResultType _resultType;

	private string _reason;

	private string _primaryText;

	private string _secondaryText;

	private string _id;

	private Guid _imageId;

	private IList _tracks;

	private DataProviderQueryStatus _tracksQueryStatus;

	private string _imageUri;

	private DataProviderObject _baseObject;

	public string Reason => _reason;

	public string PrimaryText => _primaryText;

	public string SecondaryText => _secondaryText;

	public string Id => _id;

	public MixResultType ResultType => _resultType;

	public Guid ImageId => _imageId;

	public string ImageUri => _imageUri;

	public DataProviderObject BaseObject => _baseObject;

	public IList Tracks
	{
		get
		{
			return _tracks;
		}
		set
		{
			if (value != _tracks)
			{
				_tracks = value;
				NotifyPropertyChanged("Tracks");
			}
		}
	}

	public DataProviderQueryStatus TracksQueryStatus
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _tracksQueryStatus;
		}
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (value != _tracksQueryStatus)
			{
				_tracksQueryStatus = value;
				NotifyPropertyChanged("TracksQueryStatus");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected MixResult()
	{
	}

	protected void Initialize(MixResultType resultType, string reason, string primaryText, string secondaryText, string id, string imageUri, Guid imageId, DataProviderObject baseObject)
	{
		_resultType = resultType;
		_reason = reason;
		_primaryText = primaryText;
		_secondaryText = secondaryText;
		_id = id;
		_imageId = imageId;
		_imageUri = imageUri;
		_baseObject = baseObject;
	}

	public static MixResult CreateInstance(MixResultType resultType, string reason, string primaryText, string secondaryText, string id, string imageUri, Guid imageId, DataProviderObject baseObject)
	{
		MixResult mixResult = new MixResult();
		mixResult.Initialize(resultType, reason, primaryText, secondaryText, id, imageUri, imageId, baseObject);
		return mixResult;
	}

	internal virtual bool IsDuplicate(MixResult compareTo)
	{
		if (ResultType != compareTo.ResultType)
		{
			return false;
		}
		if (Id == compareTo.Id)
		{
			return true;
		}
		return false;
	}

	protected void NotifyPropertyChanged(string propertyName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
