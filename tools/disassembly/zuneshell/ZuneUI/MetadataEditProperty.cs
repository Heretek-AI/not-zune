using Microsoft.Zune.ErrorMapperApi;

namespace ZuneUI;

public class MetadataEditProperty : INotifyPropertyChangedImpl
{
	private PropertyDescriptor _descriptor;

	private string _originalValue;

	private string _currentValue;

	private bool _modified;

	private bool _valid = true;

	private object _state;

	private HRESULT _externalError = HRESULT._S_OK;

	public string OriginalValue
	{
		get
		{
			return _originalValue;
		}
		set
		{
			if (_originalValue != value)
			{
				_originalValue = value;
				NotifyPropertyChanged("OriginalValue", propogate: true);
			}
			Value = _originalValue;
		}
	}

	public string Value
	{
		get
		{
			return _currentValue;
		}
		set
		{
			SetValue(value, propagate: true);
		}
	}

	public bool Valid
	{
		get
		{
			return _valid;
		}
		internal set
		{
			if (_valid != value)
			{
				_valid = value;
				NotifyPropertyChanged("Valid", propogate: true);
				NotifyPropertyChanged("ErrorMessage", propogate: true);
			}
		}
	}

	public bool Required => _descriptor.IsRequired(_state);

	public bool Modified
	{
		get
		{
			return _modified;
		}
		internal set
		{
			if (_modified != value)
			{
				_modified = value;
				NotifyPropertyChanged("Modified", propogate: true);
			}
		}
	}

	public PropertyDescriptor Descriptor => _descriptor;

	public HRESULT ExternalError
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _externalError;
		}
		set
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			if (((HRESULT)(ref _externalError)).Int != ((HRESULT)(ref value)).Int)
			{
				_externalError = value;
				NotifyPropertyChanged("ExternalError", propogate: true);
				UpdateValid();
			}
		}
	}

	public string ErrorMessage
	{
		get
		{
			string result = null;
			if (((HRESULT)(ref _externalError)).IsError)
			{
				ErrorMapperResult mappedErrorDescriptionAndUrl = ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref _externalError)).Int);
				if (mappedErrorDescriptionAndUrl != null)
				{
					result = mappedErrorDescriptionAndUrl.Description;
				}
			}
			else if (!Valid)
			{
				result = _descriptor.DefaultError;
			}
			return result;
		}
	}

	public object State
	{
		get
		{
			return _state;
		}
		set
		{
			if (_state != value)
			{
				_state = value;
				NotifyPropertyChanged("State", propogate: true);
				OnStateChanged();
			}
		}
	}

	public string OverlayContent => _descriptor.GetOverlayString(_state);

	public string LabelContent => _descriptor.GetLabelString(_state);

	internal MetadataEditProperty(PropertyDescriptor descriptor, string originalValue)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		_descriptor = descriptor;
		_originalValue = originalValue;
		_currentValue = originalValue;
		UpdateValidAndModified();
	}

	internal void SetValue(string value, bool propagate)
	{
		if (_currentValue != value)
		{
			_currentValue = value;
			NotifyPropertyChanged("Value", propagate);
			UpdateValidAndModified();
		}
	}

	public object ConvertToData()
	{
		return _descriptor.ConvertFromString(_currentValue, _state);
	}

	public void ConvertFromData(object data)
	{
		OriginalValue = _descriptor.ConvertToString(data, _state);
	}

	private void OnStateChanged()
	{
		UpdateValid();
		NotifyPropertyChanged("OverlayContent", propogate: true);
		NotifyPropertyChanged("LabelContent", propogate: true);
	}

	private void UpdateValidAndModified()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Modified = _currentValue != _originalValue;
		if (Modified && ((HRESULT)(ref _externalError)).IsError)
		{
			_externalError = HRESULT._S_OK;
		}
		UpdateValid();
	}

	private void UpdateValid()
	{
		Valid = _descriptor.IsValid(_currentValue, _state) && ((HRESULT)(ref _externalError)).IsSuccess;
	}
}
