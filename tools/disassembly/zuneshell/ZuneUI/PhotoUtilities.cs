using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.Win32;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Util;

namespace ZuneUI;

public static class PhotoUtilities
{
	public static bool IsPhotoGalleryInstalled => InstalledProductChecker.IsInstalled(ClientConfiguration.Pictures.WinLivePhotoGalleryUpgradeCode, ClientConfiguration.Pictures.WinLivePhotoGalleryMajorVersion, ClientConfiguration.Pictures.WinLivePhotoGalleryMinorVersion);

	public static string PhotoGalleryExecutablePath
	{
		get
		{
			string empty = string.Empty;
			try
			{
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
				empty = Path.Combine(folderPath, Shell.LoadString(StringId.IDS_PHOTOS_PHOTO_GALLERY_PATH));
				if (File.Exists(empty))
				{
					return empty;
				}
			}
			catch (Exception)
			{
			}
			string empty2 = string.Empty;
			try
			{
				string environmentVariable = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
				empty2 = Path.Combine(environmentVariable, Shell.LoadString(StringId.IDS_PHOTOS_PHOTO_GALLERY_PATH));
				if (File.Exists(empty2))
				{
					return empty2;
				}
			}
			catch (Exception)
			{
			}
			return string.Empty;
		}
	}

	public static bool IsMovieMakerInstalled => InstalledProductChecker.IsInstalled(ClientConfiguration.Pictures.WinLiveMovieMakerUpgradeCode, ClientConfiguration.Pictures.WinLiveMovieMakerMajorVersion, ClientConfiguration.Pictures.WinLiveMovieMakerMinorVersion);

	public static string MovieMakerExecutablePath
	{
		get
		{
			string result = string.Empty;
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows Live\\Movie Maker");
				if (registryKey == null)
				{
					registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows Live\\Movie Maker");
				}
				if (registryKey == null)
				{
					throw new Exception("Could not open registry key");
				}
				string text = (string)registryKey.GetValue("InstallLocation", string.Empty);
				if (!string.IsNullOrEmpty(text))
				{
					result = Path.Combine(text, "MovieMaker.exe");
				}
			}
			catch (Exception)
			{
			}
			return result;
		}
	}

	public static bool IsPhotoGalleryAvailable
	{
		get
		{
			if (!OSVersion.IsXP())
			{
				return IsPhotoGalleryInstalled;
			}
			return false;
		}
	}

	public static bool IsMovieMakerAvailable
	{
		get
		{
			if (!OSVersion.IsXP())
			{
				return IsMovieMakerInstalled;
			}
			return false;
		}
	}

	public static void PlaySlideShow(int folderId, int startIndex)
	{
		PlaySlideShow(folderId, string.Empty, startIndex);
	}

	public static void PlaySlideShow(int folderId, string sort, int startIndex)
	{
		SlideShowState slideShowState = new SlideShowState(null);
		SlideshowLand slideshowLand = new SlideshowLand();
		slideshowLand.SlideShowState = slideShowState;
		slideShowState.Sort = sort;
		slideShowState.Index = ((startIndex >= 0) ? startIndex : 0);
		slideShowState.FolderId = folderId;
		ZuneShell.DefaultInstance.NavigateToPage(slideshowLand);
	}

	public static void ViewInPhotoGallery(string path)
	{
		if (!IsPhotoGalleryAvailable)
		{
			return;
		}
		try
		{
			string arguments = string.Empty;
			if (!string.IsNullOrEmpty(path))
			{
				arguments = ((!File.Exists(path)) ? string.Format(Shell.LoadString(StringId.IDS_PHOTOS_PHOTO_GALLERY_OPEN_FOLDER), path, path) : string.Format(Shell.LoadString(StringId.IDS_PHOTOS_PHOTO_GALLERY_OPEN_FILE), Path.GetDirectoryName(path), path));
			}
			if (!string.IsNullOrEmpty(PhotoGalleryExecutablePath))
			{
				Process process = new Process();
				process.StartInfo.FileName = PhotoGalleryExecutablePath;
				process.StartInfo.WorkingDirectory = Path.GetDirectoryName(PhotoGalleryExecutablePath);
				process.StartInfo.Arguments = arguments;
				process.Start();
				SQMLog.Log((SQMDataId)220, 1);
			}
		}
		catch (FileNotFoundException)
		{
		}
		catch (Win32Exception)
		{
		}
	}

	public static string WriteMovieMakerMedia(IList mediaFiles)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		string path = Guid.NewGuid().ToString() + ".xml";
		string text = Path.Combine(Path.GetTempPath(), path);
		XmlWriterSettings val = new XmlWriterSettings();
		val.Indent = true;
		XmlWriter val2 = XmlWriter.Create(text, val);
		try
		{
			val2.WriteStartElement("MovieMaker");
			val2.WriteStartElement("Content");
			foreach (string mediaFile in mediaFiles)
			{
				val2.WriteStartElement("ContentFile");
				val2.WriteAttributeString("Filename", mediaFile);
				val2.WriteEndElement();
			}
			val2.WriteEndElement();
			val2.WriteStartElement("AutoEdit");
			val2.WriteAttributeString("Style", "FadeReveal");
			val2.WriteEndElement();
			val2.WriteStartElement("DeleteOnClose");
			val2.WriteEndElement();
			val2.WriteEndElement();
			val2.Flush();
			return text;
		}
		finally
		{
			((IDisposable)val2)?.Dispose();
		}
	}

	public static void LaunchMovieMaker(IList mediaFiles)
	{
		if (!IsMovieMakerAvailable)
		{
			return;
		}
		string arg = WriteMovieMakerMedia(mediaFiles);
		string format = Shell.LoadString(StringId.IDS_PHOTOS_MOVIE_MAKER_ARGS);
		try
		{
			if (!string.IsNullOrEmpty(MovieMakerExecutablePath))
			{
				Process process = new Process();
				process.StartInfo.FileName = MovieMakerExecutablePath;
				process.StartInfo.WorkingDirectory = Path.GetDirectoryName(MovieMakerExecutablePath);
				process.StartInfo.Arguments = string.Format(format, arg);
				process.Start();
				SQMLog.Log((SQMDataId)219, 1);
			}
		}
		catch (FileNotFoundException)
		{
		}
		catch (Win32Exception)
		{
		}
	}
}
