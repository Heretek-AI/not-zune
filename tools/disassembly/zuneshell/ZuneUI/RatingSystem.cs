using System.Globalization;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class RatingSystem : RatingSystemBase
{
	private string CurrentLanguage => CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpper();

	public string Title => GetString(((RatingSystemBase)this).Title);

	public string Description => GetString(((RatingSystemBase)this).Description);

	public string BlockText => GetString(((RatingSystemBase)this).BlockText);

	public RatingSystem(RatingSystemBase details)
		: base(details.Name, details.Title, details.Description, details.BlockText, details.UseImages, details.ShowBlockUnrated, details.DefaultLanguage, details.Strings, details.Ratings)
	{
	}

	private string GetString(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return null;
		}
		if (((RatingSystemBase)this).Strings.ContainsKey(CurrentLanguage))
		{
			if (((RatingSystemBase)this).Strings[CurrentLanguage].ContainsKey(id))
			{
				return ((RatingSystemBase)this).Strings[CurrentLanguage][id];
			}
			return null;
		}
		if (((RatingSystemBase)this).Strings[((RatingSystemBase)this).DefaultLanguage].ContainsKey(id))
		{
			return ((RatingSystemBase)this).Strings[((RatingSystemBase)this).DefaultLanguage][id];
		}
		return null;
	}

	public string GetRatingName(int order)
	{
		for (int i = 0; i < ((RatingSystemBase)this).Ratings.Length; i++)
		{
			if (((RatingSystemBase)this).Ratings[i].Order == order)
			{
				return GetString(((RatingSystemBase)this).Ratings[i].TextId);
			}
		}
		return null;
	}

	public string GetRatingDescription(int order)
	{
		for (int i = 0; i < ((RatingSystemBase)this).Ratings.Length; i++)
		{
			if (((RatingSystemBase)this).Ratings[i].Order == order)
			{
				return GetString(((RatingSystemBase)this).Ratings[i].DescriptionId);
			}
		}
		return null;
	}

	public string GetRatingToolTip(int order)
	{
		for (int i = 0; i < ((RatingSystemBase)this).Ratings.Length; i++)
		{
			if (((RatingSystemBase)this).Ratings[i].Order == order)
			{
				return GetString(((RatingSystemBase)this).Ratings[i].ToolTipId);
			}
		}
		return null;
	}
}
