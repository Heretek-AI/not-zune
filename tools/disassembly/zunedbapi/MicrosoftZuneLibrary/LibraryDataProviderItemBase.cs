using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Iris;

namespace MicrosoftZuneLibrary;

public class LibraryDataProviderItemBase : DataProviderObject, IDatabaseMedia
{
	protected uint m_uTraceId;

	private static Stack m_thumbnailsToProcess = new Stack();

	private static bool m_thumbnailWorkerAlive = false;

	private static object m_lockObject = new object();

	private AsyncGetThumbnailState[] m_fThumbnailRequested;

	private Image[] m_thumbnail;

	private long[] m_thumbnailSize;

	private Dictionary<string, object> m_setProperties;

	private bool m_fSlowDataThumbnailExtraction;

	private bool m_fHasGottenSlowDataRequest;

	private static int TinyThumbnailSize = 38;

	private static int SmallThumbnailSize = 86;

	private static int LargeThumbnailSize = 159;

	private static int SuperLargeThumbnailSize = 240;

	private static int NowPlayingThumbnailSize = 200;

	private static int c_MaxThumbnails = 3;

	private static int[] s_thumbnailSizes = new int[3] { 86, 159, 240 };

	internal unsafe LibraryDataProviderItemBase(DataProviderQuery query, object typeCookie)
		: base(query, typeCookie)
	{
		m_uTraceId = global::_003CModule_003E.MicrosoftZuneLibrary_002E_003FA0xb7c00b43_002Es_uNextTraceId;
		global::_003CModule_003E.MicrosoftZuneLibrary_002E_003FA0xb7c00b43_002Es_uNextTraceId++;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 10, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
	}

