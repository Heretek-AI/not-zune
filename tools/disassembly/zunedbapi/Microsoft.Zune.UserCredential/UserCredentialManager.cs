using System;
using System.Threading;

namespace Microsoft.Zune.UserCredential;

public class UserCredentialManager
{
	private static UserCredentialManager sm_instance = null;

	private static object sm_lock = new object();

	public static UserCredentialManager Instance
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
						UserCredentialManager userCredentialManager = new UserCredentialManager();
						Thread.MemoryBarrier();
						sm_instance = userCredentialManager;
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

	public unsafe int SetCredentialHandler(UserCredentialHandler credentialHandler)
	{
		if (null == credentialHandler)
		{
			return -2147467261;
		}
		int num = 0;
		CUserCredentialProviderProxy* ptr = (CUserCredentialProviderProxy*)global::_003CModule_003E.@new(12u);
		CUserCredentialProviderProxy* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUserCredential_002ECUserCredentialProviderProxy_002E_007Bctor_007D(ptr));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		CUserCredentialProviderProxy* ptr3 = ptr2;
		IUserCredentialManager* ptr4 = null;
		try
		{
			if (ptr2 == null)
			{
				num = -2147024882;
			}
			else
			{
				num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_41c80590_c50b_4d27_b860_7c87f3f0cb54, (void**)(&ptr4));
				if (num >= 0)
				{
					num = global::_003CModule_003E.Microsoft_002EZune_002EUserCredential_002ECUserCredentialProviderProxy_002EInitialize(ptr2, credentialHandler);
					if (num >= 0)
					{
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IUserCredentialManagerProvider*, int>)(int)(*(uint*)(*(int*)ptr4 + 12)))((nint)ptr4, (IUserCredentialManagerProvider*)ptr2);
					}
				}
			}
		}
		finally
		{
			if (null != ptr4)
			{
				IUserCredentialManager* intPtr = ptr4;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr4 = null;
			}
			if (null != ptr3)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr3 + 8)))((nint)ptr3);
			}
		}
		return num;
	}

	private UserCredentialManager()
	{
	}
}
