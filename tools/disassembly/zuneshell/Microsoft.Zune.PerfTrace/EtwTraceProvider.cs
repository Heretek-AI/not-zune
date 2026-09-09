using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Microsoft.Zune.PerfTrace;

internal class EtwTraceProvider
{
	internal sealed class RequestCodes
	{
		internal const uint GetAllData = 0u;

		internal const uint GetSingleInstance = 1u;

		internal const uint SetSingleInstance = 2u;

		internal const uint SetSingleItem = 3u;

		internal const uint EnableEvents = 4u;

		internal const uint DisableEvents = 5u;

		internal const uint EnableCollection = 6u;

		internal const uint DisableCollection = 7u;

		internal const uint RegInfo = 8u;

		internal const uint ExecuteMethod = 9u;

		private RequestCodes()
		{
		}
	}

	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct MofField
	{
		[FieldOffset(0)]
		internal unsafe void* DataPointer;

		[FieldOffset(8)]
		internal uint DataLength;

		[FieldOffset(12)]
		internal uint DataType;
	}

	[StructLayout(LayoutKind.Explicit, Size = 304)]
	internal struct BaseEvent
	{
		[FieldOffset(0)]
		internal uint BufferSize;

		[FieldOffset(4)]
		internal byte EventType;

		[FieldOffset(5)]
		internal byte Level;

		[FieldOffset(6)]
		internal ushort Version;

		[FieldOffset(8)]
		internal ulong HistoricalContext;

		[FieldOffset(16)]
		internal long TimeStamp;

		[FieldOffset(24)]
		internal Guid Guid;

		[FieldOffset(40)]
		internal uint ClientContext;

		[FieldOffset(44)]
		internal uint Flags;

		[FieldOffset(48)]
		internal MofField UserData;
	}

	internal unsafe delegate uint EtwProc(uint requestCode, IntPtr requestContext, IntPtr bufferSize, byte* buffer);

	internal struct CSTRACE_GUID_REGISTRATION
	{
		internal unsafe Guid* Guid;

		internal uint RegHandle;
	}

	internal struct TraceGuidRegistration
	{
		internal unsafe Guid* Guid;

		internal unsafe void* RegHandle;
	}

	private const ushort _version = 0;

	private EtwProc _etwProc;

	private ulong _registrationHandle;

	private ulong _traceHandle;

	private byte _level;

	private uint _flags;

	private bool _enabled;

	internal uint Flags => _flags;

	internal byte Level => _level;

	internal bool IsEnabled => _enabled;

	internal EtwTraceProvider(Guid controlGuid, string regPath)
	{
		_level = 0;
		_flags = 0u;
		_enabled = false;
		_traceHandle = 0uL;
		_registrationHandle = 0uL;
		RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(regPath);
		int num = 1;
		if (registryKey != null)
		{
			object value = registryKey.GetValue("EtwEnabled");
			if (value is int)
			{
				num = (int)value;
			}
		}
		if (num <= 0)
		{
			_enabled = false;
		}
		else
		{
			Register(controlGuid);
		}
	}

	~EtwTraceProvider()
	{
		UnregisterTraceGuids(_registrationHandle);
		GC.KeepAlive(_etwProc);
	}

	internal unsafe uint ControllerChangeCallback(uint requestCode, IntPtr context, IntPtr bufferSize, byte* byteBuffer)
	{
		try
		{
			switch (requestCode)
			{
			case 4u:
				_traceHandle = ((BaseEvent*)byteBuffer)->HistoricalContext;
				_flags = (uint)GetTraceEnableFlags(((BaseEvent*)byteBuffer)->HistoricalContext);
				_level = GetTraceEnableLevel(((BaseEvent*)byteBuffer)->HistoricalContext);
				if (_flags == 0 && _level == 0)
				{
					_flags = uint.MaxValue;
					_level = 4;
				}
				_enabled = true;
				break;
			case 5u:
				_enabled = false;
				_traceHandle = 0uL;
				_level = 0;
				_flags = 0u;
				break;
			default:
				_enabled = false;
				_traceHandle = 0uL;
				break;
			}
			return 0u;
		}
		catch (Exception ex)
		{
			if (ex is NullReferenceException || ex is SEHException)
			{
				throw;
			}
			return 0u;
		}
	}

