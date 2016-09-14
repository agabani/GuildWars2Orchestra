using System.Runtime.InteropServices;

namespace GuildWars2Orchestra.GuildWars2.Extern
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Input
    {
        internal InputType type;
        internal InputUnion U;
        internal static int Size => Marshal.SizeOf(typeof(Input));
    }
}