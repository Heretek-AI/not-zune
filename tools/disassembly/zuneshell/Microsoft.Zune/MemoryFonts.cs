using System;
using System.Runtime.InteropServices;

namespace Microsoft.Zune;

internal class MemoryFonts
{
	public const uint LOAD_LIBRARY_AS_DATAFILE = 2u;

	public const int RT_DATA = 10;

	public static bool TryLoadFromResource(string resourceDllName, string fontResourceName)
	{
		IntPtr intPtr = IntPtr.Zero;
		try
		{
			intPtr = LoadLibraryEx(resourceDllName, IntPtr.Zero, 2u);
			if (intPtr == IntPtr.Zero)
			{
				return false;
			}
			IntPtr intPtr2 = FindResource(intPtr, fontResourceName, (IntPtr)10);
			if (intPtr2 == IntPtr.Zero)
			{
				return false;
			}
			IntPtr intPtr3 = LoadResource(intPtr, intPtr2);
			if (intPtr3 == IntPtr.Zero)
			{
				return false;
			}
			IntPtr fontBuffer = LockResource(intPtr3);
			int fontButtonSize = SizeofResource(intPtr, intPtr2);
			if (AddFontMemResourceEx(fontBuffer, fontButtonSize, IntPtr.Zero, out var _) == IntPtr.Zero)
			{
				return false;
			}
			return true;
		}
		finally
		{
			if (intPtr != IntPtr.Zero)
			{
				FreeLibrary(intPtr);
			}
		}
	}

	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	public static extern IntPtr LoadLibraryEx(string moduleName, IntPtr reserved, uint flags);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	public static extern bool FreeLibrary(IntPtr instanceHandle);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "FindResourceW")]
	public static extern IntPtr FindResource(IntPtr instanceHandle, string resource, IntPtr type);

	[DllImport("kernel32.dll")]
	public static extern IntPtr LoadResource(IntPtr instanceHandle, IntPtr resourceHandle);

	[DllImport("kernel32.dll")]
	public static extern int SizeofResource(IntPtr instanceHandle, IntPtr resourceHandle);

	[DllImport("kernel32.dll")]
	public static extern IntPtr LockResource(IntPtr resourceHandle);

	[DllImport("gdi32.dll")]
	public static extern IntPtr AddFontMemResourceEx(IntPtr fontBuffer, int fontButtonSize, IntPtr reserved, out uint fontsInstalled);
}
