using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class UrlHelper
{
	private static string _endPoint = null;

	private static Size[] _acceptableValues = (Size[])(object)new Size[25]
	{
		new Size(1920, 1080),
		new Size(1280, 720),
		new Size(854, 480),
		new Size(853, 480),
		new Size(480, 480),
		new Size(420, 320),
		new Size(320, 320),
		new Size(258, 258),
		new Size(258, 194),
		new Size(240, 240),
		new Size(234, 320),
		new Size(172, 258),
		new Size(160, 160),
		new Size(160, 120),
		new Size(107, 160),
		new Size(100, 100),
		new Size(72, 72),
		new Size(64, 64),
		new Size(64, 48),
		new Size(60, 60),
		new Size(52, 52),
		new Size(50, 50),
		new Size(44, 44),
		new Size(43, 64),
		new Size(40, 40)
	};

	private static int[] _acceptableWidths = new int[22]
	{
		1920, 1280, 854, 853, 480, 420, 320, 258, 240, 234,
		172, 160, 107, 100, 72, 64, 60, 52, 50, 44,
		43, 40
	};

	private static int[] _acceptableHeights = new int[18]
	{
		1080, 720, 480, 320, 258, 240, 194, 160, 120, 100,
		72, 64, 60, 52, 50, 48, 44, 40
	};

	public static string GetReturnUrl()
	{
		string endPointUri = Service.GetEndPointUri((EServiceEndpointId)12);
		return endPointUri + Shell.LoadString(StringId.IDS_RETURN_URL);
	}

	public static string MakeUrlWithReturnParams(string urlPath)
	{
		string returnUrl = GetReturnUrl();
		return MakeUrl(urlPath, "ru", returnUrl, "aru", returnUrl);
	}

	public static string MakeUrl(string urlPath)
	{
		return MakeUrlEx(urlPath, (string[])null);
	}

	public static string MakeUrl(string urlPath, string paramName1, string paramValue1)
	{
		return MakeUrlEx(urlPath, paramName1, paramValue1);
	}

	public static string MakeUrl(string urlPath, string paramName1, string paramValue1, string paramName2, string paramValue2)
	{
		return MakeUrlEx(urlPath, paramName1, paramValue1, paramName2, paramValue2);
	}

	public static string MakeUrl(string urlPath, string paramName1, string paramValue1, string paramName2, string paramValue2, string paramName3, string paramValue3)
	{
		return MakeUrlEx(urlPath, paramName1, paramValue1, paramName2, paramValue2, paramName3, paramValue3);
	}

	public static string MakeUrl(string urlPath, string paramName1, string paramValue1, string paramName2, string paramValue2, string paramName3, string paramValue3, string paramName4, string paramValue4)
	{
		return MakeUrlEx(urlPath, paramName1, paramValue1, paramName2, paramValue2, paramName3, paramValue3, paramName4, paramValue4);
	}

	public static string MakeUrlEx(string urlPath, params string[] args)
	{
		StringBuilder stringBuilder = new StringBuilder(256);
		stringBuilder.Append(Uri.EscapeUriString(urlPath));
		if (args != null && args.Length > 0)
		{
			for (int i = 0; i + 1 < args.Length; i += 2)
			{
				AppendParam(i == 0, stringBuilder, args[i], args[i + 1]);
			}
		}
		return stringBuilder.ToString();
	}

	public static void AppendParam(bool first, StringBuilder args, string paramName, string paramValue)
	{
		if (first)
		{
			args.Append("?");
		}
		else
		{
			args.Append('&');
		}
		args.Append(paramName);
		args.Append('=');
		args.Append(Uri.EscapeDataString(paramValue));
	}

	public static string PrependHttpIfMissing(string url)
	{
		if (!url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
		{
			StringBuilder stringBuilder = new StringBuilder("http://", url.Length + 7);
			stringBuilder.Append(url);
			return stringBuilder.ToString();
		}
		return url;
	}

	public static bool ValidUri(string url)
	{
		try
		{
			new Uri(url);
		}
		catch (Exception)
		{
			return false;
		}
		return true;
	}

	public static string StripVersion(string url)
	{
		Match match = Regex.Match(url, "^/v[0-9]+\\.[0-9]+/");
		if (match.Success)
		{
			url = url.Substring(url.IndexOf("/", 1));
		}
		return url;
	}

	public static string StripLocale(string url)
	{
		Match match = Regex.Match(url, "^/[a-zA-Z]{2}-[a-zA-Z]{2}/");
		if (match.Success)
		{
			url = url.Substring(url.IndexOf("/", 1));
		}
		return url;
	}

	private static void FindMatchingHeight(ref int width, out int height)
	{
		int i;
		for (i = 0; i < _acceptableWidths.Length - 1 && width <= _acceptableWidths[i + 1]; i++)
		{
		}
		width = _acceptableWidths[i];
		height = 0;
		Size[] acceptableValues = _acceptableValues;
		foreach (Size val in acceptableValues)
		{
			if (val.Width == width)
			{
				height = val.Height;
				break;
			}
		}
	}

	private static void FindMatchingWidth(out int width, ref int height)
	{
		int i;
		for (i = 0; i < _acceptableHeights.Length - 1 && height <= _acceptableHeights[i + 1]; i++)
		{
		}
		height = _acceptableHeights[i];
		width = 0;
		Size[] acceptableValues = _acceptableValues;
		foreach (Size val in acceptableValues)
		{
			if (val.Height == height)
			{
				width = val.Width;
				break;
			}
		}
	}

	private static void CalculateImageUriSize(ref int width, ref int height)
	{
		if (width == 0)
		{
			if (height == 0)
			{
				width = ImageConstants.Default.Width;
				height = ImageConstants.Default.Height;
			}
			else
			{
				FindMatchingWidth(out width, ref height);
			}
			return;
		}
		if (height == 0)
		{
			FindMatchingHeight(ref width, out height);
			return;
		}
		int i;
		for (i = 0; i < _acceptableValues.Length - 1 && width <= _acceptableValues[i + 1].Width; i++)
		{
		}
		for (width = _acceptableValues[i].Width; i < _acceptableValues.Length - 1 && width == _acceptableValues[i + 1].Width && height <= _acceptableValues[i + 1].Height; i++)
		{
		}
		height = _acceptableValues[i].Height;
	}

	public static string MakeCatalogImageUri(Guid imageId)
	{
		return MakeCatalogImageUri(imageId, 0, 0);
	}

	public static string MakeCatalogImageUri(Guid imageId, int width, int height)
	{
		return MakeCatalogImageUri(imageId, width, height, ImageIdType.ImageId, ImageRequested.PrimaryImage);
	}

	public static string MakeCatalogImageUri(Guid imageId, int width, int height, ImageIdType imageIdType, ImageRequested requestedImage)
	{
		return MakeCatalogImageUri(imageId, width, height, imageIdType, requestedImage, forceImageResize: false, ignoreZeroLengths: false);
	}

	public static string MakeCatalogImageUri(Guid imageId, int width, int height, ImageIdType imageIdType, ImageRequested requestedImage, bool forceImageResize, bool ignoreZeroLengths)
	{
		if (_endPoint == null)
		{
			_endPoint = Service.GetEndPointUri((EServiceEndpointId)30);
		}
		string empty = string.Empty;
		empty = imageIdType switch
		{
			ImageIdType.MovieId => $"{_endPoint}/movie/{imageId.ToString()}/{requestedImage.ToString()}", 
			ImageIdType.ArtistId => $"{_endPoint}/music/artist/{imageId.ToString()}/{requestedImage.ToString()}", 
			ImageIdType.MovieTrailerId => $"{_endPoint}/movieTrailer/{imageId.ToString()}/{requestedImage.ToString()}", 
			ImageIdType.ParentalRatingId => $"{Service.GetEndPointUri((EServiceEndpointId)16)}/apps/{imageId.ToString()}/ratingImage", 
			_ => $"{_endPoint}/image/{imageId.ToString()}", 
		};
		List<string> list = new List<string>();
		if (forceImageResize)
		{
			list.AddRange(new string[2] { "resize", "true" });
		}
		int width2 = width;
		int height2 = height;
		if (width2 != ImageConstants.NowPlaying.Width && height2 != ImageConstants.NowPlaying.Height)
		{
			CalculateImageUriSize(ref width2, ref height2);
		}
		if (width > 0 || !ignoreZeroLengths)
		{
			list.AddRange(new string[2]
			{
				"width",
				width2.ToString()
			});
		}
		if (height > 0 || !ignoreZeroLengths)
		{
			list.AddRange(new string[2]
			{
				"height",
				height2.ToString()
			});
		}
		return MakeUrlEx(empty, list.ToArray());
	}
}
