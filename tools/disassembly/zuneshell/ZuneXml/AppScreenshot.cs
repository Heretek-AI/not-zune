using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class AppScreenshot : Thumbnail
{
	internal override Guid ImageId => (Guid)GetProperty("ImageId");

	internal override string Id => (string)GetProperty("Id");

	internal override string Title => (string)GetProperty("Title");

	internal override string SortTitle => (string)GetProperty("SortTitle");

	internal static XmlDataProviderObject ConstructAppScreenshotObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new AppScreenshot(owner, objectTypeCookie);
	}

	internal AppScreenshot(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
