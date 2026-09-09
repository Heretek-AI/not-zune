using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ZuneUI;

internal static class ShellInterop
{
	public static void OpenFolderAndSelectItem(string path)
	{
		Thread thread = new Thread((ParameterizedThreadStart)delegate
		{
			if (SHParseDisplayName(path, IntPtr.Zero, out var ppidl, 0, IntPtr.Zero) == 0)
			{
				SHOpenFolderAndSelectItems(ppidl, 0, IntPtr.Zero, 0);
				ILFree(ppidl);
			}
		});
		thread.TrySetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	[DllImport("shell32.dll", CharSet = CharSet.Auto)]
	private static extern int SHParseDisplayName(string path, IntPtr pbc, out IntPtr ppidl, int sfgaoIn, IntPtr psfgaoOut);

	[DllImport("shell32.dll")]
	private static extern int SHOpenFolderAndSelectItems(IntPtr pidl, int cidl, IntPtr apidl, int dwFlags);

	[DllImport("shell32.dll")]
	private static extern int ILFree(IntPtr pidl);
}
