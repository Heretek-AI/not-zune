using System;
using System.Runtime.InteropServices;

namespace ZuneUI;

public static class SoundHelper
{
	private const uint SND_ASYNC = 1u;

	private const uint SND_NODEFAULT = 2u;

	private const uint SND_RESOURCE = 262148u;

	private const uint SND_SYSTEM = 2097152u;

	private const uint LOAD_LIBRARY_AS_DATAFILE = 2u;

	private static IntPtr s_hModZuneShellResources;

	public static void Play(SoundId soundId)
	{
		if (!((Shell)ZuneShell.DefaultInstance).PlaySounds)
		{
			return;
		}
		string text = null;
		switch (soundId)
		{
		case SoundId.DownloadComplete:
			text = "Download.wav";
			break;
		case SoundId.BurnComplete:
			text = "BurnComplete.wav";
			break;
		case SoundId.RipComplete:
			text = "RipComplete.wav";
			break;
		case SoundId.Inbox:
			text = "Inbox.wav";
			break;
		}
		if (text != null)
		{
			uint num = 262151u;
			if (Environment.OSVersion.Version.Major >= 6)
			{
				num |= 0x200000;
			}
			if (s_hModZuneShellResources == IntPtr.Zero)
			{
				s_hModZuneShellResources = LoadLibraryEx("ZuneShellResources.dll", IntPtr.Zero, 2u);
			}
			PlaySound(text, s_hModZuneShellResources, num);
		}
	}

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern IntPtr LoadLibraryEx(string lpModuleName, IntPtr mustBeZero, uint dwFlags);

	[DllImport("winmm.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	private static extern bool PlaySound(string pszSound, IntPtr hmod, uint fdwSound);
}
