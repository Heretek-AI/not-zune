using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

namespace MicrosoftZuneLibrary;

[StructLayout(LayoutKind.Sequential, Size = 32)]
[MiscellaneousBits(64)]
[NativeCppClass]
[DebugInfoInPDB]
internal struct FirmwareUpdateMediator
{
}
