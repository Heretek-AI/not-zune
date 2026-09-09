using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Iris;

namespace ZuneUI;

public static class FolderBrowseDialog
{
	private delegate int BFFCALLBACK(IntPtr hwnd, uint uMsg, IntPtr lParam, IntPtr lpData);

	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	private struct BROWSEINFO
	{
		public IntPtr hwndOwner;

		public IntPtr pidlRoot;

		[MarshalAs(UnmanagedType.LPTStr)]
		public string pszDisplayName;

		[MarshalAs(UnmanagedType.LPTStr)]
		public string pszTitle;

		public uint ulFlags;

		[MarshalAs(UnmanagedType.FunctionPtr)]
		public BFFCALLBACK lpfn;

		public IntPtr lParam;

		public int iImage;
	}

	private const int BIF_RETURNONLYFSDIRS = 1;

	private const int BIF_EDITBOX = 16;

	private const int BIF_NEWDIALOGSTYLE = 64;

	private const int BIF_VALIDATE = 32;

	private const int BFFM_INITIALIZED = 1;

	private const int BFFM_SELCHANGED = 2;

	private const int BFFM_VALIDATEFAILED = 4;

	private const int BFFM_ENABLEOK = 1125;

	private const int MAX_PATH = 260;

	private static readonly IntPtr NoValidation = IntPtr.Zero;

	private static readonly IntPtr ValidateWritable = new IntPtr(1);

	public static void Show(string title, DeferredInvokeHandler callback)
	{
		Show(title, callback, validate: false);
	}

	public static void Show(string title, DeferredInvokeHandler callback, bool validate)
	{
		IntPtr winHandle = Application.Window.Handle;
		Thread thread = new Thread((ParameterizedThreadStart)delegate
		{
			BROWSEINFO bi = new BROWSEINFO
			{
				hwndOwner = winHandle,
				pidlRoot = IntPtr.Zero,
				pszDisplayName = new string(' ', 261)
			};
			if (!string.IsNullOrEmpty(title))
			{
				bi.pszTitle = title;
			}
			bi.ulFlags = 112u;
			bi.lpfn = Validate;
			bi.lParam = (validate ? ValidateWritable : NoValidation);
			bi.iImage = 0;
			IntPtr intPtr = SHBrowseForFolder(ref bi);
			string text = null;
			if (intPtr != IntPtr.Zero)
			{
				StringBuilder stringBuilder = new StringBuilder(261);
				if (SHGetPathFromIDList(intPtr, stringBuilder) != 0)
				{
					text = stringBuilder.ToString();
				}
			}
			Marshal.FreeCoTaskMem(intPtr);
			Application.DeferredInvoke(callback, (object)text);
		});
		thread.TrySetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	private static int Validate(IntPtr hwnd, uint uMsg, IntPtr lParam, IntPtr lpData)
	{
		switch (uMsg)
		{
		case 2u:
			if (lpData == ValidateWritable)
			{
				bool lParam2 = false;
				StringBuilder stringBuilder = new StringBuilder(260);
				if (SHGetPathFromIDList(lParam, stringBuilder) != 0)
				{
					lParam2 = CanWriteToFolder(stringBuilder.ToString());
				}
				SendMessage(hwnd, 1125, 0, lParam2);
			}
			break;
		case 4u:
			return 1;
		}
		return 0;
	}

	public static bool CanWriteToFolder(string folder)
	{
		try
		{
			string pathRoot = Path.GetPathRoot(folder);
			if (pathRoot.Length == 1 || (pathRoot.Length >= 2 && pathRoot[1] == Path.VolumeSeparatorChar))
			{
				DriveInfo driveInfo = new DriveInfo(pathRoot);
				if (driveInfo.DriveType == DriveType.CDRom || driveInfo.DriveType == DriveType.Removable)
				{
					return false;
				}
			}
			StringBuilder stringBuilder = new StringBuilder(260);
			if (GetTempFileName(folder, "tmp", 0u, stringBuilder) == 0)
			{
				return false;
			}
			using (FileStream fileStream = File.Create(stringBuilder.ToString(), 2, FileOptions.DeleteOnClose))
			{
				fileStream.WriteByte(65);
			}
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	[DllImport("Shell32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr SHBrowseForFolder(ref BROWSEINFO bi);

	[DllImport("Shell32.dll", CharSet = CharSet.Auto)]
	private static extern int SHGetPathFromIDList(IntPtr pidl, [Out] StringBuilder Path);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, bool lParam);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	internal static extern uint GetTempFileName(string tmpPath, string prefix, uint uniqueIdOrZero, StringBuilder tmpFileName);
}
