using System;
using System.Collections.Specialized;
using Microsoft.Iris;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;
using UIXControls;

namespace ZuneUI;

public class AlbumArtUpdateHandler : ModelItem
{
	private const string _supportedExtensionsFilter = "*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.dib";

	private object _album;

	private string _path;

	private bool _done;

	private Image _image;

	private BooleanChoice _neverShowPasteMessage;

	private static string[] _supportedExtensions = new string[6] { ".bmp", ".jpg", ".jpeg", ".gif", ".png", ".dib" };

	public object Album => _album;

	public string Path
	{
		get
		{
			return _path;
		}
		private set
		{
			if (value != _path)
			{
				_path = value;
				((ModelItem)this).FirePropertyChanged("Path");
				Image = null;
			}
		}
	}

	public bool Done
	{
		get
		{
			return _done;
		}
		private set
		{
			if (value != _done)
			{
				_done = value;
				((ModelItem)this).FirePropertyChanged("Done");
			}
		}
	}

	public Image Image
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			if (_image == null && !string.IsNullOrEmpty(_path))
			{
				_image = new Image("file://" + _path);
			}
			return _image;
		}
		private set
		{
			if (value != _image)
			{
				_image = value;
				((ModelItem)this).FirePropertyChanged("Image");
			}
		}
	}

	public AlbumArtUpdateHandler(object album)
	{
		_album = album;
	}

	protected override void OnDispose(bool disposing)
	{
		if (_neverShowPasteMessage != null)
		{
			((ModelItem)_neverShowPasteMessage).Dispose();
			_neverShowPasteMessage = null;
		}
		((ModelItem)this).OnDispose(disposing);
	}

	public void Start()
	{
		Start(allowPaste: true);
	}

	public void Start(bool allowPaste)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		Done = false;
		Path = null;
		if (allowPaste && CanPaste())
		{
			if (ZuneShell.DefaultInstance.Management.ConfirmPasteAlbumArt)
			{
				if (_neverShowPasteMessage == null)
				{
					_neverShowPasteMessage = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_DONT_SHOW_THIS_MESSAGE_AGAIN));
				}
				string text = null;
				object album = Album;
				DataProviderObject val = (DataProviderObject)((album is DataProviderObject) ? album : null);
				if (val != null)
				{
					text = val.GetProperty("Title") as string;
				}
				string text2 = ((!string.IsNullOrEmpty(text)) ? string.Format(Shell.LoadString(StringId.IDS_CONFIRM_PASTE_ALBUM_ART), text) : Shell.LoadString(StringId.IDS_CONFIRM_PASTE_ALBUM_ART_NO_NAME));
				_neverShowPasteMessage.Value = false;
				MessageBox.Show(Shell.LoadString(StringId.IDS_CONFIRM_PASTE_ALBUM_ART_TITLE), text2, (EventHandler)null, (EventHandler)ConfirmPaste, (EventHandler)null, (EventHandler)null, _neverShowPasteMessage);
			}
			else
			{
				ConfirmPaste(this, null);
			}
		}
		else
		{
			BrowseForArt();
		}
	}

	public static bool CanPaste()
	{
		bool flag = Clipboard.ContainsData((ClipboardDataType)2);
		if (!flag && Clipboard.ContainsData((ClipboardDataType)15))
		{
			flag = GetClipboardFile() != null;
		}
		return flag;
	}

	private void BrowseForArt()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		FileOpenDialog.Show(Shell.LoadString(StringId.IDS_CHOOSE_ALBUM_ART), FileOpenDialog.MyPicturesPath, new string[2]
		{
			Shell.LoadString(StringId.IDS_ALBUM_ART_DIALOG_ALL_PICTURES),
			"*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.dib"
		}, (DeferredInvokeHandler)delegate(object args)
		{
			Path = (string)args;
			Done = true;
		});
	}

	private void ConfirmPaste(object sender, EventArgs args)
	{
		if (_neverShowPasteMessage != null)
		{
			ZuneShell.DefaultInstance.Management.ConfirmPasteAlbumArt = !_neverShowPasteMessage.Value;
		}
		if (Paste())
		{
			Done = true;
		}
		else
		{
			BrowseForArt();
		}
	}

	private bool Paste()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		bool flag = false;
		if (_album is LibraryDataProviderListItem)
		{
			LibraryDataProviderListItem val = (LibraryDataProviderListItem)_album;
			SafeBitmap image = Clipboard.GetImage();
			if (image != null)
			{
				flag = ((LibraryDataProviderItemBase)val).SetNewThumbnail(image);
			}
			if (!flag)
			{
				string clipboardFile = GetClipboardFile();
				if (clipboardFile != null)
				{
					Path = clipboardFile;
					flag = true;
				}
			}
		}
		return flag;
	}

	private static string GetClipboardFile()
	{
		StringCollection fileDropList = Clipboard.GetFileDropList();
		if (fileDropList != null)
		{
			StringEnumerator enumerator = fileDropList.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					string[] supportedExtensions = _supportedExtensions;
					foreach (string value in supportedExtensions)
					{
						if (current.EndsWith(value, StringComparison.InvariantCultureIgnoreCase))
						{
							return current;
						}
					}
				}
			}
			finally
			{
				if (enumerator is IDisposable disposable)
				{
					disposable.Dispose();
				}
			}
		}
		return null;
	}
}
