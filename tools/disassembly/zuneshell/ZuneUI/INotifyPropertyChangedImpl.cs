using System.ComponentModel;

namespace ZuneUI;

public class INotifyPropertyChangedImpl : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	protected void NotifyPropertyChanged(string propertyName, bool propogate)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new MetadataPropertyChangedEventArgs(propertyName, propogate));
		}
	}
}
