using System;
using Microsoft.Zune.Shell;
using MicrosoftZuneLibrary;
using UIXControls;

namespace ZuneUI;

public class AlbumMetadataPropertySource : PropertySource
{
	private static PropertySource _instance;

	public static PropertySource Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new AlbumMetadataPropertySource();
			}
			return _instance;
		}
	}

	public override bool NeedsCommit => true;

	protected AlbumMetadataPropertySource()
	{
	}

	public override object Get(object media, PropertyDescriptor property)
	{
		AlbumMetadata val = (AlbumMetadata)((media is AlbumMetadata) ? media : null);
		string descriptorName = property.DescriptorName;
		if (descriptorName == MetadataEditMedia.s_Title.DescriptorName)
		{
			return val.AlbumTitle;
		}
		if (descriptorName == MetadataEditMedia.s_AlbumTitleYomi.DescriptorName)
		{
			return val.AlbumTitleYomi;
		}
		if (descriptorName == MetadataEditMedia.s_Artist.DescriptorName)
		{
			return val.AlbumArtist;
		}
		if (descriptorName == MetadataEditMedia.s_AlbumArtistYomi.DescriptorName)
		{
			return val.AlbumArtistYomi;
		}
		if (descriptorName == MetadataEditMedia.s_TrackCount.DescriptorName)
		{
			return val.TrackCount;
		}
		if (descriptorName == MetadataEditMedia.s_CoverUrl.DescriptorName)
		{
			return val.CoverUrl;
		}
		if (descriptorName == MetadataEditMedia.s_ReleaseYear.DescriptorName)
		{
			return val.ReleaseYear;
		}
		return null;
	}

	public override void Set(object media, PropertyDescriptor property, object value)
	{
		AlbumMetadata val = (AlbumMetadata)((media is AlbumMetadata) ? media : null);
		string descriptorName = property.DescriptorName;
		if (descriptorName == MetadataEditMedia.s_Title.DescriptorName)
		{
			val.AlbumTitle = (string)value;
		}
		else if (descriptorName == MetadataEditMedia.s_AlbumTitleYomi.DescriptorName)
		{
			val.AlbumTitleYomi = (string)value;
		}
		else if (descriptorName == MetadataEditMedia.s_Artist.DescriptorName)
		{
			val.AlbumArtist = (string)value;
		}
		else if (descriptorName == MetadataEditMedia.s_AlbumArtistYomi.DescriptorName)
		{
			val.AlbumArtistYomi = (string)value;
		}
		else if (descriptorName == MetadataEditMedia.s_CoverUrl.DescriptorName)
		{
			val.CoverUrl = (string)value;
		}
		else if (descriptorName == MetadataEditMedia.s_ReleaseYear.DescriptorName)
		{
			val.ReleaseYear = (int)value;
		}
	}

	public override void Commit(object media)
	{
		AlbumMetadata val = (AlbumMetadata)((media is AlbumMetadata) ? media : null);
		try
		{
			ZuneApplication.ZuneLibrary.UpdateAlbumMetadata(val.MediaId, val);
		}
		catch (Exception)
		{
			MessageBox.Show(Shell.LoadString(StringId.IDS_EMI_UPDATEFAILED_TITLE), Shell.LoadString(StringId.IDS_EMI_UPDATEFAILED), (EventHandler)null);
		}
	}
}
