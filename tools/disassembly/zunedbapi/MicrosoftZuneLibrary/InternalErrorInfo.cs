using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

namespace MicrosoftZuneLibrary;

[StructLayout(LayoutKind.Sequential, Size = 40)]
[NativeCppClass]
[MiscellaneousBits(64)]
[DebugInfoInPDB]
internal struct InternalErrorInfo
{
}
