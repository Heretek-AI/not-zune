using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using MicrosoftZuneLibrary;
using ZuneUI;

namespace Microsoft.Zune.Playlist;

public class AutoPlaylistBuilder : IDisposable
{
	private unsafe IAutoPlaylistRules* m_pAutoPlaylistRules;

	private int m_currentRuleSetGroup;

	private Dictionary<GroupAndAtom, AtomRules> m_groupAndAtomToRules;

	private EMediaTypes m_type;

	public EMediaTypes Schema => m_type;

	public unsafe AutoPlaylistBuilder(int playlistId)
	{
		m_currentRuleSetGroup = 0;
		IPlaylistManager* nativePlaylistManager = PlaylistManager.Instance.NativePlaylistManager;
		Unsafe.SkipInit(out IAutoPlaylistRules* ptr);
		if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, IAutoPlaylistRules**, int>)(int)(*(uint*)(*(int*)nativePlaylistManager + 56)))((nint)nativePlaylistManager, playlistId, &ptr) < 0)
		{
			return;
		}
		m_pAutoPlaylistRules = ptr;
		PlaylistManager.Instance.GetAutoPlaylistSchema(playlistId, out m_type);
		int num = 0;
		int num2 = 0;
		bool flag = true;
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		Unsafe.SkipInit(out uint atom);
		Unsafe.SkipInit(out EAutoPlaylistRuleOperators op);
		do
		{
			// IL initblk instruction
			Unsafe.InitBlock(ref cComPropVariant, 0, 16);
			try
			{
				int num3 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaTypes, uint, uint, uint*, EAutoPlaylistRuleOperators*, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)ptr + 20)))((nint)ptr, m_type, (uint)num, (uint)num2, &atom, &op, (tagPROPVARIANT*)(&cComPropVariant));
				if (num3 >= 0)
				{
					if (num3 == 1)
					{
						if (num2 == 0)
						{
							flag = false;
						}
						else
						{
							num2 = 0;
							num++;
						}
					}
					else
					{
						CacheCriterion(num, (int)atom, (tagPROPVARIANT*)(&cComPropVariant), op);
						num2++;
					}
					goto IL_00b0;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
				throw;
			}
			global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
			break;
			IL_00b0:
			global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
		}
		while (flag);
		int num4 = 0;
		Unsafe.SkipInit(out CComPropVariant cComPropVariant2);
		// IL initblk instruction
		Unsafe.InitBlock(ref cComPropVariant2, 0, 16);
		int num5;
		Unsafe.SkipInit(out uint atom2);
		try
		{
			num5 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint*, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)ptr + 36)))((nint)ptr, 0u, &atom2, (tagPROPVARIANT*)(&cComPropVariant2));
			if (num5 < 0)
			{
				goto IL_0159;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant2);
			throw;
		}
		while (true)
		{
			try
			{
				if (num5 == 1)
				{
					break;
				}
				CacheCriterion(0, (int)atom2, (tagPROPVARIANT*)(&cComPropVariant2), (EAutoPlaylistRuleOperators)8);
				num4++;
				goto IL_0121;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant2);
				throw;
			}
			IL_0121:
			global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant2);
			// IL initblk instruction
			Unsafe.InitBlock(ref cComPropVariant2, 0, 16);
			try
			{
				num5 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint*, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)ptr + 36)))((nint)ptr, (uint)num4, &atom2, (tagPROPVARIANT*)(&cComPropVariant2));
				if (num5 >= 0)
				{
					continue;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant2);
				throw;
			}
			break;
		}
		goto IL_0159;
		IL_0159:
		global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant2);
	}

	public AutoPlaylistBuilder(EMediaTypes type)
	{
		InitializeNewBuilder(type);
	}

	public AutoPlaylistBuilder()
	{
		InitializeNewBuilder(EMediaTypes.eMediaTypeAudio);
	}

	private unsafe void _007EAutoPlaylistBuilder()
	{
		IAutoPlaylistRules* pAutoPlaylistRules = m_pAutoPlaylistRules;
		if (null != pAutoPlaylistRules)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pAutoPlaylistRules + 8)))((nint)pAutoPlaylistRules);
			m_pAutoPlaylistRules = null;
		}
	}

	public unsafe void Clear()
	{
		m_currentRuleSetGroup = 0;
		IAutoPlaylistRules* pAutoPlaylistRules = m_pAutoPlaylistRules;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pAutoPlaylistRules + 12)))((nint)pAutoPlaylistRules);
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		// IL initblk instruction
		Unsafe.InitBlock(ref cComPropVariant, 0, 16);
		try
		{
			*(short*)(&cComPropVariant) = 3;
			Unsafe.As<CComPropVariant, short>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)) = 1;
			IAutoPlaylistRules* pAutoPlaylistRules2 = m_pAutoPlaylistRules;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaTypes, uint, uint, EAutoPlaylistRuleOperators, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)pAutoPlaylistRules2 + 16)))((nint)pAutoPlaylistRules2, m_type, (uint)m_currentRuleSetGroup, 204u, (EAutoPlaylistRuleOperators)1, (tagPROPVARIANT*)(&cComPropVariant));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
			throw;
		}
		global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
	}

	public unsafe HRESULT AddCriterion(string atomName, PlaylistRuleOperator op, object value)
	{
		int num = ZuneQueryList.AtomNameToAtom(atomName);
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		// IL initblk instruction
		Unsafe.InitBlock(ref cComPropVariant, 0, 16);
		HRESULT result;
		try
		{
			int num2 = ZuneQueryList.ConvertTypeToPropVariant(null, value, (tagPROPVARIANT*)(&cComPropVariant));
			if (num2 >= 0)
			{
				IAutoPlaylistRules* pAutoPlaylistRules = m_pAutoPlaylistRules;
				num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaTypes, uint, uint, EAutoPlaylistRuleOperators, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)pAutoPlaylistRules + 16)))((nint)pAutoPlaylistRules, m_type, (uint)m_currentRuleSetGroup, (uint)num, (EAutoPlaylistRuleOperators)op, (tagPROPVARIANT*)(&cComPropVariant));
			}
			result = num2;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
			throw;
		}
		global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
		return result;
	}

	public unsafe HRESULT AddSort(string sort)
	{
		bool[] ascendings = null;
		string[] sorts = null;
		int num = 0;
		if (LibraryDataProvider.GetSortAttributes(sort, out sorts, out ascendings))
		{
			int num2 = 0;
			if (0 < (nint)sorts.LongLength)
			{
				do
				{
					int num3 = ZuneQueryList.AtomNameToAtom(sorts[num2]);
					IAutoPlaylistRules* pAutoPlaylistRules = m_pAutoPlaylistRules;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaTypes, uint, uint, int, int>)(int)(*(uint*)(*(int*)pAutoPlaylistRules + 24)))((nint)pAutoPlaylistRules, m_type, (uint)m_currentRuleSetGroup, (uint)num3, ascendings[num2] ? 1 : 0);
					if (num < 0)
					{
						break;
					}
					num2++;
				}
				while (num2 < (nint)sorts.LongLength);
			}
		}
		return num;
	}

	public unsafe HRESULT AddFilter(string atomName, object value)
	{
		int num = ZuneQueryList.AtomNameToAtom(atomName);
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		// IL initblk instruction
		Unsafe.InitBlock(ref cComPropVariant, 0, 16);
		HRESULT result;
		try
		{
			int num2 = ZuneQueryList.ConvertTypeToPropVariant(null, value, (tagPROPVARIANT*)(&cComPropVariant));
			if (num2 >= 0)
			{
				IAutoPlaylistRules* pAutoPlaylistRules = m_pAutoPlaylistRules;
				num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)pAutoPlaylistRules + 32)))((nint)pAutoPlaylistRules, (uint)num, (tagPROPVARIANT*)(&cComPropVariant));
			}
			result = num2;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
			throw;
		}
		global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
		return result;
	}

	public unsafe HRESULT SetRules(int playlistId)
	{
		IPlaylistManager* nativePlaylistManager = PlaylistManager.Instance.NativePlaylistManager;
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, IAutoPlaylistRules*, int>)(int)(*(uint*)(*(int*)nativePlaylistManager + 60)))((nint)nativePlaylistManager, playlistId, m_pAutoPlaylistRules);
	}

	public AtomRules GetCriterionByAtomName(int ruleSetGroup, string atomName)
	{
		AtomRules value = null;
		GroupAndAtom key = new GroupAndAtom(ruleSetGroup, ZuneQueryList.AtomNameToAtom(atomName));
		Dictionary<GroupAndAtom, AtomRules> groupAndAtomToRules = m_groupAndAtomToRules;
		if (groupAndAtomToRules != null && groupAndAtomToRules.TryGetValue(key, out value))
		{
			return value;
		}
		return null;
	}

	public object GetFilterByAtomName(string atomName)
	{
		return GetCriterionByAtomName(0, atomName)?.values[0];
	}

	public unsafe string GetSort(int ruleSetGroup)
	{
		StringBuilder stringBuilder = null;
		int num = 0;
		IAutoPlaylistRules* pAutoPlaylistRules = m_pAutoPlaylistRules;
		Unsafe.SkipInit(out uint atom);
		Unsafe.SkipInit(out int num3);
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaTypes, uint, uint, uint*, int*, int>)(int)(*(uint*)(*(int*)pAutoPlaylistRules + 28)))((nint)pAutoPlaylistRules, m_type, (uint)ruleSetGroup, 0u, &atom, &num3);
		if (num2 >= 0)
		{
			while (num2 != 1)
			{
				string value = ((num3 == 0) ? "-" : "+") + ZuneQueryList.AtomToAtomName((int)atom);
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(value);
				}
				else
				{
					stringBuilder.Append(",");
					stringBuilder.Append(value);
				}
				num++;
				pAutoPlaylistRules = m_pAutoPlaylistRules;
				num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaTypes, uint, uint, uint*, int*, int>)(int)(*(uint*)(*(int*)pAutoPlaylistRules + 28)))((nint)pAutoPlaylistRules, m_type, (uint)ruleSetGroup, (uint)num, &atom, &num3);
				if (num2 < 0)
				{
					break;
				}
			}
		}
		return stringBuilder?.ToString();
	}

	private unsafe void InitializeNewBuilder(EMediaTypes type)
	{
		IPlaylistManager* nativePlaylistManager = PlaylistManager.Instance.NativePlaylistManager;
		Unsafe.SkipInit(out IAutoPlaylistRules* pAutoPlaylistRules);
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IAutoPlaylistRules**, int>)(int)(*(uint*)(*(int*)nativePlaylistManager + 64)))((nint)nativePlaylistManager, &pAutoPlaylistRules);
		m_pAutoPlaylistRules = pAutoPlaylistRules;
		m_currentRuleSetGroup = 0;
		m_type = type;
		if (num < 0)
		{
			throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
		}
	}

	private unsafe void CacheCriterion(int group, int atom, tagPROPVARIANT* vtValue, EAutoPlaylistRuleOperators op)
	{
		AtomRules value = null;
		if (m_groupAndAtomToRules == null)
		{
			m_groupAndAtomToRules = new Dictionary<GroupAndAtom, AtomRules>();
		}
		GroupAndAtom key = new GroupAndAtom(group, atom);
		if (!m_groupAndAtomToRules.TryGetValue(key, out value))
		{
			value = new AtomRules();
			m_groupAndAtomToRules[key] = value;
		}
		object value2 = PropVariantToObject(vtValue);
		value.values.Add(value2);
		value.operators.Add((PlaylistRuleOperator)op);
	}

	private unsafe static object PropVariantToObject(tagPROPVARIANT* pvtValue)
	{
		Type type;
		switch ((int)(*(ushort*)pvtValue))
		{
		case 8:
			type = typeof(string);
			break;
		case 3:
			type = typeof(int);
			break;
		case 0:
		case 1:
			return null;
		default:
			type = null;
			break;
		}
		if ((object)type != null)
		{
			return ZuneQueryList.MarshalResult(type, pvtValue, null);
		}
		return null;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_007EAutoPlaylistBuilder();
		}
		else
		{
			base.Finalize();
		}
	}

	public virtual sealed void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
