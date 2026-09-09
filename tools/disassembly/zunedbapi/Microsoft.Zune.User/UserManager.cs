using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Zune.User;

public class UserManager
{
	private static UserManager sm_instance = null;

	private static object sm_lock = new object();

	public static UserManager Instance
	{
		get
		{
			if (sm_instance == null)
			{
				try
				{
					Monitor.Enter(sm_lock);
					if (sm_instance == null)
					{
						UserManager userManager = new UserManager();
						Thread.MemoryBarrier();
						sm_instance = userManager;
					}
				}
				finally
				{
					Monitor.Exit(sm_lock);
				}
			}
			return sm_instance;
		}
	}

	public unsafe int GetUserIdList(IList userIdList)
	{
		IUserManager* ptr = null;
		Unsafe.SkipInit(out DynamicArray_003Cint_003E dynamicArray_003Cint_003E);
		global::_003CModule_003E.DynamicArray_003Cint_003E_002E_007Bctor_007D(&dynamicArray_003Cint_003E);
		int num;
		try
		{
			num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_c9e0f18a_6c53_47d0_991e_dbd4fe395101, (void**)(&ptr));
			if (num >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, DynamicArray_003Cint_003E*, int>)(int)(*(uint*)(*(int*)ptr + 12)))((nint)ptr, &dynamicArray_003Cint_003E);
				if (num >= 0)
				{
					int num2 = 0;
					if (0 < Unsafe.As<DynamicArray_003Cint_003E, int>(ref Unsafe.AddByteOffset(ref dynamicArray_003Cint_003E, 8)))
					{
						do
						{
							int* ptr2 = global::_003CModule_003E.DynamicArray_003Cint_003E_002E_005B_005D(&dynamicArray_003Cint_003E, num2);
							userIdList.Add(*ptr2);
							num2++;
						}
						while (num2 < Unsafe.As<DynamicArray_003Cint_003E, int>(ref Unsafe.AddByteOffset(ref dynamicArray_003Cint_003E, 8)));
					}
				}
			}
			if (null != ptr)
			{
				IUserManager* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DynamicArray_003Cint_003E*, void>)(&global::_003CModule_003E.DynamicArray_003Cint_003E_002E_007Bdtor_007D), &dynamicArray_003Cint_003E);
			throw;
		}
		global::_003CModule_003E.DynamicArray_003Cint_003E_002E_007Bdtor_007D(&dynamicArray_003Cint_003E);
		return num;
	}

	public unsafe int FindUserByPassportId(string passportId, out int userId)
	{
		userId = -1;
		IUserManager* ptr = null;
		int num = -1;
		fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(passportId)))
		{
			int num2 = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_c9e0f18a_6c53_47d0_991e_dbd4fe395101, (void**)(&ptr));
			if (num2 >= 0)
			{
				int num3 = *(int*)ptr + 20;
				num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int*, int>)(int)(*(uint*)num3))((nint)ptr, ptr2, &num);
			}
			if (0 == num2)
			{
				userId = num;
			}
			if (null != ptr)
			{
				IUserManager* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
			return num2;
		}
	}

	public unsafe int RefreshUserTile(int userId)
	{
		IUserManager* ptr = null;
		int num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_c9e0f18a_6c53_47d0_991e_dbd4fe395101, (void**)(&ptr));
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)ptr + 24)))((nint)ptr, userId);
		}
		if (null != ptr)
		{
			IUserManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return num;
	}

	public unsafe int CleanupUserData(int userId)
	{
		IUserManager* ptr = null;
		int num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_c9e0f18a_6c53_47d0_991e_dbd4fe395101, (void**)(&ptr));
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, userId);
		}
		if (null != ptr)
		{
			IUserManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return num;
	}
}
