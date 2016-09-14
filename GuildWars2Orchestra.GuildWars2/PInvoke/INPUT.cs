using System.Runtime.InteropServices;

namespace GuildWars2Orchestra.GuildWars2.PInvoke
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Input
    {
        internal InputType type;
        internal InputUnion U;
        internal static int Size
        {
            get { return Marshal.SizeOf(typeof(Input)); }
        }
    }
}