using System.Runtime.CompilerServices;

namespace Microsoft.Zune.Playlist;

internal struct GroupAndAtom(int group, int atom)
{
	private int groupAndAtom = ((ushort)group << 16) | (ushort)atom;

	public int Group => Unsafe.As<int, ushort>(ref Unsafe.AddByteOffset(ref groupAndAtom, 2));

	public int Atom => (ushort)groupAndAtom;
}
