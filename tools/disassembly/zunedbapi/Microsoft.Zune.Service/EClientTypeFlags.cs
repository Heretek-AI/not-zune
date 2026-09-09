using System;

namespace Microsoft.Zune.Service;

[Flags]
public enum EClientTypeFlags
{
	Zune3Device = 2,
	PC = 1,
	None = 0,
	All = 0xFF
}
