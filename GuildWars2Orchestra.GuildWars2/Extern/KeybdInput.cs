using System;
using System.Runtime.InteropServices;

namespace GuildWars2Orchestra.GuildWars2.Extern
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct KeybdInput
    {
        internal VirtualKeyShort wVk;
        internal ScanCodeShort wScan;
        internal KeyEventF dwFlags;
        internal int time;
        internal UIntPtr dwExtraInfo;
    }
}