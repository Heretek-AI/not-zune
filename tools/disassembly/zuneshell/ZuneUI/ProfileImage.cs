using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class ProfileImage
{
	private ProfileImageType m_type;

	private string m_typeName;

	private SafeBitmapWithData m_image;

	private string m_source;

	private static Size s_defaultTileSize;

	private static Size s_defaultBackgroundSize;

	public ProfileImageType Type => m_type;

	public string TypeName
	{
		get
		{
			if (m_typeName == null)
			{
				if (m_type == ProfileImageType.Background)
				{
					m_typeName = "background";
				}
				else if (m_type == ProfileImageType.Tile)
				{
					m_typeName = "usertile";
				}
				else
				{
					m_typeName = string.Empty;
				}
			}
			return m_typeName;
		}
	}

	public SafeBitmapWithData Image => m_image;

	public string ResourceId
	{
		get
		{
			string result = null;
			int num = -1;
			if (!string.IsNullOrEmpty(Source))
			{
				num = Source.IndexOf('!') + 1;
			}
			if (num > 0 && Source.Length > num)
			{
				result = Source.Substring(num);
			}
			return result;
		}
	}

	public string Source => m_source;

	public int Width
	{
		get
		{
			int result = 0;
			if (m_type == ProfileImageType.Background)
			{
				result = DefaultBackgroundSize.Width;
			}
			else if (m_type == ProfileImageType.Tile)
			{
				result = DefaultTileSize.Width;
			}
			return result;
		}
	}

	public int Height
	{
		get
		{
			int result = 0;
			if (m_type == ProfileImageType.Background)
			{
				result = DefaultBackgroundSize.Height;
			}
			else if (m_type == ProfileImageType.Tile)
			{
				result = DefaultTileSize.Height;
			}
			return result;
		}
	}

	public static Size DefaultTileSize
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			if (s_defaultTileSize == null)
			{
				s_defaultTileSize = new Size(64, 64);
			}
			return s_defaultTileSize;
		}
	}

	public static Size DefaultBackgroundSize
	{
		get
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			if (s_defaultBackgroundSize == null)
			{
				s_defaultBackgroundSize = new Size(535, 196);
			}
			return s_defaultBackgroundSize;
		}
	}

	public static string[] SupportedFileTypes
	{
		get
		{
			string text = Shell.LoadString(StringId.IDS_IMAGE_ALL_FILTER_NAME);
			string text2 = Shell.LoadString(StringId.IDS_IMAGE_JPEG_FILTER_NAME);
			string text3 = Shell.LoadString(StringId.IDS_IMAGE_PNG_FILTER_NAME);
			string text4 = Shell.LoadString(StringId.IDS_IMAGE_BITMAP_FILTER_NAME);
			string text5 = Shell.LoadString(StringId.IDS_IMAGE_GIF_FILTER_NAME);
			string text6 = Shell.LoadString(StringId.IDS_IMAGE_TIFF_FILTER_NAME);
			return new string[12]
			{
				text, "*.jpg;*.jpeg;*.jpe;*.jfif;*.png;*.bmp;*.dip;*.gif;*.tif;*.tiff", text4, "*.bmp;*.dib", text2, "*.jpg;*.jpeg;*.jpe;*.jfif", text5, "*.gif", text6, "*.tif;*.tiff",
				text3, "*.png"
			};
		}
	}

	public ProfileImage(ProfileImageType type, SafeBitmapWithData image)
	{
		m_type = type;
		m_image = image;
	}

	public ProfileImage(ProfileImageType type, SafeBitmapWithData image, int srcX, int srcY, int srcWidth, int srcHeight)
	{
		m_type = type;
		SafeBitmapWithData image2 = image.Clone(srcX, srcY, srcWidth, srcHeight, Width, Height);
		m_image = image2;
	}

	public ProfileImage(ProfileImageType type, string source)
	{
		m_type = type;
		m_source = source;
	}
}
