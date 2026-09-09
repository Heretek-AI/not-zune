using System.ComponentModel;
using Microsoft.Iris;

namespace ZuneUI;

public class SlideshowLand : PlaybackPage, ISlideShowStateOwner, INotifyPropertyChanged
{
	private SlideShowState _state;

	public SlideShowState SlideShowState
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
				((ModelItem)this).FirePropertyChanged("SlideShowState");
			}
		}
	}

	private static string PhotoSlideshowTemplate => "res://ZuneShellResources!PhotoSlideShow.uix#PhotoSlideShow";

	public SlideshowLand()
	{
		base.BackgroundUI = PhotoSlideshowTemplate;
		base.TransportControlStyle = TransportControlStyle.Photo;
		base.AutoHideToolbars = true;
		base.ShowAppBackground = true;
		base.NoStackPage = true;
	}

	public void MoveToNextSlide()
	{
		SlideShowState.Index += 1;
	}

	public void MoveToPreviousSlide()
	{
		SlideShowState.Index -= 1;
	}

	public void TogglePlayOrPauseSlideshow()
	{
		SlideShowState.Play = !SlideShowState.Play;
	}
}
