using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using MicrosoftZuneInterop;
using ZuneUI;

namespace MicrosoftZuneLibrary;

public unsafe class ZuneLibraryCDDevice(IWMPCDDevice* pDevice) : IDisposable
{
	private unsafe IWMPCDDevice* m_pDevice = pDevice;

	private unsafe IBurnPublisher* m_pBurnPublisher = null;

	private int m_hrBurnPublisherCreate;

	private int m_fIsBurner = 0;

	private bool _disposed;

	private uint m_dwBurnAdviseCookie;

	private OnSessionProgressHandler m_SessionProgressHandler;

	private OnItemProgressHandler m_ItemProgressHandler;

	private OnItemErrorHandler m_ItemErrorHandler;

	private OnBurnStateChangeHandler m_BurnStateChangeHandler;

	private OnSetDriveLockedForBurningHandler m_SetDriveLockedForBurningHandler;

	private OnQueryCancelHandler m_QueryCancelHandler;

	private unsafe IBurnPublisher* BurnPublisher
	{
		get
		{
			if (m_pBurnPublisher == null && m_pDevice != null)
			{
				fixed (int* ptr = &m_fIsBurner)
				{
					try
					{
						IBurnPublisher* pBurnPublisher = null;
						int num = *(int*)m_pDevice + 96;
						if ((m_hrBurnPublisherCreate = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, IBurnPublisher**, int>)(int)(*(uint*)num))((nint)m_pDevice, ptr, &pBurnPublisher)) >= 0)
						{
							m_pBurnPublisher = pBurnPublisher;
						}
					}
					catch
					{
						//try-fault
						ptr = null;
						throw;
					}
				}
			}
			return m_pBurnPublisher;
		}
	}

	public unsafe static bool IsImapiv2Installed
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool flag = true;
			if (!global::_003CModule_003E.IsLonghornOrBetter())
			{
				Unsafe.SkipInit(out IUnknown* ptr);
				flag = global::_003CModule_003E.CoCreateInstance((_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E.CLSID_MsftDiscMaster2), null, 23u, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._GUID_27354130_7f64_5b0f_8f00_5d77afbe261e), (void**)(&ptr)) >= 0;
				if (flag)
				{
					IUnknown* intPtr = ptr;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				}
			}
			return flag;
		}
	}

	public unsafe EBurnState CurrentBurnState
	{
		get
		{
			EBurnState result = EBurnState.ebsUnknown;
			IBurnPublisher* pBurnPublisher = m_pBurnPublisher;
			Unsafe.SkipInit(out __MIDL___MIDL_itf_wmpcd_0000_0014_0001 _MIDL___MIDL_itf_wmpcd_0000_0014_);
			if (pBurnPublisher != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, __MIDL___MIDL_itf_wmpcd_0000_0014_0001*, int>)(int)(*(uint*)(*(int*)pBurnPublisher + 60)))((nint)pBurnPublisher, &_MIDL___MIDL_itf_wmpcd_0000_0014_) >= 0)
			{
				result = (EBurnState)_MIDL___MIDL_itf_wmpcd_0000_0014_;
			}
			return result;
		}
	}

	public unsafe bool IsBurner
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IWMPCDDevice* pDevice = m_pDevice;
			if (pDevice != null)
			{
				if (m_pBurnPublisher == null)
				{
					Unsafe.SkipInit(out int num);
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pDevice + 100)))((nint)pDevice, &num) >= 0 && num != 0)
					{
						result = true;
					}
				}
				else if (m_fIsBurner != 0)
				{
					result = true;
				}
			}
			return result;
		}
	}

	public unsafe long SpaceAvailable
	{
		get
		{
			long result = 0L;
			IBurnPublisher* burnPublisher = BurnPublisher;
			Unsafe.SkipInit(out ulong num);
			if (burnPublisher != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ulong*, int>)(int)(*(uint*)(*(int*)burnPublisher + 12)))((nint)burnPublisher, &num) >= 0)
			{
				result = (long)num;
			}
			return result;
		}
	}

	public unsafe uint TimeAvailable
	{
		get
		{
			uint result = 0u;
			IBurnPublisher* burnPublisher = BurnPublisher;
			if (burnPublisher != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)burnPublisher + 20)))((nint)burnPublisher, &result);
			}
			return result;
		}
	}

	public unsafe bool IsDoorOpen
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IWMPCDDevice* pDevice = m_pDevice;
			Unsafe.SkipInit(out int num);
			if (pDevice != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pDevice + 44)))((nint)pDevice, &num) >= 0)
			{
				bool flag = ((num != 0) ? true : false);
				result = flag;
			}
			return result;
		}
	}

	public unsafe bool IsDVD
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IBurnPublisher* burnPublisher = BurnPublisher;
			Unsafe.SkipInit(out uint num);
			if (burnPublisher != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)burnPublisher + 36)))((nint)burnPublisher, &num) >= 0)
			{
				bool flag = (byte)((num >> 5) & 1) != 0;
				result = flag;
			}
			return result;
		}
	}

	public unsafe bool IsCDRW
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IBurnPublisher* burnPublisher = BurnPublisher;
			Unsafe.SkipInit(out uint num);
			if (burnPublisher != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)burnPublisher + 36)))((nint)burnPublisher, &num) >= 0)
			{
				bool flag = (byte)((num >> 4) & 1) != 0;
				result = flag;
			}
			return result;
		}
	}

	public unsafe bool IsWriteable
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IBurnPublisher* burnPublisher = BurnPublisher;
			Unsafe.SkipInit(out uint num);
			if (burnPublisher != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)burnPublisher + 36)))((nint)burnPublisher, &num) >= 0)
			{
				bool flag = (byte)((num >> 3) & 1) != 0;
				result = flag;
			}
			return result;
		}
	}

	public unsafe bool IsBlank
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IBurnPublisher* burnPublisher = BurnPublisher;
			Unsafe.SkipInit(out uint num);
			if (burnPublisher != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)burnPublisher + 36)))((nint)burnPublisher, &num) >= 0)
			{
				bool flag = (byte)((num >> 2) & 1) != 0;
				result = flag;
			}
			return result;
		}
	}

	public unsafe bool IsDriveReady
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IWMPCDDevice* pDevice = m_pDevice;
			Unsafe.SkipInit(out int num);
			if (pDevice != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pDevice + 92)))((nint)pDevice, &num) >= 0)
			{
				bool flag = ((num != 0) ? true : false);
				result = flag;
			}
			return result;
		}
	}

	public unsafe bool IsMediaLoaded
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IWMPCDDevice* pDevice = m_pDevice;
			Unsafe.SkipInit(out int num);
			if (pDevice != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pDevice + 40)))((nint)pDevice, &num) >= 0)
			{
				bool flag = ((num != 0) ? true : false);
				result = flag;
			}
			return result;
		}
	}

	public unsafe char DrivePath
	{
		[return: MarshalAs(UnmanagedType.U2)]
		get
		{
			IWMPCDDevice* pDevice = m_pDevice;
			if (pDevice != null)
			{
				return (char)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort>)(int)(*(uint*)(*(int*)pDevice + 16)))((nint)pDevice);
			}
			return '\0';
		}
	}

	public unsafe string TOC
	{
		get
		{
			string result = null;
			if (m_pDevice != null && IsMediaLoaded)
			{
				IWMPCDDevice* pDevice = m_pDevice;
				Unsafe.SkipInit(out IWMPCDMediaInfo* ptr);
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IWMPCDMediaInfo**, int>)(int)(*(uint*)(*(int*)pDevice + 56)))((nint)pDevice, &ptr) >= 0)
				{
					ushort* ptr2 = null;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)ptr + 16)))((nint)ptr, &ptr2) >= 0)
					{
						result = new string((char*)ptr2);
						global::_003CModule_003E.SysFreeString(ptr2);
					}
					IWMPCDMediaInfo* intPtr = ptr;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				}
			}
			return result;
		}
	}

	[SpecialName]
	public virtual event OnQueryCancelHandler QueryCancelHandler
	{
		add
		{
			m_QueryCancelHandler = (OnQueryCancelHandler)Delegate.Combine(m_QueryCancelHandler, value);
		}
		remove
		{
			m_QueryCancelHandler = (OnQueryCancelHandler)Delegate.Remove(m_QueryCancelHandler, value);
		}
	}

	[SpecialName]
	public virtual event OnSetDriveLockedForBurningHandler SetDriveLockedForBurningHandler
	{
		add
		{
			m_SetDriveLockedForBurningHandler = (OnSetDriveLockedForBurningHandler)Delegate.Combine(m_SetDriveLockedForBurningHandler, value);
		}
		remove
		{
			m_SetDriveLockedForBurningHandler = (OnSetDriveLockedForBurningHandler)Delegate.Remove(m_SetDriveLockedForBurningHandler, value);
		}
	}

	[SpecialName]
	public virtual event OnBurnStateChangeHandler BurnStateChangeHandler
	{
		add
		{
			m_BurnStateChangeHandler = (OnBurnStateChangeHandler)Delegate.Combine(m_BurnStateChangeHandler, value);
		}
		remove
		{
			m_BurnStateChangeHandler = (OnBurnStateChangeHandler)Delegate.Remove(m_BurnStateChangeHandler, value);
		}
	}

	[SpecialName]
	public virtual event OnItemErrorHandler ItemErrorHandler
	{
		add
		{
			m_ItemErrorHandler = (OnItemErrorHandler)Delegate.Combine(m_ItemErrorHandler, value);
		}
		remove
		{
			m_ItemErrorHandler = (OnItemErrorHandler)Delegate.Remove(m_ItemErrorHandler, value);
		}
	}

	[SpecialName]
	public virtual event OnItemProgressHandler ItemProgressHandler
	{
		add
		{
			m_ItemProgressHandler = (OnItemProgressHandler)Delegate.Combine(m_ItemProgressHandler, value);
		}
		remove
		{
			m_ItemProgressHandler = (OnItemProgressHandler)Delegate.Remove(m_ItemProgressHandler, value);
		}
	}

	[SpecialName]
	public virtual event OnSessionProgressHandler SessionProgressHandler
	{
		add
		{
			m_SessionProgressHandler = (OnSessionProgressHandler)Delegate.Combine(m_SessionProgressHandler, value);
		}
		remove
		{
			m_SessionProgressHandler = (OnSessionProgressHandler)Delegate.Remove(m_SessionProgressHandler, value);
		}
	}

	private void _007EZuneLibraryCDDevice()
	{
		_0021ZuneLibraryCDDevice();
	}

	private unsafe void _0021ZuneLibraryCDDevice()
	{
		if (!_disposed)
		{
			IWMPCDDevice* pDevice = m_pDevice;
			if (pDevice != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pDevice + 8)))((nint)pDevice);
				m_pDevice = null;
			}
			IBurnPublisher* pBurnPublisher = m_pBurnPublisher;
			if (pBurnPublisher != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pBurnPublisher + 8)))((nint)pBurnPublisher);
				m_pBurnPublisher = null;
			}
			_disposed = true;
		}
	}

	public unsafe int GetTrackUrl(uint dwTrackNum, StringBuilder strBuilder)
	{
		int num = -2147418113;
		strBuilder.Length = 0;
		if (m_pDevice != null && IsMediaLoaded)
		{
			IWMPCDDevice* pDevice = m_pDevice;
			Unsafe.SkipInit(out IWMPCDMediaInfo* ptr);
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IWMPCDMediaInfo**, int>)(int)(*(uint*)(*(int*)pDevice + 56)))((nint)pDevice, &ptr);
			if (num >= 0)
			{
				ushort* ptr2 = null;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, ushort**, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, dwTrackNum, &ptr2);
				if (num >= 0)
				{
					strBuilder.Append(new string((char*)ptr2));
					global::_003CModule_003E.SysFreeString(ptr2);
				}
				IWMPCDMediaInfo* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
		}
		return num;
	}

	public unsafe HRESULT SetBurnPlaylist(int iPlaylistId)
	{
		IBurnPublisher* burnPublisher = BurnPublisher;
		if (burnPublisher == null)
		{
			return m_hrBurnPublisherCreate;
		}
		QueryPropertyBag queryPropertyBag = new QueryPropertyBag();
		IQueryPropertyBag* iQueryPropertyBag = queryPropertyBag.GetIQueryPropertyBag();
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)15, 0);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)10, iPlaylistId);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)22, 1);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)21, 437);
		IPlaylist* ptr = null;
		IDatabaseQueryResults* ptr2 = null;
		int num = global::_003CModule_003E.ZuneLibraryExports_002EQueryDatabase(EQueryType.eQueryTypePlaylistContentByPlaylistId, iQueryPropertyBag, &ptr2, null);
		if (num >= 0)
		{
			num = global::_003CModule_003E.ZuneLibraryExports_002ECreateEmptyPlaylist(&ptr);
		}
		uint num2 = 0u;
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)ptr2 + 44)))((nint)ptr2, &num2);
			if (num >= 0)
			{
				if (num2 != 0)
				{
					uint num3 = 0u;
					Unsafe.SkipInit(out CComPropVariant cComPropVariant);
					Unsafe.SkipInit(out CComPropVariant cComPropVariant2);
					while (num3 < num2)
					{
						// IL initblk instruction
						Unsafe.InitBlock(ref cComPropVariant, 0, 16);
						try
						{
							// IL initblk instruction
							Unsafe.InitBlock(ref cComPropVariant2, 0, 16);
							try
							{
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)ptr2 + 24)))((nint)ptr2, num3, 233u, (tagPROPVARIANT*)(&cComPropVariant));
								if (num >= 0)
								{
									num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)ptr2 + 24)))((nint)ptr2, num3, 234u, (tagPROPVARIANT*)(&cComPropVariant2));
									if (num >= 0)
									{
										num = global::_003CModule_003E.ZuneLibraryExports_002EAddItemToPlaylist(Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)), Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant2, 8)), ptr);
									}
								}
							}
							catch
							{
								//try-fault
								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant2);
								throw;
							}
							global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant2);
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
							throw;
						}
						global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
						num3++;
						if (num < 0)
						{
							break;
						}
					}
				}
				if (num >= 0)
				{
					if (ptr == null)
					{
						goto IL_0178;
					}
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IPlaylist*, int>)(int)(*(uint*)(*(int*)burnPublisher + 44)))((nint)burnPublisher, ptr);
				}
			}
		}
		if (null != ptr)
		{
			IPlaylist* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			ptr = null;
		}
		goto IL_0178;
		IL_0178:
		if (null != ptr2)
		{
			IDatabaseQueryResults* intPtr2 = ptr2;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
			ptr2 = null;
		}
		((IDisposable)queryPropertyBag)?.Dispose();
		return num;
	}

	public unsafe HRESULT StartBurn()
	{
		IBurnPublisher* burnPublisher = BurnPublisher;
		if (burnPublisher == null)
		{
			return m_hrBurnPublisherCreate;
		}
		int num = AdviseForBurnPublisherEvents();
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)burnPublisher + 72)))((nint)burnPublisher, 1);
			if (num >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)burnPublisher + 104)))((nint)burnPublisher);
				if (num >= 0)
				{
					goto IL_004a;
				}
			}
		}
		UnadviseForBurnPublisherEvents();
		goto IL_004a;
		IL_004a:
		return num;
	}

	public unsafe int StopBurn()
	{
		IBurnPublisher* burnPublisher = BurnPublisher;
		if (burnPublisher == null)
		{
			return m_hrBurnPublisherCreate;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)burnPublisher + 108)))((nint)burnPublisher);
	}

	public unsafe HRESULT EraseDisc()
	{
		IBurnPublisher* burnPublisher = BurnPublisher;
		if (burnPublisher == null)
		{
			return m_hrBurnPublisherCreate;
		}
		int num = AdviseForBurnPublisherEvents();
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)burnPublisher + 68)))((nint)burnPublisher);
			if (num >= 0)
			{
				goto IL_0038;
			}
		}
		UnadviseForBurnPublisherEvents();
		goto IL_0038;
		IL_0038:
		return new HRESULT(num);
	}

	public unsafe int SetActive([MarshalAs(UnmanagedType.U1)] bool fActive)
	{
		IBurnPublisher* burnPublisher = BurnPublisher;
		if (burnPublisher == null)
		{
			return m_hrBurnPublisherCreate;
		}
		int num = (fActive ? 1 : 0);
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)burnPublisher + 72)))((nint)burnPublisher, num);
	}

	public unsafe int Eject()
	{
		int result = -2147418113;
		IWMPCDDevice* pDevice = m_pDevice;
		if (pDevice != null)
		{
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pDevice + 84)))((nint)pDevice);
		}
		return result;
	}

	public unsafe HRESULT SetVolumeLabelW(string strVolumeLabel)
	{
		IBurnPublisher* burnPublisher = BurnPublisher;
		if (burnPublisher == null)
		{
			return m_hrBurnPublisherCreate;
		}
		if (string.IsNullOrEmpty(strVolumeLabel))
		{
			return -2147024809;
		}
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strVolumeLabel)))
		{
			ushort* ptr2 = global::_003CModule_003E.SysAllocString(ptr);
			if (ptr2 == null)
			{
				return -2147024882;
			}
			HRESULT result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)burnPublisher + 32)))((nint)burnPublisher, ptr2);
			global::_003CModule_003E.SysFreeString(ptr2);
			return result;
		}
	}

	public unsafe int Close()
	{
		int result = -2147418113;
		IWMPCDDevice* pDevice = m_pDevice;
		if (pDevice != null)
		{
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pDevice + 88)))((nint)pDevice);
		}
		return result;
	}

	internal void ItemProgress(int lMediaIndex, EBurnProgressStatus status, int nPercent)
	{
		m_ItemProgressHandler?.Invoke(lMediaIndex, status, nPercent);
	}

	internal void ItemError(int lMediaIndex, int hrError)
	{
		m_ItemErrorHandler?.Invoke(lMediaIndex, hrError);
	}

	internal void SessionProgress(int lSessonSecondsRemaining, int lTotalSessionSeconds)
	{
		m_SessionProgressHandler?.Invoke(lSessonSecondsRemaining, lTotalSessionSeconds);
	}

	internal void BurnStateChange(EBurnState burnState)
	{
		m_BurnStateChangeHandler?.Invoke(burnState);
		if (burnState == EBurnState.ebsStopped)
		{
			UnadviseForBurnPublisherEvents();
			SetActive(fActive: false);
		}
	}

	internal void SetDriveLockedForBurning([MarshalAs(UnmanagedType.U1)] bool fLocked)
	{
		m_SetDriveLockedForBurningHandler?.Invoke(fLocked);
	}

	internal unsafe void QueryCancel(bool* pfCancel)
	{
		m_QueryCancelHandler?.Invoke(pfCancel);
	}

	private unsafe int AdviseForBurnPublisherEvents()
	{
		CBurnPublisherCallback* ptr = (CBurnPublisherCallback*)global::_003CModule_003E.@new(12u);
		CBurnPublisherCallback* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.MicrosoftZuneLibrary_002ECBurnPublisherCallback_002E_007Bctor_007D(ptr, this));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		int result;
		if (ptr2 == null)
		{
			result = -2147024882;
		}
		else
		{
			IBurnPublisher* burnPublisher = BurnPublisher;
			Unsafe.SkipInit(out uint dwBurnAdviseCookie);
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IBurnPublisherCallback*, uint*, int>)(int)(*(uint*)(*(int*)burnPublisher + 96)))((nint)burnPublisher, (IBurnPublisherCallback*)ptr2, &dwBurnAdviseCookie);
			m_dwBurnAdviseCookie = dwBurnAdviseCookie;
		}
		return result;
	}

	private unsafe void UnadviseForBurnPublisherEvents()
	{
		if (m_dwBurnAdviseCookie != 0)
		{
			IBurnPublisher* burnPublisher = BurnPublisher;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)(*(int*)burnPublisher + 100)))((nint)burnPublisher, m_dwBurnAdviseCookie);
			m_dwBurnAdviseCookie = 0u;
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_007EZuneLibraryCDDevice();
			return;
		}
		try
		{
			_0021ZuneLibraryCDDevice();
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

	~ZuneLibraryCDDevice()
	{
		Dispose(false);
	}
}
