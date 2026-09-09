using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class InboxImageDataProviderObject : DataProviderObject
{
	public static readonly string PropertyName_InLibrary = "InLibrary";

	public static readonly string PropertyName_ImagePath = "ImagePath";

	public static readonly string PropertyName_Photo = "Photo";

	private bool _inLibrary;

	private string _imagePath;

	private Image _photo;

	public bool InLibrary
	{
		get
		{
			return _inLibrary;
		}
		set
		{
			if (_inLibrary != value)
			{
				_inLibrary = value;
				((DataProviderObject)this).FirePropertyChanged(PropertyName_InLibrary);
			}
		}
	}

	public string ImagePath
	{
		get
		{
			return _imagePath;
		}
		set
		{
			if (_imagePath != value)
			{
				_imagePath = value;
				((DataProviderObject)this).FirePropertyChanged(PropertyName_ImagePath);
			}
		}
	}

	public Image Photo
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			if (_photo == null && !string.IsNullOrEmpty(_imagePath))
			{
				_photo = new Image("file://" + _imagePath);
			}
			return _photo;
		}
	}

	public InboxImageDataProviderObject(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		if (propertyName == PropertyName_InLibrary)
		{
			return InLibrary;
		}
		if (propertyName == PropertyName_ImagePath)
		{
			return ImagePath;
		}
		if (propertyName == PropertyName_Photo)
		{
			return Photo;
		}
		return null;
	}

	public override void SetProperty(string propertyName, object value)
	{
		if (propertyName == PropertyName_InLibrary)
		{
			InLibrary = (bool)value;
			return;
		}
		if (propertyName == PropertyName_ImagePath)
		{
			ImagePath = (string)value;
			return;
		}
		throw new ApplicationException("unexpected property name");
	}

	internal void TransferToAppThread()
	{
	}
}
