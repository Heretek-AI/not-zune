using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Iris;

namespace ZuneUI;

public static class Win32MessageBox
{
	public static void Show(string text, string caption, Win32MessageBoxType type, DeferredInvokeHandler callback)
	{
		IntPtr winHandle = Application.Window.Handle;
		Thread thread = new Thread((ParameterizedThreadStart)delegate
		{
			int num = MessageBox(winHandle, text, caption, type);
			if (callback != null)
			{
				Application.DeferredInvoke(callback, (object)num);
			}
		});
		thread.Start();
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern int MessageBox(IntPtr hWnd, string text, string caption, Win32MessageBoxType type);
}
