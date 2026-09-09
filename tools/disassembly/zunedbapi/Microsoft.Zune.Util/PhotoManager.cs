using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ZuneUI;

namespace Microsoft.Zune.Util;

public class PhotoManager
{
	private static PhotoManager sm_PhotoManager = null;

	private static object sm_lock = new object();

	public static PhotoManager Instance
	{
		get
		{
			if (sm_PhotoManager == null)
			{
				try
				{
					Monitor.Enter(sm_lock);
					if (sm_PhotoManager == null)
					{
						PhotoManager photoManager = new PhotoManager();
						Thread.MemoryBarrier();
						sm_PhotoManager = photoManager;
					}
				}
				finally
				{
					Monitor.Exit(sm_lock);
				}
			}
			return sm_PhotoManager;
		}
	}

	public unsafe HRESULT SetWindowHandle(IntPtr hWnd)
	{
		int num = 0;
		IMetadataManager* ptr = null;
		try
		{
			num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71, (void**)(&ptr));
			if (num >= 0)
			{
				int num2 = *(int*)ptr + 220;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HWND__*, int>)(int)(*(uint*)num2))((nint)ptr, (HWND__*)hWnd.ToPointer());
			}
		}
		finally
		{
			if (null != ptr)
			{
				IMetadataManager* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr = null;
			}
		}
		return new HRESULT(num);
	}

	public unsafe HRESULT FindFolder(string szFolderName, out int nFolderId)
	{
		int num = 1;
		nFolderId = -1;
		IMetadataManager* ptr = null;
		IFolderProvider* ptr2 = null;
		try
		{
			num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71, (void**)(&ptr));
			if (num >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, void**, int>)(int)(*(uint*)(*(int*)ptr + 12)))((nint)ptr, (_GUID)global::_003CModule_003E._GUID_a2889317_d0c7_41d8_abc7_1eb4cb8d46d6, (void**)(&ptr2));
			}
			Unsafe.SkipInit(out tagVARIANT tagVARIANT2);
			*(short*)(&tagVARIANT2) = 0;
			// IL initblk instruction
			Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref tagVARIANT2, 2), 0, 14);
			if (num >= 0)
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(szFolderName)))
				{
					try
					{
						int num2 = *(int*)ptr2 + 44;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, EMediaTypes, tagVARIANT*, int>)(int)(*(uint*)num2))((nint)ptr2, ptr3, EMediaTypes.eMediaTypeImage, &tagVARIANT2);
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
					nFolderId = Unsafe.As<tagVARIANT, int>(ref Unsafe.AddByteOffset(ref tagVARIANT2, 8));
				}
			}
		}
		finally
		{
			if (null != ptr2)
			{
				IFolderProvider* intPtr = ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr2 = null;
			}
			if (null != ptr)
			{
				IMetadataManager* intPtr2 = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				ptr = null;
			}
		}
		return new HRESULT(num);
	}

	public unsafe HRESULT FindPhotoContainer([In] int nMediaId, out int nFolderId)
	{
		int num = 1;
		nFolderId = -1;
		IMetadataManager* ptr = null;
		IFileProvider* ptr2 = null;
		try
		{
			num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71, (void**)(&ptr));
			if (num >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, void**, int>)(int)(*(uint*)(*(int*)ptr + 12)))((nint)ptr, (_GUID)global::_003CModule_003E._GUID_16a9f8be_e76c_4391_ad74_8df74b7a3c21, (void**)(&ptr2));
			}
			Unsafe.SkipInit(out tagVARIANT tagVARIANT2);
			*(short*)(&tagVARIANT2) = 0;
			// IL initblk instruction
			Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref tagVARIANT2, 2), 0, 14);
			if (num >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, EMediaTypes, tagVARIANT*, int>)(int)(*(uint*)(*(int*)ptr2 + 264)))((nint)ptr2, nMediaId, EMediaTypes.eMediaTypeImage, &tagVARIANT2);
				if (num >= 0)
				{
					nFolderId = Unsafe.As<tagVARIANT, int>(ref Unsafe.AddByteOffset(ref tagVARIANT2, 8));
				}
			}
		}
		finally
		{
			if (null != ptr2)
			{
				IFileProvider* intPtr = ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr2 = null;
			}
			if (null != ptr)
			{
				IMetadataManager* intPtr2 = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				ptr = null;
			}
		}
		return new HRESULT(num);
	}

	public unsafe HRESULT CreateFolder(string szFolderName, int nParentFolderId, out int nCreatedFolderId)
	{
		int num = 1;
		nCreatedFolderId = -1;
		if (string.IsNullOrEmpty(szFolderName))
		{
			return HRESULT._DB_E_BADPARAMETERNAME;
		}
		IMetadataManager* ptr = null;
		IFolderProvider* ptr2 = null;
		try
		{
			num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71, (void**)(&ptr));
			if (num >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, void**, int>)(int)(*(uint*)(*(int*)ptr + 12)))((nint)ptr, (_GUID)global::_003CModule_003E._GUID_a2889317_d0c7_41d8_abc7_1eb4cb8d46d6, (void**)(&ptr2));
			}
			Unsafe.SkipInit(out tagVARIANT tagVARIANT2);
			*(short*)(&tagVARIANT2) = 0;
			// IL initblk instruction
			Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref tagVARIANT2, 2), 0, 14);
			if (num >= 0)
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(szFolderName)))
				{
					try
					{
						int num2 = *(int*)ptr2 + 112;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int, tagVARIANT*, int>)(int)(*(uint*)num2))((nint)ptr2, ptr3, nParentFolderId, &tagVARIANT2);
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
					nCreatedFolderId = Unsafe.As<tagVARIANT, int>(ref Unsafe.AddByteOffset(ref tagVARIANT2, 8));
				}
			}
		}
		finally
		{
			if (null != ptr2)
			{
				IFolderProvider* intPtr = ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr2 = null;
			}
			if (null != ptr)
			{
				IMetadataManager* intPtr2 = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				ptr = null;
			}
		}
		return new HRESULT(num);
	}

	public unsafe HRESULT RenameFolder(int nFolderId, string szNewFolderName)
	{
		int num = 1;
		if (string.IsNullOrEmpty(szNewFolderName))
		{
			return HRESULT._DB_E_BADPARAMETERNAME;
		}
		IMetadataManager* ptr = null;
		IFolderProvider* ptr2 = null;
		try
		{
			num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71, (void**)(&ptr));
			if (num >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, void**, int>)(int)(*(uint*)(*(int*)ptr + 12)))((nint)ptr, (_GUID)global::_003CModule_003E._GUID_a2889317_d0c7_41d8_abc7_1eb4cb8d46d6, (void**)(&ptr2));
				if (num >= 0)
				{
					fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(szNewFolderName)))
					{
						try
						{
							int num2 = *(int*)ptr2 + 116;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, ushort*, byte, int>)(int)(*(uint*)num2))((nint)ptr2, nFolderId, ptr3, 0);
						}
						catch
						{
							//try-fault
							ptr3 = null;
							throw;
						}
					}
				}
			}
		}
		finally
		{
			if (null != ptr2)
			{
				IFolderProvider* intPtr = ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr2 = null;
			}
			if (null != ptr)
			{
				IMetadataManager* intPtr2 = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				ptr = null;
			}
		}
		return new HRESULT(num);
	}

	public unsafe HRESULT Move(int[] mediaIds, EMediaTypes mediaType, int nDestinationFolderId)
	{
		if (mediaIds == null)
		{
			return HRESULT._DB_E_BADPARAMETERNAME;
		}
		if (mediaIds.Length == 0)
		{
			return HRESULT._S_OK;
		}
		int num = 1;
		IMetadataManager* ptr = null;
		IFolderProvider* ptr2 = null;
		try
		{
			num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71, (void**)(&ptr));
			if (num >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, void**, int>)(int)(*(uint*)(*(int*)ptr + 12)))((nint)ptr, (_GUID)global::_003CModule_003E._GUID_a2889317_d0c7_41d8_abc7_1eb4cb8d46d6, (void**)(&ptr2));
				if (num >= 0)
				{
					fixed (int* ptr3 = &mediaIds[0])
					{
						try
						{
							int* ptr4 = ptr3;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaTypes, int*, int, int, int>)(int)(*(uint*)(*(int*)ptr2 + 128)))((nint)ptr2, mediaType, ptr4, mediaIds.Length, nDestinationFolderId);
						}
						catch
						{
							//try-fault
							ptr3 = null;
							throw;
						}
					}
				}
			}
		}
		finally
		{
			if (null != ptr2)
			{
				IFolderProvider* intPtr = ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr2 = null;
			}
			if (null != ptr)
			{
				IMetadataManager* intPtr2 = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				ptr = null;
			}
		}
		return new HRESULT(num);
	}

	public unsafe HRESULT Import(string szPath, EMediaTypes mediaType, int nDestinationFolderId)
	{
		if (string.IsNullOrEmpty(szPath))
		{
			return HRESULT._DB_E_BADPARAMETERNAME;
		}
		int num = 1;
		IMetadataManager* ptr = null;
		IFolderProvider* ptr2 = null;
		try
		{
			num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71, (void**)(&ptr));
			if (num >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, void**, int>)(int)(*(uint*)(*(int*)ptr + 12)))((nint)ptr, (_GUID)global::_003CModule_003E._GUID_a2889317_d0c7_41d8_abc7_1eb4cb8d46d6, (void**)(&ptr2));
				if (num >= 0)
				{
					fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(szPath)))
					{
						try
						{
							int num2 = *(int*)ptr2 + 132;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, EMediaTypes, ushort*, int>)(int)(*(uint*)num2))((nint)ptr2, nDestinationFolderId, mediaType, ptr3);
						}
						catch
						{
							//try-fault
							ptr3 = null;
							throw;
						}
					}
				}
			}
		}
		finally
		{
			if (null != ptr2)
			{
				IFolderProvider* intPtr = ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr2 = null;
			}
			if (null != ptr)
			{
				IMetadataManager* intPtr2 = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				ptr = null;
			}
		}
		return new HRESULT(num);
	}
}
