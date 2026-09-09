using System;
using System.Runtime.InteropServices;

namespace ZuneUI;

public static class Win32Window
{
	public static void Close(IntPtr hWnd)
	{
		PostMessage(hWnd, 16, 0, lParam: false);
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern bool PostMessage(IntPtr hWnd, int msg, int wParam, bool lParam);
}
