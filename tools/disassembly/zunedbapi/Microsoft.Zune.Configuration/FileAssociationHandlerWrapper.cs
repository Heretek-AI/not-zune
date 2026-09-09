using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Configuration;

internal class FileAssociationHandlerWrapper : IFileAssociationHandler, IDisposable
{
	private unsafe global::IFileAssociationHandler* m_pFileAssociationHandler;

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe virtual bool CanAssociationBeChanged()
	{
		global::IFileAssociationHandler* pFileAssociationHandler = m_pFileAssociationHandler;
		return (byte)((((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int>)(int)(*(uint*)(*(int*)pFileAssociationHandler + 20)))((nint)pFileAssociationHandler) != 0) ? 1u : 0u) != 0;
	}

	public unsafe virtual int GetFileAssociationInfoList(out IList<FileAssociationInfo> fileAssociationInfoList)
	{
		global::FileAssociationInfo* ptr = null;
		uint num = 0u;
		fileAssociationInfoList = new List<FileAssociationInfo>();
		global::IFileAssociationHandler* pFileAssociationHandler = m_pFileAssociationHandler;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, global::FileAssociationInfo*, uint*, int>)(int)(*(uint*)(*(int*)pFileAssociationHandler + 12)))((nint)pFileAssociationHandler, null, &num);
		if (num2 >= 0)
		{
			ptr = (global::FileAssociationInfo*)global::_003CModule_003E.new_005B_005D((num > 214748364) ? uint.MaxValue : (num * 20));
			if (ptr == null)
			{
				num2 = -2147024882;
			}
			else
			{
				// IL initblk instruction
				Unsafe.InitBlock(ptr, 0, num * 20);
				pFileAssociationHandler = m_pFileAssociationHandler;
				num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, global::FileAssociationInfo*, uint*, int>)(int)(*(uint*)(*(int*)pFileAssociationHandler + 12)))((nint)pFileAssociationHandler, ptr, &num);
				if (num2 >= 0)
				{
					uint num3 = 0u;
					if (0 < num)
					{
						global::FileAssociationInfo* ptr2 = (global::FileAssociationInfo*)((byte*)ptr + 8);
						do
						{
							FileAssociationInfo item = new FileAssociationInfo(new string((char*)(int)(*((uint*)ptr2 - 2))), new string((char*)(int)(*((uint*)ptr2 - 1))), new string((char*)(int)(*(uint*)ptr2)), isCurrentlyOwned: (((int*)ptr2)[1] != 0) ? true : false, mediaType: ((EMediaTypes*)ptr2)[2]);
							fileAssociationInfoList.Add(item);
							num3++;
							ptr2 = (global::FileAssociationInfo*)((byte*)ptr2 + 20);
						}
						while (num3 < num);
					}
				}
			}
		}
		CleanupFileInfoArray(ptr, num);
		return num2;
	}

	public unsafe virtual int SetFileAssociationInfo(IList<FileAssociationInfo> fileAssociationInfoList)
	{
		uint count = (uint)fileAssociationInfoList.Count;
		global::FileAssociationInfo* ptr = (global::FileAssociationInfo*)global::_003CModule_003E.new_005B_005D((count > 214748364) ? uint.MaxValue : (count * 20));
		int num;
		if (ptr == null)
		{
			num = -2147024882;
		}
		else
		{
			uint num2 = 0u;
			if (0 >= count)
			{
				goto IL_0053;
			}
			global::FileAssociationInfo* ptr2 = ptr;
			while (true)
			{
				num = FileInfoToStruct(fileAssociationInfoList[(int)num2], ptr2);
				if (num < 0)
				{
					break;
				}
				num2++;
				ptr2 = (global::FileAssociationInfo*)((byte*)ptr2 + 20);
				if (num2 < count)
				{
					continue;
				}
				goto IL_0053;
			}
		}
		goto IL_006c;
		IL_0053:
		global::IFileAssociationHandler* pFileAssociationHandler = m_pFileAssociationHandler;
		num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, global::FileAssociationInfo*, uint, int>)(int)(*(uint*)(*(int*)pFileAssociationHandler + 16)))((nint)pFileAssociationHandler, ptr, count);
		goto IL_006c;
		IL_006c:
		CleanupFileInfoArray(ptr, count);
		return num;
	}

	private unsafe void _007EFileAssociationHandlerWrapper()
	{
		global::IFileAssociationHandler* pFileAssociationHandler = m_pFileAssociationHandler;
		if (pFileAssociationHandler != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pFileAssociationHandler + 8)))((nint)pFileAssociationHandler);
		}
	}

	internal unsafe FileAssociationHandlerWrapper(global::IFileAssociationHandler* pFilessociationManager)
	{
		m_pFileAssociationHandler = pFilessociationManager;
	}

	internal unsafe int FileInfoToStruct(FileAssociationInfo fileAssocInfo, global::FileAssociationInfo* pFileAssocInfo)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(fileAssocInfo.Extension)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(fileAssocInfo.ProgId)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(fileAssocInfo.Description)))
				{
					*(int*)pFileAssocInfo = (int)global::_003CModule_003E.SysAllocString(ptr);
					((int*)pFileAssocInfo)[1] = (int)global::_003CModule_003E.SysAllocString(ptr2);
					ushort* ptr4 = global::_003CModule_003E.SysAllocString(ptr3);
					((int*)pFileAssocInfo)[2] = (int)ptr4;
					((int*)pFileAssocInfo)[3] = (fileAssocInfo.IsCurrentlyOwned ? 1 : 0);
					if (*(int*)pFileAssocInfo != 0 && ((int*)pFileAssocInfo)[1] != 0 && ptr4 != null)
					{
						return 0;
					}
					return -2147024882;
				}
			}
		}
	}

	internal unsafe void CleanupFileInfoArray(global::FileAssociationInfo* rgFileInfo, uint cFileInfo)
	{
		if (rgFileInfo == null)
		{
			return;
		}
		if (0 < cFileInfo)
		{
			global::FileAssociationInfo* ptr = (global::FileAssociationInfo*)((byte*)rgFileInfo + 8);
			uint num = cFileInfo;
			do
			{
				int num2 = *((int*)ptr - 2);
				if (num2 != 0)
				{
					global::_003CModule_003E.SysFreeString((ushort*)num2);
				}
				int num3 = *((int*)ptr - 1);
				if (num3 != 0)
				{
					global::_003CModule_003E.SysFreeString((ushort*)num3);
				}
				uint num4 = *(uint*)ptr;
				if (num4 != 0)
				{
					global::_003CModule_003E.SysFreeString((ushort*)(int)num4);
				}
				ptr = (global::FileAssociationInfo*)((byte*)ptr + 20);
				num--;
			}
			while (num != 0);
		}
		global::_003CModule_003E.delete_005B_005D(rgFileInfo);
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_007EFileAssociationHandlerWrapper();
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
