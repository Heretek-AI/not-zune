using Microsoft.Zune.Util;

namespace ZuneUI;

public class ProfileCategories
{
	private static Category _badges;

	private static Category _biography;

	private static Category _favorites;

	private static Category _lastPlayed;

	private static Category _topArtists;

	private static Category _comments;

	public static Category Badges
	{
		get
		{
			if (_badges == null)
			{
				_badges = new Category(StringId.IDS_PROFILE_BADGES_PIVOT, "res://ZuneShellResources!ProfileCategories.uix#ProfileCategoryBadges", allowScrolling: false, (SQMDataId)67);
			}
			return _badges;
		}
	}

	public static Category Biography
	{
		get
		{
			if (_biography == null)
			{
				_biography = new Category(StringId.IDS_PROFILE_BIOGRAPHY_PIVOT, "res://ZuneShellResources!ProfileCategories.uix#ProfileCategoryBiography", allowScrolling: false, (SQMDataId)68);
			}
			return _biography;
		}
	}

	public static Category Comments
	{
		get
		{
			if (_comments == null)
			{
				_comments = new Category(StringId.IDS_PROFILE_COMMENTS_PIVOT, "res://ZuneShellResources!ProfileCategories.uix#ProfileCategoryComments", allowScrolling: false, (SQMDataId)63);
			}
			return _comments;
		}
	}

	public static Category Favorites
	{
		get
		{
			if (_favorites == null)
			{
				_favorites = new Category(StringId.IDS_PROFILE_FAVORITES_PIVOT, "res://ZuneShellResources!ProfileCategories.uix#ProfileCategoryFavorites", allowScrolling: false, (SQMDataId)66);
			}
			return _favorites;
		}
	}

	public static Category RecentlyPlayed
	{
		get
		{
			if (_lastPlayed == null)
			{
				_lastPlayed = new Category(StringId.IDS_PROFILE_RECENTLY_PLAYED_PIVOT, "res://ZuneShellResources!ProfileCategories.uix#ProfileCategoryRecentlyPlayed", allowScrolling: false, (SQMDataId)64);
			}
			return _lastPlayed;
		}
	}

	public static Category TopArtists
	{
		get
		{
			if (_topArtists == null)
			{
				_topArtists = new Category(StringId.IDS_PROFILE_MOST_PLAYED_ARTISTS_PIVOT, "res://ZuneShellResources!ProfileCategories.uix#ProfileCategoryTopArtists", allowScrolling: false, (SQMDataId)65);
			}
			return _topArtists;
		}
	}
}
