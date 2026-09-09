using System;

namespace Microsoft.Zune.Configuration;

public class FileAssociationHandlerFactory
{
	public unsafe static IFileAssociationHandler CreateFileAssociationHandler()
	{
		FileAssociationHandlerWrapper fileAssociationHandlerWrapper = null;
		global::IFileAssociationHandler* ptr = null;
		try
		{
			int num = global::_003CModule_003E.ZuneLibraryExports_002ECreateNativeFileAssociationHandler((void**)(&ptr));
			if (num >= 0)
			{
				fileAssociationHandlerWrapper = new FileAssociationHandlerWrapper(ptr);
				ptr = null;
				return fileAssociationHandlerWrapper;
			}
			throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
		}
		finally
		{
			if (ptr != null)
			{
				global::IFileAssociationHandler* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
		}
	}
}
