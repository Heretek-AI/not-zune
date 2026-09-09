using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Iris;

namespace Microsoft.Zune.Util;

public class FeaturesChangedApi : IDisposable
{
	private static FeaturesChangedApi m_singletonInstance;

	private FeaturesChangedHandler m_featuresChangedHandler;

	private readonly CComPtrMgd_003CIAsyncCallback_003E m_spAsyncCallback;

	public static FeaturesChangedApi Instance
	{
		get
		{
			if (m_singletonInstance == null)
			{
				m_singletonInstance = new FeaturesChangedApi();
				m_singletonInstance.Initialize();
			}
			return m_singletonInstance;
		}
	}

	public static bool HasInstance
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return m_singletonInstance != null;
		}
	}

	[SpecialName]
	public unsafe event FeaturesChangedHandler OnFeaturesChangedEvent
	{
		add
		{
			m_featuresChangedHandler = (FeaturesChangedHandler)Delegate.Combine(m_featuresChangedHandler, value);
			bool featuresHaveChanged = false;
			IFeatureEnablementManager* ptr = null;
			int singleton = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_9581b41a_b5cf_4ebf_9d1a_975477e081ca, (void**)(&ptr));
			if (singleton >= 0)
			{
				singleton = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, bool*, int>)(int)(*(uint*)(*(int*)ptr + 44)))((nint)ptr, &featuresHaveChanged);
				if (singleton >= 0)
				{
					value(featuresHaveChanged);
				}
			}
			if (null != ptr)
			{
				IFeatureEnablementManager* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
		}
		remove
		{
			m_featuresChangedHandler = (FeaturesChangedHandler)Delegate.Remove(m_featuresChangedHandler, value);
		}
	}

	public void FeaturesHaveChanged([MarshalAs(UnmanagedType.U1)] bool fFeaturesHaveChanged)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(FeaturesHaveChangedAsync), (object)fFeaturesHaveChanged);
	}

	public void FeaturesHaveChangedAsync(object args)
	{
		m_featuresChangedHandler((bool)args);
	}

	private FeaturesChangedApi()
	{
		CComPtrMgd_003CIAsyncCallback_003E spAsyncCallback = new CComPtrMgd_003CIAsyncCallback_003E();
		try
		{
			m_spAsyncCallback = spAsyncCallback;
			base._002Ector();
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spAsyncCallback).Dispose();
			throw;
		}
	}

	private unsafe int Initialize()
	{
		int num = 0;
		FeatureChangedInteropWrapper* ptr = (FeatureChangedInteropWrapper*)global::_003CModule_003E.@new(12u);
		FeatureChangedInteropWrapper* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EFeatureChangedInteropWrapper_002E_007Bctor_007D(ptr, FeaturesHaveChanged));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		if (null != ptr2)
		{
			num = -2147024882;
		}
		Unsafe.SkipInit(out CComPtrNtv_003CIAsyncCallback_003E cComPtrNtv_003CIAsyncCallback_003E);
		*(int*)(&cComPtrNtv_003CIAsyncCallback_003E) = 0;
		try
		{
			if (num >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)ptr2)))((nint)ptr2, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._GUID_f5fcfd66_9e9a_436a_8b10_aeb4d6ce2b3d), (void**)(&cComPtrNtv_003CIAsyncCallback_003E));
				if (num >= 0)
				{
					m_spAsyncCallback.op_Assign((IAsyncCallback*)(int)(*(uint*)(&cComPtrNtv_003CIAsyncCallback_003E)));
				}
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIAsyncCallback_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIAsyncCallback_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIAsyncCallback_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIAsyncCallback_003E_002ERelease(&cComPtrNtv_003CIAsyncCallback_003E);
		return num;
	}

	public void _007EFeaturesChangedApi()
	{
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				return;
			}
			finally
			{
				((IDisposable)m_spAsyncCallback).Dispose();
			}
		}
		base.Finalize();
	}

	public virtual sealed void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
