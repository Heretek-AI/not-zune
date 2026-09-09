using System.Collections;

namespace ZuneUI;

public class StringListPropertyDescriptor : PropertyDescriptor
{
	public StringListPropertyDescriptor(string name, string multiValueString, string unknownString)
		: base(name, multiValueString, unknownString)
	{
	}

	public override string ConvertToString(object value)
	{
		if (value != null)
		{
			return TrackDetails.ContributingArtistListToString((IList)value);
		}
		return null;
	}

	public override object ConvertFromString(string value)
	{
		if (value != null)
		{
			return TrackDetails.ContributingArtistStringToList(value);
		}
		return null;
	}
}