	private unsafe uint Register(Guid controlGuid)
	{
		TraceGuidRegistration guidReg = default(TraceGuidRegistration);
		Guid guid = new Guid(3029687280u, 15089, 18240, 180, 117, 153, 5, 93, 63, 233, 170);
		_etwProc = ControllerChangeCallback;
		guidReg.Guid = &guid;
		guidReg.RegHandle = null;
		return RegisterTraceGuids(_etwProc, null, ref controlGuid, 1u, ref guidReg, null, null, out _registrationHandle);
	}

	internal void TraceEvent(byte level, Guid eventGuid, byte eventType)
	{
		TraceEvent(level, eventGuid, eventType, null, null);
	}

	internal void TraceEvent(byte level, Guid eventGuid, byte eventType, object data0)
	{
		TraceEvent(level, eventGuid, eventType, data0, null);
	}

	internal void TraceEvent(byte level, Guid eventGuid, byte eventType, object data0, object data1)
	{
		TraceEvent(level, eventGuid, eventType, data0, data1, null, null, null, null, null, null, null);
	}

	internal void TraceEvent(byte level, Guid eventGuid, byte eventType, object data0, object data1, object data2)
	{
		TraceEvent(level, eventGuid, eventType, data0, data1, data2, null, null, null, null, null, null);
	}

	internal void TraceEvent(byte level, Guid eventGuid, byte eventType, object data0, object data1, object data2, object data3)
	{
		TraceEvent(level, eventGuid, eventType, data0, data1, data2, data3, null, null, null, null, null);
	}

	internal void TraceEvent(byte level, Guid eventGuid, byte eventType, object data0, object data1, object data2, object data3, object data4)
	{
		TraceEvent(level, eventGuid, eventType, data0, data1, data2, data3, data4, null, null, null, null);
	}

	internal void TraceEvent(byte level, Guid eventGuid, byte eventType, object data0, object data1, object data2, object data3, object data4, object data5)
	{
		TraceEvent(level, eventGuid, eventType, data0, data1, data2, data3, data4, data5, null, null, null);
	}

	internal void TraceEvent(byte level, Guid eventGuid, byte eventType, object data0, object data1, object data2, object data3, object data4, object data5, object data6)
	{
		TraceEvent(level, eventGuid, eventType, data0, data1, data2, data3, data4, data5, data6, null, null);
	}

	internal void TraceEvent(byte level, Guid eventGuid, byte eventType, object data0, object data1, object data2, object data3, object data4, object data5, object data6, object data7)
	{
		TraceEvent(level, eventGuid, eventType, data0, data1, data2, data3, data4, data5, data6, data7, null);
	}

