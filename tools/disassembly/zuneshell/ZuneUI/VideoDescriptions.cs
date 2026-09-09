using Microsoft.Iris;

namespace ZuneUI;

public static class VideoDescriptions
{
	private static string _tvViewHeader = Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_TV);

	private static string _musicViewHeader = null;

	private static string _movieViewHeader = null;

	private static string _otherViewHeader = null;

	private static string _personalViewHeader = null;

	private static string _seriesDescription = Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_TV_SERIES);

	private static string _shortsDescription = Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_TV_SHORTS);

	private static string _newsDescription = Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_TV_NEWS);

	private static string _musicDescription = Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_MUSIC);

	private static string _moviesDescription = Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_MOVIES);

	private static string _otherDescription = Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_OTHER);

	private static string _personalDescription = Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_PERSONAL);

	private static VideoViewCategory _tvCategory = new VideoViewCategory(_tvViewHeader, _seriesDescription);

	private static VideoViewCategory _shortsCategory = new VideoViewCategory(_tvViewHeader, _shortsDescription);

	private static VideoViewCategory _newsCategory = new VideoViewCategory(_tvViewHeader, _newsDescription);

	private static VideoViewCategory _musicCategory = new VideoViewCategory(_musicViewHeader, _musicDescription);

	private static VideoViewCategory _moviesCategory = new VideoViewCategory(_movieViewHeader, _moviesDescription);

	private static VideoViewCategory _otherCategory = new VideoViewCategory(_otherViewHeader, _otherDescription);

	private static VideoViewCategory _personalCategory = new VideoViewCategory(_personalViewHeader, _personalDescription);

	private static GroupedList _groupedCategories;

	public static GroupedList Categories
	{
		get
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			if (_groupedCategories == null)
			{
				_groupedCategories = new GroupedList();
				_groupedCategories.Comparer = new VideoViewCategoryComparer();
				_groupedCategories.Source = new VideoViewCategory[7] { _tvCategory, _shortsCategory, _newsCategory, _musicCategory, _moviesCategory, _otherCategory, _personalCategory };
			}
			return _groupedCategories;
		}
	}

	public static int GetCategoryId(VideoCategory category)
	{
		return (int)category;
	}

	public static VideoCategory GetCategory(int id)
	{
		return (VideoCategory)id;
	}

	public static string GetDescription(int categoryId)
	{
		return GetDescription((VideoCategory)categoryId);
	}

	public static string GetDescription(VideoCategory category)
	{
		return category switch
		{
			VideoCategory.TV => _seriesDescription, 
			VideoCategory.Shorts => _shortsDescription, 
			VideoCategory.News => _newsDescription, 
			VideoCategory.Music => _musicDescription, 
			VideoCategory.Movies => _moviesDescription, 
			VideoCategory.Personal => _personalDescription, 
			_ => _otherDescription, 
		};
	}

	public static VideoCategory GetCategory(string description)
	{
		if (description == _seriesDescription)
		{
			return VideoCategory.TV;
		}
		if (description == _shortsDescription)
		{
			return VideoCategory.Shorts;
		}
		if (description == _newsDescription)
		{
			return VideoCategory.News;
		}
		if (description == _musicDescription)
		{
			return VideoCategory.Music;
		}
		if (description == _moviesDescription)
		{
			return VideoCategory.Movies;
		}
		if (description == _personalDescription)
		{
			return VideoCategory.Personal;
		}
		return VideoCategory.Other;
	}
}
