using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class Telemetry
{
	private const string sc_EpochTime = "@epochTime";

	private const string sc_SessionEventType = "@EventType";

	private const string sc_SessionData = "@Data";

	private static Telemetry m_instance;

	private Queue<TelemetryInfo> m_queue;

	private bool m_uploadAllowed;

	private static string[] _supportedTags = new string[25]
	{
		"AlbumId", "ArtistId", "CategoryId", "ChannelId", "ChannelUrl", "EpisodeId", "AppId", "GenreId", "MovieId", "MovieTrailerId",
		"NetworkId", "PlaylistId", "PodcastId", "SelectedPivot", "SeasonId", "SeriesId", "ShortId", "SubGenreId", "VideoId", "TrackId",
		"HubId", "PlaylistCategoryId", "Context", "SelectionId", "SelectionTitle"
	};

	private Hashtable m_sqmToEventMap;

	public static Telemetry Instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = new Telemetry();
			}
			return m_instance;
		}
	}

	private Telemetry()
	{
		m_queue = new Queue<TelemetryInfo>();
		m_sqmToEventMap = new Hashtable();
		m_sqmToEventMap[(object)(SQMDataId)152] = new DatapointInfo((ETelemetryEvent)1);
		m_sqmToEventMap[(object)(SQMDataId)139] = new DatapointInfo((ETelemetryEvent)4);
		m_sqmToEventMap[(object)(SQMDataId)196] = new DatapointInfo((ETelemetryEvent)3);
		m_sqmToEventMap[(object)(SQMDataId)185] = new DatapointInfo((ETelemetryEvent)2, "MixViewTime", fSess: true);
		m_sqmToEventMap[(object)(SQMDataId)210] = new DatapointInfo((ETelemetryEvent)8, "PlaybackAudio", fSess: true);
		m_sqmToEventMap[(object)(SQMDataId)211] = new DatapointInfo((ETelemetryEvent)8, "PlaybackVideo", fSess: true);
		m_sqmToEventMap[(object)(SQMDataId)212] = new DatapointInfo((ETelemetryEvent)8, "PlaybackPhoto", fSess: true);
	}

	internal void StartUpload()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		lock (m_queue)
		{
			m_uploadAllowed = true;
			if (m_queue.Count > 0)
			{
				Application.DeferredInvoke(new DeferredInvokeHandler(ProcessQueue), (object)null, (DeferredInvokePriority)1);
			}
		}
	}

	private Hashtable FilterPageArgs(IDictionary args)
	{
		Hashtable hashtable = new Hashtable();
		if (args != null)
		{
			foreach (DictionaryEntry arg in args)
			{
				if (arg.Key is string && Array.IndexOf(_supportedTags, (string)arg.Key) != -1 && (arg.Value is string || arg.Value is Guid || arg.Value is int))
				{
					hashtable.Add(arg.Key, arg.Value);
				}
			}
		}
		return hashtable;
	}

	public void ReportNavigation(string command, IDictionary args)
	{
		if (command != null)
		{
			Hashtable args2 = FilterPageArgs(args);
			TelemetryInfo info = new TelemetryInfo((ETelemetryEvent)(-1), command, args2, fSession: false);
			QueueTelemetry(info);
		}
	}

	public void ReportPlaybackTime(int timeData)
	{
		if (timeData > 0)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("@Data", timeData);
			hashtable.Add("@EventType", "CumulativePlaybackTime");
			TelemetryInfo info = new TelemetryInfo((ETelemetryEvent)8, "", hashtable, fSession: true);
			QueueTelemetry(info);
		}
	}

	public void ReportEvent(SQMDataPoint datapoint, int nData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Invalid comparison between Unknown and I4
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		SQMDataId id = datapoint.id;
		bool flag = (int)datapoint.action == 0 || (int)datapoint.action == 1;
		if ((int)id != 0 && flag && m_sqmToEventMap.ContainsKey(id))
		{
			Hashtable hashtable = new Hashtable();
			DatapointInfo datapointInfo = (DatapointInfo)m_sqmToEventMap[id];
			if (datapointInfo.IsSession)
			{
				hashtable.Add("@Data", nData);
				hashtable.Add("@EventType", datapointInfo.TypeName);
			}
			TelemetryInfo info = new TelemetryInfo(datapointInfo.Event, "", hashtable, datapointInfo.IsSession);
			QueueTelemetry(info);
		}
	}

	public void ReportSearch(string search)
	{
		string uri = "Search";
		Hashtable hashtable = new Hashtable();
		hashtable.Add("zune_query", search);
		TelemetryInfo info = new TelemetryInfo((ETelemetryEvent)(-1), uri, hashtable, fSession: false);
		QueueTelemetry(info);
	}

	public void ReportPageLoad(string pageUri, int pageLoadTime, IDictionary args)
	{
		if (!string.IsNullOrEmpty(pageUri))
		{
			string uri = "PageLoadTime";
			Hashtable hashtable = FilterPageArgs(args);
			hashtable.Add(pageUri, pageLoadTime);
			TelemetryInfo info = new TelemetryInfo((ETelemetryEvent)(-1), uri, hashtable, fSession: false);
			QueueTelemetry(info);
		}
	}

	private void QueueTelemetry(TelemetryInfo info)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		lock (m_queue)
		{
			m_queue.Enqueue(info);
			if (m_uploadAllowed && m_queue.Count == 1)
			{
				Application.DeferredInvoke(new DeferredInvokeHandler(ProcessQueue), (object)null, (DeferredInvokePriority)1);
			}
		}
	}

	private void ProcessQueue(object state)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		TelemetryInfo telemetryInfo = null;
		bool flag;
		lock (m_queue)
		{
			if (m_queue.Count > 0)
			{
				telemetryInfo = m_queue.Dequeue();
			}
			flag = m_queue.Count > 0;
		}
		if (telemetryInfo != null)
		{
			SendTelemetry(telemetryInfo);
		}
		if (flag)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(ProcessQueue), (object)null, (DeferredInvokePriority)1);
		}
	}

	private void SendTelemetryInfo(TelemetryInfo info)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(SendTelemetry), (object)info, (DeferredInvokePriority)1);
	}

	private void SendTelemetry(object state)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Invalid comparison between Unknown and I4
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		TelemetryInfo telemetryInfo = (TelemetryInfo)state;
		telemetryInfo.Args.Add("@epochTime", telemetryInfo.elapsedTime);
		if (telemetryInfo.fSessionDatapoint)
		{
			int num = Convert.ToInt32(telemetryInfo.Args["@Data"]);
			string text = telemetryInfo.Args["@EventType"].ToString();
			TelemetryAPI.AddToSessionEvent(telemetryInfo.eEvent, text, num);
		}
		else if ((int)telemetryInfo.eEvent == -1)
		{
			TelemetryAPI.SendDatapoint(telemetryInfo.dcsUri, telemetryInfo.Args);
		}
		else if (telemetryInfo.Args.Contains("@EventType"))
		{
			TelemetryAPI.SendEvent(telemetryInfo.eEvent, telemetryInfo.Args["@EventType"].ToString());
		}
		else
		{
			TelemetryAPI.SendEvent(telemetryInfo.eEvent, (string)null);
		}
	}
}
