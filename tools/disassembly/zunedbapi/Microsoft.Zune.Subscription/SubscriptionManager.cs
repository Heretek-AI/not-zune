using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Zune.Subscription;

public class SubscriptionManager : IDisposable
{
	private unsafe ISubscriptionManager* m_pSubscriptionManager;

	private SubscriptionEventHandler m_SubscriptionEventHandler;

	private static SubscriptionManager sm_instance = null;

	private static object sm_lock = new object();

	public static SubscriptionManager Instance
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
						SubscriptionManager subscriptionManager = new SubscriptionManager();
						Thread.MemoryBarrier();
						sm_instance = subscriptionManager;
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

	[SpecialName]
	public event SubscriptionEventHandler OnForegroundSubscriptionChanged
	{
		add
		{
			m_SubscriptionEventHandler = (SubscriptionEventHandler)Delegate.Combine(m_SubscriptionEventHandler, value);
		}
		remove
		{
			m_SubscriptionEventHandler = (SubscriptionEventHandler)Delegate.Remove(m_SubscriptionEventHandler, value);
		}
	}

	private void _007ESubscriptionManager()
	{
		_0021SubscriptionManager();
	}

	private unsafe void _0021SubscriptionManager()
	{
		ISubscriptionManager* pSubscriptionManager = m_pSubscriptionManager;
		if (pSubscriptionManager != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pSubscriptionManager + 8)))((nint)pSubscriptionManager);
			m_pSubscriptionManager = null;
		}
	}

	public unsafe int Subscribe(int subscriptionMediaId, EMediaTypes eSubscriptionMediaType)
	{
		int num = 0;
		if (m_pSubscriptionManager == null)
		{
			num = -2147467261;
		}
		CSubscriptionEventProxy* ptr = null;
		if (num >= 0)
		{
			SubscribeToEvent(subscriptionMediaId, eSubscriptionMediaType, null, SubscriptionAction.RefreshFinished, userInitiated: true, &ptr);
			int num2 = *(int*)m_pSubscriptionManager + 12;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, EMediaTypes, int>)(int)(*(uint*)num2))((nint)m_pSubscriptionManager, GetLastSignedInUserId(), subscriptionMediaId, eSubscriptionMediaType);
			if (num < 0)
			{
				if (ptr == null)
				{
					goto IL_0063;
				}
				global::_003CModule_003E.Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002EUninitialize(ptr);
			}
			if (null != ptr)
			{
				CSubscriptionEventProxy* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
		}
		goto IL_0063;
		IL_0063:
		return num;
	}

	public unsafe int Subscribe(string feedUrl, string subscriptionTitle, Guid serviceId, [MarshalAs(UnmanagedType.U1)] bool isPersonalChannel, EMediaTypes eSubscriptionMediaType, ESubscriptionSource subscriptionSource, out int subscriptionMediaId)
	{
		int num = 0;
		if (m_pSubscriptionManager == null || feedUrl == null)
		{
			num = -2147467261;
		}
		int num2 = -1;
		int lastSignedInUserId = GetLastSignedInUserId();
		Unsafe.SkipInit(out _GUID gUID);
		*(int*)(&gUID) = 0;
		// IL initblk instruction
		Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref gUID, 4), 0, 12);
		Unsafe.SkipInit(out tagPROPVARIANT tagPROPVARIANT2);
		*(short*)(&tagPROPVARIANT2) = 1;
		if (serviceId != Guid.Empty)
		{
			gUID = global::_003CModule_003E.GuidToGUID(serviceId);
			*(short*)(&tagPROPVARIANT2) = 72;
			Unsafe.As<tagPROPVARIANT, int>(ref Unsafe.AddByteOffset(ref tagPROPVARIANT2, 8)) = (int)(&gUID);
		}
		if (num >= 0 && EMediaTypes.eMediaTypePlaylist != eSubscriptionMediaType)
		{
			num = ValidateUrl(ref feedUrl);
		}
		IMSMediaSchemaPropertySet* ptr = null;
		if (num >= 0)
		{
			if (eSubscriptionMediaType != EMediaTypes.eMediaTypePlaylist)
			{
				if (eSubscriptionMediaType == EMediaTypes.eMediaTypePodcastSeries && 72 == *(ushort*)(&tagPROPVARIANT2))
				{
					num = global::_003CModule_003E.ZuneLibraryExports_002ECreatePropertySet((_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E.ID_MS_MEDIA_SCHEMA_SERIES), 3229616385u, &ptr);
					if (num < 0)
					{
						goto IL_0270;
					}
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, tagPROPVARIANT, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, 67133455u, tagPROPVARIANT2);
				}
			}
			else
			{
				Unsafe.SkipInit(out CComPropVariant cComPropVariant);
				global::_003CModule_003E.CComPropVariant_002E_007Bctor_007D(&cComPropVariant);
				try
				{
					Unsafe.SkipInit(out CComPropVariant cComPropVariant2);
					global::_003CModule_003E.CComPropVariant_002E_007Bctor_007D(&cComPropVariant2);
					try
					{
						Unsafe.SkipInit(out CComPropVariant cComPropVariant3);
						global::_003CModule_003E.CComPropVariant_002E_007Bctor_007D(&cComPropVariant3);
						try
						{
							EPlaylistType nSrc = (isPersonalChannel ? ((EPlaylistType)6) : ((EPlaylistType)5));
							global::_003CModule_003E.CComPropVariant_002E_003D(&cComPropVariant, (int)nSrc);
							global::_003CModule_003E.CComPropVariant_002E_003D(&cComPropVariant3, lastSignedInUserId);
							num = global::_003CModule_003E.ZuneLibraryExports_002ECreatePropertySet((_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E.ID_MS_MEDIA_SCHEMA_PLAYLIST), 3229617665u, &ptr);
							if (num >= 0)
							{
								int num3;
								fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(subscriptionTitle)))
								{
									try
									{
										Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant2, 8)) = (int)global::_003CModule_003E.SysAllocString(ptr2);
										*(short*)(&cComPropVariant2) = 8;
										num3 = ((Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant2, 8)) == 0) ? (-2147024882) : num);
										num = num3;
									}
									catch
									{
										//try-fault
										ptr2 = null;
										throw;
									}
								}
								if (num3 >= 0)
								{
									tagPROPVARIANT tagPROPVARIANT3 = (tagPROPVARIANT)cComPropVariant2;
									num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, tagPROPVARIANT, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, 16777217u, tagPROPVARIANT3);
									if (num >= 0)
									{
										tagPROPVARIANT tagPROPVARIANT4 = (tagPROPVARIANT)cComPropVariant;
										num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, tagPROPVARIANT, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, 100663307u, tagPROPVARIANT4);
										if (num >= 0)
										{
											tagPROPVARIANT tagPROPVARIANT5 = (tagPROPVARIANT)cComPropVariant3;
											num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, tagPROPVARIANT, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, 100663309u, tagPROPVARIANT5);
											if (num >= 0 && 72 == *(ushort*)(&tagPROPVARIANT2))
											{
												num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, tagPROPVARIANT, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, 67108873u, tagPROPVARIANT2);
											}
										}
									}
								}
							}
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant3);
							throw;
						}
						global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D(&cComPropVariant3);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant2);
						throw;
					}
					global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D(&cComPropVariant2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
					throw;
				}
				global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D(&cComPropVariant);
			}
			if (num >= 0)
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(feedUrl)))
				{
					try
					{
						int num4 = *(int*)m_pSubscriptionManager + 16;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, ushort*, EMediaTypes, ESubscriptionSource, IMSMediaSchemaPropertySet*, int*, int>)(int)(*(uint*)num4))((nint)m_pSubscriptionManager, lastSignedInUserId, ptr3, eSubscriptionMediaType, subscriptionSource, ptr, &num2);
					}
					catch
					{
						//try-fault
						ptr3 = null;
						throw;
					}
				}
				if (num >= 0)
				{
					subscriptionMediaId = num2;
					if (null != m_SubscriptionEventHandler)
					{
						SubscriptonEventArguments args = new SubscriptonEventArguments(SubscriptionAction.Subscribed, subscriptionTitle, eSubscriptionMediaType, userInitiated: true);
						m_SubscriptionEventHandler(args);
					}
				}
			}
			goto IL_0270;
		}
		goto IL_027f;
		IL_027f:
		return num;
		IL_0270:
		if (ptr != null)
		{
			IMSMediaSchemaPropertySet* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		goto IL_027f;
	}

	public unsafe int Unsubscribe(int subscriptionMediaId, EMediaTypes eSubscriptionMediaType, [MarshalAs(UnmanagedType.U1)] bool deleteContent)
	{
		ISubscriptionManager* pSubscriptionManager = m_pSubscriptionManager;
		int num;
		if (pSubscriptionManager == null)
		{
			num = -2147467261;
		}
		else
		{
			int num2 = *(int*)pSubscriptionManager + 20;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, EMediaTypes, int, int>)(int)(*(uint*)num2))((nint)m_pSubscriptionManager, GetLastSignedInUserId(), subscriptionMediaId, eSubscriptionMediaType, deleteContent ? 1 : 0);
			if (num >= 0 && null != m_SubscriptionEventHandler)
			{
				SubscriptonEventArguments args = new SubscriptonEventArguments(SubscriptionAction.Unsubscribed, null, eSubscriptionMediaType, userInitiated: true);
				m_SubscriptionEventHandler(args);
			}
		}
		return num;
	}

	public unsafe int Refresh(int subscriptionMediaId, EMediaTypes eSubscriptionMediaType, [MarshalAs(UnmanagedType.U1)] bool refreshCache)
	{
		CSubscriptionEventProxy* ptr = null;
		CSubscriptionEventProxy* ptr2 = null;
		int num;
		if (m_pSubscriptionManager == null)
		{
			num = -2147467261;
		}
		else
		{
			SubscribeToEvent(subscriptionMediaId, eSubscriptionMediaType, null, SubscriptionAction.RefreshStarted, refreshCache, &ptr);
			SubscribeToEvent(subscriptionMediaId, eSubscriptionMediaType, null, SubscriptionAction.RefreshFinished, refreshCache, &ptr2);
			ISubscriptionManager* pSubscriptionManager = m_pSubscriptionManager;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, EMediaTypes, int, int>)(int)(*(uint*)(*(int*)pSubscriptionManager + 24)))((nint)pSubscriptionManager, subscriptionMediaId, eSubscriptionMediaType, refreshCache ? 1 : 0);
			if (num < 0)
			{
				if (ptr != null)
				{
					global::_003CModule_003E.Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002EUninitialize(ptr);
				}
				if (ptr2 != null)
				{
					global::_003CModule_003E.Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002EUninitialize(ptr2);
				}
			}
			if (null != ptr)
			{
				CSubscriptionEventProxy* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
			if (null != ptr2)
			{
				CSubscriptionEventProxy* intPtr2 = ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
			}
		}
		return num;
	}

	public unsafe int SetSeriesUrl(int subscriptionMediaId, string feedUrl)
	{
		int num;
		if (m_pSubscriptionManager == null || feedUrl == null)
		{
			num = -2147467261;
		}
		else
		{
			num = ValidateUrl(ref feedUrl);
			if (num >= 0)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(feedUrl)))
				{
					try
					{
						int num2 = *(int*)m_pSubscriptionManager + 36;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, EMediaTypes, ushort*, int>)(int)(*(uint*)num2))((nint)m_pSubscriptionManager, GetLastSignedInUserId(), subscriptionMediaId, EMediaTypes.eMediaTypePodcastSeries, ptr);
					}
					catch
					{
						//try-fault
						ptr = null;
						throw;
					}
				}
			}
		}
		return num;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool FindByUrl(string feedUrl, EMediaTypes eSubscriptionMediaType, out int subscriptionMediaId, out bool isSubscribed)
	{
		bool result = false;
		int num = 0;
		int num2 = -1;
		int num3;
		if (!(feedUrl == null) && m_pSubscriptionManager != null)
		{
			subscriptionMediaId = -1;
			if (EMediaTypes.eMediaTypePlaylist != eSubscriptionMediaType)
			{
				num3 = ValidateUrl(ref feedUrl);
				if (num3 < 0)
				{
					goto IL_0063;
				}
			}
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(feedUrl)))
			{
				try
				{
					int num4 = *(int*)m_pSubscriptionManager + 40;
					num3 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, ushort*, EMediaTypes, int*, int*, int>)(int)(*(uint*)num4))((nint)m_pSubscriptionManager, GetLastSignedInUserId(), ptr, eSubscriptionMediaType, &num2, &num);
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
			goto IL_0063;
		}
		goto IL_007b;
		IL_007b:
		return result;
		IL_0063:
		if (0 == num3)
		{
			subscriptionMediaId = num2;
			int num5 = ((num != 0) ? 1 : 0);
			isSubscribed = (byte)num5 != 0;
			result = true;
		}
		goto IL_007b;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool FindByServiceId(Guid serviceId, EMediaTypes eSubscriptionMediaType, out int subscriptionMediaId, out bool isSubscribed)
	{
		subscriptionMediaId = -1;
		isSubscribed = false;
		bool result = false;
		int num = 0;
		int num2 = -1;
		_GUID gUID = global::_003CModule_003E.GuidToGUID(serviceId);
		ISubscriptionManager* pSubscriptionManager = m_pSubscriptionManager;
		if (0 == ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, _GUID*, EMediaTypes, int*, int*, int>)(int)(*(uint*)(*(int*)pSubscriptionManager + 44)))((nint)pSubscriptionManager, GetLastSignedInUserId(), &gUID, eSubscriptionMediaType, &num2, &num))
		{
			subscriptionMediaId = num2;
			int num3 = ((num != 0) ? 1 : 0);
			isSubscribed = (byte)num3 != 0;
			result = true;
		}
		return result;
	}

	public unsafe int SetCredentialHandler(EMediaTypes eSubscriptionMediaType, SubscriptionCredentialHandler credentialHandler)
	{
		if (null == credentialHandler)
		{
			return -2147467261;
		}
		int num = 0;
		CSubscriptionCredentialProviderProxy* ptr = (CSubscriptionCredentialProviderProxy*)global::_003CModule_003E.@new(12u);
		CSubscriptionCredentialProviderProxy* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002ESubscription_002ECSubscriptionCredentialProviderProxy_002E_007Bctor_007D(ptr));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		CSubscriptionCredentialProviderProxy* ptr3 = ptr2;
		try
		{
			if (ptr2 == null)
			{
				num = -2147024882;
			}
			else
			{
				num = global::_003CModule_003E.Microsoft_002EZune_002ESubscription_002ECSubscriptionCredentialProviderProxy_002EInitialize(ptr2, credentialHandler);
				if (num >= 0)
				{
					ISubscriptionManager* pSubscriptionManager = m_pSubscriptionManager;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaTypes, ISubscriptionManagerCredentialProvider*, int>)(int)(*(uint*)(*(int*)pSubscriptionManager + 48)))((nint)pSubscriptionManager, eSubscriptionMediaType, (ISubscriptionManagerCredentialProvider*)ptr2);
				}
			}
		}
		finally
		{
			if (ptr3 != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr3 + 8)))((nint)ptr3);
			}
		}
		return num;
	}

	public unsafe int SetManagementSettings(int subscriptionMediaId, uint keepEpisodes, ESeriesPlaybackOrder playbackOrder)
	{
		ISubscriptionManager* pSubscriptionManager = m_pSubscriptionManager;
		int result;
		if (pSubscriptionManager == null)
		{
			result = -2147467261;
		}
		else
		{
			int num = *(int*)pSubscriptionManager + 52;
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, EMediaTypes, uint, ESeriesPlaybackOrder, int>)(int)(*(uint*)num))((nint)m_pSubscriptionManager, GetLastSignedInUserId(), subscriptionMediaId, EMediaTypes.eMediaTypePodcastSeries, keepEpisodes, playbackOrder);
		}
		return result;
	}

	public unsafe int GetManagementSettings(int subscriptionMediaId, out uint keepEpisodes, out ESeriesPlaybackOrder playbackOrder)
	{
		int num = 0;
		ISubscriptionManager* pSubscriptionManager = m_pSubscriptionManager;
		if (pSubscriptionManager == null)
		{
			num = -2147467261;
		}
		uint num2 = 0u;
		ESeriesPlaybackOrder eSeriesPlaybackOrder = ESeriesPlaybackOrder.eSeriesPlaybackOrderNewestFirst;
		if (num >= 0)
		{
			int num3 = *(int*)pSubscriptionManager + 56;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, EMediaTypes, uint*, ESeriesPlaybackOrder*, int>)(int)(*(uint*)num3))((nint)m_pSubscriptionManager, GetLastSignedInUserId(), subscriptionMediaId, EMediaTypes.eMediaTypePodcastSeries, &num2, &eSeriesPlaybackOrder);
			if (num >= 0)
			{
				keepEpisodes = num2;
				playbackOrder = eSeriesPlaybackOrder;
			}
		}
		return num;
	}

	public unsafe int DownloadEpisode(int subscriptionMediaId, int subscriptionItemMediaId)
	{
		ISubscriptionManager* pSubscriptionManager = m_pSubscriptionManager;
		int result;
		if (pSubscriptionManager == null)
		{
			result = -2147467261;
		}
		else
		{
			int num = *(int*)pSubscriptionManager + 60;
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, int, EMediaTypes, int>)(int)(*(uint*)num))((nint)m_pSubscriptionManager, GetLastSignedInUserId(), subscriptionMediaId, subscriptionItemMediaId, EMediaTypes.eMediaTypePodcastSeries);
		}
		return result;
	}

	public unsafe int DeleteEpisode(int subscriptionMediaId, int subscriptionItemMediaId)
	{
		ISubscriptionManager* pSubscriptionManager = m_pSubscriptionManager;
		int result;
		if (pSubscriptionManager == null)
		{
			result = -2147467261;
		}
		else
		{
			int num = *(int*)pSubscriptionManager + 64;
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, int, EMediaTypes, int>)(int)(*(uint*)num))((nint)m_pSubscriptionManager, GetLastSignedInUserId(), subscriptionMediaId, subscriptionItemMediaId, EMediaTypes.eMediaTypePodcastSeries);
		}
		return result;
	}

	public unsafe int SaveEpisodeToCollection(int subscriptionMediaId, int subscriptionItemMediaId)
	{
		ISubscriptionManager* pSubscriptionManager = m_pSubscriptionManager;
		int result;
		if (pSubscriptionManager == null)
		{
			result = -2147467261;
		}
		else
		{
			int num = *(int*)pSubscriptionManager + 72;
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, int, EMediaTypes, int>)(int)(*(uint*)num))((nint)m_pSubscriptionManager, GetLastSignedInUserId(), subscriptionMediaId, subscriptionItemMediaId, EMediaTypes.eMediaTypePodcastSeries);
		}
		return result;
	}

	public unsafe int SetEpisodeSeriesUrl(IList subscriptionItemMediaIdList, string feedUrl)
	{
		int num = ((m_pSubscriptionManager != null && !(feedUrl == null)) ? ValidateUrl(ref feedUrl) : (-2147467261));
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(feedUrl)))
		{
			int* ptr2 = null;
			int count = subscriptionItemMediaIdList.Count;
			if (num >= 0)
			{
				try
				{
					int* ptr3 = (int*)global::_003CModule_003E.new_005B_005D(((uint)count > 1073741823u) ? uint.MaxValue : ((uint)(count << 2)));
					ptr2 = ptr3;
					if (ptr3 == null)
					{
						num = -2147024882;
					}
					if (num >= 0)
					{
						for (int i = 0; i < count; i++)
						{
							*(int*)(i * 4 + (byte*)ptr2) = (int)subscriptionItemMediaIdList[i];
						}
						int num2 = *(int*)m_pSubscriptionManager + 76;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, EMediaTypes, int, int*, ushort*, int>)(int)(*(uint*)num2))((nint)m_pSubscriptionManager, GetLastSignedInUserId(), EMediaTypes.eMediaTypePodcastSeries, count, ptr2, ptr);
					}
				}
				finally
				{
					if (ptr2 != null)
					{
						global::_003CModule_003E.delete_005B_005D(ptr2);
					}
				}
			}
			return num;
		}
	}

	protected unsafe int SubscribeToEvent(int subscriptionMediaId, EMediaTypes eSubscriptionMediaType, string subscriptionTitle, SubscriptionAction targetAction, [MarshalAs(UnmanagedType.U1)] bool userInitiated, CSubscriptionEventProxy** ppSubscriptionEventProxy)
	{
		if (ppSubscriptionEventProxy == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 1475u);
			return -2147467261;
		}
		CSubscriptionEventProxy* ptr2;
		int num;
		if (null != m_SubscriptionEventHandler && (EMediaTypes.eMediaTypePodcastSeries == eSubscriptionMediaType || EMediaTypes.eMediaTypePlaylist == eSubscriptionMediaType || EMediaTypes.eMediaTypeUser == eSubscriptionMediaType))
		{
			CSubscriptionEventProxy* ptr = (CSubscriptionEventProxy*)global::_003CModule_003E.@new(48u);
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002E_007Bctor_007D(ptr));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 == null)
			{
				num = -2147024882;
			}
			else
			{
				if (null == subscriptionTitle)
				{
					num = GetSubscriptionTitle(subscriptionMediaId, eSubscriptionMediaType, out subscriptionTitle);
					if (num < 0)
					{
						goto IL_0099;
					}
				}
				num = global::_003CModule_003E.Microsoft_002EZune_002ESubscription_002ECSubscriptionEventProxy_002EInitialize(ptr2, m_SubscriptionEventHandler, subscriptionMediaId, eSubscriptionMediaType, subscriptionTitle, targetAction, userInitiated);
				if (num < 0)
				{
					goto IL_0099;
				}
				*(int*)ppSubscriptionEventProxy = (int)ptr2;
			}
		}
		else
		{
			num = 1;
		}
		goto IL_00a9;
		IL_00a9:
		return num;
		IL_0099:
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		goto IL_00a9;
	}

	protected unsafe int GetSubscriptionTitle(int subscriptionMediaId, EMediaTypes subscriptionMediaType, out string subscriptionTitle)
	{
		subscriptionTitle = null;
		Unsafe.SkipInit(out DBPropertyRequestStruct dBPropertyRequestStruct);
		global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bctor_007D(&dBPropertyRequestStruct, 344u);
		int num;
		try
		{
			switch (subscriptionMediaType)
			{
			case EMediaTypes.eMediaTypePlaylist:
				break;
			default:
				num = 1;
				goto IL_00b5;
			case EMediaTypes.eMediaTypeUser:
				subscriptionTitle = string.Empty;
				goto IL_0041;
			case EMediaTypes.eMediaTypePodcastSeries:
				goto IL_004b;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D), &dBPropertyRequestStruct);
			throw;
		}
		try
		{
			num = global::_003CModule_003E.ZuneLibraryExports_002EGetFieldValues(subscriptionMediaId, EListType.ePlaylistList, 1, &dBPropertyRequestStruct, null);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D), &dBPropertyRequestStruct);
			throw;
		}
		goto IL_0088;
		IL_0041:
		global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D(&dBPropertyRequestStruct);
		return 0;
		IL_004b:
		try
		{
			num = global::_003CModule_003E.ZuneLibraryExports_002EGetFieldValues(subscriptionMediaId, EListType.ePodcastList, 1, &dBPropertyRequestStruct, null);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D), &dBPropertyRequestStruct);
			throw;
		}
		goto IL_0088;
		IL_00b5:
		global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D(&dBPropertyRequestStruct);
		return num;
		IL_0088:
		try
		{
			if (0 == num && Unsafe.As<DBPropertyRequestStruct, int>(ref Unsafe.AddByteOffset(ref dBPropertyRequestStruct, 4)) >= 0)
			{
				subscriptionTitle = new string((char*)(int)Unsafe.As<DBPropertyRequestStruct, uint>(ref Unsafe.AddByteOffset(ref dBPropertyRequestStruct, 16)));
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D), &dBPropertyRequestStruct);
			throw;
		}
		goto IL_00b5;
	}

	private unsafe SubscriptionManager()
	{
		ISubscriptionManager* pSubscriptionManager = null;
		int singleton = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_9dc7c984_41d5_4130_a5ac_46d0825cd29d, (void**)(&pSubscriptionManager));
		if (singleton >= 0)
		{
			m_pSubscriptionManager = pSubscriptionManager;
			return;
		}
		throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(singleton));
	}

	private int ValidateUrl([In][Out] ref string feedUrl)
	{
		if (feedUrl == null)
		{
			return -1072884976;
		}
		int num = global::_003CModule_003E.Microsoft_002EZune_002ESubscription_002EIsWellFormedUriString(ref feedUrl);
		if (num == -1072884976)
		{
			feedUrl = "http://" + feedUrl;
			num = global::_003CModule_003E.Microsoft_002EZune_002ESubscription_002EIsWellFormedUriString(ref feedUrl);
		}
		return num;
	}

	private unsafe int GetLastSignedInUserId()
	{
		int result = 1;
		IService* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&ptr)) >= 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, int*, int>)(int)(*(uint*)(*(int*)ptr + 192)))((nint)ptr, null, &result);
		}
		if (null != ptr)
		{
			IService* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return result;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021SubscriptionManager();
			return;
		}
		try
		{
			_0021SubscriptionManager();
		}
		finally
		{
			base.Finalize();
		}
	}

	public virtual sealed void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	~SubscriptionManager()
	{
		Dispose(false);
	}
}
