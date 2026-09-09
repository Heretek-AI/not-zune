using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MicrosoftZuneLibrary;

namespace Microsoft.Zune.Messaging;

public class MessagingService : IDisposable
{
	private static MessagingService m_singletonInstance = null;

	private unsafe IZuneNetMessaging* m_pMessaging = null;

	public static bool HasInstance
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return m_singletonInstance != null;
		}
	}

	public static MessagingService Instance
	{
		get
		{
			if (m_singletonInstance == null)
			{
				m_singletonInstance = new MessagingService();
			}
			return m_singletonInstance;
		}
	}

	private unsafe MessagingService()
	{
		IZuneNetMessaging* pMessaging = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bf368f0d_4743_439c_9142_e487c9534104, (void**)(&pMessaging)) >= 0)
		{
			m_pMessaging = pMessaging;
		}
	}

	private void _007EMessagingService()
	{
		_0021MessagingService();
	}

	private unsafe void _0021MessagingService()
	{
		IZuneNetMessaging* pMessaging = m_pMessaging;
		if (pMessaging != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pMessaging + 8)))((nint)pMessaging);
			m_pMessaging = null;
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool MessageSetRead(string strMessageUrl)
	{
		bool result = false;
		if (m_pMessaging != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strMessageUrl)))
			{
				try
				{
					int num = *(int*)m_pMessaging + 12;
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num))((nint)m_pMessaging, ptr) >= 0;
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool MessageDelete(string strMessageUrl)
	{
		bool result = false;
		if (m_pMessaging != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strMessageUrl)))
			{
				try
				{
					int num = *(int*)m_pMessaging + 16;
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num))((nint)m_pMessaging, ptr) >= 0;
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool AcceptFriend(string strPostUrl)
	{
		bool result = false;
		if (m_pMessaging != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strPostUrl)))
			{
				try
				{
					int num = *(int*)m_pMessaging + 20;
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num))((nint)m_pMessaging, ptr) >= 0;
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool RejectFriend(string strPostUrl)
	{
		bool result = false;
		if (m_pMessaging != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strPostUrl)))
			{
				try
				{
					int num = *(int*)m_pMessaging + 24;
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num))((nint)m_pMessaging, ptr) >= 0;
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool ManageFriend(FriendAction eAction, string strPostUrl, string strZuneTag)
	{
		bool result = false;
		if (m_pMessaging != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strPostUrl)))
			{
				try
				{
					fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strZuneTag)))
					{
						try
						{
							int num = *(int*)m_pMessaging + 28;
							result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EFriendAction, ushort*, ushort*, int>)(int)(*(uint*)num))((nint)m_pMessaging, (EFriendAction)eAction, ptr, ptr2) >= 0;
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
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool AddComment(string strPostUrl, string strZuneTag, string strMessage, CommentCallback callback)
	{
		bool result = false;
		if (m_pMessaging != null)
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIMessagingCallback_003E cComPtrNtv_003CIMessagingCallback_003E);
			*(int*)(&cComPtrNtv_003CIMessagingCallback_003E) = 0;
			try
			{
				int num;
				if (callback != null)
				{
					num = global::_003CModule_003E.Microsoft_002EZune_002EMessaging_002EAddCommentCallbackWrapper_002ECreateInstance(callback, (IMessagingCallback**)(&cComPtrNtv_003CIMessagingCallback_003E));
					if (num < 0)
					{
						goto IL_007e;
					}
				}
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strPostUrl)))
				{
					try
					{
						fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strZuneTag)))
						{
							try
							{
								fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strMessage)))
								{
									try
									{
										int num2 = *(int*)m_pMessaging + 56;
										num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, ushort*, IMessagingCallback*, int>)(int)(*(uint*)num2))((nint)m_pMessaging, ptr, ptr2, ptr3, (IMessagingCallback*)(int)(*(uint*)(&cComPtrNtv_003CIMessagingCallback_003E)));
									}
									catch
									{
										//try-fault
										ptr3 = null;
										throw;
									}
								}
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
				if (num < 0)
				{
					goto IL_007e;
				}
				int num3 = 1;
				goto IL_0081;
				IL_007e:
				num3 = 0;
				goto IL_0081;
				IL_0081:
				result = (byte)num3 != 0;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIMessagingCallback_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIMessagingCallback_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIMessagingCallback_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIMessagingCallback_003E_002ERelease(&cComPtrNtv_003CIMessagingCallback_003E);
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool DeleteComment(string strPostUrl, string strZuneTag, MessagingCallback callback)
	{
		bool result = false;
		if (m_pMessaging != null)
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIMessagingCallback_003E cComPtrNtv_003CIMessagingCallback_003E);
			*(int*)(&cComPtrNtv_003CIMessagingCallback_003E) = 0;
			try
			{
				int num;
				if (callback != null)
				{
					num = global::_003CModule_003E.Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002ECreateInstance(callback, null, (IMessagingCallback**)(&cComPtrNtv_003CIMessagingCallback_003E));
					if (num < 0)
					{
						goto IL_006d;
					}
				}
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strPostUrl)))
				{
					try
					{
						fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strZuneTag)))
						{
							try
							{
								int num2 = *(int*)m_pMessaging + 60;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, IMessagingCallback*, int>)(int)(*(uint*)num2))((nint)m_pMessaging, ptr, ptr2, (IMessagingCallback*)(int)(*(uint*)(&cComPtrNtv_003CIMessagingCallback_003E)));
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
				if (num < 0)
				{
					goto IL_006d;
				}
				int num3 = 1;
				goto IL_006f;
				IL_006d:
				num3 = 0;
				goto IL_006f;
				IL_006f:
				result = (byte)num3 != 0;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIMessagingCallback_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIMessagingCallback_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIMessagingCallback_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIMessagingCallback_003E_002ERelease(&cComPtrNtv_003CIMessagingCallback_003E);
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool Compose(string strPostUrl, string strMessage, string strRecipientZuneTags, string strRequestType, IPropertySetMessageData messageData, MessagingCallback callback, object state)
	{
		bool result = false;
		IMessagingCallback* ptr2;
		int num3;
		if (messageData != null && m_pMessaging != null)
		{
			IMSMediaSchemaPropertySet* ptr = null;
			int num = messageData.GetPropertySet(&ptr);
			ptr2 = null;
			if (num >= 0)
			{
				if (callback != null)
				{
					num = global::_003CModule_003E.Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002ECreateInstance(callback, state, &ptr2);
				}
				if (num >= 0)
				{
					fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strPostUrl)))
					{
						try
						{
							fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strMessage)))
							{
								try
								{
									fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strRecipientZuneTags)))
									{
										try
										{
											fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strRequestType)))
											{
												try
												{
													int num2 = *(int*)m_pMessaging + 32;
													num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, ushort*, ushort*, IMSMediaSchemaPropertySet*, IMessagingCallback*, int>)(int)(*(uint*)num2))((nint)m_pMessaging, ptr3, ptr4, ptr5, ptr6, ptr, ptr2);
												}
												catch
												{
													//try-fault
													ptr6 = null;
													throw;
												}
											}
										}
										catch
										{
											//try-fault
											ptr5 = null;
											throw;
										}
									}
								}
								catch
								{
									//try-fault
									ptr4 = null;
									throw;
								}
							}
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
						num3 = 1;
						goto IL_00b2;
					}
				}
			}
			num3 = 0;
			goto IL_00b2;
		}
		goto IL_00c8;
		IL_00c8:
		return result;
		IL_00b2:
		result = (byte)num3 != 0;
		if (ptr2 != null)
		{
			IMessagingCallback* intPtr = ptr2;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		goto IL_00c8;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool Compose(string strPostUrl, string strMessage, MessagingCallback callback, object state)
	{
		bool result = false;
		if (m_pMessaging != null)
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIMessagingCallback_003E cComPtrNtv_003CIMessagingCallback_003E);
			*(int*)(&cComPtrNtv_003CIMessagingCallback_003E) = 0;
			try
			{
				int num;
				if (callback != null)
				{
					num = global::_003CModule_003E.Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002ECreateInstance(callback, state, (IMessagingCallback**)(&cComPtrNtv_003CIMessagingCallback_003E));
					if (num < 0)
					{
						goto IL_006e;
					}
				}
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strPostUrl)))
				{
					try
					{
						fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strMessage)))
						{
							try
							{
								int num2 = *(int*)m_pMessaging + 36;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, IMessagingCallback*, int>)(int)(*(uint*)num2))((nint)m_pMessaging, ptr, ptr2, (IMessagingCallback*)(int)(*(uint*)(&cComPtrNtv_003CIMessagingCallback_003E)));
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
				if (num < 0)
				{
					goto IL_006e;
				}
				int num3 = 1;
				goto IL_0070;
				IL_006e:
				num3 = 0;
				goto IL_0070;
				IL_0070:
				result = (byte)num3 != 0;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIMessagingCallback_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIMessagingCallback_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIMessagingCallback_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIMessagingCallback_003E_002ERelease(&cComPtrNtv_003CIMessagingCallback_003E);
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool ManageFavorites(FavoritesAction eAction, string strFavoritesUrl, string strInstructions, MessagingCallback callback, object state)
	{
		bool result = false;
		IMessagingCallback* ptr;
		int num3;
		if (m_pMessaging != null)
		{
			ptr = null;
			int num;
			if (callback != null)
			{
				num = global::_003CModule_003E.Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002ECreateInstance(callback, state, &ptr);
				if (num < 0)
				{
					goto IL_006e;
				}
			}
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strFavoritesUrl)))
			{
				try
				{
					fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strInstructions)))
					{
						try
						{
							int num2 = *(int*)m_pMessaging + 40;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EFavoritesAction, ushort*, ushort*, IMessagingCallback*, int>)(int)(*(uint*)num2))((nint)m_pMessaging, (EFavoritesAction)eAction, ptr2, ptr3, ptr);
						}
						catch
						{
							//try-fault
							ptr3 = null;
							throw;
						}
					}
				}
				catch
				{
					//try-fault
					ptr2 = null;
					throw;
				}
			}
			if (num < 0)
			{
				goto IL_006e;
			}
			num3 = 1;
			goto IL_0071;
		}
		goto IL_0085;
		IL_0071:
		result = (byte)num3 != 0;
		if (ptr != null)
		{
			IMessagingCallback* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		goto IL_0085;
		IL_0085:
		return result;
		IL_006e:
		num3 = 0;
		goto IL_0071;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool ManageProfile(string strProfileUrl, string strFieldValue, MessagingCallback callback, object state)
	{
		bool result = false;
		IMessagingCallback* ptr;
		int num3;
		if (m_pMessaging != null)
		{
			ptr = null;
			int num;
			if (callback != null)
			{
				num = global::_003CModule_003E.Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002ECreateInstance(callback, state, &ptr);
				if (num < 0)
				{
					goto IL_006b;
				}
			}
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strProfileUrl)))
			{
				try
				{
					fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strFieldValue)))
					{
						try
						{
							int num2 = *(int*)m_pMessaging + 44;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, IMessagingCallback*, int>)(int)(*(uint*)num2))((nint)m_pMessaging, ptr2, ptr3, ptr);
						}
						catch
						{
							//try-fault
							ptr3 = null;
							throw;
						}
					}
				}
				catch
				{
					//try-fault
					ptr2 = null;
					throw;
				}
			}
			if (num < 0)
			{
				goto IL_006b;
			}
			num3 = 1;
			goto IL_006e;
		}
		goto IL_0082;
		IL_006e:
		result = (byte)num3 != 0;
		if (ptr != null)
		{
			IMessagingCallback* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		goto IL_0082;
		IL_0082:
		return result;
		IL_006b:
		num3 = 0;
		goto IL_006e;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool ManageProfileImage(string strProfileImageUrl, string strProfileImageResource, MessagingCallback callback, object state)
	{
		bool result = false;
		if (m_pMessaging != null && strProfileImageResource != null && strProfileImageUrl != null)
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIMessagingCallback_003E cComPtrNtv_003CIMessagingCallback_003E);
			*(int*)(&cComPtrNtv_003CIMessagingCallback_003E) = 0;
			try
			{
				int num;
				if (callback != null)
				{
					num = global::_003CModule_003E.Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002ECreateInstance(callback, state, (IMessagingCallback**)(&cComPtrNtv_003CIMessagingCallback_003E));
					if (num < 0)
					{
						goto IL_0086;
					}
				}
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strProfileImageUrl)))
				{
					try
					{
						fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strProfileImageResource)))
						{
							try
							{
								int num2 = *(int*)m_pMessaging + 48;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, IMessagingCallback*, int>)(int)(*(uint*)num2))((nint)m_pMessaging, ptr, ptr2, (IMessagingCallback*)(int)(*(uint*)(&cComPtrNtv_003CIMessagingCallback_003E)));
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
				if (num < 0)
				{
					goto IL_0086;
				}
				int num3 = 1;
				goto IL_0088;
				IL_0086:
				num3 = 0;
				goto IL_0088;
				IL_0088:
				result = (byte)num3 != 0;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIMessagingCallback_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIMessagingCallback_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIMessagingCallback_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIMessagingCallback_003E_002ERelease(&cComPtrNtv_003CIMessagingCallback_003E);
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool ManageProfileImage(string strProfileImageUrl, SafeBitmap profileImage, MessagingCallback callback, object state)
	{
		bool result = false;
		if (m_pMessaging != null && profileImage != null && strProfileImageUrl != null)
		{
			int num = 0;
			Unsafe.SkipInit(out CComPtrNtv_003CIMessagingCallback_003E cComPtrNtv_003CIMessagingCallback_003E);
			*(int*)(&cComPtrNtv_003CIMessagingCallback_003E) = 0;
			try
			{
				if (callback != null)
				{
					num = global::_003CModule_003E.Microsoft_002EZune_002EMessaging_002EMessagingCallbackWrapper_002ECreateInstance(callback, state, (IMessagingCallback**)(&cComPtrNtv_003CIMessagingCallback_003E));
				}
				bool success = false;
				int num3;
				if (num >= 0)
				{
					profileImage.DangerousAddRef(ref success);
					if (!success)
					{
						num = -2147467259;
					}
					if (num >= 0)
					{
						fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strProfileImageUrl)))
						{
							try
							{
								HBITMAP__* ptr2 = (HBITMAP__*)(int)profileImage.DangerousGetHandle();
								int num2 = *(int*)m_pMessaging + 52;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, HBITMAP__*, IMessagingCallback*, int>)(int)(*(uint*)num2))((nint)m_pMessaging, ptr, ptr2, (IMessagingCallback*)(int)(*(uint*)(&cComPtrNtv_003CIMessagingCallback_003E)));
							}
							catch
							{
								//try-fault
								ptr = null;
								throw;
							}
						}
					}
					if (success)
					{
						profileImage.DangerousRelease();
					}
					if (num >= 0)
					{
						num3 = 1;
						goto IL_00a4;
					}
				}
				num3 = 0;
				goto IL_00a4;
				IL_00a4:
				result = (byte)num3 != 0;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIMessagingCallback_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIMessagingCallback_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIMessagingCallback_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIMessagingCallback_003E_002ERelease(&cComPtrNtv_003CIMessagingCallback_003E);
		}
		return result;
	}

	public unsafe string GetInboxPhotoUrl(string title, string collectionName)
	{
		string result = null;
		if (m_pMessaging != null)
		{
			ushort* ptr = null;
			int num = 0;
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(title)))
			{
				try
				{
					fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(collectionName)))
					{
						try
						{
							int num2 = *(int*)m_pMessaging + 76;
							if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, ushort**, int*, int>)(int)(*(uint*)num2))((nint)m_pMessaging, ptr2, ptr3, &ptr, &num) >= 0 && num != 0)
							{
								result = new string((char*)ptr);
							}
							if (ptr != null)
							{
								global::_003CModule_003E.SysFreeString(ptr);
							}
						}
						catch
						{
							//try-fault
							ptr3 = null;
							throw;
						}
					}
				}
				catch
				{
					//try-fault
					ptr2 = null;
					throw;
				}
			}
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool AddInboxPhoto(string title, string collectionName, string localFilePath)
	{
		bool result = false;
		if (m_pMessaging != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(title)))
			{
				try
				{
					fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(collectionName)))
					{
						try
						{
							fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(localFilePath)))
							{
								try
								{
									int num = *(int*)m_pMessaging + 72;
									result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, ushort*, int>)(int)(*(uint*)num))((nint)m_pMessaging, ptr, ptr2, ptr3) >= 0;
								}
								catch
								{
									//try-fault
									ptr3 = null;
									throw;
								}
							}
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
		}
		return result;
	}

	public unsafe int GetInboxDownloadFolderId(string collectionName)
	{
		int result = 0;
		if (m_pMessaging != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(collectionName)))
			{
				try
				{
					int num = *(int*)m_pMessaging + 80;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int*, int>)(int)(*(uint*)num))((nint)m_pMessaging, ptr, &result) < 0)
					{
						result = 0;
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
		return result;
	}

	public unsafe void InitiateUploadDeviceCartItems()
	{
		IZuneNetMessaging* pMessaging = m_pMessaging;
		if (pMessaging != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IProgressNotify*, int>)(int)(*(uint*)(*(int*)pMessaging + 68)))((nint)pMessaging, null);
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021MessagingService();
			return;
		}
		try
		{
			_0021MessagingService();
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

	~MessagingService()
	{
		Dispose(false);
	}
}
