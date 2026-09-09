using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal class Series : Media
{
	internal override MediaRights Rights => null;

	internal override MiniArtist PrimaryArtist => null;

	internal override IList Artists => null;

	internal DateTime ReleaseDate => (DateTime)base.GetProperty("ReleaseDate");

	internal string Rating => (string)base.GetProperty("Rating");

	internal int SeasonCount => (int)base.GetProperty("SeasonCount");

	internal string ProductionCompany => (string)base.GetProperty("ProductionCompany");

	internal string Description => (string)base.GetProperty("Description");

	internal IList Categories => (IList)base.GetProperty("Categories");

	internal Network Network => (Network)base.GetProperty("Network");

	internal override Guid Id => (Guid)base.GetProperty("Id");

	internal override string Title => (string)base.GetProperty("Title");

	internal override Guid ImageId => (Guid)base.GetProperty("ImageId");

	internal override string SortTitle => (string)base.GetProperty("SortTitle");

	internal override double Popularity => (double)base.GetProperty("Popularity");

	internal static XmlDataProviderObject ConstructSeriesObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Series(owner, objectTypeCookie);
	}

	internal Series(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"Rights" => Rights, 
			"PrimaryArtist" => PrimaryArtist, 
			"Artists" => Artists, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
