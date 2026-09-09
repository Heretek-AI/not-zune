using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace ZuneUI;

internal class CryptoHelper : IDisposable
{
	private const uint PROV_RSA_FULL = 1u;

	private const uint CRYPT_VERIFYCONTEXT = 4026531840u;

	private const uint CRYPT_NEWKEYSET = 8u;

	private const uint ALG_CLASS_DATA_ENCRYPT = 24576u;

	private const uint ALG_CLASS_HASH = 32768u;

	private const uint ALG_TYPE_ANY = 0u;

	private const uint ALG_TYPE_STREAM = 2048u;

	private const uint ALG_SID_RC4 = 1u;

	private const uint ALG_SID_MD5 = 3u;

	private const uint CALG_MD5 = 32771u;

	private const uint CALG_RC4 = 26625u;

	private const uint NTE_BAD_KEYSET = 2148073494u;

	private IntPtr m_hCryptoProvider;

	private IntPtr m_hKey;

	public CryptoHelper(string key)
	{
		IntPtr phHash = IntPtr.Zero;
		byte[] bytes = Encoding.Unicode.GetBytes(key);
		try
		{
			if (!CryptAcquireContext(out m_hCryptoProvider, null, null, 1u, 4026531840u))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (-2146893802 != lastWin32Error)
				{
					throw new Win32Exception(lastWin32Error);
				}
				if (!CryptAcquireContext(out m_hCryptoProvider, null, null, 1u, 4026531848u))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			if (!CryptCreateHash(m_hCryptoProvider, 32771u, IntPtr.Zero, 0u, out phHash))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			if (!CryptHashData(phHash, bytes, (uint)bytes.Length, 0u))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			if (!CryptDeriveKey(m_hCryptoProvider, 26625u, phHash, 8388608u, out m_hKey))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}
		finally
		{
			if (phHash != IntPtr.Zero)
			{
				CryptDestroyHash(phHash);
			}
		}
	}

	public string Encrypt(string data)
	{
		byte[] bytes = Encoding.Unicode.GetBytes(data);
		uint pdwDataLen = (uint)bytes.Length;
		if (!CryptEncrypt(m_hKey, IntPtr.Zero, final: true, 0u, bytes, ref pdwDataLen, pdwDataLen))
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		return Convert.ToBase64String(bytes);
	}

	public string Decrypt(string data)
	{
		byte[] array = Convert.FromBase64String(data);
		uint pdwDataLen = (uint)array.Length;
		if (!CryptDecrypt(m_hKey, IntPtr.Zero, final: true, 0u, array, ref pdwDataLen))
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		return Encoding.Unicode.GetString(array);
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	public void Dispose(bool disposing)
	{
		if (IntPtr.Zero != m_hKey)
		{
			CryptDestroyKey(m_hKey);
		}
		if (IntPtr.Zero != m_hCryptoProvider)
		{
			CryptReleaseContext(m_hCryptoProvider, 0u);
		}
	}

	~CryptoHelper()
	{
		Dispose(disposing: false);
	}

	[DllImport("advapi32.dll", SetLastError = true)]
	private static extern bool CryptAcquireContext(out IntPtr phProv, string pszContainer, string pszProvider, uint dwProvType, uint dwFlags);

	[DllImport("advapi32.dll", SetLastError = true)]
	private static extern bool CryptReleaseContext(IntPtr hProv, uint dwFlags);

	[DllImport("advapi32.dll", SetLastError = true)]
	private static extern bool CryptCreateHash(IntPtr hProv, uint algId, IntPtr hKey, uint dwFlags, out IntPtr phHash);

	[DllImport("advapi32.dll", SetLastError = true)]
	private static extern bool CryptHashData(IntPtr hHash, byte[] pbData, uint dwDataLen, uint dwFlags);

	[DllImport("advapi32.dll", SetLastError = true)]
	private static extern bool CryptDestroyHash(IntPtr hHash);

	[DllImport("advapi32.dll", SetLastError = true)]
	private static extern bool CryptDeriveKey(IntPtr hProv, uint algId, IntPtr hBaseData, uint dwFlags, out IntPtr phKey);

	[DllImport("advapi32.dll", SetLastError = true)]
	private static extern bool CryptDestroyKey(IntPtr hHashKey);

	[DllImport("advapi32.dll", SetLastError = true)]
	private static extern bool CryptEncrypt(IntPtr hKey, IntPtr hHash, bool final, uint dwFlags, byte[] pbData, ref uint pdwDataLen, uint dwBufLen);

	[DllImport("advapi32.dll", SetLastError = true)]
	private static extern bool CryptDecrypt(IntPtr hKey, IntPtr hHash, bool final, uint dwFlags, byte[] pbData, ref uint pdwDataLen);
}
