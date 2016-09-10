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
        public enum Controls
        {
            WeaponSkill1,
            WeaponSkill2,
            WeaponSkill3,
            WeaponSkill4,
            WeaponSkill5,
            HealingSkill,
            UtilitySkill1,
            UtilitySkill2,
            UtilitySkill3,
            EliteSkill,
        }

        private static readonly Dictionary<Controls, KeyboardKey> KeyboardKey = new Dictionary<Controls, KeyboardKey>
        {
            {Controls.WeaponSkill1, new KeyboardKey(Keys.D1)},
            {Controls.WeaponSkill2, new KeyboardKey(Keys.D2)},
            {Controls.WeaponSkill3, new KeyboardKey(Keys.D3)},
            {Controls.WeaponSkill4, new KeyboardKey(Keys.D4)},
            {Controls.WeaponSkill5, new KeyboardKey(Keys.D5)},
            {Controls.HealingSkill, new KeyboardKey(Keys.D6)},
            {Controls.UtilitySkill1, new KeyboardKey(Keys.D7)},
            {Controls.UtilitySkill2, new KeyboardKey(Keys.D8)},
            {Controls.UtilitySkill3, new KeyboardKey(Keys.D9)},
            {Controls.EliteSkill, new KeyboardKey(Keys.D0)}
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

        public void Press(Controls key)
        {
            KeyboardKey[key].Press();
        }

        public void Release(Controls key)
        {
            KeyboardKey[key].Release();
        }

        public void PressAndRelease(Controls key)
        {
            KeyboardKey[key].PressAndRelease();
        }
    }
}