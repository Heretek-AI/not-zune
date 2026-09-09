using System;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Shell;

[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
[Guid("53048eb3-0906-4bd2-890d-48daf1f25413")]
[ComDefaultInterface(typeof(ILaunchZuneShell))]
public sealed class LaunchZuneShell : ILaunchZuneShell
{
	private delegate int LaunchDelegate();

	private static LaunchDelegate s_launch;

	private static string s_args;

	private static IntPtr s_hWndSplashScreen;

	[STAThread]
	public IntPtr GetLaunchDelegate(string args, IntPtr hWndSplashScreen)
	{
		s_args = args;
		s_hWndSplashScreen = hWndSplashScreen;
		s_launch = LaunchZuneShellHelper;
		return Marshal.GetFunctionPointerForDelegate((Delegate)s_launch);
	}

	private int LaunchZuneShellHelper()
	{
		return ZuneApplication.Launch(s_args, s_hWndSplashScreen);
	}

	public IntPtr GetRenderWindow()
	{
		return ZuneApplication.GetRenderWindow();
	}

	public void ProcessMessageFromCommandLine(string args)
	{
		ZuneApplication.ProcessMessageFromCommandLine(args);
	}

	public void SetDesktopLockState(bool locked)
	{
		ZuneApplication.SetDesktopLockState(locked);
	}
}
