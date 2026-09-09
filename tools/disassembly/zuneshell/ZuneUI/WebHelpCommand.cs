using System;
using System.Runtime.InteropServices;
using Microsoft.Iris;

namespace ZuneUI;

public class WebHelpCommand : Command
{
	private const int SW_SHOWNORMAL = 1;

	private string _url;

	public string Url
	{
		get
		{
			return _url;
		}
		set
		{
			if (_url != value)
			{
				_url = value;
				((ModelItem)this).FirePropertyChanged("Url");
			}
		}
	}

	protected override void OnInvoked()
	{
		if (_url != null)
		{
			ShellExecute(IntPtr.Zero, "open", _url, null, null, 1);
		}
		((Command)this).OnInvoked();
	}

	[DllImport("shell32.dll")]
	private static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);
}
