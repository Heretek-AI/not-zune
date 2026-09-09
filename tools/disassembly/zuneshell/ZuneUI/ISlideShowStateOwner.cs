using System.ComponentModel;

namespace ZuneUI;

public interface ISlideShowStateOwner : INotifyPropertyChanged
{
	SlideShowState SlideShowState { get; }
}
