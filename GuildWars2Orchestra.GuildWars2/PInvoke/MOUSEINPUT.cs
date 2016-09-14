using System;
using System.Runtime.InteropServices;

namespace GuildWars2Orchestra.GuildWars2.PInvoke
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MOUSEINPUT
    {
        internal int dx;
        internal int dy;
        internal int mouseData;
        internal MOUSEEVENTF dwFlags;
        internal uint time;
        internal UIntPtr dwExtraInfo;
    }
}