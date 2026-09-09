using System;
using System.Runtime.InteropServices;
using ZuneUI;

namespace Microsoft.Zune.Util;

public static class SQMLog
{
	private static SQMStreamPositionType[] c_rgSPTArrayAllStrings = (SQMStreamPositionType[])(object)new SQMStreamPositionType[9]
	{
		(SQMStreamPositionType)2,
		(SQMStreamPositionType)2,
		(SQMStreamPositionType)2,
		(SQMStreamPositionType)2,
		(SQMStreamPositionType)2,
		(SQMStreamPositionType)2,
		(SQMStreamPositionType)2,
		(SQMStreamPositionType)2,
		(SQMStreamPositionType)2
	};

	public static void Log(SQMDataId sqmDataId, int nData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected I4, but got Unknown
		SQMDataPoint val = FindDataPoint(sqmDataId);
		if ((int)val.id != 0)
		{
			SQMAction action = val.action;
			switch ((int)action)
			{
			case 0:
				SQMAddWrapper(val.GetName(), nData);
				break;
			case 1:
				SQMAddWrapper(val.GetName(), 1);
				break;
			case 2:
				SQMSetFlagWrapper(val.GetName(), (nData != 0) ? true : false);
				break;
			case 3:
				SQMSetBitsWrapper(val.GetName(), (uint)nData);
				break;
			default:
				throw new ArgumentException("Datapoint " + val.GetName() + " is not a simple DWORD-type datapoint.  Use LogToStream for stream-type datapoints.");
			}
			Telemetry.Instance.ReportEvent(val, nData);
		}
	}

