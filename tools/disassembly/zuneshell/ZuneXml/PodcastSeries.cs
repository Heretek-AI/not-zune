using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneXml;

internal class PodcastSeries : Media
{
	private int _dbMediaId = -1;

	internal override MediaRights Rights => null;

	internal override MiniArtist PrimaryArtist => null;

	internal override IList Artists => null;

	internal override double Popularity => 0.0;

	internal virtual int LibraryId
	{
		get
		{
			return GetLibraryId();
		}
		set
		{
			if (_dbMediaId != value)
			{
				_dbMediaId = value;
				((DataProviderObject)this).FirePropertyChanged("LibraryId");
			}
		}
	}

	internal string ShortDescription => (string)base.GetProperty("ShortDescription");

	internal string LongDescription => (string)base.GetProperty("LongDescription");

	internal bool Explicit => (bool)base.GetProperty("Explicit");

	internal string Author => (string)base.GetProperty("Author");

	internal string SourceUrl => (string)base.GetProperty("SourceUrl");

	internal DateTime ReleaseDate => (DateTime)base.GetProperty("ReleaseDate");

	internal IList Categories => (IList)base.GetProperty("Categories");

	internal string Type => (string)base.GetProperty("Type");

	internal string WebsiteUrl => (string)base.GetProperty("WebsiteUrl");

	internal override Guid Id => (Guid)base.GetProperty("Id");

	internal override string Title => (string)base.GetProperty("Title");

	internal override string SortTitle => (string)base.GetProperty("SortTitle");

	internal override Guid ImageId => (Guid)base.GetProperty("ImageId");

	protected int GetLibraryId()
	{
		int result = -1;
		if (Id != Guid.Empty)
		{
			ZuneApplication.Service.InVisibleCollection(Id, (EContentType)6, ref result);
		}
		return result;
	}

	internal static XmlDataProviderObject ConstructPodcastSeriesObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new PodcastSeries(owner, objectTypeCookie);
	}

	internal PodcastSeries(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"LibraryId" => LibraryId, 
			"Rights" => Rights, 
			"PrimaryArtist" => PrimaryArtist, 
			"Artists" => Artists, 
			"Popularity" => Popularity, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
