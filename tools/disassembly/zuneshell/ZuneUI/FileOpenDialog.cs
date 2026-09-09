using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Iris;

namespace ZuneUI;

public class FileOpenDialog
{
	private delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	private class OpenFileName : IDisposable
	{
		public int lStructSize;

		public IntPtr hwndOwner;

		public IntPtr hInstance;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpstrFilter;

		public IntPtr lpstrCustomFilter;

		public int nMaxCustFilter;

		public int nFilterIndex;

		public IntPtr lpstrFile;

		public int nMaxFile;

		public IntPtr lpstrFileTitle;

		public int nMaxFileTitle;

		public string lpstrInitialDir;

		public string lpstrTitle;

		public int Flags;

		public short nFileOffset;

		public short nFileExtension;

		public string lpstrDefExt;

		public IntPtr lCustData;

		public WndProc lpfnHook;

		public string lpTemplateName;

		public IntPtr pvReserved;

		public int dwReserved;

		public int FlagsEx;

		public OpenFileName(IntPtr handle)
		{
			IntPtr ptr = Marshal.AllocHGlobal(520);
			Marshal.WriteInt32(ptr, 0);
			lpstrCustomFilter = IntPtr.Zero;
			nMaxFile = 260;
			lpstrFile = ptr;
			lpstrFileTitle = IntPtr.Zero;
			nMaxFileTitle = 260;
			lCustData = IntPtr.Zero;
			pvReserved = IntPtr.Zero;
			hwndOwner = handle;
			lStructSize = Marshal.SizeOf(typeof(OpenFileName));
		}

		public void Dispose()
		{
			Marshal.FreeHGlobal(lpstrFile);
		}
	}

	private enum OpenFileNameFlags
	{
		OFN_READONLY = 1,
		OFN_OVERWRITEPROMPT = 2,
		OFN_HIDEREADONLY = 4,
		OFN_NOCHANGEDIR = 8,
		OFN_SHOWHELP = 0x10,
		OFN_ENABLEHOOK = 0x20,
		OFN_ENABLETEMPLATE = 0x40,
		OFN_ENABLETEMPLATEHANDLE = 0x80,
		OFN_NOVALIDATE = 0x100,
		OFN_ALLOWMULTISELECT = 0x200,
		OFN_EXTENSIONDIFFERENT = 0x400,
		OFN_PATHMUSTEXIST = 0x800,
		OFN_FILEMUSTEXIST = 0x1000,
		OFN_CREATEPROMPT = 0x2000,
		OFN_SHAREAWARE = 0x4000,
		OFN_NOREADONLYRETURN = 0x8000,
		OFN_NOTESTFILECREATE = 0x10000,
		OFN_NONETWORKBUTTON = 0x20000,
		OFN_NOLONGNAMES = 0x40000,
		OFN_EXPLORER = 0x80000,
		OFN_NODEREFERENCELINKS = 0x100000,
		OFN_LONGNAMES = 0x200000,
		OFN_ENABLEINCLUDENOTIFY = 0x400000,
		OFN_ENABLESIZING = 0x800000,
		OFN_DONTADDTORECENT = 0x2000000,
		OFN_FORCESHOWHIDDEN = 0x10000000
	}

	private string m_filePath;

	public string FilePath
	{
		get
		{
			return m_filePath;
		}
		private set
		{
			m_filePath = value;
		}
	}

	public static string MyPicturesPath => Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

	private FileOpenDialog()
	{
	}

	public static FileOpenDialog Show(string title, string initialPath, Command doneCommand)
	{
		return Show(title, initialPath, null, doneCommand);
	}

	public static FileOpenDialog Show(string title, string initialPath, string[] fileFilters, Command doneCommand)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		FileOpenDialog dialog = new FileOpenDialog();
		FileOpenDialog.Show(title, initialPath, fileFilters, (DeferredInvokeHandler)delegate(object args)
		{
			dialog.FilePath = (string)args;
			if (doneCommand != null)
			{
				doneCommand.Invoke();
			}
		});
		return dialog;
	}

	public static void Show(string title, string initialPath, DeferredInvokeHandler callback)
	{
		Show(title, initialPath, null, callback);
	}

	public static void Show(string title, string initialPath, string[] fileFilters, DeferredInvokeHandler callback)
	{
		IntPtr winHandle = Application.Window.Handle;
		Thread thread = new Thread((ParameterizedThreadStart)delegate
		{
			using OpenFileName openFileName = new OpenFileName(winHandle);
			if (!string.IsNullOrEmpty(title))
			{
				openFileName.lpstrTitle = title;
			}
			if (!string.IsNullOrEmpty(initialPath))
			{
				openFileName.lpstrInitialDir = initialPath;
			}
			if (fileFilters != null)
			{
				openFileName.lpstrFilter = string.Join("\0", fileFilters) + "\0";
			}
			string text = null;
			if (GetOpenFileName(openFileName))
			{
				text = Marshal.PtrToStringUni(openFileName.lpstrFile);
			}
			Application.DeferredInvoke(callback, (object)text);
		});
		thread.TrySetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	[DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern bool GetOpenFileName([In][Out] OpenFileName ofn);
}
