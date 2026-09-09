using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class WindowSnapSimulator : SingletonModelItem<WindowSnapSimulator>
{
	private enum WindowSnappedness
	{
		Unsnapped,
		Left,
		Right,
		Ineligible
	}

	private class WindowPlacement
	{
		private WindowPosition _position;

		private WindowSize _size;

		public WindowPosition Position
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return _position;
			}
			set
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				_position = value;
			}
		}

		public WindowSize Size
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return _size;
			}
			set
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				_size = value;
			}
		}
	}

	private class Monitor : IComparable<Monitor>
	{
		private MonitorSize _dimensions;

		private WindowPlacement _leftSnap;

		private WindowPlacement _rightSnap;

		private int _hotspotSize;

		private IHotspot _topHotspot;

		private IHotspot _leftHotspot;

		private IHotspot _rightHotspot;

		public int Left => _dimensions.WorkArea.Left;

		public int Right => _dimensions.WorkArea.Right;

		public int Top => _dimensions.WorkArea.Top;

		public int Bottom => _dimensions.WorkArea.Bottom;

		public WindowPlacement LeftSnap
		{
			get
			{
				return _leftSnap;
			}
			private set
			{
				_leftSnap = value;
			}
		}

		public WindowPlacement RightSnap
		{
			get
			{
				return _rightSnap;
			}
			private set
			{
				_rightSnap = value;
			}
		}

		public Monitor(MonitorSize dimensions)
		{
			_dimensions = dimensions;
			LeftSnap = new WindowPlacement();
			RightSnap = new WindowPlacement();
		}

		public void InitializeHotspots(List<Monitor> monitorList, float hotspotSizePercentage)
		{
			_hotspotSize = (int)((float)((Right - Left + 1 + (Bottom - Top + 1)) / 2) * hotspotSizePercentage);
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			foreach (Monitor monitor in monitorList)
			{
				RECT totalArea = monitor._dimensions.TotalArea;
				if (totalArea.Top + 1 == Top)
				{
					flag = false;
				}
				if (totalArea.Right + 1 == Left)
				{
					flag2 = false;
				}
				if (totalArea.Left - 1 == Right)
				{
					flag3 = false;
				}
			}
			if (flag)
			{
				_topHotspot = new MaximizeHotspot();
			}
			if (flag2)
			{
				_leftHotspot = new PlacementHotspot(LeftSnap);
			}
			if (flag3)
			{
				_rightHotspot = new PlacementHotspot(RightSnap);
			}
		}

		public IHotspot GetHotspotForCursor(int cursorX, int cursorY)
		{
			IHotspot result = null;
			if (cursorY <= Top + _hotspotSize && _topHotspot != null)
			{
				result = _topHotspot;
			}
			else if (cursorX <= Left + _hotspotSize)
			{
				result = _leftHotspot;
			}
			else if (cursorX >= Right - _hotspotSize)
			{
				result = _rightHotspot;
			}
			return result;
		}

		public int CompareTo(Monitor other)
		{
			int num = Top.CompareTo(other.Top);
			if (num == 0)
			{
				return Left.CompareTo(other.Left);
			}
			return num;
		}
	}

	private interface IHotspot
	{
		void Snap();
	}

	private class MaximizeHotspot : IHotspot
	{
		public void Snap()
		{
			SingletonModelItem<WindowSnapSimulator>.Instance.ShiftUp();
		}
	}

	private class PlacementHotspot : IHotspot
	{
		private WindowPlacement _placement;

		public PlacementHotspot(WindowPlacement placement)
		{
			_placement = placement;
		}

		public void Snap()
		{
			SingletonModelItem<WindowSnapSimulator>.Instance.Snap(_placement);
		}
	}

	private const float c_hotspotPercentage = 0.015f;

	private Window _window;

	private bool _isInitialized;

	private List<Monitor> _monitors;

	private IHotspot _currentHotspot;

	private bool _isSnapping;

	public bool IsSnapping
	{
		get
		{
			return _isSnapping;
		}
		private set
		{
			if (_isSnapping != value)
			{
				_isSnapping = value;
				((ModelItem)this).FirePropertyChanged("IsSnapping");
			}
		}
	}

	private bool IsInitialized
	{
		get
		{
			return _isInitialized;
		}
		set
		{
			_isInitialized = value;
		}
	}

	public void Phase3Init()
	{
		Initialize();
	}

	private void Initialize()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		if (!IsInitialized)
		{
			InitializeMonitors();
			_window = Application.Window;
			_window.PropertyChanged += OnWindowPropertyChanged;
			Win7ShellManager.Instance.OnMonitorChange += new MonitorChangeHandler(OnMonitorChangeDetected);
			if (OSVersion.IsWin7())
			{
				Win7ShellManager.Instance.OnWindowPositionKeyPress += new WindowPositionKeyPressHandler(OnWin7KeypressDetected);
			}
			Shell shell = (Shell)ZuneShell.DefaultInstance;
			if (shell.NormalWindowSize == null)
			{
				WindowSize clientSize = _window.ClientSize;
				int width = ((WindowSize)(ref clientSize)).Width;
				WindowSize clientSize2 = _window.ClientSize;
				shell.NormalWindowSize = new Size(width, ((WindowSize)(ref clientSize2)).Height);
			}
			if (shell.NormalWindowPosition == null)
			{
				WindowPosition position = _window.Position;
				int x = ((WindowPosition)(ref position)).X;
				WindowPosition position2 = _window.Position;
				shell.NormalWindowPosition = new Point(x, ((WindowPosition)(ref position2)).Y);
			}
			IsInitialized = true;
			UpdateIsSnapping();
		}
	}

	private void InitializeMonitors()
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		_monitors = new List<Monitor>();
		MonitorDetector monitorDetector = new MonitorDetector();
		List<MonitorSize> list = monitorDetector.DetectMonitors();
		foreach (MonitorSize item in list)
		{
			Monitor monitor = new Monitor(item);
			int num = Math.Max((monitor.Right - monitor.Left + 1) / 2, Shell.MinimumWindowWidth);
			int num2 = Math.Max(monitor.Bottom - monitor.Top + 1, Shell.MinimumWindowHeight);
			monitor.LeftSnap.Position = new WindowPosition(monitor.Left, monitor.Top);
			monitor.LeftSnap.Size = new WindowSize(num, num2);
			monitor.RightSnap.Position = new WindowPosition(monitor.Right - num, monitor.Top);
			monitor.RightSnap.Size = new WindowSize(num, num2);
			_monitors.Add(monitor);
		}
		_monitors.Sort();
		foreach (Monitor monitor2 in _monitors)
		{
			monitor2.InitializeHotspots(_monitors, 0.015f);
		}
	}

	public void ShiftUp()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (IsInitialized && (int)_window.WindowState == 0)
		{
			if (IsSnapping)
			{
				Monitor windowMonitor = GetWindowMonitor();
				Unsnap(windowMonitor);
			}
			_window.WindowState = (WindowState)2;
		}
	}

	public void ShiftDown()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected I4, but got Unknown
		if (!IsInitialized)
		{
			return;
		}
		WindowState windowState = _window.WindowState;
		switch ((int)windowState)
		{
		case 2:
			_window.WindowState = (WindowState)0;
			break;
		case 0:
			if (IsSnapping)
			{
				Unsnap();
			}
			else
			{
				_window.WindowState = (WindowState)1;
			}
			break;
		case 1:
			break;
		}
	}

	public void ShiftLeft()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Invalid comparison between Unknown and I4
		if (IsInitialized)
		{
			if ((int)_window.WindowState == 2)
			{
				_window.WindowState = (WindowState)0;
			}
			Monitor windowMonitor = GetWindowMonitor();
			switch (GetWindowSnappedness(windowMonitor))
			{
			case WindowSnappedness.Unsnapped:
				Snap(windowMonitor.LeftSnap);
				break;
			case WindowSnappedness.Left:
			{
				Monitor monitor = FindPreviousMonitor(windowMonitor);
				Snap(monitor.RightSnap);
				break;
			}
			case WindowSnappedness.Right:
				Unsnap(windowMonitor);
				break;
			}
		}
	}

	public void ShiftRight()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Invalid comparison between Unknown and I4
		if (IsInitialized)
		{
			if ((int)_window.WindowState == 2)
			{
				_window.WindowState = (WindowState)0;
			}
			Monitor windowMonitor = GetWindowMonitor();
			switch (GetWindowSnappedness(windowMonitor))
			{
			case WindowSnappedness.Unsnapped:
				Snap(windowMonitor.RightSnap);
				break;
			case WindowSnappedness.Right:
			{
				Monitor monitor = FindNextMonitor(windowMonitor);
				Snap(monitor.LeftSnap);
				break;
			}
			case WindowSnappedness.Left:
				Unsnap(windowMonitor);
				break;
			}
		}
	}

	public void ShiftMonitorLeft()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (!IsInitialized)
		{
			return;
		}
		Monitor windowMonitor = GetWindowMonitor();
		WindowSnappedness windowSnappedness = GetWindowSnappedness(windowMonitor);
		Monitor monitor = null;
		if (!OSVersion.IsWin7())
		{
			monitor = FindPreviousMonitor(windowMonitor);
		}
		switch (windowSnappedness)
		{
		case WindowSnappedness.Unsnapped:
			if (!OSVersion.IsWin7())
			{
				MoveWindowToMonitor(windowMonitor, _window.Position, monitor);
			}
			break;
		case WindowSnappedness.Right:
			if (OSVersion.IsWin7())
			{
				Snap(windowMonitor.RightSnap);
			}
			else
			{
				Snap(monitor.RightSnap);
			}
			break;
		case WindowSnappedness.Left:
			if (OSVersion.IsWin7())
			{
				Snap(windowMonitor.LeftSnap);
			}
			else
			{
				Snap(monitor.LeftSnap);
			}
			break;
		}
	}

	public void ShiftMonitorRight()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (!IsInitialized)
		{
			return;
		}
		Monitor windowMonitor = GetWindowMonitor();
		WindowSnappedness windowSnappedness = GetWindowSnappedness(windowMonitor);
		Monitor monitor = null;
		if (!OSVersion.IsWin7())
		{
			monitor = FindNextMonitor(windowMonitor);
		}
		switch (windowSnappedness)
		{
		case WindowSnappedness.Unsnapped:
			if (!OSVersion.IsWin7())
			{
				MoveWindowToMonitor(windowMonitor, _window.Position, monitor);
			}
			break;
		case WindowSnappedness.Right:
			if (OSVersion.IsWin7())
			{
				Snap(windowMonitor.RightSnap);
			}
			else
			{
				Snap(monitor.RightSnap);
			}
			break;
		case WindowSnappedness.Left:
			if (OSVersion.IsWin7())
			{
				Snap(windowMonitor.LeftSnap);
			}
			else
			{
				Snap(monitor.LeftSnap);
			}
			break;
		}
	}

	public void Unsnap()
	{
		if (IsSnapping && IsInitialized)
		{
			Unsnap(GetWindowMonitor());
		}
	}

	public void DragBegun()
	{
		if (IsInitialized)
		{
			int x = 0;
			int y = 0;
			MousePosition.GetCursorScreenPosition(out x, out y);
			_currentHotspot = GetWindowMonitor(x, y).GetHotspotForCursor(x, y);
		}
		else
		{
			_currentHotspot = null;
		}
	}

	public bool ReactToDrag()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Invalid comparison between Unknown and I4
		IHotspot hotspot = null;
		if (IsInitialized)
		{
			int x = 0;
			int y = 0;
			MousePosition.GetCursorScreenPosition(out x, out y);
			hotspot = GetWindowMonitor(x, y).GetHotspotForCursor(x, y);
			if (hotspot != _currentHotspot)
			{
				if (IsSnapping)
				{
					Unsnap();
				}
				if ((int)_window.WindowState == 2)
				{
					_window.WindowState = (WindowState)0;
				}
				_currentHotspot = hotspot;
				hotspot?.Snap();
			}
		}
		return hotspot != null;
	}

	public void DragEnded()
	{
		_currentHotspot = null;
	}

	public void SetWindowPosition(int left, int top, int width, int height)
	{
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		Initialize();
		Monitor windowMonitor = GetWindowMonitor(left + width / 2, top + height / 2);
		if (windowMonitor != null)
		{
			if (width > windowMonitor.Right - windowMonitor.Left)
			{
				width = windowMonitor.Right - windowMonitor.Left;
			}
			if (height > windowMonitor.Bottom - windowMonitor.Top)
			{
				height = windowMonitor.Bottom - windowMonitor.Top;
			}
			if (left < windowMonitor.Left)
			{
				left = windowMonitor.Left;
			}
			else if (left + width > windowMonitor.Right)
			{
				left = windowMonitor.Right - width;
			}
			if (top < windowMonitor.Top)
			{
				top = windowMonitor.Top;
			}
			else if (top + height > windowMonitor.Bottom)
			{
				top = windowMonitor.Bottom - height;
			}
		}
		_window.Position = new WindowPosition(left, top);
		_window.ClientSize = new WindowSize(width, height);
	}

	private void Snap(WindowPlacement placement)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (IsInitialized)
		{
			_window.Position = placement.Position;
			_window.ClientSize = placement.Size;
		}
	}

	private void Unsnap(Monitor monitor)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		if (IsSnapping && IsInitialized)
		{
			Shell shell = (Shell)ZuneShell.DefaultInstance;
			WindowPosition val = default(WindowPosition);
			((WindowPosition)(ref val))._002Ector(shell.NormalWindowPosition.X, shell.NormalWindowPosition.Y);
			WindowSize val2 = default(WindowSize);
			((WindowSize)(ref val2))._002Ector(shell.NormalWindowSize.Width, shell.NormalWindowSize.Height);
			Monitor windowMonitor = GetWindowMonitor(val, val2);
			if (monitor == windowMonitor)
			{
				_window.Position = val;
			}
			else
			{
				MoveWindowToMonitor(windowMonitor, val, monitor);
			}
			_window.ClientSize = val2;
		}
	}

	private void MoveWindowToMonitor(Monitor currentMonitor, WindowPosition currentPosition, Monitor destinationMonitor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		int num = destinationMonitor.Left - currentMonitor.Left;
		int num2 = destinationMonitor.Top - currentMonitor.Top;
		_window.Position = new WindowPosition(((WindowPosition)(ref currentPosition)).X + num, ((WindowPosition)(ref currentPosition)).Y + num2);
	}

	private Monitor FindNextMonitor(Monitor monitor)
	{
		int num = _monitors.IndexOf(monitor) + 1;
		if (num >= _monitors.Count)
		{
			num = 0;
		}
		return _monitors[num];
	}

	private Monitor FindPreviousMonitor(Monitor monitor)
	{
		int num = _monitors.IndexOf(monitor) - 1;
		if (num < 0)
		{
			num = _monitors.Count - 1;
		}
		return _monitors[num];
	}

	private WindowSnappedness GetWindowSnappedness(Monitor monitor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Invalid comparison between Unknown and I4
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		WindowSnappedness result = WindowSnappedness.Ineligible;
		if (IsInitialized && (int)_window.WindowState != 2 && (int)_window.WindowState != 1)
		{
			result = ((_window.Position == monitor.LeftSnap.Position && _window.ClientSize == monitor.LeftSnap.Size) ? WindowSnappedness.Left : ((_window.Position == monitor.RightSnap.Position && _window.ClientSize == monitor.RightSnap.Size) ? WindowSnappedness.Right : WindowSnappedness.Unsnapped));
		}
		return result;
	}

	private Monitor GetWindowMonitor()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		return GetWindowMonitor(_window.Position, _window.ClientSize);
	}

	private Monitor GetWindowMonitor(WindowPlacement placement)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		return GetWindowMonitor(placement.Position, placement.Size);
	}

	private Monitor GetWindowMonitor(WindowPosition position, WindowSize size)
	{
		int x = ((WindowPosition)(ref position)).X + ((WindowSize)(ref size)).Width / 2;
		int y = ((WindowPosition)(ref position)).Y + ((WindowSize)(ref size)).Height / 2;
		return GetWindowMonitor(x, y);
	}

	private Monitor GetWindowMonitor(int x, int y)
	{
		Monitor result = null;
		if (IsInitialized)
		{
			if (_monitors.Count > 1)
			{
				int num = int.MaxValue;
				foreach (Monitor monitor in _monitors)
				{
					int num2 = 0;
					int num3 = 0;
					if (x < monitor.Left)
					{
						num2 = monitor.Left - x;
					}
					else if (x > monitor.Right)
					{
						num2 = x - monitor.Right;
					}
					if (y < monitor.Top)
					{
						num3 = monitor.Top - y;
					}
					else if (y > monitor.Bottom)
					{
						num3 = y - monitor.Bottom;
					}
					int num4 = num2 + num3;
					if (num4 < num)
					{
						result = monitor;
						num = num4;
					}
				}
			}
			else
			{
				result = _monitors[0];
			}
		}
		return result;
	}

	private void UpdateIsSnapping()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		bool isSnapping = false;
		if ((int)_window.WindowState == 0)
		{
			WindowSnappedness windowSnappedness = GetWindowSnappedness(GetWindowMonitor());
			if (windowSnappedness == WindowSnappedness.Left || windowSnappedness == WindowSnappedness.Right)
			{
				isSnapping = true;
			}
		}
		IsSnapping = isSnapping;
	}

	private void OnWindowPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "Position" || args.PropertyName == "ClientSize" || args.PropertyName == "WindowState")
		{
			UpdateIsSnapping();
		}
	}

	private void OnWin7KeypressDetected(WindowPositionKeys key)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected I4, but got Unknown
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Invalid comparison between Unknown and I4
			WindowPositionKeys val = key;
			switch ((int)val)
			{
			case 0:
				ShiftLeft();
				break;
			case 1:
				ShiftRight();
				break;
			case 3:
				if ((int)_window.WindowState == 2)
				{
					ShiftDown();
				}
				break;
			case 4:
				ShiftMonitorLeft();
				break;
			case 5:
				ShiftMonitorRight();
				break;
			case 2:
				break;
			}
		}, (object)null);
	}

	private void OnMonitorChangeDetected()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			Unsnap();
			InitializeMonitors();
		}, (object)null);
	}
}
