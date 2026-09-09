using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;

namespace Microsoft.Zune.Configuration;

public class CConfigurationManagedBase : IDisposable
{
	private string m_basePath;

	private string m_instance;

	private object m_lock;

	private unsafe HKEY__* m_hHive;

	private ConfigurationChangeEventHandler m_configurationChangeEventHandler;

	private unsafe NotificationMarshaller* m_pNotificationMarshaller;

	public unsafe string ConfigurationAbsolutePath
	{
		get
		{
			IConfigurationManager* ptr = null;
			int configurationManagerInstance = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr);
			if (configurationManagerInstance >= 0)
			{
				IConfigurationManager* intPtr = ptr;
				return string.Concat(new string((char*)((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, ushort*>)(int)(*(uint*)(*(int*)intPtr + 96)))((nint)intPtr)) + "\\", ConfigurationPath);
			}
			throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(configurationManagerInstance));
		}
	}

	public string ConfigurationPath => (!(m_basePath == null)) ? string.Concat(m_basePath + "\\", m_instance) : m_instance;

	[SpecialName]
	public unsafe event ConfigurationChangeEventHandler OnConfigurationChanged
	{
		add
		{
			try
			{
				Monitor.Enter(m_lock);
				if (null == m_configurationChangeEventHandler)
				{
					Subscribe(OnNativeConfigChangedCallback);
				}
				m_configurationChangeEventHandler = (ConfigurationChangeEventHandler)Delegate.Combine(m_configurationChangeEventHandler, value);
			}
			finally
			{
				Monitor.Exit(m_lock);
			}
		}
		remove
		{
			try
			{
				Monitor.Enter(m_lock);
				if (null == (m_configurationChangeEventHandler = (ConfigurationChangeEventHandler)Delegate.Remove(m_configurationChangeEventHandler, value)))
				{
					Unsubscribe();
				}
			}
			finally
			{
				Monitor.Exit(m_lock);
			}
		}
	}

	public unsafe CConfigurationManagedBase(RegistryHive hive, string basePath, string instance)
	{
		m_basePath = basePath;
		m_instance = instance;
		m_pNotificationMarshaller = null;
		base._002Ector();
		switch (hive)
		{
		default:
			throw new ArgumentException("hive");
		case RegistryHive.LocalMachine:
			m_hHive = (HKEY__*)(-2147483646);
			break;
		case RegistryHive.CurrentUser:
			m_hHive = (HKEY__*)(-2147483647);
			break;
		}
		m_lock = new object();
	}

	private void _007ECConfigurationManagedBase()
	{
		_0021CConfigurationManagedBase();
	}

	private unsafe void _0021CConfigurationManagedBase()
	{
		NotificationMarshaller* pNotificationMarshaller = m_pNotificationMarshaller;
		if (pNotificationMarshaller != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pNotificationMarshaller + 8)))((nint)pNotificationMarshaller);
			m_pNotificationMarshaller = null;
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool GetBoolProperty(string propertyName, [MarshalAs(UnmanagedType.U1)] bool defaultValue)
	{
		int num = 0;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num2 = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					if (num2 >= 0)
					{
						int num3 = *(int*)ptr4 + 4;
						num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, int*, int, int>)(int)(*(uint*)num3))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, &num, defaultValue ? 1 : 0);
						if (num2 >= 0)
						{
							return (num != 0) ? true : false;
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num2));
				}
			}
		}
	}

	public unsafe void SetBoolProperty(string propertyName, [MarshalAs(UnmanagedType.U1)] bool value)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					if (num >= 0)
					{
						int num2 = *(int*)ptr4 + 8;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, int, int>)(int)(*(uint*)num2))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, value ? 1 : 0);
						if (num >= 0)
						{
							return;
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
				}
			}
		}
	}

	public unsafe int GetIntProperty(string propertyName, int defaultValue)
	{
		int result = 0;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					if (num >= 0)
					{
						int num2 = *(int*)ptr4 + 16;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, int*, int, int>)(int)(*(uint*)num2))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, &result, defaultValue);
						if (num >= 0)
						{
							return result;
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
				}
			}
		}
	}

	public unsafe void SetIntProperty(string propertyName, int value)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					if (num >= 0)
					{
						int num2 = *(int*)ptr4 + 20;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, int, int>)(int)(*(uint*)num2))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, value);
						if (num >= 0)
						{
							return;
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
				}
			}
		}
	}

	public unsafe long GetInt64Property(string propertyName, long defaultValue)
	{
		long result = 0L;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					if (num >= 0)
					{
						int num2 = *(int*)ptr4 + 28;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, long*, long, int>)(int)(*(uint*)num2))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, &result, defaultValue);
						if (num >= 0)
						{
							return result;
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
				}
			}
		}
	}

	public unsafe void SetInt64Property(string propertyName, long value)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					if (num >= 0)
					{
						int num2 = *(int*)ptr4 + 32;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, long, int>)(int)(*(uint*)num2))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, value);
						if (num >= 0)
						{
							return;
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
				}
			}
		}
	}

	public unsafe double GetDoubleProperty(string propertyName, double defaultValue)
	{
		double result = 0.0;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					if (num >= 0)
					{
						int num2 = *(int*)ptr4 + 40;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, double*, double, int>)(int)(*(uint*)num2))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, &result, defaultValue);
						if (num >= 0)
						{
							return result;
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
				}
			}
		}
	}

	public unsafe void SetDoubleProperty(string propertyName, double value)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					if (num >= 0)
					{
						int num2 = *(int*)ptr4 + 44;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, double, int>)(int)(*(uint*)num2))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, value);
						if (num >= 0)
						{
							return;
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
				}
			}
		}
	}

	public unsafe DateTime GetDateTimeProperty(string propertyName, DateTime defaultValue)
	{
		DateTime dateTime = default(DateTime);
		Unsafe.SkipInit(out _FILETIME fILETIME);
		*(int*)(&fILETIME) = 0;
		// IL initblk instruction
		Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref fILETIME, 4), 0, 4);
		Unsafe.SkipInit(out _FILETIME fILETIME2);
		Unsafe.WriteUnaligned(ref *(byte*)(&fILETIME2), defaultValue.ToFileTimeUtc());
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					if (num >= 0)
					{
						int num2 = *(int*)ptr4 + 52;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, _FILETIME*, _FILETIME, int>)(int)(*(uint*)num2))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, &fILETIME, fILETIME2);
						if (num >= 0)
						{
							return DateTime.FromFileTimeUtc(Unsafe.ReadUnaligned<long>(ref *(byte*)(&fILETIME)));
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
				}
			}
		}
	}

	public unsafe void SetDateTimeProperty(string propertyName, DateTime value)
	{
		Unsafe.SkipInit(out _FILETIME fILETIME);
		Unsafe.WriteUnaligned(ref *(byte*)(&fILETIME), value.ToFileTimeUtc());
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					if (num >= 0)
					{
						int num2 = *(int*)ptr4 + 56;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, _FILETIME, int>)(int)(*(uint*)num2))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, fILETIME);
						if (num >= 0)
						{
							return;
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
				}
			}
		}
	}

	public unsafe string GetStringProperty(string propertyName, string defaultValue)
	{
		string result = null;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(defaultValue)))
					{
						IConfigurationManager* ptr5 = null;
						int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr5);
						uint num2 = 0u;
						if (num >= 0)
						{
							int num3 = *(int*)ptr5 + 64;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, ushort*, uint*, ushort*, int>)(int)(*(uint*)num3))((nint)ptr5, m_hHive, ptr, ptr2, ptr3, null, &num2, ptr4);
							if (num >= 0)
							{
								ushort* ptr6 = (ushort*)global::_003CModule_003E.new_005B_005D((num2 > int.MaxValue) ? uint.MaxValue : (num2 << 1));
								if (ptr6 == null)
								{
									num = -2147024882;
								}
								if (num >= 0)
								{
									int num4 = *(int*)ptr5 + 64;
									num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, ushort*, uint*, ushort*, int>)(int)(*(uint*)num4))((nint)ptr5, m_hHive, ptr, ptr2, ptr3, ptr6, &num2, ptr4);
									if (num >= 0)
									{
										result = new string((char*)ptr6);
									}
								}
								if (ptr6 != null)
								{
									global::_003CModule_003E.delete_005B_005D(ptr6);
								}
								if (num >= 0)
								{
									return result;
								}
							}
						}
						throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
					}
				}
			}
		}
	}

	public unsafe void SetStringProperty(string propertyName, string value)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(value)))
					{
						IConfigurationManager* ptr5 = null;
						int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr5);
						if (num >= 0)
						{
							int num2 = *(int*)ptr5 + 68;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, ushort*, int>)(int)(*(uint*)num2))((nint)ptr5, m_hHive, ptr, ptr2, ptr3, ptr4);
							if (num >= 0)
							{
								return;
							}
						}
						throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
					}
				}
			}
		}
	}

	public unsafe IList<string> GetStringListProperty(string propertyName)
	{
		List<string> list = null;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					ushort* ptr5 = null;
					uint num2 = 0u;
					if (num >= 0)
					{
						int num3 = *(int*)ptr4 + 72;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, ushort*, uint*, int>)(int)(*(uint*)num3))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, null, &num2);
						if (num >= 0)
						{
							ptr5 = (ushort*)global::_003CModule_003E.new_005B_005D((num2 > int.MaxValue) ? uint.MaxValue : (num2 << 1));
							if (ptr5 == null)
							{
								num = -2147024882;
							}
							if (num >= 0)
							{
								int num4 = *(int*)ptr4 + 72;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, ushort*, uint*, int>)(int)(*(uint*)num4))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, ptr5, &num2);
								if (num >= 0)
								{
									list = new List<string>();
									ushort* ptr6 = ptr5;
									if (*ptr5 != 0)
									{
										do
										{
											string item = new string((char*)ptr6);
											list.Add(item);
											ptr6 = (ushort*)(global::_003CModule_003E.lstrlenW(ptr6) * 2 + (byte*)ptr6) + 1;
										}
										while (*ptr6 != 0);
									}
									goto IL_00e7;
								}
							}
						}
					}
					if (num == -2147024894)
					{
						num = 0;
						list = null;
					}
					goto IL_00e7;
					IL_00e7:
					if (ptr5 != null)
					{
						global::_003CModule_003E.delete_005B_005D(ptr5);
					}
					if (num < 0)
					{
						throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
					}
					return list;
				}
			}
		}
	}

	public unsafe void SetStringListProperty(string propertyName, IList<string> value)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					uint num2 = 0u;
					if (num >= 0)
					{
						int num3 = 0;
						if (0 < value.Count)
						{
							do
							{
								num2 = (uint)(value[num3].Length + (int)num2 + 1);
								num3++;
							}
							while (num3 < value.Count);
						}
						num2++;
						ushort* ptr5 = (ushort*)global::_003CModule_003E.new_005B_005D((num2 > int.MaxValue) ? uint.MaxValue : (num2 << 1));
						if (ptr5 == null)
						{
							num = -2147024882;
						}
						else
						{
							// IL initblk instruction
							Unsafe.InitBlock(ptr5, 0, num2 << 1);
							ushort* ptr6 = ptr5;
							int num4 = 0;
							if (0 < value.Count)
							{
								do
								{
									int length = value[num4].Length;
									fixed (ushort* s = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(value[num4])))
									{
										try
										{
											if (global::_003CModule_003E.wmemcpy_s(ptr6, num2, s, (uint)length) != 0)
											{
												goto end_IL_00bd;
											}
											num2 = (uint)(-1 - length) + num2;
											ptr6 = (ushort*)(length * 2 + (byte*)ptr6) + 1;
											goto end_IL_00bd_2;
											end_IL_00bd:;
										}
										catch
										{
											//try-fault
											s = null;
											throw;
										}
										try
										{
											num = -2147418113;
										}
										catch
										{
											//try-fault
											s = null;
											throw;
										}
										break;
										end_IL_00bd_2:;
									}
									num4++;
								}
								while (num4 < value.Count);
							}
							if (num >= 0)
							{
								int num5 = *(int*)ptr4 + 76;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, ushort*, int>)(int)(*(uint*)num5))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, ptr5);
							}
						}
						if (ptr5 != null)
						{
							global::_003CModule_003E.delete_005B_005D(ptr5);
						}
						if (num >= 0)
						{
							return;
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
				}
			}
		}
	}

	public unsafe byte[] GetBinaryProperty(string propertyName)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					IConfigurationManager* ptr4 = null;
					int num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr4);
					uint num2 = 0u;
					if (num >= 0)
					{
						int num3 = *(int*)ptr4 + 80;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, byte*, uint*, int>)(int)(*(uint*)num3))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, null, &num2);
						if (num >= 0)
						{
							byte[] array = new byte[num2];
							fixed (byte* ptr5 = &array[0])
							{
								try
								{
									int num4 = *(int*)ptr4 + 80;
									num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, byte*, uint*, int>)(int)(*(uint*)num4))((nint)ptr4, m_hHive, ptr, ptr2, ptr3, ptr5, &num2);
								}
								catch
								{
									//try-fault
									ptr5 = null;
									throw;
								}
							}
							if (num >= 0)
							{
								return array;
							}
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
				}
			}
		}
	}

	public unsafe void SetBinaryProperty(string propertyName, byte[] value)
	{
		//The blocks IL_0030, IL_003e, IL_0060 are reachable both inside and outside the pinned region starting at IL_002f. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
				{
					/*pinned*/ref byte reference = ref *(byte*)null;
					IConfigurationManager* ptr5;
					int num;
					if (value.Length != 0)
					{
						fixed (byte* ptr4 = &value[0])
						{
							ptr5 = null;
							num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr5);
							if (num >= 0)
							{
								int num2 = *(int*)ptr5 + 84;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, byte*, uint, int>)(int)(*(uint*)num2))((nint)ptr5, m_hHive, ptr, ptr2, ptr3, ptr4, (uint)value.Length);
								if (num >= 0)
								{
									return;
								}
							}
							throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
						}
					}
					ptr5 = null;
					num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr5);
					if (num >= 0)
					{
						int num2 = *(int*)ptr5 + 84;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, ushort*, byte*, uint, int>)(int)(*(uint*)num2))((nint)ptr5, m_hHive, ptr, ptr2, ptr3, (byte*)Unsafe.AsPointer(ref reference), (uint)value.Length);
						if (num >= 0)
						{
							return;
						}
					}
					throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
				}
			}
		}
	}

	[SpecialName]
	public void raise_OnConfigurationChanged(object sender, ConfigurationChangeEventArgs args)
	{
		ConfigurationChangeEventHandler configurationChangeEventHandler = m_configurationChangeEventHandler;
		if (configurationChangeEventHandler != null)
		{
			configurationChangeEventHandler(sender, args);
		}
	}

	private unsafe void OnNativeConfigChangedCallback(ushort* pwszPropertyName)
	{
		string propertyName = new string((char*)pwszPropertyName);
		raise_OnConfigurationChanged(this, new ConfigurationChangeEventArgs(propertyName));
	}

	private unsafe void Subscribe(NativeConfigurationChangeEventHandler handler)
	{
		int num = 0;
		IConfigurationManager* ptr = null;
		if (m_pNotificationMarshaller != null)
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 1181u);
			num = -2147418113;
		}
		fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				if (num >= 0)
				{
					num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr);
					if (num >= 0)
					{
						NotificationMarshaller* ptr4 = (NotificationMarshaller*)global::_003CModule_003E.@new(12u);
						NotificationMarshaller* ptr5;
						try
						{
							ptr5 = ((ptr4 == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EConfiguration_002ENotificationMarshaller_002E_007Bctor_007D(ptr4, handler));
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.delete(ptr4);
							throw;
						}
						m_pNotificationMarshaller = ptr5;
						if (ptr5 == null)
						{
							num = -2147024882;
						}
						if (num >= 0)
						{
							int num2 = *(int*)ptr + 88;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, INotifySubscriber*, int>)(int)(*(uint*)num2))((nint)ptr, m_hHive, ptr2, ptr3, (INotifySubscriber*)ptr5);
							if (num >= 0)
							{
								return;
							}
						}
					}
				}
				throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
			}
		}
	}

	private unsafe void Unsubscribe()
	{
		int num = 0;
		IConfigurationManager* ptr = null;
		if (m_pNotificationMarshaller == null)
		{
			num = -2147418113;
		}
		fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_basePath)))
		{
			fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_instance)))
			{
				if (num >= 0)
				{
					num = global::_003CModule_003E.GetConfigurationManagerInstance(&ptr);
					if (num >= 0)
					{
						int num2 = *(int*)ptr + 92;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HKEY__*, ushort*, ushort*, INotifySubscriber*, int>)(int)(*(uint*)num2))((nint)ptr, m_hHive, ptr2, ptr3, (INotifySubscriber*)m_pNotificationMarshaller);
						if (num >= 0)
						{
							NotificationMarshaller* pNotificationMarshaller = m_pNotificationMarshaller;
							if (pNotificationMarshaller != null)
							{
								((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pNotificationMarshaller + 8)))((nint)pNotificationMarshaller);
								m_pNotificationMarshaller = null;
							}
							return;
						}
					}
				}
				throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
			}
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021CConfigurationManagedBase();
			return;
		}
		try
		{
			_0021CConfigurationManagedBase();
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

	~CConfigurationManagedBase()
	{
		Dispose(false);
	}
}
