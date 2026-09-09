using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal abstract class Media : MiniMedia
{
	internal string Artist
	{
		get
		{
			string result = null;
			MiniArtist primaryArtist = PrimaryArtist;
			if (primaryArtist != null)
			{
				result = primaryArtist.Title;
			}
			else
			{
				IList artists = Artists;
				if (artists != null)
				{
					{
						IEnumerator enumerator = artists.GetEnumerator();
						try
						{
							if (enumerator.MoveNext())
							{
								MiniArtist miniArtist = (MiniArtist)enumerator.Current;
								result = miniArtist.Title;
							}
						}
						finally
						{
							IDisposable disposable = enumerator as IDisposable;
							if (disposable != null)
							{
								disposable.Dispose();
							}
						}
					}
				}
			}
			return result;
		}
	}

	internal abstract string SortTitle { get; }

	internal abstract Guid ImageId { get; }

	internal abstract MediaRights Rights { get; }

	internal abstract MiniArtist PrimaryArtist { get; }

	internal abstract IList Artists { get; }

	internal abstract double Popularity { get; }

	protected Media(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
