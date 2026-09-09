using System;
using System.Collections;

namespace ZuneUI;

public class TelemetryInfo
{
	public ETelemetryEvent eEvent = (ETelemetryEvent)(-1);

	public string dcsUri = "";

	public IDictionary Args;

	public DateTime utcTime;

	public long elapsedTime;

	public bool fSessionDatapoint;

	public TelemetryInfo(ETelemetryEvent evt, string uri, IDictionary args, bool fSession)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		eEvent = evt;
		dcsUri = uri;
		Args = args;
		utcTime = DateTime.UtcNow;
		fSessionDatapoint = fSession;
		elapsedTime = (long)utcTime.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
	}
}
