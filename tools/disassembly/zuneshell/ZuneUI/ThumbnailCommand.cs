using System.ComponentModel;
using Microsoft.Iris;

namespace ZuneUI;

public class ThumbnailCommand : Command, IThumbnailCommand, ICommand, INotifyPropertyChanged
{
	private Image _image;

	public virtual Image Image
	{
		get
		{
			return _image;
		}
		set
		{
			if (_image != value)
			{
				_image = value;
				((ModelItem)this).FirePropertyChanged("Image");
			}
		}
	}

	public string ImagePath
	{
		set
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			Image = new Image(value);
		}
	}

	public ThumbnailCommand()
		: this(null)
	{
	}

	public ThumbnailCommand(IModelItemOwner owner)
		: base(owner)
	{
	}
}
