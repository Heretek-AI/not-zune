using System.ComponentModel;

namespace ZuneUI;

public class NotifyPropertyChangedImpl : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual void FirePropertyChanged(string propertyName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