	internal unsafe uint TraceEvent(byte level, Guid eventGuid, byte evtype, object data0, object data1, object data2, object data3, object data4, object data5, object data6, object data7, object data8)
	{
		uint num = 0u;
		char* ptr = stackalloc char[144];
		uint offSet = 0u;
		char* ptr2 = ptr;
		int num2 = 0;
		uint num3 = 0u;
		int num4 = 0;
		string text2;
		string text3;
		string text4;
		string text5;
		string text6;
		string text7;
		string text8;
		string text9;
		string text = (text2 = (text3 = (text4 = (text5 = (text6 = (text7 = (text8 = (text9 = ""))))))));
		BaseEvent baseEvent = default(BaseEvent);
		baseEvent.ClientContext = 0u;
		baseEvent.Flags = 1179648u;
		baseEvent.Guid = eventGuid;
		baseEvent.EventType = evtype;
		baseEvent.Level = level;
		baseEvent.Version = 0;
		MofField* ptr3 = null;
		if (data0 != null)
		{
			num3++;
			ptr3 = &baseEvent.UserData + num4++;
			if ((text = ProcessOneObject(data0, ptr3, ptr2, ref offSet)) != null)
			{
				num2 |= 1;
			}
		}
		if (data1 != null)
		{
			num3++;
			ptr3 = &baseEvent.UserData + num4++;
			ptr2 = ptr + offSet;
			if ((text2 = ProcessOneObject(data1, ptr3, ptr2, ref offSet)) != null)
			{
				num2 |= 2;
			}
		}
		if (data2 != null)
		{
			num3++;
			ptr3 = &baseEvent.UserData + num4++;
			ptr2 = ptr + offSet;
			if ((text3 = ProcessOneObject(data2, ptr3, ptr2, ref offSet)) != null)
			{
				num2 |= 4;
			}
		}
		if (data3 != null)
		{
			num3++;
			ptr3 = &baseEvent.UserData + num4++;
			ptr2 = ptr + offSet;
			if ((text4 = ProcessOneObject(data3, ptr3, ptr2, ref offSet)) != null)
			{
				num2 |= 8;
			}
		}
		if (data4 != null)
		{
			num3++;
			ptr3 = &baseEvent.UserData + num4++;
			ptr2 = ptr + offSet;
			if ((text5 = ProcessOneObject(data4, ptr3, ptr2, ref offSet)) != null)
			{
				num2 |= 0x10;
			}
		}
		if (data5 != null)
		{
			num3++;
			ptr3 = &baseEvent.UserData + num4++;
			ptr2 = ptr + offSet;
			if ((text6 = ProcessOneObject(data5, ptr3, ptr2, ref offSet)) != null)
			{
				num2 |= 0x20;
			}
		}
		if (data6 != null)
		{
			num3++;
			ptr3 = &baseEvent.UserData + num4++;
			ptr2 = ptr + offSet;
			if ((text7 = ProcessOneObject(data6, ptr3, ptr2, ref offSet)) != null)
			{
				num2 |= 0x40;
			}
		}
		if (data7 != null)
		{
			num3++;
			ptr3 = &baseEvent.UserData + num4++;
			ptr2 = ptr + offSet;
			if ((text8 = ProcessOneObject(data7, ptr3, ptr2, ref offSet)) != null)
			{
				num2 |= 0x80;
			}
		}
		if (data8 != null)
		{
			num3++;
			ptr3 = &baseEvent.UserData + num4++;
			ptr2 = ptr + offSet;
			if ((text9 = ProcessOneObject(data8, ptr3, ptr2, ref offSet)) != null)
			{
				num2 |= 0x100;
			}
		}
		if (ptr2 - ptr > 144)
		{
			return 1u;
		}
		fixed (char* dataPointer = text)
		{
			fixed (char* dataPointer2 = text2)
			{
				fixed (char* dataPointer3 = text3)
				{
					fixed (char* dataPointer4 = text4)
					{
						fixed (char* dataPointer5 = text5)
						{
							fixed (char* dataPointer6 = text6)
							{
								fixed (char* dataPointer7 = text7)
								{
									fixed (char* dataPointer8 = text8)
									{
										fixed (char* dataPointer9 = text9)
										{
											int num5 = 0;
											if ((num2 & 1) != 0)
											{
												(&baseEvent.UserData)[num5].DataLength = (uint)((text.Length + 1) * 2);
												(&baseEvent.UserData)[num5].DataPointer = dataPointer;
											}
											num5++;
											if ((num2 & 2) != 0)
											{
												(&baseEvent.UserData)[num5].DataLength = (uint)((text2.Length + 1) * 2);
												(&baseEvent.UserData)[num5].DataPointer = dataPointer2;
											}
											num5++;
											if ((num2 & 4) != 0)
											{
												(&baseEvent.UserData)[num5].DataLength = (uint)((text3.Length + 1) * 2);
												(&baseEvent.UserData)[num5].DataPointer = dataPointer3;
											}
											num5++;
											if ((num2 & 8) != 0)
											{
												(&baseEvent.UserData)[num5].DataLength = (uint)((text4.Length + 1) * 2);
												(&baseEvent.UserData)[num5].DataPointer = dataPointer4;
											}
											num5++;
											if ((num2 & 0x10) != 0)
											{
												(&baseEvent.UserData)[num5].DataLength = (uint)((text5.Length + 1) * 2);
												(&baseEvent.UserData)[num5].DataPointer = dataPointer5;
											}
											num5++;
											if ((num2 & 0x20) != 0)
											{
												(&baseEvent.UserData)[num5].DataLength = (uint)((text6.Length + 1) * 2);
												(&baseEvent.UserData)[num5].DataPointer = dataPointer6;
											}
											num5++;
											if ((num2 & 0x40) != 0)
											{
												(&baseEvent.UserData)[num5].DataLength = (uint)((text7.Length + 1) * 2);
												(&baseEvent.UserData)[num5].DataPointer = dataPointer7;
											}
											num5++;
											if ((num2 & 0x80) != 0)
											{
												(&baseEvent.UserData)[num5].DataLength = (uint)((text8.Length + 1) * 2);
												(&baseEvent.UserData)[num5].DataPointer = dataPointer8;
											}
											num5++;
											if ((num2 & 0x100) != 0)
											{
												(&baseEvent.UserData)[num5].DataLength = (uint)((text9.Length + 1) * 2);
												(&baseEvent.UserData)[num5].DataPointer = dataPointer9;
											}
											baseEvent.BufferSize = (uint)(48 + num4 * sizeof(MofField));
											num = TraceEvent(_traceHandle, (char*)(&baseEvent));
										}
									}
								}
							}
						}
					}
				}
			}
		}
		return num;
	}

