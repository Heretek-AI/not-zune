using System.ComponentModel;

namespace ZuneUI;

public class MetadataPropertyChangedEventArgs : PropertyChangedEventArgs
{
	public bool Propagate;

	public MetadataPropertyChangedEventArgs(string propertyName, bool propagate)
		: base(propertyName)
	{
		Propagate = propagate;
	}
}
