using MicrosoftZuneLibrary;

namespace ZuneUI;

public class TrackMetadataPropertySource : PropertySource
{
	private static PropertySource _instance;

	public static PropertySource Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new TrackMetadataPropertySource();
			}
			return _instance;
		}
	}

	protected TrackMetadataPropertySource()
	{
	}

	public override object Get(object media, PropertyDescriptor property)
	{
		TrackMetadata val = (TrackMetadata)((media is TrackMetadata) ? media : null);
		string descriptorName = property.DescriptorName;
		if (descriptorName == MetadataEditMedia.s_Title.DescriptorName)
		{
			return val.TrackTitle;
		}
		if (descriptorName == MetadataEditMedia.s_Artist.DescriptorName)
		{
			return val.TrackArtist;
		}
		if (descriptorName == MetadataEditMedia.s_Genre.DescriptorName)
		{
			return val.Genre;
		}
		if (descriptorName == MetadataEditMedia.s_Composer.DescriptorName)
		{
			return val.Composer;
		}
		if (descriptorName == MetadataEditMedia.s_Conductor.DescriptorName)
		{
			return val.Conductor;
		}
		if (descriptorName == MetadataEditMedia.s_TrackNumber.DescriptorName)
		{
			return val.TrackNumber;
		}
		if (descriptorName == MetadataEditMedia.s_DiscNumber.DescriptorName)
		{
			return val.DiscNumber;
		}
		if (descriptorName == MetadataEditMedia.s_MediaId.DescriptorName)
		{
			return val.MediaId;
		}
		return null;
	}

	public override void Set(object media, PropertyDescriptor property, object value)
	{
		TrackMetadata val = (TrackMetadata)((media is TrackMetadata) ? media : null);
		string descriptorName = property.DescriptorName;
		if (descriptorName == MetadataEditMedia.s_Title.DescriptorName)
		{
			val.TrackTitle = (string)value;
		}
		else if (descriptorName == MetadataEditMedia.s_Artist.DescriptorName)
		{
			val.TrackArtist = (string)value;
		}
		else if (descriptorName == MetadataEditMedia.s_Genre.DescriptorName)
		{
			val.Genre = (string)value;
		}
		else if (descriptorName == MetadataEditMedia.s_Composer.DescriptorName)
		{
			val.Composer = (string)value;
		}
		else if (descriptorName == MetadataEditMedia.s_Conductor.DescriptorName)
		{
			val.Conductor = (string)value;
		}
		else if (descriptorName == MetadataEditMedia.s_TrackNumber.DescriptorName)
		{
			val.TrackNumber = (int)value;
		}
		else if (descriptorName == MetadataEditMedia.s_DiscNumber.DescriptorName)
		{
			val.DiscNumber = (int)value;
		}
		else if (descriptorName == MetadataEditMedia.s_MediaId.DescriptorName)
		{
			val.MediaId = (int)value;
		}
	}
}
