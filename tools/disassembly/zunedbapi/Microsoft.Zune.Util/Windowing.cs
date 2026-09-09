using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Zune.Util;

public class Windowing
{
	public unsafe static void ForceSetForegroundWindow(IntPtr hwnd)
	{
		Unsafe.SkipInit(out tagINPUT tagINPUT2);
		// IL initblk instruction
		Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref tagINPUT2, 4), 0, 24);
		*(int*)(&tagINPUT2) = 1;
		Unsafe.As<tagINPUT, short>(ref Unsafe.AddByteOffset(ref tagINPUT2, 4)) = 0;
		global::_003CModule_003E.SendInput(1u, &tagINPUT2, 28);
		global::_003CModule_003E.SetForegroundWindow((HWND__*)hwnd.ToPointer());
	}
}