	private unsafe string ProcessOneObject(object data, MofField* mofField, char* ptr, ref uint offSet)
	{
		return EncodeObject(data, mofField, ptr, ref offSet);
	}

	private unsafe string EncodeObject(object data, MofField* mofField, char* ptr, ref uint offSet)
	{
		if (data == null)
		{
			mofField->DataLength = 0u;
			mofField->DataPointer = null;
			return null;
		}
		Type type = data.GetType();
		if (type.IsEnum)
		{
			data = Convert.ChangeType(data, Enum.GetUnderlyingType(type), CultureInfo.InvariantCulture);
		}
		if (data is sbyte)
		{
			mofField->DataLength = 1u;
			*(sbyte*)ptr = (sbyte)data;
			mofField->DataPointer = ptr;
			offSet++;
		}
		else if (data is byte)
		{
			mofField->DataLength = 1u;
			*(byte*)ptr = (byte)data;
			mofField->DataPointer = ptr;
			offSet++;
		}
		else if (data is short)
		{
			mofField->DataLength = 2u;
			*ptr = (char)(short)data;
			mofField->DataPointer = ptr;
			offSet += 2u;
		}
		else if (data is ushort)
		{
			mofField->DataLength = 2u;
			*ptr = (char)(ushort)data;
			mofField->DataPointer = ptr;
			offSet += 2u;
		}
		else if (data is int)
		{
			mofField->DataLength = 4u;
			*(int*)ptr = (int)data;
			mofField->DataPointer = ptr;
			offSet += 4u;
		}
		else if (data is uint)
		{
			mofField->DataLength = 4u;
			*(uint*)ptr = (uint)data;
			mofField->DataPointer = ptr;
			offSet += 4u;
		}
		else if (data is long)
		{
			mofField->DataLength = 8u;
			*(long*)ptr = (long)data;
			mofField->DataPointer = ptr;
			offSet += 8u;
		}
		else if (data is ulong)
		{
			mofField->DataLength = 8u;
			*(ulong*)ptr = (ulong)data;
			mofField->DataPointer = ptr;
			offSet += 8u;
		}
		else if (data is char)
		{
			mofField->DataLength = 2u;
			*ptr = (char)data;
			mofField->DataPointer = ptr;
			offSet += 2u;
		}
		else if (data is float)
		{
			mofField->DataLength = 4u;
			*(float*)ptr = (float)data;
			mofField->DataPointer = ptr;
			offSet += 4u;
		}
		else if (data is double)
		{
			mofField->DataLength = 8u;
			*(double*)ptr = (double)data;
			mofField->DataPointer = ptr;
			offSet += 8u;
		}
		else if (data is bool)
		{
			mofField->DataLength = 1u;
			*(bool*)ptr = (bool)data;
			mofField->DataPointer = ptr;
			offSet++;
		}
		else
		{
			if (!(data is decimal))
			{
				return data.ToString();
			}
			mofField->DataLength = 16u;
			*(decimal*)ptr = (decimal)data;
			mofField->DataPointer = ptr;
			offSet += 16u;
		}
		return null;
	}

	[DllImport("advapi32", CharSet = CharSet.Unicode, ExactSpelling = true)]
	internal static extern int GetTraceEnableFlags(ulong traceHandle);

	[DllImport("advapi32", CharSet = CharSet.Unicode, ExactSpelling = true)]
	internal static extern byte GetTraceEnableLevel(ulong traceHandle);

	[DllImport("advapi32", CharSet = CharSet.Unicode, EntryPoint = "RegisterTraceGuidsW", ExactSpelling = true)]
	internal unsafe static extern uint RegisterTraceGuids([In] EtwProc cbFunc, [In] void* context, [In] ref Guid controlGuid, [In] uint guidCount, ref TraceGuidRegistration guidReg, [In] string mofImagePath, [In] string mofResourceName, out ulong regHandle);

	[DllImport("advapi32", CharSet = CharSet.Unicode, ExactSpelling = true)]
	internal static extern int UnregisterTraceGuids(ulong regHandle);

	[DllImport("advapi32", CharSet = CharSet.Unicode, ExactSpelling = true)]
	internal unsafe static extern uint TraceEvent(ulong traceHandle, char* header);
}
