using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class Contributor : XmlDataProviderObject
{
	internal Guid ContributorId => (Guid)GetProperty("ContributorId");

	internal string ContributorName => (string)GetProperty("ContributorName");

	internal int RoleId => (int)GetProperty("RoleId");

	internal string RoleName => (string)GetProperty("RoleName");

	internal static XmlDataProviderObject ConstructContributorObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Contributor(owner, objectTypeCookie);
	}

	internal Contributor(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
