using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Iris;
using Microsoft.Zune.Shell;
using MicrosoftZuneLibrary;
using UIXControls;

namespace ZuneUI;

public class MetadataEditAlbum : MetadataEditMedia
{
	private static PropertyDescriptor[] s_properties = new PropertyDescriptor[9]
	{
		MetadataEditMedia.s_Title,
		MetadataEditMedia.s_AlbumTitleYomi,
		MetadataEditMedia.s_Artist,
		MetadataEditMedia.s_AlbumArtistYomi,
		MetadataEditMedia.s_Genre,
		MetadataEditMedia.s_Conductor,
		MetadataEditMedia.s_Composer,
		MetadataEditMedia.s_ReleaseYear,
		MetadataEditMedia.s_CoverUrl
	};

	private List<PropertyDescriptor> _linkedProperties = new List<PropertyDescriptor>(new PropertyDescriptor[3]
	{
		MetadataEditMedia.s_Genre,
		MetadataEditMedia.s_Conductor,
		MetadataEditMedia.s_Composer
	});

	private List<MetadataEditTrack> _trackList = new List<MetadataEditTrack>();

	public MetadataEditAlbum(AlbumMetadata albumMetadata)
	{
		InitializeFromMetadataList(new List<AlbumMetadata>((IEnumerable<AlbumMetadata>)(object)new AlbumMetadata[1] { albumMetadata }));
	}

	public MetadataEditAlbum(IList albumList)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		List<AlbumMetadata> list = new List<AlbumMetadata>(albumList.Count);
		try
		{
			foreach (LibraryDataProviderListItem album in albumList)
			{
				LibraryDataProviderListItem val = album;
				int num = (int)((DataProviderObject)val).GetProperty("LibraryId");
				AlbumMetadata albumMetadata = ZuneApplication.ZuneLibrary.GetAlbumMetadata(num);
				list.Add(albumMetadata);
			}
		}
		catch (Exception)
		{
			_creationFailed = true;
			list.Clear();
			MessageBox.Show(Shell.LoadString(StringId.IDS_EMI_UPDATEFAILED_TITLE), Shell.LoadString(StringId.IDS_EMI_UPDATEFAILED), (EventHandler)null);
		}
		InitializeFromMetadataList(list);
	}

	private void InitializeFromMetadataList(List<AlbumMetadata> albumMetadataList)
	{
		_source = AlbumMetadataPropertySource.Instance;
		Initialize(albumMetadataList, s_properties);
		foreach (AlbumMetadata albumMetadata in albumMetadataList)
		{
			for (uint num = 0u; num < albumMetadata.TrackCount; num++)
			{
				_trackList.Add(new MetadataEditTrack(albumMetadata.GetTrack(num)));
			}
			foreach (PropertyDescriptor linkedProperty in _linkedProperties)
			{
				GetProperty(linkedProperty).OriginalValue = GetPropertyStringFromTracks(linkedProperty);
				GetProperty(linkedProperty).PropertyChanged += LinkedAlbumPropertyChanged;
				foreach (MetadataEditTrack track in _trackList)
				{
					track.GetProperty(linkedProperty).PropertyChanged += LinkedTrackPropertyChanged;
				}
			}
		}
	}

	public override void Commit()
	{
		foreach (MetadataEditTrack track in _trackList)
		{
			track.Commit();
		}
		base.Commit();
	}

	public override bool IsValid()
	{
		foreach (MetadataEditTrack track in _trackList)
		{
			if (!track.IsValid())
			{
				return false;
			}
		}
		return base.IsValid();
	}

	public override bool IsModified()
	{
		foreach (MetadataEditTrack track in _trackList)
		{
			if (track.IsModified())
			{
				return true;
			}
		}
		return base.IsModified();
	}

	private void LinkedTrackPropertyChanged(object sender, PropertyChangedEventArgs ebase)
	{
		MetadataPropertyChangedEventArgs e = ebase as MetadataPropertyChangedEventArgs;
		PropertyDescriptor descriptor = ((MetadataEditProperty)sender).Descriptor;
		if (e.PropertyName == "Value" && _linkedProperties.Contains(descriptor) && e.Propagate)
		{
			GetProperty(descriptor).SetValue(GetPropertyStringFromTracks(descriptor), propagate: false);
		}
	}

	private void LinkedAlbumPropertyChanged(object sender, PropertyChangedEventArgs ebase)
	{
		MetadataPropertyChangedEventArgs e = ebase as MetadataPropertyChangedEventArgs;
		PropertyDescriptor descriptor = ((MetadataEditProperty)sender).Descriptor;
		if (!(e.PropertyName == "Value") || !_linkedProperties.Contains(descriptor) || !e.Propagate)
		{
			return;
		}
		foreach (MetadataEditTrack track in _trackList)
		{
			track.GetProperty(descriptor).SetValue(GetProperty(descriptor).Value, propagate: false);
		}
	}

	private string GetPropertyStringFromTracks(PropertyDescriptor descriptor)
	{
		string text = descriptor.UnknownString;
		foreach (MetadataEditTrack track in _trackList)
		{
			string value = track.GetProperty(descriptor).Value;
			text = AggregateString(text, value, descriptor);
		}
		return text;
	}

	public IList GetTracks()
	{
		return GetTracks(filterAndSort: false);
	}

	public IList GetTracks(bool filterAndSort)
	{
		if (filterAndSort)
		{
			List<MetadataEditTrack> list = new List<MetadataEditTrack>();
			foreach (MetadataEditTrack track in _trackList)
			{
				if (track.GetProperty(MetadataEditMedia.s_MediaId).Value != "-1")
				{
					list.Add(track);
				}
			}
			list.Sort(new TrackComparer());
			return list;
		}
		return _trackList;
	}

	public int GetFirstInvalidTrackIndex()
	{
		int num = 0;
		foreach (MetadataEditTrack track in _trackList)
		{
			if (!track.IsValid())
			{
				return num;
			}
			num++;
		}
		return -1;
	}
}
