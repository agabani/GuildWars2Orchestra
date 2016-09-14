using System;
using System.Runtime.InteropServices;

namespace GuildWars2Orchestra.GuildWars2.Extern
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MouseInput
    {
        internal int dx;
        internal int dy;
        internal int mouseData;
        internal MouseEventF dwFlags;
        internal uint time;
        internal UIntPtr dwExtraInfo;
    }
}