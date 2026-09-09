using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Util;

public class UsageDataService
{
	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static bool GetPostUsageDataFlagForSignedInUser()
	{
		bool result = false;
		IUsageDataManager* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_2f36e709_c431_4836_ab2b_ab57aef0cf1a, (void**)(&ptr)) >= 0)
		{
			int num = 0;
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)ptr + 12)))((nint)ptr, &num) == 0 && num != 0)
			{
				result = true;
			}
		}
		if (null != ptr)
		{
			IUsageDataManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return result;
	}

	public unsafe static void SetPostUsageDataFlagForSignedInUser([MarshalAs(UnmanagedType.U1)] bool fCanPostUsageData)
	{
		IUsageDataManager* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_2f36e709_c431_4836_ab2b_ab57aef0cf1a, (void**)(&ptr)) >= 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)ptr + 16)))((nint)ptr, fCanPostUsageData ? 1 : 0);
		}
		if (null != ptr)
		{
			IUsageDataManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
	}

	public unsafe static void ReportTrackSubscriptionPlayback(Guid guidTrackId, string strReferrer)
	{
		IUsageDataManager* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_2f36e709_c431_4836_ab2b_ab57aef0cf1a, (void**)(&ptr)) >= 0)
		{
			_GUID gUID = global::_003CModule_003E.GuidToGUID(guidTrackId);
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strReferrer)))
			{
				try
				{
					int num = *(int*)ptr + 52;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, ushort*, int>)(int)(*(uint*)num))((nint)ptr, &gUID, ptr2);
				}
				catch
				{
					//try-fault
					ptr2 = null;
					throw;
				}
			}
		}
		if (null != ptr)
		{
			IUsageDataManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
	}

	public unsafe static void ReportTrackPreviewPlayback(Guid guidTrackId, string strReferrer)
	{
		IUsageDataManager* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_2f36e709_c431_4836_ab2b_ab57aef0cf1a, (void**)(&ptr)) >= 0)
		{
			_GUID gUID = global::_003CModule_003E.GuidToGUID(guidTrackId);
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strReferrer)))
			{
				try
				{
					int num = *(int*)ptr + 56;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, ushort*, int>)(int)(*(uint*)num))((nint)ptr, &gUID, ptr2);
				}
				catch
				{
					//try-fault
					ptr2 = null;
					throw;
				}
			}
		}
		if (null != ptr)
		{
			IUsageDataManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
	}

	public unsafe static void ReportTrackSubscriptionSkipPlay(Guid guidTrackId, string strReferrer)
	{
		IUsageDataManager* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_2f36e709_c431_4836_ab2b_ab57aef0cf1a, (void**)(&ptr)) >= 0)
		{
			_GUID gUID = global::_003CModule_003E.GuidToGUID(guidTrackId);
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strReferrer)))
			{
				try
				{
					int num = *(int*)ptr + 60;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, ushort*, int>)(int)(*(uint*)num))((nint)ptr, &gUID, ptr2);
				}
				catch
				{
					//try-fault
					ptr2 = null;
					throw;
				}
			}
		}
		if (null != ptr)
		{
			IUsageDataManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
	}

	public unsafe static void ReportTrackPreviewSkipPlay(Guid guidTrackId, string strReferrer)
	{
		IUsageDataManager* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_2f36e709_c431_4836_ab2b_ab57aef0cf1a, (void**)(&ptr)) >= 0)
		{
			_GUID gUID = global::_003CModule_003E.GuidToGUID(guidTrackId);
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strReferrer)))
			{
				try
				{
					int num = *(int*)ptr + 64;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, ushort*, int>)(int)(*(uint*)num))((nint)ptr, &gUID, ptr2);
				}
				catch
				{
					//try-fault
					ptr2 = null;
					throw;
				}
			}
		}
		if (null != ptr)
		{
			IUsageDataManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
	}

	public unsafe static void ReportTrackAddToCollection(Guid guidMediaId, string strReferrer)
	{
		IUsageDataManager* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_2f36e709_c431_4836_ab2b_ab57aef0cf1a, (void**)(&ptr)) >= 0)
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strReferrer)))
			{
				try
				{
					_GUID gUID = global::_003CModule_003E.GuidToGUID(guidMediaId);
					int num = *(int*)ptr + 68;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, ushort*, int>)(int)(*(uint*)num))((nint)ptr, &gUID, ptr2);
				}
				catch
				{
					//try-fault
					ptr2 = null;
					throw;
				}
			}
		}
		if (null != ptr)
		{
			IUsageDataManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
	}

	public unsafe static void ReportPlaylistDownload([MarshalAs(UnmanagedType.U1)] bool purchase, Guid guidMediaId)
	{
		IUsageDataManager* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_2f36e709_c431_4836_ab2b_ab57aef0cf1a, (void**)(&ptr)) >= 0)
		{
			_GUID gUID = global::_003CModule_003E.GuidToGUID(guidMediaId);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, _GUID*, int>)(int)(*(uint*)(*(int*)ptr + 20)))((nint)ptr, purchase ? 1 : 0, &gUID);
		}
		if (null != ptr)
		{
			IUsageDataManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
	}

	private UsageDataService()
	{
	}
}
