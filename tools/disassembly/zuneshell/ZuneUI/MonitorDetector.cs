using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ZuneUI;

internal class MonitorDetector
{
	private delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, [In] ref RECT lprcMonitor, IntPtr dwData);

	private struct MONITORINFO
	{
		public int cbSize;

		public RECT rcMonitor;

		public RECT rcWorkArea;

		public int dwFlags;
	}

	private List<MonitorSize> _listInProgress;

	public List<MonitorSize> DetectMonitors()
	{
		List<MonitorSize> result = (_listInProgress = new List<MonitorSize>());
		EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnumerated, IntPtr.Zero);
		_listInProgress = null;
		return result;
	}

	private bool MonitorEnumerated(IntPtr hMonitor, IntPtr hdcMonitor, [In] ref RECT lprcMonitor, IntPtr dwData)
	{
		MONITORINFO lpmi = default(MONITORINFO);
		lpmi.cbSize = Marshal.SizeOf((object)lpmi);
		if (GetMonitorInfo(hMonitor, ref lpmi))
		{
			lpmi.rcMonitor.Right--;
			lpmi.rcMonitor.Bottom--;
			lpmi.rcWorkArea.Right--;
			lpmi.rcWorkArea.Bottom--;
			_listInProgress.Add(new MonitorSize(lpmi.rcMonitor, lpmi.rcWorkArea));
		}
		return true;
	}

	[DllImport("user32.dll")]
	private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

	[DllImport("user32.dll")]
	private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);
}
