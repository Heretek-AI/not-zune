using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Iris;

namespace Microsoft.Zune.Util;

public class TaskbarPlayer : ModelItem, IDisposable
{
	internal uint m_uTaskbarPlayerStateMsg = 0u;

	internal uint m_uTaskbarPlayerCommandMsg = 0u;

	private TaskbarPlayerCommandHandler m_commandHandler;

	private bool m_fInitialized = false;

	private bool m_fPopupVisible = false;

	private bool m_fToolbarVisible = false;

	private bool m_fInteractivePopupTimer = false;

	private bool m_fHidePopupTimer = false;

	private bool m_fShowToolbar = false;

	private Command m_restoreCommand;

	private unsafe HWND__* m_hWndCommandDispatcher = null;

	private unsafe HWND__* m_hWndTaskbarPlayer = null;

	private unsafe HWND__* m_hWndFrame = null;

	private Point m_popupPosition;

	private Size m_popupSize;

	private WindowState m_restoreState;

	private Point m_restorePosition;

	private Size m_restoreSize;

	private static object sm_lock = new object();

	private static TaskbarPlayer sm_taskbarPlayer = null;

	public Size RestoreSize => m_restoreSize;

	public Point RestorePosition => m_restorePosition;

	public WindowState RestoreState
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return m_restoreState;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (m_restoreState != value)
			{
				m_restoreState = value;
				((ModelItem)this).FirePropertyChanged("RestoreState");
			}
		}
	}

	public Size PopupSize => m_popupSize;

	public Point PopupPosition => m_popupPosition;

	public bool PopupVisible
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return m_fPopupVisible;
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			if (m_fPopupVisible != value)
			{
				m_fPopupVisible = value;
				((ModelItem)this).FirePropertyChanged("PopupVisible");
			}
		}
	}

	public bool ToolbarVisible
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return m_fToolbarVisible;
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			if (m_fToolbarVisible != value)
			{
				m_fToolbarVisible = value;
				((ModelItem)this).FirePropertyChanged("ToolbarVisible");
			}
		}
	}

	public unsafe bool EnableToolbar
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			if (Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, int>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand) == 0)
			{
				global::_003CModule_003E.CoCreateInstance((_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E.CLSID_TrayDeskBand), null, 4u, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E.IID_ITrayDeskBand), (void**)Unsafe.AsPointer(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand));
			}
			if (Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, int>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand) != 0)
			{
				result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, uint>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand) + 20)))((IntPtr)Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, int>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand), (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._GUID_f2d3efa4_12f4_466b_a41c_d9ec613ad509)) == 0;
			}
			return result;
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			if (Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, int>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand) == 0)
			{
				global::_003CModule_003E.CoCreateInstance((_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E.CLSID_TrayDeskBand), null, 4u, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E.IID_ITrayDeskBand), (void**)Unsafe.AsPointer(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand));
			}
			if (Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, int>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand) != 0)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, uint>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand) + 24)))((IntPtr)Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, int>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand));
				if (value)
				{
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, uint>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand) + 12)))((IntPtr)Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, int>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand), (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._GUID_f2d3efa4_12f4_466b_a41c_d9ec613ad509));
				}
				else
				{
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, uint>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand) + 16)))((IntPtr)Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, int>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand), (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._GUID_f2d3efa4_12f4_466b_a41c_d9ec613ad509));
				}
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, uint>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand) + 24)))((IntPtr)Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, int>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand));
			}
		}
	}

	public unsafe bool ShowToolbar
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return m_fShowToolbar;
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			if (m_fShowToolbar != value)
			{
				m_fShowToolbar = value;
				((ModelItem)this).FirePropertyChanged("ShowToolbar");
				if (value)
				{
					HMONITOR__* ptr = global::_003CModule_003E.MonitorFromWindow(m_hWndFrame, 2u);
					if (ptr != null)
					{
						Unsafe.SkipInit(out tagMONITORINFO tagMONITORINFO2);
						// IL initblk instruction
						Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 4), 0, 36);
						*(int*)(&tagMONITORINFO2) = 40;
						Unsafe.SkipInit(out tagWINDOWPLACEMENT tagWINDOWPLACEMENT2);
						// IL initblk instruction
						Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref tagWINDOWPLACEMENT2, 4), 0, 40);
						*(int*)(&tagWINDOWPLACEMENT2) = 44;
						if (global::_003CModule_003E.GetMonitorInfoW(ptr, &tagMONITORINFO2) != 0 && global::_003CModule_003E.GetWindowPlacement(m_hWndFrame, &tagWINDOWPLACEMENT2) != 0)
						{
							WindowState val = (WindowState)(Unsafe.As<tagWINDOWPLACEMENT, int>(ref Unsafe.AddByteOffset(ref tagWINDOWPLACEMENT2, 4)) & 2);
							RestoreState = (WindowState)(int)val;
							global::_003CModule_003E.OffsetRect((tagRECT*)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref tagWINDOWPLACEMENT2, 28)), Unsafe.As<tagMONITORINFO, int>(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 20)), Unsafe.As<tagMONITORINFO, int>(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 24)));
							Unsafe.SkipInit(out tagRECT tagRECT2);
							if (global::_003CModule_003E.IntersectRect(&tagRECT2, (tagRECT*)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref tagWINDOWPLACEMENT2, 28)), (tagRECT*)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 20))) == 0)
							{
								global::_003CModule_003E.CopyRect((tagRECT*)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref tagWINDOWPLACEMENT2, 28)), (tagRECT*)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 20)));
								global::_003CModule_003E.InflateRect((tagRECT*)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref tagWINDOWPLACEMENT2, 28)), -100, -100);
							}
							RestorePosition.X = Unsafe.As<tagWINDOWPLACEMENT, int>(ref Unsafe.AddByteOffset(ref tagWINDOWPLACEMENT2, 28));
							RestorePosition.Y = Unsafe.As<tagWINDOWPLACEMENT, int>(ref Unsafe.AddByteOffset(ref tagWINDOWPLACEMENT2, 32));
							RestoreSize.Width = Unsafe.As<tagWINDOWPLACEMENT, int>(ref Unsafe.AddByteOffset(ref tagWINDOWPLACEMENT2, 36)) - Unsafe.As<tagWINDOWPLACEMENT, int>(ref Unsafe.AddByteOffset(ref tagWINDOWPLACEMENT2, 28));
							RestoreSize.Height = Unsafe.As<tagWINDOWPLACEMENT, int>(ref Unsafe.AddByteOffset(ref tagWINDOWPLACEMENT2, 40)) - Unsafe.As<tagWINDOWPLACEMENT, int>(ref Unsafe.AddByteOffset(ref tagWINDOWPLACEMENT2, 32));
						}
					}
				}
			}
			DispatchCommand(ETaskbarPlayerCommand.PC_Connect, 0);
			if (value)
			{
				HWND__* hWndTaskbarPlayer = m_hWndTaskbarPlayer;
				if (hWndTaskbarPlayer == null || global::_003CModule_003E.IsWindow(hWndTaskbarPlayer) == 0)
				{
					return;
				}
			}
			ToolbarVisible = value;
		}
	}

	public Command Restore => m_restoreCommand;

	public static TaskbarPlayer Instance
	{
		get
		{
			if (sm_taskbarPlayer == null)
			{
				sm_taskbarPlayer = new TaskbarPlayer();
			}
			return sm_taskbarPlayer;
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool Initialize(IntPtr hWndFrame, TaskbarPlayerCommandHandler commandHandler)
	{
		if (!m_fInitialized)
		{
			m_hWndFrame = (HWND__*)hWndFrame.ToPointer();
			m_commandHandler = commandHandler;
			global::_003CModule_003E.gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E_002E_003D((gcroot_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003ATaskbarPlayer_0020_005E_003E*)Unsafe.AsPointer(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002Esm_gcTaskbarPlayer), this);
			m_uTaskbarPlayerStateMsg = global::_003CModule_003E.RegisterWindowMessageW((ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1DE_0040LJLIMGOK_0040_003F_0024AAZ_003F_0024AAu_003F_0024AAn_003F_0024AAe_003F_0024AAT_003F_0024AAa_003F_0024AAs_003F_0024AAk_003F_0024AAb_003F_0024AAa_003F_0024AAr_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAe_003F_0024AAr_003F_0024AAS_003F_0024AAt_003F_0024AAa_003F_0024AAt_003F_0024AAe_003F_0024AAM_003F_0024AAs_003F_0024AAg_003F_0024AA_003F_0024AA_0040));
			m_uTaskbarPlayerCommandMsg = global::_003CModule_003E.RegisterWindowMessageW((ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1DI_0040BOCHIFKJ_0040_003F_0024AAZ_003F_0024AAu_003F_0024AAn_003F_0024AAe_003F_0024AAT_003F_0024AAa_003F_0024AAs_003F_0024AAk_003F_0024AAb_003F_0024AAa_003F_0024AAr_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAe_003F_0024AAr_003F_0024AAC_003F_0024AAo_003F_0024AAm_003F_0024AAm_003F_0024AAa_003F_0024AAn_003F_0024AAd_003F_0024AAM_003F_0024AAs_003F_0024AAg_003F_0024AA_003F_0024AA_0040));
			HINSTANCE__* ptr = (HINSTANCE__*)global::_003CModule_003E.GetWindowLongW(m_hWndFrame, -6);
			Unsafe.SkipInit(out tagWNDCLASSW tagWNDCLASSW2);
			*(int*)(&tagWNDCLASSW2) = 0;
			// IL initblk instruction
			Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref tagWNDCLASSW2, 4), 0, 36);
			if (global::_003CModule_003E.GetClassInfoW(ptr, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1EG_0040LILHHEEP_0040_003F_0024AAZ_003F_0024AAu_003F_0024AAn_003F_0024AAe_003F_0024AAT_003F_0024AAa_003F_0024AAs_003F_0024AAk_003F_0024AAb_003F_0024AAa_003F_0024AAr_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAe_003F_0024AAr_003F_0024AAC_003F_0024AAo_003F_0024AAm_003F_0024AAm_003F_0024AAa_003F_0024AAn_003F_0024AAd_003F_0024AAD_003F_0024AAi_003F_0024AAs_003F_0024AAp_003F_0024AAa_003F_0024AAt_003F_0024AAc_003F_0024AAh_0040), &tagWNDCLASSW2) == 0)
			{
				*(int*)(&tagWNDCLASSW2) = 512;
				Unsafe.As<tagWNDCLASSW, int>(ref Unsafe.AddByteOffset(ref tagWNDCLASSW2, 4)) = (int)global::_003CModule_003E.__unep_0040_003FCommandDispatcherWindowProc_0040Util_0040Zune_0040Microsoft_0040_0040_0024_0024FYGJPAUHWND___0040_0040IIJ_0040Z;
				Unsafe.As<tagWNDCLASSW, int>(ref Unsafe.AddByteOffset(ref tagWNDCLASSW2, 16)) = (int)ptr;
				Unsafe.As<tagWNDCLASSW, int>(ref Unsafe.AddByteOffset(ref tagWNDCLASSW2, 24)) = (int)global::_003CModule_003E.LoadCursorW(null, (ushort*)32514);
				Unsafe.As<tagWNDCLASSW, int>(ref Unsafe.AddByteOffset(ref tagWNDCLASSW2, 28)) = 1;
				Unsafe.As<tagWNDCLASSW, int>(ref Unsafe.AddByteOffset(ref tagWNDCLASSW2, 36)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1EG_0040LILHHEEP_0040_003F_0024AAZ_003F_0024AAu_003F_0024AAn_003F_0024AAe_003F_0024AAT_003F_0024AAa_003F_0024AAs_003F_0024AAk_003F_0024AAb_003F_0024AAa_003F_0024AAr_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAe_003F_0024AAr_003F_0024AAC_003F_0024AAo_003F_0024AAm_003F_0024AAm_003F_0024AAa_003F_0024AAn_003F_0024AAd_003F_0024AAD_003F_0024AAi_003F_0024AAs_003F_0024AAp_003F_0024AAa_003F_0024AAt_003F_0024AAc_003F_0024AAh_0040);
				global::_003CModule_003E.RegisterClassW(&tagWNDCLASSW2);
			}
			if (global::_003CModule_003E.CreateWindowExW(128u, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1EG_0040LILHHEEP_0040_003F_0024AAZ_003F_0024AAu_003F_0024AAn_003F_0024AAe_003F_0024AAT_003F_0024AAa_003F_0024AAs_003F_0024AAk_003F_0024AAb_003F_0024AAa_003F_0024AAr_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAe_003F_0024AAr_003F_0024AAC_003F_0024AAo_003F_0024AAm_003F_0024AAm_003F_0024AAa_003F_0024AAn_003F_0024AAd_003F_0024AAD_003F_0024AAi_003F_0024AAs_003F_0024AAp_003F_0024AAa_003F_0024AAt_003F_0024AAc_003F_0024AAh_0040), (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1EG_0040LILHHEEP_0040_003F_0024AAZ_003F_0024AAu_003F_0024AAn_003F_0024AAe_003F_0024AAT_003F_0024AAa_003F_0024AAs_003F_0024AAk_003F_0024AAb_003F_0024AAa_003F_0024AAr_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAe_003F_0024AAr_003F_0024AAC_003F_0024AAo_003F_0024AAm_003F_0024AAm_003F_0024AAa_003F_0024AAn_003F_0024AAd_003F_0024AAD_003F_0024AAi_003F_0024AAs_003F_0024AAp_003F_0024AAa_003F_0024AAt_003F_0024AAc_003F_0024AAh_0040), 2147483648u, 0, 0, 0, 0, null, null, ptr, null) != null)
			{
				m_fInitialized = true;
			}
		}
		return m_fInitialized;
	}

	public unsafe void UpdateToolbar(ETaskbarPlayerState state)
	{
		HWND__* hWndTaskbarPlayer = m_hWndTaskbarPlayer;
		if (hWndTaskbarPlayer != null && global::_003CModule_003E.IsWindow(hWndTaskbarPlayer) != 0)
		{
			if (ShowToolbar)
			{
				state |= ETaskbarPlayerState.PS_Minimized;
			}
			global::_003CModule_003E.PostMessageW(m_hWndTaskbarPlayer, m_uTaskbarPlayerStateMsg, (uint)state, 0);
		}
	}

	internal unsafe void OnCreate(HWND__* hWnd)
	{
		m_hWndCommandDispatcher = hWnd;
	}

	internal unsafe void OnDestory(HWND__* hWnd)
	{
		StopTimer(hWnd, 1u, ref m_fInteractivePopupTimer);
		StopTimer(hWnd, 2u, ref m_fHidePopupTimer);
		m_hWndCommandDispatcher = null;
	}

	internal unsafe void OnTimer(HWND__* hWnd, uint dwTimerId)
	{
		switch (dwTimerId)
		{
		case 2u:
			if (MouseInWindow(m_hWndFrame) == 0 && MouseInWindow(m_hWndTaskbarPlayer) == 0)
			{
				DispatchCommand(ETaskbarPlayerCommand.PC_Popup, 0);
				StopTimer(hWnd, 1u, ref m_fInteractivePopupTimer);
			}
			StopTimer(hWnd, 2u, ref m_fHidePopupTimer);
			break;
		case 1u:
			if (MouseInWindow(m_hWndFrame) == 0 && MouseInWindow(m_hWndTaskbarPlayer) == 0)
			{
				StartTimer(hWnd, 2u, 500u, ref m_fHidePopupTimer);
			}
			else
			{
				StopTimer(hWnd, 2u, ref m_fHidePopupTimer);
			}
			break;
		}
	}

	internal unsafe void OnCommandMsg(ETaskbarPlayerCommand command, int param)
	{
		if (command == ETaskbarPlayerCommand.PC_Connect && param != 0)
		{
			m_hWndTaskbarPlayer = (HWND__*)param;
			if (ShowToolbar)
			{
				ToolbarVisible = true;
			}
		}
		DispatchCommand(command, param);
	}

	private unsafe TaskbarPlayer()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			m_restoreCommand = new Command();
			m_popupPosition = new Point();
			m_popupSize = new Size();
			m_restoreState = (WindowState)2;
			m_restorePosition = new Point();
			m_restoreSize = new Size();
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)this).Dispose();
			throw;
		}
	}

	private void _007ETaskbarPlayer()
	{
		_0021TaskbarPlayer();
	}

	private unsafe void _0021TaskbarPlayer()
	{
		HWND__* hWndCommandDispatcher = m_hWndCommandDispatcher;
		if (hWndCommandDispatcher != null && global::_003CModule_003E.IsWindow(hWndCommandDispatcher) != 0)
		{
			global::_003CModule_003E.PostMessageW(m_hWndCommandDispatcher, 16u, 0u, 0);
		}
		if (Unsafe.As<CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E, int>(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand) != 0)
		{
			global::_003CModule_003E.CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E_002ERelease((CComPtrNtv_003CMicrosoft_003A_003AZune_003A_003AUtil_003A_003AITrayDeskBand_003E*)Unsafe.AsPointer(ref global::_003CModule_003E.Microsoft_002EZune_002EUtil_002E_003FA0xfcba3ca2_002Es_spTrayDeskBand));
		}
	}

	private unsafe void DisplayPopup(int x, int y)
	{
		if (PopupVisible)
		{
			return;
		}
		Unsafe.SkipInit(out tagPOINT tagPOINT2);
		*(int*)(&tagPOINT2) = x;
		Unsafe.As<tagPOINT, int>(ref Unsafe.AddByteOffset(ref tagPOINT2, 4)) = y;
		HMONITOR__* ptr = global::_003CModule_003E.MonitorFromPoint(tagPOINT2, 2u);
		if (ptr != null)
		{
			Unsafe.SkipInit(out tagMONITORINFO tagMONITORINFO2);
			// IL initblk instruction
			Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 4), 0, 36);
			*(int*)(&tagMONITORINFO2) = 40;
			if (global::_003CModule_003E.GetMonitorInfoW(ptr, &tagMONITORINFO2) != 0)
			{
				if (x < Unsafe.As<tagMONITORINFO, int>(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 20)))
				{
					PopupPosition.X = Unsafe.As<tagMONITORINFO, int>(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 20));
				}
				else if (x > Unsafe.As<tagMONITORINFO, int>(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 28)) - PopupSize.Width)
				{
					PopupPosition.X = Unsafe.As<tagMONITORINFO, int>(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 28)) - PopupSize.Width;
				}
				else
				{
					PopupPosition.X = x;
				}
				if (y < Unsafe.As<tagMONITORINFO, int>(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 24)))
				{
					PopupPosition.Y = Unsafe.As<tagMONITORINFO, int>(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 24));
				}
				else if (y > Unsafe.As<tagMONITORINFO, int>(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 32)) - PopupSize.Height)
				{
					PopupPosition.Y = Unsafe.As<tagMONITORINFO, int>(ref Unsafe.AddByteOffset(ref tagMONITORINFO2, 32)) - PopupSize.Height;
				}
				else
				{
					PopupPosition.Y = y;
				}
			}
		}
		PopupVisible = true;
		StartTimer(m_hWndCommandDispatcher, 1u, 250u, ref m_fInteractivePopupTimer);
	}

	private void DispatchCommand(ETaskbarPlayerCommand command, int value)
	{
		if (m_commandHandler == null)
		{
			return;
		}
		switch (command)
		{
		case ETaskbarPlayerCommand.PC_Restore:
			if (ToolbarVisible)
			{
				ShowToolbar = false;
				PopupVisible = false;
				GetKeyboardFocus();
				Restore.Invoke();
			}
			break;
		case ETaskbarPlayerCommand.PC_Popup:
			if (value != 0)
			{
				DisplayPopup((ushort)(value >>> 16), (ushort)value);
			}
			else
			{
				PopupVisible = false;
			}
			break;
		}
		m_commandHandler(command, value);
	}

	private unsafe void GetKeyboardFocus()
	{
		HWND__* hWndFrame = m_hWndFrame;
		if (hWndFrame != null && global::_003CModule_003E.IsWindow(hWndFrame) != 0 && global::_003CModule_003E.GetForegroundWindow() != m_hWndFrame)
		{
			Unsafe.SkipInit(out tagINPUT tagINPUT2);
			// IL initblk instruction
			Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref tagINPUT2, 4), 0, 24);
			*(int*)(&tagINPUT2) = 1;
			Unsafe.As<tagINPUT, short>(ref Unsafe.AddByteOffset(ref tagINPUT2, 4)) = 0;
			global::_003CModule_003E.SendInput(1u, &tagINPUT2, 28);
			global::_003CModule_003E.SetForegroundWindow(m_hWndFrame);
		}
	}

	private unsafe static int MouseInWindow(HWND__* hWnd)
	{
		int result = 0;
		if (hWnd != null && global::_003CModule_003E.IsWindow(hWnd) != 0)
		{
			Unsafe.SkipInit(out tagPOINT tagPOINT2);
			global::_003CModule_003E.GetCursorPos(&tagPOINT2);
			global::_003CModule_003E.ScreenToClient(hWnd, &tagPOINT2);
			Unsafe.SkipInit(out tagRECT tagRECT2);
			global::_003CModule_003E.GetClientRect(hWnd, &tagRECT2);
			result = global::_003CModule_003E.PtInRect(&tagRECT2, tagPOINT2);
		}
		return result;
	}

	private unsafe static void StartTimer(HWND__* hWnd, uint uTimerId, uint msDuration, ref bool fTimer)
	{
		if (!fTimer)
		{
			global::_003CModule_003E.SetTimer(hWnd, uTimerId, msDuration, null);
			fTimer = true;
		}
	}

	private unsafe static void StopTimer(HWND__* hWnd, uint uTimerId, ref bool fTimer)
	{
		if (fTimer)
		{
			global::_003CModule_003E.KillTimer(hWnd, uTimerId);
			fTimer = false;
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007ETaskbarPlayer();
				return;
			}
			finally
			{
				((ModelItem)this).Dispose();
			}
		}
		try
		{
			_0021TaskbarPlayer();
		}
		finally
		{
			((ModelItem)this).Finalize();
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	~TaskbarPlayer()
	{
		Dispose(false);
	}
}
