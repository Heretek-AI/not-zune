using System.Collections.Generic;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class FamilySettings
{
	private Dictionary<string, FamilySetting> _settings;

	private int _userId;

	public int UserId
	{
		get
		{
			return _userId;
		}
		set
		{
			_userId = value;
		}
	}

	public Dictionary<string, FamilySetting> Settings
	{
		get
		{
			return _settings;
		}
		set
		{
			_settings = value;
		}
	}

	public FamilySettings(int userId)
	{
		_userId = userId;
		_settings = null;
		ReloadSettings();
	}

	public FamilySettings()
	{
		_userId = -1;
		_settings = new Dictionary<string, FamilySetting>();
	}

	public void ReloadSettings()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		Settings = new Dictionary<string, FamilySetting>();
		int[] array = default(int[]);
		FamilySettingsManager.Instance.GetSettingIdsForUser(UserId, ref array);
		if (array != null && array.Length > 0)
		{
			string text = default(string);
			int ratingLevel = default(int);
			bool blockUnrated = default(bool);
			for (int i = 0; i < array.Length; i++)
			{
				FamilySettingsManager.Instance.GetSetting(array[i], ref text, ref ratingLevel, ref blockUnrated);
				Settings[text] = new FamilySetting(array[i], text, ratingLevel, blockUnrated);
			}
		}
	}

	public void CommitSettings()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (UserId == -1)
		{
			return;
		}
		int ratingId = default(int);
		foreach (FamilySetting value in Settings.Values)
		{
			if (value.HasChanged)
			{
				FamilySettingsManager.Instance.AddSetting(value.RatingId, UserId, value.RatingSystem, value.RatingLevel, value.BlockUnrated, ref ratingId);
				if (value.RatingId == -1)
				{
					value.RatingId = ratingId;
				}
			}
		}
	}

	public void SetSetting(string ratingSystem, int ratingLevel, bool blockUnrated)
	{
		if (!string.IsNullOrEmpty(ratingSystem))
		{
			if (Settings.ContainsKey(ratingSystem))
			{
				FamilySetting familySetting = Settings[ratingSystem];
				familySetting.RatingLevel = ratingLevel;
				familySetting.BlockUnrated = blockUnrated;
			}
			else
			{
				Settings.Add(ratingSystem, new FamilySetting(ratingSystem, ratingLevel, blockUnrated));
			}
		}
	}
}