	public static void LogToStream(SQMDataId sqmDataId, params string[] args)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Invalid comparison between Unknown and I4
		SQMDataPoint val = FindDataPoint(sqmDataId);
		if ((int)val.id != 0)
		{
			if ((int)val.action != 5)
			{
				throw new ArgumentException("Datapoint " + val.GetName() + " is not a MixedStream-type datapoint.  Error in parameters/paramtypes, or in datapoint declaration.");
			}
			if (args.Length > 9)
			{
				throw new ArgumentException("Too many (or too few) number params.  SQM only allows up to 9 params. Datapoint: " + val.GetName(), "args.Length");
			}
			if (args.Length != val.argCount)
			{
				throw new ArgumentException("Passed-in arg count doesn't match datapoint declaration: " + val.GetName(), "args.Length");
			}
			if (args.Length == 1)
			{
				SQMAddToStream(val.GetName(), c_rgSPTArrayAllStrings, (uint)args.Length, args[0]);
			}
			else if (args.Length == 2)
			{
				SQMAddToStream(val.GetName(), c_rgSPTArrayAllStrings, (uint)args.Length, args[0], args[1]);
			}
			else if (args.Length == 3)
			{
				SQMAddToStream(val.GetName(), c_rgSPTArrayAllStrings, (uint)args.Length, args[0], args[1], args[2]);
			}
			else if (args.Length == 4)
			{
				SQMAddToStream(val.GetName(), c_rgSPTArrayAllStrings, (uint)args.Length, args[0], args[1], args[2], args[3]);
			}
			else if (args.Length == 5)
			{
				SQMAddToStream(val.GetName(), c_rgSPTArrayAllStrings, (uint)args.Length, args[0], args[1], args[2], args[3], args[4]);
			}
			else if (args.Length == 6)
			{
				SQMAddToStream(val.GetName(), c_rgSPTArrayAllStrings, (uint)args.Length, args[0], args[1], args[2], args[3], args[4], args[5]);
			}
			else if (args.Length == 7)
			{
				SQMAddToStream(val.GetName(), c_rgSPTArrayAllStrings, (uint)args.Length, args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
			}
			else if (args.Length == 8)
			{
				SQMAddToStream(val.GetName(), c_rgSPTArrayAllStrings, (uint)args.Length, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
			}
			else if (args.Length == 9)
			{
				SQMAddToStream(val.GetName(), c_rgSPTArrayAllStrings, (uint)args.Length, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
			}
		}
	}

	public static void LogToStream(SQMDataId sqmDataId, params uint[] args)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Invalid comparison between Unknown and I4
		SQMDataPoint val = FindDataPoint(sqmDataId);
		if ((int)val.id != 0)
		{
			if ((int)val.action != 4)
			{
				throw new ArgumentException("Datapoint " + val.GetName() + " is not a NumStream-type datapoint.  Error in parameters or in datapoint declaration.");
			}
			if (args.Length < 1 || args.Length > 9)
			{
				throw new ArgumentException("Too many (or too few) number params.  SQM only allows up to 9 params. Datapoint: " + val.GetName(), "args.Length");
			}
			if (args.Length != val.argCount)
			{
				throw new ArgumentException("Passed-in arg count doesn't match datapoint declaration: " + val.GetName(), "args.Length");
			}
			if (args.Length == 1)
			{
				SQMAddNumbersToStream(val.GetName(), (uint)args.Length, args[0]);
			}
			else if (args.Length == 2)
			{
				SQMAddNumbersToStream(val.GetName(), (uint)args.Length, args[0], args[1]);
			}
			else if (args.Length == 3)
			{
				SQMAddNumbersToStream(val.GetName(), (uint)args.Length, args[0], args[1], args[2]);
			}
			else if (args.Length == 4)
			{
				SQMAddNumbersToStream(val.GetName(), (uint)args.Length, args[0], args[1], args[2], args[3]);
			}
			else if (args.Length == 5)
			{
				SQMAddNumbersToStream(val.GetName(), (uint)args.Length, args[0], args[1], args[2], args[3], args[4]);
			}
			else if (args.Length == 6)
			{
				SQMAddNumbersToStream(val.GetName(), (uint)args.Length, args[0], args[1], args[2], args[3], args[4], args[5]);
			}
			else if (args.Length == 7)
			{
				SQMAddNumbersToStream(val.GetName(), (uint)args.Length, args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
			}
			else if (args.Length == 8)
			{
				SQMAddNumbersToStream(val.GetName(), (uint)args.Length, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
			}
			else if (args.Length == 9)
			{
				SQMAddNumbersToStream(val.GetName(), (uint)args.Length, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
			}
		}
	}

	private static SQMDataPoint FindDataPoint(SQMDataId sqmDataId)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		SQMDataPoint[] s_rgSQMDataPoints = SQMData.s_rgSQMDataPoints;
		foreach (SQMDataPoint val in s_rgSQMDataPoints)
		{
			if (val.id == sqmDataId)
			{
				return val;
			}
		}
		return SQMData.s_sqmDataPointInvalid;
	}

	[DllImport("ZuneNativeLib", CharSet = CharSet.Unicode)]
	private static extern void SQMAddWrapper(string sqmDataId, int nData);

	[DllImport("ZuneNativeLib", CharSet = CharSet.Unicode)]
	private static extern void SQMSetFlagWrapper(string sqmDataId, bool fSet);

	[DllImport("ZuneNativeLib", CharSet = CharSet.Unicode)]
	private static extern void SQMSetBitsWrapper(string sqmDataId, uint dwBits);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	private static extern void SQMAddNumbersToStream(string sqmDataId, uint countTotal, uint dw1);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	private static extern void SQMAddNumbersToStream(string sqmDataId, uint countTotal, uint dw1, uint dw2);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	private static extern void SQMAddNumbersToStream(string sqmDataId, uint countTotal, uint dw1, uint dw2, uint dw3);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	private static extern void SQMAddNumbersToStream(string sqmDataId, uint countTotal, uint dw1, uint dw2, uint dw3, uint dw4);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	private static extern void SQMAddNumbersToStream(string sqmDataId, uint countTotal, uint dw1, uint dw2, uint dw3, uint dw4, uint dw5);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	private static extern void SQMAddNumbersToStream(string sqmDataId, uint countTotal, uint dw1, uint dw2, uint dw3, uint dw4, uint dw5, uint dw6);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	private static extern void SQMAddNumbersToStream(string sqmDataId, uint countTotal, uint dw1, uint dw2, uint dw3, uint dw4, uint dw5, uint dw6, uint dw7);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	private static extern void SQMAddNumbersToStream(string sqmDataId, uint countTotal, uint dw1, uint dw2, uint dw3, uint dw4, uint dw5, uint dw6, uint dw7, uint dw8);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	private static extern void SQMAddNumbersToStream(string sqmDataId, uint countTotal, uint dw1, uint dw2, uint dw3, uint dw4, uint dw5, uint dw6, uint dw7, uint dw8, uint dw9);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "SQMAddToStreamWsz")]
	private static extern void SQMAddToStream(string sqmDataId, [MarshalAs(UnmanagedType.LPArray)] SQMStreamPositionType[] sptTypes, uint countTotal, string s1);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "SQMAddToStreamWsz")]
	private static extern void SQMAddToStream(string sqmDataId, [MarshalAs(UnmanagedType.LPArray)] SQMStreamPositionType[] sptTypes, uint countTotal, string s1, string s2);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "SQMAddToStreamWsz")]
	private static extern void SQMAddToStream(string sqmDataId, [MarshalAs(UnmanagedType.LPArray)] SQMStreamPositionType[] sptTypes, uint countTotal, string s1, string s2, string s3);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "SQMAddToStreamWsz")]
	private static extern void SQMAddToStream(string sqmDataId, [MarshalAs(UnmanagedType.LPArray)] SQMStreamPositionType[] sptTypes, uint countTotal, string s1, string s2, string s3, string s4);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "SQMAddToStreamWsz")]
	private static extern void SQMAddToStream(string sqmDataId, [MarshalAs(UnmanagedType.LPArray)] SQMStreamPositionType[] sptTypes, uint countTotal, string s1, string s2, string s3, string s4, string s5);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "SQMAddToStreamWsz")]
	private static extern void SQMAddToStream(string sqmDataId, [MarshalAs(UnmanagedType.LPArray)] SQMStreamPositionType[] sptTypes, uint countTotal, string s1, string s2, string s3, string s4, string s5, string s6);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "SQMAddToStreamWsz")]
	private static extern void SQMAddToStream(string sqmDataId, [MarshalAs(UnmanagedType.LPArray)] SQMStreamPositionType[] sptTypes, uint countTotal, string s1, string s2, string s3, string s4, string s5, string s6, string s7);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "SQMAddToStreamWsz")]
	private static extern void SQMAddToStream(string sqmDataId, [MarshalAs(UnmanagedType.LPArray)] SQMStreamPositionType[] sptTypes, uint countTotal, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8);

	[DllImport("ZuneNativeLib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "SQMAddToStreamWsz")]
	private static extern void SQMAddToStream(string sqmDataId, [MarshalAs(UnmanagedType.LPArray)] SQMStreamPositionType[] sptTypes, uint countTotal, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9);
}
