using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MicrosoftZunePlayback;

public class PlayerInterop : IDisposable
{
	private unsafe IMCPlayer* _uPlayer = null;

	private unsafe IMCTransport* _uTransport = null;

	private unsafe IMCPlayerSetUri* _uSetUri = null;

	private unsafe IMCDynamicImage* _uDynamicImage = null;

	private unsafe IMCVolumeControl* _uVolumeControl;

	private unsafe IZuneSpectrumMgr* _uZuneSpectrumMgr = null;

	private unsafe CPlayerInteropEventSink* _uEventSink;

	private MCPlayerState _state = MCPlayerState.Uninitialized;

	private long _duration = 0L;

	private bool _isReady = false;

	private Announcement _firstDenial;

	private string _currentUri;

	private int _currentUriID;

	private Dictionary<string, object> _properties;

	private MCTransportState _transportState = MCTransportState.Invalid;

	private float _rate = 1f;

	private bool _endOfMedia = false;

	private long _position = 0L;

	private long _minSeekPosition = 0L;

	private long _maxSeekPosition = 0L;

	private bool _resetOnStop = false;

	private TimeSpan _positionEventInterval = TimeSpan.FromMilliseconds(100.0);

	private int _nDynamicImage = 0;

	private int _volume;

	private bool _mute;

	private bool _canChangeVideoRate = false;

	private bool _canSeek = true;

	private IntPtr _windowHandle;

	private VideoWindow _windowHost;

	private static PlayerInterop _singletonInstance = null;

	private unsafe void* _gotStateCloseEvent;

	private unsafe void* _gotStateUninitializeEvent;

	private volatile bool _fShuttingDown;

	private PlayerPropertyChangedEventHandler _003Cbacking_store_003EPlayerPropertyChanged;

	private PlayerBandwithUpdateEventHandler _003Cbacking_store_003EPlayerBandwithUpdate;

	private EventHandler _003Cbacking_store_003EStatusChanged;

	private AnnouncementHandler _003Cbacking_store_003EAlertSent;

	private EventHandler _003Cbacking_store_003ETransportStatusChanged;

	private EventHandler _003Cbacking_store_003ETransportPositionChanged;

	private EventHandler _003Cbacking_store_003EUriSet;

	public int CurrentUriID => _currentUriID;

	public string CurrentUri => _currentUri;

