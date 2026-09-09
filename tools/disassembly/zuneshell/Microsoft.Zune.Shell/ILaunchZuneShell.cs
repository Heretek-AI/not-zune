using System;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Shell;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("79493c51-2a79-410c-ab77-da1ead887f11")]
internal interface ILaunchZuneShell
{
	IntPtr GetLaunchDelegate(string args, IntPtr hWndSplashScreen);

	IntPtr GetRenderWindow();

	void ProcessMessageFromCommandLine(string cmdLine);

	void SetDesktopLockState(bool locked);
}
