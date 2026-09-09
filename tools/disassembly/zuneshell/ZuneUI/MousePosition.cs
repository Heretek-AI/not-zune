using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Shell;

namespace ZuneUI;

public class MousePosition : ModelItem
{
	private struct POINT
	{
		public int X;

		public int Y;
	}

	private int _x;

	private int _y;

	private Timer _checkPostion;

	private bool _invokeOutstanding;

	public int X
	{
		get
		{
			return _x;
		}
		private set
		{
			if (value != _x)
			{
				_x = value;
				((ModelItem)this).FirePropertyChanged("X");
			}
		}
	}

	public int Y
	{
		get
		{
			return _y;
		}
		private set
		{
			if (value != _y)
			{
				_y = value;
				((ModelItem)this).FirePropertyChanged("Y");
			}
		}
	}

	public MousePosition()
	{
		_invokeOutstanding = false;
		_checkPostion = new Timer(CheckPosition, null, 0, 16);
	}

	private void CheckPosition(object state)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		if (!_invokeOutstanding)
		{
			_invokeOutstanding = true;
			GetCursorScreenPosition(out var x, out var y);
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredUpdatePosition), (object)new object[2] { x, y });
		}
	}

	private void DeferredUpdatePosition(object arg)
	{
		object[] array = (object[])arg;
		int num = (int)array[0];
		int num2 = (int)array[1];
		if (!GetWindowRect(ZuneApplication.GetRenderWindow(), out var lpRect))
		{
			lpRect.Left = 0;
			lpRect.Top = 0;
			lpRect.Right = 1;
			lpRect.Bottom = 1;
		}
		num = ((num < lpRect.Left) ? lpRect.Left : num);
		num = ((num >= lpRect.Right) ? (lpRect.Right - 1) : num);
		num2 = ((num2 < lpRect.Top) ? lpRect.Top : num2);
		num2 = ((num2 >= lpRect.Bottom) ? (lpRect.Bottom - 1) : num2);
		X = num - lpRect.Left;
		Y = num2 - lpRect.Top;
		_invokeOutstanding = false;
	}

	public static void GetCursorScreenPosition(out int x, out int y)
	{
		if (GetCursorPos(out var lpPoint))
		{
			x = lpPoint.X;
			y = lpPoint.Y;
		}
		else
		{
			x = (y = 0);
		}
	}

	[DllImport("User32.dll")]
	private static extern bool GetCursorPos(out POINT lpPoint);

	[DllImport("User32.dll")]
	private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
}
