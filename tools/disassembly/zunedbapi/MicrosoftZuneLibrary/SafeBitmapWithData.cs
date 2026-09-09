#define DEBUG
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Iris;

namespace MicrosoftZuneLibrary;

[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
public class SafeBitmapWithData : SafeBitmap
{
	private unsafe void* _pImageData;

	private int _iHeight;

	private int _iWidth;

	private int DataSize => ScanLineWidth * _iHeight;

	public int Height => _iHeight;

	public int Width => _iWidth;

	public int ScanLineWidth => ((_iWidth + 1) * 3) & -4;

	internal unsafe SafeBitmapWithData(void* pData, HBITMAP__* hBitmap)
	{
		_pImageData = pData;
		base._002Ector(hBitmap);
		int num;
		try
		{
			byte condition = (byte)((pData != null) ? 1 : 0);
			Debug.Assert(condition != 0);
			Unsafe.SkipInit(out tagBITMAP tagBITMAP2);
			if (global::_003CModule_003E.GetObjectW(hBitmap, 24, &tagBITMAP2) != 0)
			{
				goto IL_004b;
			}
			uint lastError = global::_003CModule_003E.GetLastError();
			num = (((int)lastError > 0) ? ((int)(lastError & 0xFFFF) | -2147024896) : ((int)lastError));
			if (num >= 0)
			{
				goto IL_004b;
			}
			goto end_IL_000e;
			IL_004b:
			_iWidth = Unsafe.As<tagBITMAP, int>(ref Unsafe.AddByteOffset(ref tagBITMAP2, 4));
			_iHeight = Unsafe.As<tagBITMAP, int>(ref Unsafe.AddByteOffset(ref tagBITMAP2, 8));
			goto IL_00c0;
			end_IL_000e:;
		}
		catch
		{
			//try-fault
			base.Dispose(disposing: true);
			throw;
		}
		try
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 10, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb8c78a91_002EWPP_SafeBitmap_cpp_Traceguids), (uint)num);
			}
			throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
		}
		catch
		{
			//try-fault
			base.Dispose(disposing: true);
			throw;
		}
		IL_00c0:
		try
		{
			return;
		}
		catch
		{
			//try-fault
			base.Dispose(disposing: true);
			throw;
		}
	}

	internal unsafe SafeBitmapWithData(int iHeight, int iWidth, void* pData, HBITMAP__* hBitmap)
	{
		_pImageData = pData;
		_iHeight = iHeight;
		_iWidth = iWidth;
		base._002Ector(hBitmap);
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[return: MarshalAs(UnmanagedType.U1)]
	protected unsafe override bool ReleaseHandle()
	{
		_pImageData = null;
		return base.ReleaseHandle();
	}

	public unsafe IntPtr GetData()
	{
		return (IntPtr)_pImageData;
	}

	public Image CreateImage([MarshalAs(UnmanagedType.U1)] bool antialiasEdges)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		Image result = null;
		IntPtr data = GetData();
		if (Width != 0 && Height != 0 && data != IntPtr.Zero)
		{
			result = new Image((string)null, Width, Height, -ScanLineWidth, (RawImageFormat)2, data, 0, 0, false, antialiasEdges);
		}
		return result;
	}

	public Image CreateImage()
	{
		return CreateImage(antialiasEdges: false);
	}

	public unsafe SafeBitmapWithData Clone(int srcX, int srcY, int srcWidth, int srcHeight, int dstWidth, int dstHeight)
	{
		object result = null;
		HBITMAP__* hBitmap = null;
		void* pData = null;
		if (global::_003CModule_003E.ZuneLibraryExports_002ECopyThumbnailBitmapData((HBITMAP__*)(int)handle.ToInt64(), srcX, srcY, srcWidth, srcHeight, dstWidth, dstHeight, &hBitmap, &pData) >= 0)
		{
			result = new SafeBitmapWithData(dstHeight, dstWidth, pData, hBitmap);
		}
		return (SafeBitmapWithData)result;
	}

	public unsafe static SafeBitmapWithData CreateThumbnailBitmap(string strFilename)
	{
		object result = null;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strFilename)))
		{
			Unsafe.SkipInit(out int iWidth);
			Unsafe.SkipInit(out int iHeight);
			Unsafe.SkipInit(out void* pData);
			Unsafe.SkipInit(out HBITMAP__* hBitmap);
			if (global::_003CModule_003E.ZuneLibraryExports_002EGetThumbnailBitmapData(ptr, &iWidth, &iHeight, &pData, &hBitmap) >= 0)
			{
				result = new SafeBitmapWithData(iHeight, iWidth, pData, hBitmap);
			}
			return (SafeBitmapWithData)result;
		}
	}
}
