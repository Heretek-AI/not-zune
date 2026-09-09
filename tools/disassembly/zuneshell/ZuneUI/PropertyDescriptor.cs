namespace ZuneUI;

public class PropertyDescriptor
{
	private string _name;

	private string _unknownString;

	private string _multiValueString;

	private int _maxTextLength;

	private bool _required;

	private object _defaultValue;

	public bool Required => IsRequiredInternal();

	public object DefaultValue
	{
		get
		{
			return _defaultValue;
		}
		protected set
		{
			_defaultValue = value;
		}
	}

	public virtual string DefaultError => null;

	public string DescriptorName => _name;

	public string UnknownString => _unknownString;

	public string MultiValueString => _multiValueString;

	public int MaxTextLength => _maxTextLength;

	public PropertyDescriptor(string name, string multiValueString, string unknownString, int maxTextLength, bool required, object defaultValue)
	{
		_name = name;
		_unknownString = unknownString;
		_multiValueString = multiValueString;
		_maxTextLength = maxTextLength;
		_required = required;
		_defaultValue = defaultValue;
	}

	public PropertyDescriptor(string name, string multiValueString, string unknownString, int maxTextLength, bool required)
		: this(name, multiValueString, unknownString, maxTextLength, required, null)
	{
	}

	public PropertyDescriptor(string name, string multiValueString, string unknownString, int maxTextLength)
		: this(name, multiValueString, unknownString, maxTextLength, required: false, null)
	{
	}

	public PropertyDescriptor(string name, string multiValueString, string unknownString)
		: this(name, multiValueString, unknownString, 1000, required: false, null)
	{
	}

	public PropertyDescriptor(string name, string multiValueString, string unknownString, bool required)
		: this(name, multiValueString, unknownString, 1000, required, null)
	{
		_required = required;
	}

	public virtual string ConvertToString(object value, object state)
	{
		return ConvertToString(value);
	}

	public virtual string ConvertToString(object value)
	{
		return value?.ToString();
	}

	public virtual object ConvertFromString(string value, object state)
	{
		return ConvertFromString(value);
	}

	public virtual object ConvertFromString(string value)
	{
		return value?.Trim();
	}

	public bool IsValid(string value, object state)
	{
		string text = value;
		if (text != null)
		{
			text = text.Trim();
		}
		if (_required && (string.IsNullOrEmpty(text) || text == _unknownString))
		{
			return false;
		}
		if (text != null && text.Length > _maxTextLength)
		{
			return false;
		}
		return IsValidInternal(text, state);
	}

	public virtual bool IsValidInternal(string value, object state)
	{
		return IsValidInternal(value);
	}

	public virtual bool IsValidInternal(string value)
	{
		return true;
	}

	public bool IsRequired(object state)
	{
		return IsRequiredInternal(state);
	}

	public virtual bool IsRequiredInternal(object state)
	{
		return IsRequiredInternal();
	}

	public virtual bool IsRequiredInternal()
	{
		return _required;
	}

	internal virtual string GetOverlayString(object state)
	{
		return string.Empty;
	}

	internal virtual string GetLabelString(object state)
	{
		return string.Empty;
	}
}
