using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class Review : XmlDataProviderObject
{
	internal DateTime Date => (DateTime)GetProperty("Date");

	internal string Title => (string)GetProperty("Title");

	internal string Comment => (string)GetProperty("Comment");

	internal string UserName => (string)GetProperty("UserName");

	internal float Rating => (float)GetProperty("Rating");

	internal static XmlDataProviderObject ConstructReviewObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Review(owner, objectTypeCookie);
	}

	internal Review(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
