using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Subscription;

namespace ZuneUI;

public class SeriesSettings : ModelItem
{
	private Choice m_playbackChoice;

	private Choice m_keepEpisodesChoice;

	private Choice m_syncChoice;

	private Choice m_keepEpisodesChoicePerPhone;

	private SubscriptionManager m_subscriptionManager;

	private int m_seriesId;

	private UIDevice m_device;

	private uint keepEpisodesOriginalValue;

	private ESeriesPlaybackOrder playbackOrderOriginalValue;

	private PodcastSyncLimit syncRuleOriginalValue;

	private int keepEpisodesPerPhoneOriginalValue;

	public Choice PlaybackChoice => m_playbackChoice;

	public Choice KeepEpisodesChoice => m_keepEpisodesChoice;

	public Choice SyncChoice => m_syncChoice;

	public Choice KeepEpisodesChoicePerPhone => m_keepEpisodesChoicePerPhone;

	public SeriesSettings(SubscriptionManager subscriptionManager, int seriesId)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected I4, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		m_subscriptionManager = subscriptionManager;
		m_seriesId = seriesId;
		m_device = SyncControls.Instance.CurrentDevice;
		keepEpisodesOriginalValue = (uint)ClientConfiguration.Series.PodcastDefaultKeepEpisodes;
		playbackOrderOriginalValue = (ESeriesPlaybackOrder)ClientConfiguration.Series.PodcastDefaultPlaybackOrder;
		m_subscriptionManager.GetManagementSettings(m_seriesId, ref keepEpisodesOriginalValue, ref playbackOrderOriginalValue);
		m_keepEpisodesChoice = new Choice((IModelItemOwner)(object)this);
		m_keepEpisodesChoice.Options = NamedIntOption.PodcastKeepOptions;
		NamedIntOption.SelectOptionByValue(m_keepEpisodesChoice, (int)keepEpisodesOriginalValue);
		m_playbackChoice = new Choice((IModelItemOwner)(object)this);
		m_playbackChoice.Options = NamedIntOption.PodcastPlaybackOptions;
		NamedIntOption.SelectOptionByValue(m_playbackChoice, (int)playbackOrderOriginalValue);
		m_syncChoice = new Choice((IModelItemOwner)(object)this);
		m_syncChoice.Options = NamedIntOption.PodcastSyncOptions;
		if (m_device.IsValid)
		{
			syncRuleOriginalValue = m_device.GetPodcastSyncLimit(m_seriesId);
			NamedIntOption.SelectOptionByValue(m_syncChoice, (int)syncRuleOriginalValue);
		}
		m_keepEpisodesChoicePerPhone = new Choice((IModelItemOwner)(object)this);
		m_keepEpisodesChoicePerPhone.Options = NamedIntOption.PodcastKeepOptions;
		if (m_device.IsValid)
		{
			keepEpisodesPerPhoneOriginalValue = m_device.GetPodcastSyncLimitWithValue(m_seriesId);
			NamedIntOption.SelectOptionByValue(m_keepEpisodesChoicePerPhone, keepEpisodesPerPhoneOriginalValue);
		}
	}

	public void Apply()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		uint value = (uint)((NamedIntOption)m_keepEpisodesChoice.ChosenValue).Value;
		ESeriesPlaybackOrder val = (ESeriesPlaybackOrder)((NamedIntOption)m_playbackChoice.ChosenValue).Value;
		if (value != keepEpisodesOriginalValue || val != playbackOrderOriginalValue)
		{
			m_subscriptionManager.SetManagementSettings(m_seriesId, value, val);
			flag = true;
		}
		if (m_device.IsValid)
		{
			PodcastSyncLimit value2 = (PodcastSyncLimit)((NamedIntOption)m_syncChoice.ChosenValue).Value;
			if (value2 != syncRuleOriginalValue)
			{
				m_device.SetPodcastSyncLimit(m_seriesId, value2);
				flag = true;
			}
		}
		if (flag)
		{
			SyncControls.Instance.CurrentDevice.BeginSync(userInitiated: true, syncOnNextNotify: false);
		}
	}
}
