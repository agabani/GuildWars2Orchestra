using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ManagedWinapi;
using ManagedWinapi.Windows;

namespace GuildWars2Orchestra.Controls
{
    public class GuildWarsKeyboard : IKeyboard
    {
        private static readonly Dictionary<string, KeyboardKey> KeyboardKey = new Dictionary<string, KeyboardKey>
        {
            {"0", new KeyboardKey(Keys.D0)},
            {"1", new KeyboardKey(Keys.D1)},
            {"2", new KeyboardKey(Keys.D2)},
            {"3", new KeyboardKey(Keys.D3)},
            {"4", new KeyboardKey(Keys.D4)},
            {"5", new KeyboardKey(Keys.D5)},
            {"6", new KeyboardKey(Keys.D6)},
            {"7", new KeyboardKey(Keys.D7)},
            {"8", new KeyboardKey(Keys.D8)},
            {"9", new KeyboardKey(Keys.D9)}
        };

        public GuildWarsKeyboard()
        {
            var process = Process.GetProcesses()
                .FirstOrDefault(
                    p => p.ProcessName.Equals("GW2-64", StringComparison.OrdinalIgnoreCase) ||
                         p.ProcessName.Equals("GW2", StringComparison.OrdinalIgnoreCase));

            if (process == null)
            {
                throw new ApplicationException("Guild Wars 2 is not running");
            }

            var systemWindow = SystemWindow.AllToplevelWindows
                .First(w => w.HWnd == process.MainWindowHandle);

            SystemWindow.ForegroundWindow = systemWindow;
        }

        public void Press(string key)
        {
            KeyboardKey[key].Press();
        }

        public void Release(string key)
        {
            KeyboardKey[key].Release();
        }

        public void PressAndRelease(string key)
        {
            KeyboardKey[key].PressAndRelease();
        }
    }
}