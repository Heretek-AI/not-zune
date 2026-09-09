using System.Collections;
using System.ComponentModel;
using Microsoft.Iris;

namespace ZuneUI;

public interface IThumbnailCommand : ICommand, INotifyPropertyChanged
{
	Image Image { get; }

	IDictionary Data { get; }

	string Description { get; }

	bool Selected { get; }
}
