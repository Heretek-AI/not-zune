using System;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune;

namespace ZuneUI;

public class FontLoader
{
	private List<string> _fonts;

	private string _resourceDll;

	private bool _loaded;

	private bool _loadQueued;

	public List<string> Fonts
	{
		get
		{
			return _fonts;
		}
		set
		{
			_fonts = value;
			QueueFontLoading();
		}
	}

	public string Resource
	{
		set
		{
			_resourceDll = value;
			QueueFontLoading();
		}
	}

	private void QueueFontLoading()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		if (_loaded)
		{
			throw new InvalidOperationException("This is a one trick pony, sorry.");
		}
		if (!_loadQueued)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(LoadFonts), (object)null);
			_loadQueued = true;
		}
	}

	private void LoadFonts(object args)
	{
		_loadQueued = false;
		if (_fonts == null || _fonts.Count == 0)
		{
			return;
		}
		if (_resourceDll == null)
		{
			throw new InvalidOperationException("Must specify a Resource to retrieve the fonts from.");
		}
		foreach (string font in _fonts)
		{
			bool flag = MemoryFonts.TryLoadFromResource(_resourceDll, font);
		}
		_loaded = true;
	}
}
