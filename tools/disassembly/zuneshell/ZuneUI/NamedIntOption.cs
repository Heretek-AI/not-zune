using System;
using Microsoft.Iris;

namespace ZuneUI;

internal class NamedIntOption : Command
{
	private static NamedIntOption[] _keepOptions;

	private static NamedIntOption[] _playbackOptions;

	private static NamedIntOption[] _syncOptions;

	private static NamedIntOption[] _screenGraphicsOptions;

	private int _value;

	public int Value => _value;

	public static NamedIntOption[] PodcastKeepOptions
	{
		get
		{
			if (_keepOptions == null)
			{
				string format = Shell.LoadString(StringId.IDS_KEEP_N_OPTION);
				_keepOptions = new NamedIntOption[12]
				{
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_KEEP_0_OPTION), 0),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_KEEP_1_OPTION), 1),
					new NamedIntOption(null, string.Format(format, 2), 2),
					new NamedIntOption(null, string.Format(format, 3), 3),
					new NamedIntOption(null, string.Format(format, 4), 4),
					new NamedIntOption(null, string.Format(format, 5), 5),
					new NamedIntOption(null, string.Format(format, 6), 6),
					new NamedIntOption(null, string.Format(format, 7), 7),
					new NamedIntOption(null, string.Format(format, 8), 8),
					new NamedIntOption(null, string.Format(format, 9), 9),
					new NamedIntOption(null, string.Format(format, 10), 10),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_KEEP_ALL_OPTION), -1)
				};
			}
			return _keepOptions;
		}
	}

	public static NamedIntOption[] PodcastPlaybackOptions
	{
		get
		{
			if (_playbackOptions == null)
			{
				_playbackOptions = new NamedIntOption[2]
				{
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_NEWEST_FIRST_OPTION), 0),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_OLDEST_FIRST_OPTION), 1)
				};
			}
			return _playbackOptions;
		}
	}

	public static NamedIntOption[] PodcastSyncOptions
	{
		get
		{
			if (_syncOptions == null)
			{
				_syncOptions = new NamedIntOption[4]
				{
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_SYNC_UNPLAYED_OPTION), 2),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_SYNC_ALL_DOWNLOADED_OPTION), 0),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_SYNC_FIRST_UNPLAYED_OPTION), 3),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_SYNC_NONE_OPTION), 4)
				};
			}
			return _syncOptions;
		}
	}

	public static NamedIntOption[] ScreenGraphicsOptions
	{
		get
		{
			if (_screenGraphicsOptions == null)
			{
				_screenGraphicsOptions = new NamedIntOption[4]
				{
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_SCREEN_GRAPHICS_BASIC), 0),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_SCREEN_GRAPHICS_ADVANCED), 1),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_SCREEN_GRAPHICS_ADVANCED_ANIMATION), 2),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_SCREEN_GRAPHICS_PREMIUM), 3)
				};
			}
			return _screenGraphicsOptions;
		}
	}

	public NamedIntOption(IModelItemOwner owner, string description, int value)
		: base(owner, description, (EventHandler)null)
	{
		_value = value;
	}

	public static void SelectOptionByValue(Choice choice, int value)
	{
		for (int i = 0; i < choice.Options.Count; i++)
		{
			if (((NamedIntOption)choice.Options[i]).Value == value)
			{
				choice.ChosenIndex = i;
				break;
			}
		}
	}
}
