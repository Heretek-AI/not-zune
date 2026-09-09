using System;

namespace Microsoft.Zune.PerfTrace;

public sealed class PerfTrace
{
	[Flags]
	internal enum Flags : uint
	{
		DB_Mutex = 1u,
		Launch = 2u,
		QRS = 4u,
		Collection = 0x10u,
		All = uint.MaxValue
	}

	internal enum Level : byte
	{
		Fatal = 1,
		Error,
		Warning,
		Normal,
		Verbose
	}

	internal enum Tag
	{
		COMPONENT1 = 1129270577,
		COMPONENT2
	}

	public enum LAUNCH_EVENT
	{
		LAUNCHER_AT_WINMAIN = 1,
		WINMAIN_ABOUT_TO_LAUNCH,
		START_PHASE2_INIT,
		END_PHASE2_INIT,
		ZUNENATIVELIB_STARTUP,
		LAUNCHING_MANAGED_APP,
		IN_MANAGED_LAUNCH,
		REQUEST_UI_LOAD,
		REQUEST_UI_LOAD_COMPLETE,
		QUERYDB_BEGIN,
		QUERYDB_END,
		DEVICE_CONNECTED
	}

	private static readonly EtwTraceProvider _EventProvider;

	internal static readonly Guid ZUNE_ETW_CONTROL_GUID;

	internal static readonly Guid PERFTRACE_LAUNCHEVENT_GUID;

	internal static readonly Guid PerftraceUICollectionGuid;

	private PerfTrace()
	{
	}

	static PerfTrace()
	{
		ZUNE_ETW_CONTROL_GUID = new Guid(1496399467u, 53017, 20072, 142, 243, 135, 107, 12, 8, 232, 1);
		PERFTRACE_LAUNCHEVENT_GUID = new Guid(1815200785, 23272, 18087, 190, 166, 76, 236, 117, 51, 196, 218);
		PerftraceUICollectionGuid = new Guid(235915657u, 37778, 18043, 146, 252, 128, 212, 193, 153, 154, 151);
		_EventProvider = new EtwTraceProvider(ZUNE_ETW_CONTROL_GUID, "SOFTWARE\\Microsoft\\Zune");
	}

	internal static bool IsEnabled(EtwTraceProvider provider, Flags flag, Level level)
	{
		if ((uint)level <= (uint)provider.Level && provider.IsEnabled && ((uint)flag & provider.Flags) != 0)
		{
			return true;
		}
		return false;
	}

	internal static bool IsEnabled(Flags flags, Level level)
	{
		return IsEnabled(_EventProvider, flags, level);
	}

	internal static bool IsEnabled(Flags flags)
	{
		return IsEnabled(_EventProvider, flags, Level.Normal);
	}

	public static void PERFTRACE_LAUNCHEVENT(LAUNCH_EVENT launchEvent, uint data)
	{
		if (IsEnabled(Flags.Launch, Level.Normal))
		{
			_EventProvider.TraceEvent(4, PERFTRACE_LAUNCHEVENT_GUID, (byte)launchEvent, data);
		}
	}

	public static void TraceUICollectionEvent(UICollectionEvent traceEvent, string eventDetail)
	{
		if (IsEnabled(Flags.Collection, Level.Normal))
		{
			_EventProvider.TraceEvent(4, PerftraceUICollectionGuid, (byte)traceEvent, eventDetail);
		}
	}
}
