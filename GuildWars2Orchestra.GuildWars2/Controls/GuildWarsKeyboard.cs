using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ManagedWinapi;
using ManagedWinapi.Windows;

namespace GuildWars2Orchestra.GuildWars2.Controls
{
    public class GuildWarsKeyboard : IKeyboard
    {
        public enum GuildWarsSkill
        {
            Skill0,
            Skill1,
            Skill2,
            Skill3,
            Skill4,
            Skill5,
            Skill6,
            Skill7,
            Skill8,
            Skill9
        }

        private static readonly Dictionary<GuildWarsSkill, KeyboardKey> KeyboardKey = new Dictionary<GuildWarsSkill, KeyboardKey>
        {
            {GuildWarsSkill.Skill0, new KeyboardKey(Keys.D0)},
            {GuildWarsSkill.Skill1, new KeyboardKey(Keys.D1)},
            {GuildWarsSkill.Skill2, new KeyboardKey(Keys.D2)},
            {GuildWarsSkill.Skill3, new KeyboardKey(Keys.D3)},
            {GuildWarsSkill.Skill4, new KeyboardKey(Keys.D4)},
            {GuildWarsSkill.Skill5, new KeyboardKey(Keys.D5)},
            {GuildWarsSkill.Skill6, new KeyboardKey(Keys.D6)},
            {GuildWarsSkill.Skill7, new KeyboardKey(Keys.D7)},
            {GuildWarsSkill.Skill8, new KeyboardKey(Keys.D8)},
            {GuildWarsSkill.Skill9, new KeyboardKey(Keys.D9)}
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

        public void Press(GuildWarsSkill key)
        {
            KeyboardKey[key].Press();
        }

        public void Release(GuildWarsSkill key)
        {
            KeyboardKey[key].Release();
        }

        public void PressAndRelease(GuildWarsSkill key)
        {
            KeyboardKey[key].PressAndRelease();
        }
    }
}