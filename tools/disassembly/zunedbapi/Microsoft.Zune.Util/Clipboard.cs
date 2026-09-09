using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003CCppImplementationDetails_003E;
using MicrosoftZuneLibrary;

namespace Microsoft.Zune.Util;

public class Clipboard
{
	private Clipboard()
	{
	}

	private unsafe static object CopyData(ClipboardDataType type, void* handle)
	{
		int num = 0;
		object result = null;
		if (handle != null)
		{
			if (type == ClipboardDataType.Image)
			{
				HBITMAP__* ptr = (HBITMAP__*)global::_003CModule_003E.CopyImage(handle, 0u, 0, 0, 0u);
				if (ptr != null)
				{
					result = new SafeBitmap(ptr);
					goto IL_0132;
				}
				uint lastError = global::_003CModule_003E.GetLastError();
				num = (((int)lastError > 0) ? ((int)(lastError & 0xFFFF) | -2147024896) : ((int)lastError));
			}
			else if (type == ClipboardDataType.FileDropList)
			{
				StringCollection stringCollection = null;
				uint num2 = 0u;
				HDROP__* ptr2 = (HDROP__*)global::_003CModule_003E.GlobalLock(handle);
				if (ptr2 != null)
				{
					num2 = global::_003CModule_003E.DragQueryFileW(ptr2, uint.MaxValue, null, 0u);
					stringCollection = new StringCollection();
				}
				else
				{
					uint lastError2 = global::_003CModule_003E.GetLastError();
					num = (((int)lastError2 > 0) ? ((int)(lastError2 & 0xFFFF) | -2147024896) : ((int)lastError2));
				}
				uint num3 = 0u;
				if (0 < num2)
				{
					Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY0BAE_0040G _0024ArrayType_0024_0024_0024BY0BAE_0040G2);
					do
					{
						if (global::_003CModule_003E.DragQueryFileW(ptr2, num3, (ushort*)(&_0024ArrayType_0024_0024_0024BY0BAE_0040G2), 260u) != 0)
						{
							stringCollection.Add(new string((char*)(&_0024ArrayType_0024_0024_0024BY0BAE_0040G2)));
							num3++;
							continue;
						}
						uint lastError3 = global::_003CModule_003E.GetLastError();
						num = (((int)lastError3 > 0) ? ((int)(lastError3 & 0xFFFF) | -2147024896) : ((int)lastError3));
						stringCollection = null;
						break;
					}
					while (num3 < num2);
				}
				result = stringCollection;
			}
			else
			{
				num = 87;
			}
			if (num < 0 && global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 10, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xa7a1a344_002EWPP_Clipboard_cpp_Traceguids), (uint)num);
			}
		}
		goto IL_0132;
		IL_0132:
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static bool ContainsData(ClipboardDataType type)
	{
		bool result = false;
		int num;
		if (global::_003CModule_003E.OpenClipboard(null) == 0)
		{
			uint lastError = global::_003CModule_003E.GetLastError();
			num = (((int)lastError > 0) ? ((int)(lastError & 0xFFFF) | -2147024896) : ((int)lastError));
		}
		else
		{
			if (global::_003CModule_003E.IsClipboardFormatAvailable((uint)type) != 0)
			{
				result = true;
			}
			if (global::_003CModule_003E.CloseClipboard() != 0)
			{
				goto IL_0092;
			}
			uint lastError2 = global::_003CModule_003E.GetLastError();
			num = (((int)lastError2 > 0) ? ((int)(lastError2 & 0xFFFF) | -2147024896) : ((int)lastError2));
		}
		if (num < 0 && global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 11, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xa7a1a344_002EWPP_Clipboard_cpp_Traceguids), (uint)num);
		}
		goto IL_0092;
		IL_0092:
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public static bool ContainsImage()
	{
		return ContainsData(ClipboardDataType.Image);
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public static bool ContainsFileDropList()
	{
		return ContainsData(ClipboardDataType.FileDropList);
	}

	public unsafe static object GetData(ClipboardDataType type)
	{
		object result = null;
		int num = 0;
		if (global::_003CModule_003E.OpenClipboard(null) == 0)
		{
			uint lastError = global::_003CModule_003E.GetLastError();
			num = (((int)lastError > 0) ? ((int)(lastError & 0xFFFF) | -2147024896) : ((int)lastError));
		}
		else
		{
			void* ptr = ((global::_003CModule_003E.IsClipboardFormatAvailable((uint)type) == 0) ? null : global::_003CModule_003E.GetClipboardData((uint)type));
			if (ptr != null)
			{
				result = CopyData(type, ptr);
			}
			else
			{
				uint lastError2 = global::_003CModule_003E.GetLastError();
				num = (((int)lastError2 > 0) ? ((int)(lastError2 & 0xFFFF) | -2147024896) : ((int)lastError2));
			}
			if (global::_003CModule_003E.CloseClipboard() == 0)
			{
				uint lastError3 = global::_003CModule_003E.GetLastError();
				num = (((int)lastError3 > 0) ? ((int)(lastError3 & 0xFFFF) | -2147024896) : ((int)lastError3));
			}
		}
		if (num < 0 && global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 12, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xa7a1a344_002EWPP_Clipboard_cpp_Traceguids), (uint)num);
		}
		return result;
	}

	public static SafeBitmap GetImage()
	{
		return GetData(ClipboardDataType.Image) as SafeBitmap;
	}

	public static StringCollection GetFileDropList()
	{
		return GetData(ClipboardDataType.FileDropList) as StringCollection;
	}
}