	public unsafe TimeSpan PositionEventInterval
	{
		get
		{
			return _positionEventInterval;
		}
		set
		{
			if (_uTransport != null)
			{
				_positionEventInterval = value;
				int num = *(int*)_uTransport + 48;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)num))((nint)_uTransport, (uint)value.TotalMilliseconds);
			}
		}
	}

	public unsafe bool ResetOnStop
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return _resetOnStop;
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			IMCTransport* uTransport = _uTransport;
			if (uTransport != null)
			{
				_resetOnStop = value;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte, int>)(int)(*(uint*)(*(int*)uTransport + 44)))((nint)uTransport, value ? ((byte)1) : ((byte)0));
			}
		}
	}

	public long MaxSeekPosition => _maxSeekPosition;

	public long MinSeekPosition => _minSeekPosition;

	public long Position => _position;

	public bool EndOfMedia
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return _endOfMedia;
		}
	}

	public unsafe float Rate
	{
		get
		{
			return _rate;
		}
		set
		{
			IMCTransport* uTransport = _uTransport;
			if (uTransport != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, float, int>)(int)(*(uint*)(*(int*)uTransport + 40)))((nint)uTransport, value);
			}
		}
	}

	public MCTransportState TransportState => _transportState;

	public unsafe bool ShowGDIVideo
	{
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			IMCDynamicImage* uDynamicImage = _uDynamicImage;
			if (uDynamicImage != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte, int>)(int)(*(uint*)(*(int*)uDynamicImage + 20)))((nint)uDynamicImage, value ? ((byte)1) : ((byte)0));
			}
		}
	}

	public unsafe VideoWindow VideoPosition
	{
		set
		{
			_windowHost = value;
			Unsafe.SkipInit(out tagRECT tagRECT2);
			*(int*)(&tagRECT2) = value.Left;
			Unsafe.As<tagRECT, int>(ref Unsafe.AddByteOffset(ref tagRECT2, 4)) = value.Top;
			Unsafe.As<tagRECT, int>(ref Unsafe.AddByteOffset(ref tagRECT2, 8)) = value.Right;
			Unsafe.As<tagRECT, int>(ref Unsafe.AddByteOffset(ref tagRECT2, 12)) = value.Bottom;
			IMCDynamicImage* uDynamicImage = _uDynamicImage;
			if (uDynamicImage != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, tagRECT, int>)(int)(*(uint*)(*(int*)uDynamicImage + 16)))((nint)uDynamicImage, tagRECT2);
			}
		}
	}

	public IntPtr WindowHandle
	{
		get
		{
			return _windowHandle;
		}
		set
		{
			_windowHandle = value;
		}
	}

	public bool CanSeek
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return _canSeek;
		}
	}

	public bool CanChangeVideoRate
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return _canChangeVideoRate;
		}
	}

	public unsafe bool Mute
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return _mute;
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			_mute = value;
			IMCVolumeControl* uVolumeControl = _uVolumeControl;
			if (uVolumeControl != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte, int>)(int)(*(uint*)(*(int*)uVolumeControl + 16)))((nint)uVolumeControl, value ? ((byte)1) : ((byte)0));
			}
		}
	}

	public unsafe int Volume
	{
		get
		{
			return _volume;
		}
		set
		{
			_volume = value;
			if (!_mute)
			{
				IMCVolumeControl* uVolumeControl = _uVolumeControl;
				if (uVolumeControl != null)
				{
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)uVolumeControl + 12)))((nint)uVolumeControl, value);
				}
			}
		}
	}

	public int DynamicImage
	{
		get
		{
			return _nDynamicImage;
		}
		set
		{
			_nDynamicImage = value;
		}
	}

	public Announcement FirstDenial => _firstDenial;

	public bool IsReady
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return _isReady;
		}
	}

	public long Duration => _duration;

	public MCPlayerState State => _state;

	public static PlayerInterop Instance
	{
		get
		{
			if (_singletonInstance == null)
			{
				_singletonInstance = new PlayerInterop();
			}
			return _singletonInstance;
		}
	}

	[SpecialName]
	public event EventHandler UriSet
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			_003Cbacking_store_003EUriSet = (EventHandler)Delegate.Combine(_003Cbacking_store_003EUriSet, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			_003Cbacking_store_003EUriSet = (EventHandler)Delegate.Remove(_003Cbacking_store_003EUriSet, value);
		}
	}

	[SpecialName]
	public event EventHandler TransportPositionChanged
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			_003Cbacking_store_003ETransportPositionChanged = (EventHandler)Delegate.Combine(_003Cbacking_store_003ETransportPositionChanged, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			_003Cbacking_store_003ETransportPositionChanged = (EventHandler)Delegate.Remove(_003Cbacking_store_003ETransportPositionChanged, value);
		}
	}

	[SpecialName]
	public event EventHandler TransportStatusChanged
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			_003Cbacking_store_003ETransportStatusChanged = (EventHandler)Delegate.Combine(_003Cbacking_store_003ETransportStatusChanged, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			_003Cbacking_store_003ETransportStatusChanged = (EventHandler)Delegate.Remove(_003Cbacking_store_003ETransportStatusChanged, value);
		}
	}

	[SpecialName]
	public event AnnouncementHandler AlertSent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			_003Cbacking_store_003EAlertSent = (AnnouncementHandler)Delegate.Combine(_003Cbacking_store_003EAlertSent, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			_003Cbacking_store_003EAlertSent = (AnnouncementHandler)Delegate.Remove(_003Cbacking_store_003EAlertSent, value);
		}
	}

	[SpecialName]
	public event EventHandler StatusChanged
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			_003Cbacking_store_003EStatusChanged = (EventHandler)Delegate.Combine(_003Cbacking_store_003EStatusChanged, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			_003Cbacking_store_003EStatusChanged = (EventHandler)Delegate.Remove(_003Cbacking_store_003EStatusChanged, value);
		}
	}

	[SpecialName]
	public virtual event PlayerBandwithUpdateEventHandler PlayerBandwithUpdate
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			_003Cbacking_store_003EPlayerBandwithUpdate = (PlayerBandwithUpdateEventHandler)Delegate.Combine(_003Cbacking_store_003EPlayerBandwithUpdate, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			_003Cbacking_store_003EPlayerBandwithUpdate = (PlayerBandwithUpdateEventHandler)Delegate.Remove(_003Cbacking_store_003EPlayerBandwithUpdate, value);
		}
	}

	[SpecialName]
	public virtual event PlayerPropertyChangedEventHandler PlayerPropertyChanged
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			_003Cbacking_store_003EPlayerPropertyChanged = (PlayerPropertyChangedEventHandler)Delegate.Combine(_003Cbacking_store_003EPlayerPropertyChanged, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			_003Cbacking_store_003EPlayerPropertyChanged = (PlayerPropertyChangedEventHandler)Delegate.Remove(_003Cbacking_store_003EPlayerPropertyChanged, value);
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	private unsafe static bool HResultDenialsEqual(MCHResultAnnouncement* a, Announcement b)
	{
		//The blocks IL_0066, IL_006b, IL_006e, IL_0090, IL_0099, IL_00ab, IL_00af, IL_00b2, IL_00ba, IL_00c1, IL_00cf, IL_00d4, IL_00d7 are reachable both inside and outside the pinned region starting at IL_004c. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_00ab, IL_00af, IL_00b2, IL_00cf, IL_00d4, IL_00d7 are reachable both inside and outside the pinned region starting at IL_0092. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_00d4, IL_00d7 are reachable both inside and outside the pinned region starting at IL_00ba. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_00ab, IL_00af, IL_00b2, IL_00cf, IL_00d4, IL_00d7 are reachable both inside and outside the pinned region starting at IL_0092. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_00d4, IL_00d7 are reachable both inside and outside the pinned region starting at IL_00ba. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_00d4, IL_00d7 are reachable both inside and outside the pinned region starting at IL_0078. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		if (a == null)
		{
			if (b == null)
			{
				return true;
			}
		}
		else if (b != null)
		{
			bool result;
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(b.Id)))
			{
				try
				{
					fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(b.SourceFile)))
					{
						try
						{
							int num;
							ref ushort reference;
							ref ushort reference2;
							if (((int*)a)[2] == b.HResult && ((int*)a)[5] == (int)b.SourceLine)
							{
								fixed (ushort* ptr3 = &Unsafe.AsRef<ushort>(ptr))
								{
									num = *(int*)a;
									short num2 = Unsafe.ReadUnaligned<short>((void*)num);
									short num3 = Unsafe.ReadUnaligned<short>(ptr3);
									if (num2 >= num3 && num2 <= num3)
									{
										if (num2 != 0)
										{
											num += 2;
											reference = ref *ptr3;
											goto IL_0078;
										}
										int num4;
										fixed (ushort* ptr4 = &Unsafe.AsRef<ushort>(ptr2))
										{
											num4 = ((int*)a)[4];
											short num5 = Unsafe.ReadUnaligned<short>((void*)num4);
											short num6 = Unsafe.ReadUnaligned<short>(ptr4);
											int num7;
											if (num5 < num6 || num5 > num6)
											{
												num7 = 0;
												goto IL_00d7;
											}
											if (num5 == 0)
											{
												num7 = 1;
												goto IL_00d7;
											}
											num4 += 2;
											reference2 = ref *ptr4;
											goto end_IL_0099;
											IL_00d7:
											result = (byte)num7 != 0;
											goto end_IL_0050;
											end_IL_0099:;
										}
										while (true)
										{
											fixed (ushort* ptr4 = &Unsafe.Add(ref reference2, 1))
											{
												short num5 = Unsafe.ReadUnaligned<short>((void*)num4);
												short num6 = Unsafe.ReadUnaligned<short>(ptr4);
												int num7;
												if (num5 >= num6 && num5 <= num6)
												{
													if (num5 != 0)
													{
														num4 += 2;
														reference2 = ref *ptr4;
														continue;
													}
													num7 = 1;
													goto IL_00d7_2;
												}
												num7 = 0;
												goto IL_00d7_2;
												IL_00d7_2:
												result = (byte)num7 != 0;
												break;
											}
										}
									}
									else
									{
										int num7 = 0;
										result = (byte)num7 != 0;
									}
									end_IL_0050:;
								}
							}
							else
							{
								int num7 = 0;
								result = (byte)num7 != 0;
							}
							goto end_IL_002b;
							IL_0078:
							while (true)
							{
								fixed (ushort* ptr3 = &Unsafe.Add(ref reference, 1))
								{
									short num2 = Unsafe.ReadUnaligned<short>((void*)num);
									short num3 = Unsafe.ReadUnaligned<short>(ptr3);
									if (num2 >= num3 && num2 <= num3)
									{
										if (num2 != 0)
										{
											num += 2;
											reference = ref *ptr3;
											continue;
										}
										int num4;
										fixed (ushort* ptr4 = &Unsafe.AsRef<ushort>(ptr2))
										{
											num4 = ((int*)a)[4];
											short num5 = Unsafe.ReadUnaligned<short>((void*)num4);
											short num6 = Unsafe.ReadUnaligned<short>(ptr4);
											int num7;
											if (num5 < num6 || num5 > num6)
											{
												num7 = 0;
												goto IL_00d7_3;
											}
											if (num5 == 0)
											{
												num7 = 1;
												goto IL_00d7_3;
											}
											num4 += 2;
											reference2 = ref *ptr4;
											goto end_IL_0099_2;
											IL_00d7_3:
											result = (byte)num7 != 0;
											goto end_IL_0080;
											end_IL_0099_2:;
										}
										while (true)
										{
											fixed (ushort* ptr4 = &Unsafe.Add(ref reference2, 1))
											{
												short num5 = Unsafe.ReadUnaligned<short>((void*)num4);
												short num6 = Unsafe.ReadUnaligned<short>(ptr4);
												int num7;
												if (num5 >= num6 && num5 <= num6)
												{
													if (num5 != 0)
													{
														num4 += 2;
														reference2 = ref *ptr4;
														continue;
													}
													num7 = 1;
													goto IL_00d7_4;
												}
												num7 = 0;
												goto IL_00d7_4;
												IL_00d7_4:
												result = (byte)num7 != 0;
												break;
											}
										}
									}
									else
									{
										int num7 = 0;
										result = (byte)num7 != 0;
									}
									end_IL_0080:;
								}
								break;
							}
							end_IL_002b:;
						}
						catch
						{
							//try-fault
							ptr2 = null;
							throw;
						}
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
			return result;
		}
		return false;
	}

	private unsafe static Announcement MarshalAlert(MCHResultAnnouncement* hrAlert)
	{
		Announcement announcement = new Announcement();
		IntPtr ptr = new IntPtr((void*)(int)(*(uint*)hrAlert));
		announcement.Id = Marshal.PtrToStringUni(ptr);
		announcement.HResult = ((int*)hrAlert)[2];
		IntPtr ptr2 = new IntPtr((void*)(int)((uint*)hrAlert)[4]);
		announcement.SourceFile = Marshal.PtrToStringUni(ptr2);
		announcement.SourceLine = ((uint*)hrAlert)[5];
		announcement.PlaybackID = ((int*)hrAlert)[3];
		return announcement;
	}

	private unsafe PlayerInterop()
	{
		ref IntPtr windowHandle = ref _windowHandle;
		windowHandle = new IntPtr(null);
		_windowHost = null;
		_gotStateCloseEvent = null;
		_gotStateUninitializeEvent = null;
		_fShuttingDown = false;
		base._002Ector();
	}

	internal unsafe void OnAlertOccurred(MCHResultAnnouncement* pAlert)
	{
		raise_AlertSent(MarshalAlert(pAlert));
	}

	internal unsafe void OnStatusChanged(MCPlayerStatus* pStatus)
	{
		bool flag = false;
		long num = ((long*)pStatus)[1];
		if (_duration != num)
		{
			_duration = num;
			flag = true;
		}
		int num2 = *(int*)pStatus;
		if (_state != (MCPlayerState)num2)
		{
			_state = (MCPlayerState)num2;
			flag = true;
		}
		byte b = ((byte*)pStatus)[16];
		int num3 = ((b != 0) ? 1 : 0);
		if ((_isReady ? 1 : 0) != num3)
		{
			int isReady = ((b != 0) ? 1 : 0);
			_isReady = (byte)isReady != 0;
			flag = true;
		}
		uint num4 = ((uint*)pStatus)[6];
		if (num4 == 0)
		{
			if (_firstDenial != null)
			{
				_firstDenial = null;
				flag = true;
			}
		}
		else if (!HResultDenialsEqual((MCHResultAnnouncement*)(int)num4, _firstDenial))
		{
			raise_AlertSent(_firstDenial = MarshalAlert((MCHResultAnnouncement*)(int)((uint*)pStatus)[6]));
		}
		if (!_fShuttingDown && flag)
		{
			raise_StatusChanged(this, EventArgs.Empty);
		}
		if (_fShuttingDown)
		{
			switch (_state)
			{
			case MCPlayerState.Closed:
				global::_003CModule_003E.SetEvent(_gotStateCloseEvent);
				break;
			case MCPlayerState.Uninitialized:
				global::_003CModule_003E.SetEvent(_gotStateUninitializeEvent);
				break;
			}
		}
	}

	internal unsafe void OnPropertyChanged(ushort* wzKey, byte* pData, uint dataLength)
	{
		object value = null;
		IntPtr ptr = new IntPtr(wzKey);
		string text = Marshal.PtrToStringUni(ptr);
		if (pData != null)
		{
			if (string.Compare(text, "presentationinfo", StringComparison.Ordinal) == 0)
			{
				byte needOverscan = (byte)((pData[16] != 0) ? 1 : 0);
				value = new PresentationInfo((int)(double)(*(float*)pData), (int)(double)((float*)pData)[1], ((int*)pData)[2], ((int*)pData)[3], needOverscan != 0);
			}
			else if (string.Compare(text, "volumeinfo", StringComparison.Ordinal) == 0)
			{
				_volume = *(int*)pData;
				int mute = ((((int*)pData)[1] != 0) ? 1 : 0);
				_mute = (byte)mute != 0;
			}
			else if (string.Compare(text, "canchangevideorate", StringComparison.Ordinal) == 0)
			{
				byte b = *pData;
				_canChangeVideoRate = b != 0;
				value = b != 0;
			}
			else if (string.Compare(text, "mbrheuristicsdata", StringComparison.Ordinal) == 0)
			{
				Unsafe.SkipInit(out _MBRHEURISTICDATA mBRHEURISTICDATA);
				// IL cpblk instruction
				Unsafe.CopyBlock(ref mBRHEURISTICDATA, pData, 48);
				BandwidthUpdateArgs value2 = new BandwidthUpdateArgs(*(long*)(&mBRHEURISTICDATA), Unsafe.As<_MBRHEURISTICDATA, float>(ref Unsafe.AddByteOffset(ref mBRHEURISTICDATA, 8)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref mBRHEURISTICDATA, 12)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref mBRHEURISTICDATA, 16)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref mBRHEURISTICDATA, 20)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref mBRHEURISTICDATA, 24)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref mBRHEURISTICDATA, 28)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref mBRHEURISTICDATA, 32)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref mBRHEURISTICDATA, 36)), Unsafe.As<_MBRHEURISTICDATA, int>(ref Unsafe.AddByteOffset(ref mBRHEURISTICDATA, 40)), Unsafe.As<_MBRHEURISTICDATA, MBRHeuristicState>(ref Unsafe.AddByteOffset(ref mBRHEURISTICDATA, 44)));
				raise_PlayerBandwithUpdate(this, value2);
			}
		}
		_properties[text] = value;
		PlayerPropertyChangedEventArgs value3 = new PlayerPropertyChangedEventArgs(text, value);
		raise_PlayerPropertyChanged(this, value3);
	}

	internal unsafe void OnTransportStatusChanged(MCTransportStatus* pTransportStatus)
	{
		_transportState = *(MCTransportState*)pTransportStatus;
		_rate = ((float*)pTransportStatus)[1];
		int endOfMedia = (((bool*)pTransportStatus)[8] ? 1 : 0);
		_endOfMedia = (byte)endOfMedia != 0;
		int canSeek = (((bool*)pTransportStatus)[9] ? 1 : 0);
		_canSeek = (byte)canSeek != 0;
		raise_TransportStatusChanged(this, EventArgs.Empty);
	}

	internal void OnTransportPositionChanged(long position, long minSeekPosition, long maxSeekPosition)
	{
		_position = position;
		_minSeekPosition = minSeekPosition;
		_maxSeekPosition = maxSeekPosition;
		raise_TransportPositionChanged(this, EventArgs.Empty);
	}

	internal unsafe void OnUriSet(ushort* wzUri, uint dwParam)
	{
		_currentUriID = (int)dwParam;
		_currentUri = new string((char*)wzUri);
		raise_UriSet(this, EventArgs.Empty);
	}

	[SpecialName]
	protected virtual void raise_PlayerPropertyChanged(object value0, PlayerPropertyChangedEventArgs value1)
	{
		_003Cbacking_store_003EPlayerPropertyChanged?.Invoke(value0, value1);
	}

	[SpecialName]
	protected virtual void raise_PlayerBandwithUpdate(object value0, BandwidthUpdateArgs value1)
	{
		_003Cbacking_store_003EPlayerBandwithUpdate?.Invoke(value0, value1);
	}

	private void _007EPlayerInterop()
	{
		_firstDenial = null;
		Uninitialize();
	}

	public unsafe void Initialize()
	{
		IMCPlayer* ptr = null;
		IMCTransport* ptr2 = null;
		IMCPlayerSetUri* ptr3 = null;
		IMCDynamicImage* ptr4 = null;
		IMCVolumeControl* ptr5 = null;
		IZuneSpectrumMgr* ptr6 = null;
		if ((_gotStateCloseEvent = global::_003CModule_003E.CreateEventW(null, 0, 0, null)) == null)
		{
			throw new COMException("PlayerInterop failed to CreateEvent for close", 0);
		}
		if ((_gotStateUninitializeEvent = global::_003CModule_003E.CreateEventW(null, 0, 0, null)) == null)
		{
			throw new COMException("PlayerInterop failed to CreateEvent for uninit", 0);
		}
		int num = global::_003CModule_003E.WmpCoreInitialize();
		if (num < 0)
		{
			throw new COMException("Playback core initialization failed.", num);
		}
		num = global::_003CModule_003E.CWmpPlayer_GetInstance(&ptr);
		if (num >= 0 && ptr != null)
		{
			_uPlayer = ptr;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)ptr)))((nint)ptr, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._GUID_2f33a725_95cb_4080_adef_93a067a707ba), (void**)(&ptr2));
			if (num >= 0 && ptr2 != null)
			{
				_uTransport = ptr2;
				IMCPlayer* uPlayer = _uPlayer;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)uPlayer)))((nint)uPlayer, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._GUID_58864c93_45f9_4c6d_aa3f_80f6caa08281), (void**)(&ptr3));
				if (num >= 0 && ptr3 != null)
				{
					_uSetUri = ptr3;
					IMCPlayer* uPlayer2 = _uPlayer;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)uPlayer2)))((nint)uPlayer2, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._GUID_102e281e_28ad_4688_aaff_f560f8053d90), (void**)(&ptr4));
					if (num >= 0 && ptr4 != null)
					{
						_uDynamicImage = ptr4;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)ptr4 + 12)))((nint)ptr4, _nDynamicImage);
						if (num < 0)
						{
							throw new COMException("PlayerInterop failed to initialize IMCDynamicImage", num);
						}
						IMCPlayer* uPlayer3 = _uPlayer;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)uPlayer3)))((nint)uPlayer3, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._GUID_f6ba930c_78c3_488c_924d_2d3fc1e8fb70), (void**)(&ptr5));
						if (num >= 0 && ptr5 != null)
						{
							_uVolumeControl = ptr5;
							IMCPlayer* uPlayer4 = _uPlayer;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)uPlayer4)))((nint)uPlayer4, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._GUID_aff1732d_13f3_45e5_a52f_a854729e8730), (void**)(&ptr6));
							if (num >= 0 && ptr6 != null)
							{
								_uZuneSpectrumMgr = ptr6;
								CPlayerInteropEventSink* ptr7 = (CPlayerInteropEventSink*)global::_003CModule_003E.@new(24u);
								CPlayerInteropEventSink* ptr8;
								try
								{
									ptr8 = ((ptr7 == null) ? null : global::_003CModule_003E.MicrosoftZunePlayback_002ECPlayerInteropEventSink_002E_007Bctor_007D(ptr7, this));
								}
								catch
								{
									//try-fault
									global::_003CModule_003E.delete(ptr7);
									throw;
								}
								_uEventSink = ptr8;
								if (ptr8 == null)
								{
									throw new OutOfMemoryException();
								}
								int num2 = *(int*)_uPlayer + 12;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HWND__*, uint, IMCPlayerEvents*, int>)(int)(*(uint*)num2))((nint)_uPlayer, (HWND__*)_windowHandle.ToPointer(), 0u, (IMCPlayerEvents*)ptr8);
								if (num < 0)
								{
									throw new COMException("PlayerInterop failed to initialize IMCPlayer", num);
								}
								CPlayerInteropEventSink* uEventSink = _uEventSink;
								CPlayerInteropEventSink* ptr9 = (CPlayerInteropEventSink*)((uEventSink == null) ? null : ((byte*)uEventSink + 4));
								IMCTransport* uTransport = _uTransport;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IMCTransportEvents*, int>)(int)(*(uint*)(*(int*)uTransport + 12)))((nint)uTransport, (IMCTransportEvents*)ptr9);
								if (num < 0)
								{
									throw new COMException("PlayerInterop failed to initialize IMCTransport", num);
								}
								CPlayerInteropEventSink* uEventSink2 = _uEventSink;
								CPlayerInteropEventSink* ptr10 = (CPlayerInteropEventSink*)((uEventSink2 == null) ? null : ((byte*)uEventSink2 + 8));
								IMCPlayerSetUri* uSetUri = _uSetUri;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IMCPlayerSetUriEvents*, int>)(int)(*(uint*)(*(int*)uSetUri + 12)))((nint)uSetUri, (IMCPlayerSetUriEvents*)ptr10);
								if (num < 0)
								{
									throw new COMException("PlayerInterop failed to initialize IMCPlayerSetUri", num);
								}
								int num3 = *(int*)_uTransport + 48;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)num3))((nint)_uTransport, (uint)_positionEventInterval.TotalMilliseconds);
								if (num < 0 && num != -2147467263)
								{
									throw new COMException("Attempt to set initial position event firing interval failed.", num);
								}
								_properties = new Dictionary<string, object>();
								return;
							}
							throw new COMException("PlayerInterop failed to get IZuneSpectrumMgr instance.", num);
						}
						throw new COMException("PlayerInterop failed to get IMCVolumeControl instance.", num);
					}
					throw new COMException("PlayerInterop failed to get IMCDynamicImage instance.", num);
				}
				throw new COMException("PlayerInterop failed to get IMCTransport instance.", num);
			}
			throw new COMException("PlayerInterop failed to get IMCTransport instance.", num);
		}
		throw new COMException("PlayerInterop failed to get IMCPlayer instance.", num);
	}

	public unsafe void Uninitialize()
	{
		bool flag = false;
		if (_uPlayer != null)
		{
			_fShuttingDown = true;
			IMCPlayer* uPlayer = _uPlayer;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)uPlayer + 20)))((nint)uPlayer);
			global::_003CModule_003E.WaitForSingleObject(_gotStateCloseEvent, 5000u);
			IMCPlayer* uPlayer2 = _uPlayer;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)uPlayer2 + 16)))((nint)uPlayer2);
			if (global::_003CModule_003E.WaitForSingleObject(_gotStateUninitializeEvent, 5000u) != 0 || _state != MCPlayerState.Uninitialized)
			{
				global::_003CModule_003E.ZuneLibraryExports_002EShipAssert(80000u, 0u, null);
			}
			if (_state != MCPlayerState.Uninitialized)
			{
				flag = true;
			}
			IMCPlayer* uPlayer3 = _uPlayer;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)uPlayer3 + 32)))((nint)uPlayer3);
		}
		global::_003CModule_003E.CloseHandle(_gotStateCloseEvent);
		_gotStateCloseEvent = null;
		global::_003CModule_003E.CloseHandle(_gotStateUninitializeEvent);
		_gotStateUninitializeEvent = null;
		if (!flag)
		{
			CPlayerInteropEventSink* uEventSink = _uEventSink;
			if (null != uEventSink)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)uEventSink + 8)))((nint)uEventSink);
				_uEventSink = null;
			}
			IMCPlayer* uPlayer4 = _uPlayer;
			if (null != uPlayer4)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)uPlayer4 + 8)))((nint)uPlayer4);
				_uPlayer = null;
			}
			IMCTransport* uTransport = _uTransport;
			if (null != uTransport)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)uTransport + 8)))((nint)uTransport);
				_uTransport = null;
			}
			IMCPlayerSetUri* uSetUri = _uSetUri;
			if (null != uSetUri)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)uSetUri + 8)))((nint)uSetUri);
				_uSetUri = null;
			}
			IMCDynamicImage* uDynamicImage = _uDynamicImage;
			if (null != uDynamicImage)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)uDynamicImage + 8)))((nint)uDynamicImage);
				_uDynamicImage = null;
			}
			IMCVolumeControl* uVolumeControl = _uVolumeControl;
			if (null != uVolumeControl)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)uVolumeControl + 8)))((nint)uVolumeControl);
				_uVolumeControl = null;
			}
			IZuneSpectrumMgr* uZuneSpectrumMgr = _uZuneSpectrumMgr;
			if (null != uZuneSpectrumMgr)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)uZuneSpectrumMgr + 8)))((nint)uZuneSpectrumMgr);
				_uZuneSpectrumMgr = null;
			}
			global::_003CModule_003E.WmpCoreDeinitialize();
		}
	}

	public unsafe void Close()
	{
		IMCPlayer* uPlayer = _uPlayer;
		if (uPlayer != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)uPlayer + 20)))((nint)uPlayer);
		}
	}

	[SpecialName]
	protected void raise_StatusChanged(object value0, EventArgs value1)
	{
		_003Cbacking_store_003EStatusChanged?.Invoke(value0, value1);
	}

	[SpecialName]
	protected void raise_AlertSent(Announcement value0)
	{
		_003Cbacking_store_003EAlertSent?.Invoke(value0);
	}

	public unsafe void Play()
	{
		IMCVolumeControl* uVolumeControl = _uVolumeControl;
		if (uVolumeControl != null && _uTransport != null)
		{
			bool mute = _mute;
			if (mute)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte, int>)(int)(*(uint*)(*(int*)uVolumeControl + 16)))((nint)uVolumeControl, mute ? ((byte)1) : ((byte)0));
			}
			else
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)uVolumeControl + 12)))((nint)uVolumeControl, _volume);
			}
			IMCTransport* uTransport = _uTransport;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)uTransport + 20)))((nint)uTransport);
		}
	}

	public unsafe void Pause()
	{
		IMCTransport* uTransport = _uTransport;
		if (uTransport != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)uTransport + 16)))((nint)uTransport);
		}
	}

	public unsafe void Stop()
	{
		IMCTransport* uTransport = _uTransport;
		if (uTransport != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)uTransport + 24)))((nint)uTransport);
		}
	}

	public unsafe void SeekToRelativePosition(long offsetIn100nsUnits)
	{
		IMCTransport* uTransport = _uTransport;
		if (uTransport != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, long, int>)(int)(*(uint*)(*(int*)uTransport + 28)))((nint)uTransport, offsetIn100nsUnits);
		}
	}

	public unsafe void SeekToAbsolutePosition(long offsetIn100nsUnits)
	{
		IMCTransport* uTransport = _uTransport;
		if (uTransport != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, long, int>)(int)(*(uint*)(*(int*)uTransport + 32)))((nint)uTransport, offsetIn100nsUnits);
		}
	}

	public unsafe void SeekToRelativeFrame(long offsetInFrames)
	{
		IMCTransport* uTransport = _uTransport;
		if (uTransport != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, long, int>)(int)(*(uint*)(*(int*)uTransport + 36)))((nint)uTransport, offsetInFrames);
		}
	}

	[SpecialName]
	protected void raise_TransportStatusChanged(object value0, EventArgs value1)
	{
		_003Cbacking_store_003ETransportStatusChanged?.Invoke(value0, value1);
	}

	[SpecialName]
	protected void raise_TransportPositionChanged(object value0, EventArgs value1)
	{
		_003Cbacking_store_003ETransportPositionChanged?.Invoke(value0, value1);
	}

	public unsafe void SetUri(string uri, long startPositionIn100nsUnits, int UriID)
	{
		if (!(uri != null))
		{
			return;
		}
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(uri)))
		{
			try
			{
				IMCPlayerSetUri* uSetUri = _uSetUri;
				if (uSetUri != null)
				{
					int num = *(int*)uSetUri + 16;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, long, uint, int>)(int)(*(uint*)num))((nint)_uSetUri, ptr, startPositionIn100nsUnits, (uint)UriID);
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

	public unsafe void SetNextUri(string uri, long startPositionIn100nsUnits, int UriID)
	{
		if (!(uri != null))
		{
			return;
		}
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(uri)))
		{
			try
			{
				IMCPlayerSetUri* uSetUri = _uSetUri;
				if (uSetUri != null)
				{
					int num = *(int*)uSetUri + 20;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, long, uint, int>)(int)(*(uint*)num))((nint)_uSetUri, ptr, startPositionIn100nsUnits, (uint)UriID);
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

	public unsafe void CancelNext()
	{
		IMCPlayerSetUri* uSetUri = _uSetUri;
		if (uSetUri != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)uSetUri + 24)))((nint)uSetUri);
		}
	}

	[SpecialName]
	protected void raise_UriSet(object value0, EventArgs value1)
	{
		_003Cbacking_store_003EUriSet?.Invoke(value0, value1);
	}

	public unsafe void ConnectAnimationsToSpectrumAnalyzer(uint uixAnimationsId, uint bandCount, [MarshalAs(UnmanagedType.U1)] bool outputFrequencyData, [MarshalAs(UnmanagedType.U1)] bool outputWaveformData, [MarshalAs(UnmanagedType.U1)] bool enableStereoOutput)
	{
		IZuneSpectrumMgr* uZuneSpectrumMgr = _uZuneSpectrumMgr;
		if (uZuneSpectrumMgr != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint, byte, byte, byte, int>)(int)(*(uint*)(*(int*)uZuneSpectrumMgr + 12)))((nint)uZuneSpectrumMgr, uixAnimationsId, bandCount, outputFrequencyData ? ((byte)1) : ((byte)0), outputWaveformData ? ((byte)1) : ((byte)0), enableStereoOutput ? ((byte)1) : ((byte)0));
		}
	}

	public unsafe void DisconnectAnimationsFromSpectrumAnalyzer(uint uixAnimationsId)
	{
		IZuneSpectrumMgr* uZuneSpectrumMgr = _uZuneSpectrumMgr;
		if (uZuneSpectrumMgr != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)(*(int*)uZuneSpectrumMgr + 16)))((nint)uZuneSpectrumMgr, uixAnimationsId);
		}
	}

	public unsafe void ProgressivePlaybackReleaseFile()
	{
		if (_uTransport == null)
		{
			return;
		}
		CPlayerInteropEventSink* uEventSink = _uEventSink;
		if (uEventSink != null && ((int*)uEventSink)[5] == 0)
		{
			((int*)_uEventSink)[5] = (int)global::_003CModule_003E.CreateEventW(null, 0, 0, null);
			if (((int*)_uEventSink)[5] == 0)
			{
				throw new COMException("PlayerInterop failed to CreateEvent for ProgressivePlaybackReleaseFile", 0);
			}
			IMCTransport* uTransport = _uTransport;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)uTransport + 52)))((nint)uTransport);
			if (global::_003CModule_003E.WaitForSingleObject((void*)(int)((uint*)_uEventSink)[5], 30000u) != 0)
			{
				global::_003CModule_003E.ZuneLibraryExports_002EShipAssert(80001u, 0u, null);
			}
		}
	}

	public unsafe void ProgressivePlaybackReopenFile()
	{
		IMCTransport* uTransport = _uTransport;
		if (uTransport != null)
		{
			CPlayerInteropEventSink* uEventSink = _uEventSink;
			if (uEventSink != null && ((int*)uEventSink)[5] != 0)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)uTransport + 56)))((nint)uTransport);
				global::_003CModule_003E.CloseHandle((void*)(int)((uint*)_uEventSink)[5]);
				((int*)_uEventSink)[5] = 0;
			}
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_007EPlayerInterop();
		}
		else
		{
			base.Finalize();
		}
	}

	public virtual sealed void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
