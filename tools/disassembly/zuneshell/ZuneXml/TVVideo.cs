using Microsoft.Iris;

namespace ZuneXml;

internal abstract class TVVideo : RatableVideo
{
	internal abstract string ProductionCompany { get; }

	internal abstract string Copyright { get; }

	internal abstract Network Network { get; }

	protected TVVideo(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