	public unsafe override object GetProperty(string propertyName)
	{
		object obj = null;
		DataProviderMapping value = null;
		Dictionary<string, object> setProperties = m_setProperties;
		if (setProperties != null)
		{
			obj = null;
			if (setProperties.TryGetValue(propertyName, out obj))
			{
				return obj;
			}
		}
		if (((DataProviderObject)this).Mappings.TryGetValue(propertyName, out value))
		{
			if (!string.IsNullOrEmpty(value.Source))
			{
				if (value.Source == "ThumbnailPath")
				{
					throw new NotImplementedException();
				}
				int num = ThumbnailSizeIndexFromSizeName(value.Source, -1);
				if (num >= 0)
				{
					EnsureThumbnailStorage();
					Image[] thumbnail = m_thumbnail;
					if (thumbnail[num] != null)
					{
						if ((((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
						{
							fixed (ushort* a = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(thumbnail[num].Source)))
							{
								try
								{
									if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
									{
										global::_003CModule_003E.WPP_SF_DdS(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 14, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId, num, a);
									}
								}
								catch
								{
									//try-fault
									a = null;
									throw;
								}
							}
						}
						return m_thumbnail[num];
					}
					if (num != 0 || !m_fSlowDataThumbnailExtraction)
					{
						BeginGetThumbnail(propertyName, num, slowDataQuery: false);
					}
					return null;
				}
				return GetFieldValue(value.PropertyType, value.Source, value.DefaultValue);
			}
			return value.DefaultValue;
		}
		return null;
	}

	public override void SetProperty(string propertyName, object value)
	{
		DataProviderMapping value2 = null;
		if (!((DataProviderObject)this).Mappings.TryGetValue(propertyName, out value2) || string.IsNullOrEmpty(value2.Source))
		{
			return;
		}
		if (value2.Source == "ThumbnailPath")
		{
			string newThumbnail = (string)value;
			SetNewThumbnail(newThumbnail);
			return;
		}
		if (object.Equals(value, GetProperty(propertyName)))
		{
			return;
		}
		if (!(((DataProviderObject)this).TypeName == "Playlist") || !(value2.Source == "Title"))
		{
			int num = SetFieldValue(value2.Source, value);
			if (num < 0)
			{
				global::_003CModule_003E.SQMAddNumbersToStream("IgnoredErrorEvent", 1u, (uint)num);
				goto IL_00eb;
			}
			if (value2.Source == "UserRating")
			{
				DateTime utcNow = DateTime.UtcNow;
				SetFieldValue("UserLastRatedDate", utcNow);
			}
		}
		if (m_setProperties == null)
		{
			m_setProperties = new Dictionary<string, object>();
		}
		m_setProperties[propertyName] = value;
		goto IL_00eb;
		IL_00eb:
		((DataProviderObject)this).FirePropertyChanged(propertyName);
	}

	public virtual void GetMediaIdAndType(out int mediaId, out EMediaTypes mediaType)
	{
		if (((DataProviderObject)this).TypeName == "PlaylistContentItem")
		{
			mediaId = (int)GetProperty("MediaId");
			mediaType = (EMediaTypes)GetProperty("MediaType");
		}
		else if (((DataProviderObject)this).TypeName == "SyncItem")
		{
			mediaId = (int)GetProperty("LibraryId");
			mediaType = (EMediaTypes)GetProperty("MediaType");
		}
		else
		{
			mediaId = (int)GetProperty("LibraryId");
			mediaType = LibraryDataProvider.NameToMediaType(((DataProviderObject)this).TypeName);
		}
	}

	public unsafe static string GetArtUrl(int MediaId, string typeName, [MarshalAs(UnmanagedType.U1)] bool fCacheOnly)
	{
		ushort* ptr = null;
		EMediaTypes eMediaTypes = LibraryDataProvider.NameToMediaType(typeName);
		if (global::_003CModule_003E.ZuneLibraryExports_002ELocateArt(MediaId, eMediaTypes, fCacheOnly, &ptr) >= 0)
		{
			string result = new string((char*)ptr);
			global::_003CModule_003E.SysFreeString(ptr);
			return result;
		}
		return null;
	}

	public unsafe virtual void InvalidateAllProperties()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 55, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
		foreach (DataProviderMapping value in ((DataProviderObject)this).Mappings.Values)
		{
			((DataProviderObject)this).FirePropertyChanged(value.PropertyName);
		}
		m_setProperties = null;
		if (HasThumbnailArt() && (((DataProviderObject)this).TypeName == "Album" || ((DataProviderObject)this).TypeName == "Artist" || ((DataProviderObject)this).TypeName == "App" || ((DataProviderObject)this).TypeName == "Photo"))
		{
			CheckThumbnail();
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 56, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
	}

	public void SetSlowDataThumbnailExtraction([MarshalAs(UnmanagedType.U1)] bool useSlowData)
	{
		m_fSlowDataThumbnailExtraction = useSlowData;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	protected unsafe bool SetNewThumbnail(string strThumbnailPath)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 28, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
		int num = -2147418113;
		if (((DataProviderObject)this).TypeName == "Album")
		{
			int num2 = (int)GetFieldValue(typeof(int), 355u, -1);
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strThumbnailPath)))
			{
				try
				{
					num = global::_003CModule_003E.ZuneLibraryExports_002ESetAlbumArt(num2, ptr);
					if (num < 0)
					{
						global::_003CModule_003E.SQMAddNumbersToStream("IgnoredErrorEvent", 1u, (uint)num);
					}
					SetSlowDataThumbnailExtraction(useSlowData: false);
					InvalidateAllProperties();
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_Dd(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 29, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId, num);
		}
		return num >= 0;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool SetNewThumbnail(SafeBitmap safeBitmap)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 30, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
		int num = -2147418113;
		if (((DataProviderObject)this).TypeName == "Album" && safeBitmap != null)
		{
			int num2 = (int)GetFieldValue(typeof(int), 355u, -1);
			void* ptr = (void*)safeBitmap.DangerousGetHandle();
			num = global::_003CModule_003E.ZuneLibraryExports_002ESetAlbumArt(num2, (HBITMAP__*)ptr);
			if (num < 0)
			{
				global::_003CModule_003E.SQMAddNumbersToStream("IgnoredErrorEvent", 1u, (uint)num);
			}
			SetSlowDataThumbnailExtraction(useSlowData: false);
			InvalidateAllProperties();
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 31, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
		return num >= 0;
	}

	protected internal unsafe virtual void OnRequestSlowData()
	{
		DataProviderMapping value = null;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 18, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
		int thumbnailIndex = 0;
		_ = string.Empty;
		string typeName = ((DataProviderObject)this).TypeName;
		m_fHasGottenSlowDataRequest = true;
		string text;
		switch (typeName)
		{
		case "Album":
			text = "AlbumArtSmall";
			break;
		default:
			text = string.Empty;
			break;
		case "Photo":
		case "Video":
		case "MediaFolder":
		case "App":
			text = "Thumbnail";
			break;
		}
		if (!string.IsNullOrEmpty(text))
		{
			if (((DataProviderObject)this).Mappings.TryGetValue(text, out value) && !string.IsNullOrEmpty(value.Source))
			{
				thumbnailIndex = ThumbnailSizeIndexFromSizeName(value.Source, 0);
			}
			if (BeginGetThumbnail(text, thumbnailIndex, slowDataQuery: true))
			{
				goto IL_00f7;
			}
		}
		NotifySlowDataAcquireComplete();
		goto IL_00f7;
		IL_00f7:
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 19, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	protected unsafe bool BeginGetThumbnail(string thumbnailPropertyName, int thumbnailIndex, [MarshalAs(UnmanagedType.U1)] bool slowDataQuery)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 23, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
		EnsureThumbnailStorage();
		bool flag = false;
		if (m_thumbnail[thumbnailIndex] == null || ((DataProviderObject)this).TypeName == "Album")
		{
			try
			{
				Monitor.Enter(m_lockObject);
				AsyncGetThumbnailState asyncGetThumbnailState = m_fThumbnailRequested[thumbnailIndex];
				if (asyncGetThumbnailState == null)
				{
					if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
					{
						global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 24, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
						int num = 1;
					}
					else
					{
						int num = 0;
					}
					asyncGetThumbnailState = new AsyncGetThumbnailState(this);
					asyncGetThumbnailState.thumbnailPropertyName = thumbnailPropertyName;
					asyncGetThumbnailState.thumbnailIndex = thumbnailIndex;
					asyncGetThumbnailState.slowDataQuery = slowDataQuery;
					asyncGetThumbnailState.antialiasImageEdges = AntialiasImageEdges();
					asyncGetThumbnailState.MediaId = (int)GetFieldValue(typeof(int), 355u, -1);
					m_fThumbnailRequested[thumbnailIndex] = asyncGetThumbnailState;
					flag = true;
					goto IL_0181;
				}
				bool isComplete = asyncGetThumbnailState.isComplete;
				flag = !isComplete;
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
				{
					global::_003CModule_003E.WPP_SF_Dl(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 25, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId, isComplete ? 1 : 0);
					int num2 = 1;
				}
				else
				{
					int num2 = 0;
				}
				if (flag)
				{
					goto IL_0181;
				}
				goto end_IL_0069;
				IL_0181:
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
				{
					global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 26, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
					int num3 = 1;
				}
				else
				{
					int num3 = 0;
				}
				m_thumbnailsToProcess.Push(asyncGetThumbnailState);
				if (!m_thumbnailWorkerAlive)
				{
					m_thumbnailWorkerAlive = true;
					ThreadPool.QueueUserWorkItem(GetThumbnailOnWorkerThread, null);
				}
				end_IL_0069:;
			}
			finally
			{
				Monitor.Exit(m_lockObject);
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 27, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
		return flag;
	}

	protected unsafe static void GetThumbnailOnWorkerThread(object listItem)
	{
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Expected O, but got Unknown
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Expected O, but got Unknown
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Expected O, but got Unknown
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 34, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids));
		}
		bool flag = true;
		while (flag)
		{
			AsyncGetThumbnailState asyncGetThumbnailState = null;
			bool flag2 = true;
			try
			{
				Monitor.Enter(m_lockObject);
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
				{
					global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 35, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_thumbnailsToProcess.Count);
					int num = 1;
				}
				else
				{
					int num = 0;
				}
				if (m_thumbnailsToProcess.Count > 0)
				{
					listItem = m_thumbnailsToProcess.Pop();
					asyncGetThumbnailState = (AsyncGetThumbnailState)listItem;
					if (asyncGetThumbnailState != null)
					{
						flag2 = asyncGetThumbnailState.isComplete;
						if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
						{
							global::_003CModule_003E.WPP_SF_Dll(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 36, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), asyncGetThumbnailState.listItem.m_uTraceId, flag2 ? 1 : 0, asyncGetThumbnailState.slowDataQuery ? 1 : 0);
							int num2 = 1;
						}
						else
						{
							int num2 = 0;
						}
						asyncGetThumbnailState.isComplete = true;
					}
				}
				else
				{
					m_thumbnailWorkerAlive = false;
					flag = false;
					flag2 = true;
					if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
					{
						global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 37, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids));
						int num3 = 1;
					}
					else
					{
						int num4 = 0;
					}
				}
			}
			finally
			{
				Monitor.Exit(m_lockObject);
			}
			if (asyncGetThumbnailState == null)
			{
				continue;
			}
			if (flag2)
			{
				if (!asyncGetThumbnailState.slowDataQuery)
				{
					continue;
				}
				LibraryDataProviderItemBase listItem2 = asyncGetThumbnailState.listItem;
				if (listItem2 != null)
				{
					if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
					{
						global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 38, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), listItem2.m_uTraceId);
					}
					Application.DeferredInvoke(new DeferredInvokeHandler(asyncGetThumbnailState.listItem.NotifySlowDataAcquireCompleteDelegate), (object)null);
				}
				continue;
			}
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
			{
				global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 39, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), asyncGetThumbnailState.listItem.m_uTraceId);
			}
			LibraryDataProviderItemBase listItem3 = asyncGetThumbnailState.listItem;
			asyncGetThumbnailState.thumbnail = null;
			if (((DataProviderObject)listItem3).TypeName == "Album" || ((DataProviderObject)listItem3).TypeName == "Artist" || ((DataProviderObject)listItem3).TypeName == "App" || ((DataProviderObject)listItem3).TypeName == "MediaFolder" || ((DataProviderObject)listItem3).TypeName == "Photo" || ((DataProviderObject)listItem3).TypeName == "Video")
			{
				string artUrl = GetArtUrl(asyncGetThumbnailState.MediaId, ((DataProviderObject)listItem3).TypeName, fCacheOnly: false);
				if (!string.IsNullOrEmpty(artUrl))
				{
					if (!listItem3.ArtNeedsUpdating(artUrl, asyncGetThumbnailState.thumbnailIndex))
					{
						if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
						{
							global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 40, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), listItem3.m_uTraceId);
						}
						continue;
					}
					if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
					{
						global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 41, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), listItem3.m_uTraceId);
					}
					int num5 = s_thumbnailSizes[asyncGetThumbnailState.thumbnailIndex];
					asyncGetThumbnailState.thumbnail = new Image("file://" + artUrl, num5, num5, false, asyncGetThumbnailState.antialiasImageEdges);
				}
			}
			Application.DeferredInvoke(new DeferredInvokeHandler(listItem3.UpdateThumbnail), (object)asyncGetThumbnailState);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 42, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids));
		}
	}

	protected unsafe virtual void UpdateThumbnail(object args)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 43, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids));
		}
		AsyncGetThumbnailState asyncGetThumbnailState = (AsyncGetThumbnailState)args;
		if (asyncGetThumbnailState.slowDataQuery)
		{
			NotifySlowDataAcquireComplete();
		}
		uint a = (uint)(((int?)asyncGetThumbnailState.listItem?.m_uTraceId) ?? (-1));
		if (asyncGetThumbnailState.thumbnail == null)
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
			{
				global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 44, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), a);
			}
			string text = ThumbnailFallbackImageUrl();
			if (!string.IsNullOrEmpty(text))
			{
				asyncGetThumbnailState.thumbnail = new Image(text);
			}
		}
		int thumbnailIndex = asyncGetThumbnailState.thumbnailIndex;
		if (m_thumbnail[thumbnailIndex] != asyncGetThumbnailState.thumbnail)
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
			{
				global::_003CModule_003E.WPP_SF_Dd(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 45, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), a, thumbnailIndex);
			}
			m_thumbnail[asyncGetThumbnailState.thumbnailIndex] = asyncGetThumbnailState.thumbnail;
			if (asyncGetThumbnailState.thumbnailPropertyName != null)
			{
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
				{
					global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 46, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), a);
				}
				((DataProviderObject)this).FirePropertyChanged(asyncGetThumbnailState.thumbnailPropertyName);
			}
		}
		((IDisposable)asyncGetThumbnailState).Dispose();
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 47, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids));
		}
	}

	protected void CheckThumbnail()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		string artUrl = GetArtUrl((int)GetFieldValue(typeof(int), 355u, -1), ((DataProviderObject)this).TypeName, fCacheOnly: true);
		if (!string.IsNullOrEmpty(artUrl) && ArtNeedsUpdating(artUrl))
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(ClearThumbnailCacheOnAppThread), (object)null);
		}
		else if (string.IsNullOrEmpty(artUrl) && ((DataProviderObject)this).TypeName == "Photo")
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(ClearThumbnailCacheOnAppThread), (object)null);
		}
	}

	protected unsafe void ClearThumbnailCacheOnAppThread(object stateObj)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 53, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
		if (((DataProviderObject)this).TypeName == "Album")
		{
			ClearThumbnailCache();
			((DataProviderObject)this).FirePropertyChanged("AlbumArtSmall");
		}
		else if (((DataProviderObject)this).TypeName == "Photo")
		{
			ClearThumbnailCache();
			((DataProviderObject)this).FirePropertyChanged("Thumbnail");
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 54, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
	}

	protected virtual object GetFieldValue(Type type, uint atom)
	{
		return null;
	}

	protected virtual object GetFieldValue(Type type, string atomName, object defaultValue)
	{
		return null;
	}

	protected virtual object GetFieldValue(Type type, uint atom, object defaultValue)
	{
		return null;
	}

	protected virtual int SetFieldValue(string atomName, object value)
	{
		return -2147467263;
	}

	protected virtual int SetFieldValue(uint atom, object value)
	{
		return -2147467263;
	}

	protected virtual void NotifySlowDataAcquireComplete()
	{
	}

	protected void NotifySlowDataAcquireCompleteDelegate(object P_0)
	{
		NotifySlowDataAcquireComplete();
	}

	[return: MarshalAs(UnmanagedType.U1)]
	protected virtual bool AntialiasImageEdges()
	{
		return false;
	}

	protected virtual string ThumbnailFallbackImageUrl()
	{
		return null;
	}

	private unsafe void EnsureThumbnailStorage()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 15, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
		if (m_thumbnail == null)
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
			{
				global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 16, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
			}
			m_fThumbnailRequested = new AsyncGetThumbnailState[3];
			m_thumbnail = (Image[])(object)new Image[3];
			m_thumbnailSize = new long[3];
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 17, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
	}

	private unsafe void ClearThumbnailCache()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 32, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
		if (m_thumbnail != null)
		{
			string text = null;
			bool flag = AntialiasImageEdges();
			int num = 0;
			if (0 < (nint)s_thumbnailSizes.LongLength)
			{
				do
				{
					if (m_thumbnail[num] != null)
					{
						int num2 = s_thumbnailSizes[num];
						Image.RemoveCache(m_thumbnail[num].Source, num2, num2, false, flag);
						text = m_thumbnail[num].Source;
						m_thumbnail[num] = null;
					}
					m_fThumbnailRequested[num] = null;
					m_thumbnailSize[num] = 0L;
					num++;
				}
				while (num < (nint)s_thumbnailSizes.LongLength);
			}
			if (text != null)
			{
				Image.RemoveCache(text, 200, 200, false, flag);
				Image.RemoveCache(text, 38, 38, false, flag);
			}
			if (m_fHasGottenSlowDataRequest)
			{
				SetSlowDataThumbnailExtraction(useSlowData: false);
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 33, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	private bool HasThumbnailArt()
	{
		int num = ((((DataProviderObject)this).TypeName != "Album" || (bool)GetFieldValue(typeof(bool), 191u, false)) ? 1 : 0);
		return (byte)num != 0;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	private unsafe bool ArtNeedsUpdating(string strThumbnailPath, int thumbnailIndex)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 51, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
		bool flag = false;
		if (m_thumbnailSize != null)
		{
			FileInfo fileInfo = new FileInfo(strThumbnailPath);
			if (fileInfo.Exists)
			{
				try
				{
					EnsureThumbnailStorage();
					long length = fileInfo.Length;
					if (thumbnailIndex != -1)
					{
						long[] thumbnailSize = m_thumbnailSize;
						long num = thumbnailSize[thumbnailIndex];
						if (num == 0 || length != num)
						{
							thumbnailSize[thumbnailIndex] = length;
							flag = true;
						}
					}
					else
					{
						flag = true;
						for (int i = 0; i < 3; i++)
						{
							long num2 = m_thumbnailSize[i];
							if (num2 != 0 && length == num2)
							{
								flag = false;
								break;
							}
						}
					}
				}
				catch (IOException)
				{
					int num3 = 0;
					do
					{
						m_thumbnailSize[num3] = 0L;
						num3++;
					}
					while (num3 < 3);
					flag = true;
				}
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_Dl(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 52, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId, flag ? 1 : 0);
		}
		return flag;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	private bool ArtNeedsUpdating(string strThumbnailPath)
	{
		return ArtNeedsUpdating(strThumbnailPath, -1);
	}

	private static int ThumbnailSizeIndexFromSizeName(string thumbnailSizeName, int defaultSizeIndex)
	{
		switch (thumbnailSizeName)
		{
		case "LargeThumbnail":
			return 1;
		case "SuperLargeThumbnail":
			return 2;
		default:
			return defaultSizeIndex;
		case "SmallThumbnail":
		case "Thumbnail":
			return 0;
		}
	}
}
