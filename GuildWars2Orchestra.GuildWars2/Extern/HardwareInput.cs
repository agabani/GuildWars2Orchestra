using System.Runtime.InteropServices;

namespace GuildWars2Orchestra.GuildWars2.Extern
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct HardwareInput
    {
        internal int uMsg;
        internal short wParamL;
        internal short wParamH;
    }
}