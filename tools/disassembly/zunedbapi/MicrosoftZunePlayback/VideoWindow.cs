using System.Runtime.InteropServices;

namespace MicrosoftZunePlayback;

public class VideoWindow(int left, int top, int right, int bottom)
{
	private int _left = left;

	private int _top = top;

	private int _right = right;

	private int _bottom = bottom;

	public int Bottom => _bottom;

	public int Right => _right;

	public int Top => _top;

	public int Left => _left;

	[return: MarshalAs(UnmanagedType.U1)]
	public bool IsDifferent(VideoWindow challenger)
	{
		int num = ((_left != challenger._left && _top != challenger._top && _right != challenger._right && _bottom != challenger._bottom) ? 1 : 0);
		return (byte)num != 0;
	}
}
