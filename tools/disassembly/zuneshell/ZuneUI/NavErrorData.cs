using System;

namespace ZuneUI;

public class NavErrorData : IEquatable<NavErrorData>
{
	public string FailureUrl { get; private set; }

	public int ErrorCode { get; private set; }

	public NavErrorData(string failureUrl, int errorCode)
	{
		FailureUrl = failureUrl;
		ErrorCode = errorCode;
	}

	public bool Equals(NavErrorData other)
	{
		if (ErrorCode == other.ErrorCode)
		{
			return string.Compare(FailureUrl, other.FailureUrl, ignoreCase: false) == 0;
		}
		return false;
	}

	private NavErrorData()
	{
	}
}
