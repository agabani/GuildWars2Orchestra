﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GuildWars2Orchestra.GuildWars2.Extern;

namespace GuildWars2Orchestra.GuildWars2.Controls
{
    public class Keyboard : IKeyboard
    {
        private static readonly Dictionary<GuildWarsControls, ScanCodeShort> ScanCodeShorts = new Dictionary<GuildWarsControls, ScanCodeShort>
        {
            {GuildWarsControls.WeaponSkill1, ScanCodeShort.KEY_1},
            {GuildWarsControls.WeaponSkill2, ScanCodeShort.KEY_2},
            {GuildWarsControls.WeaponSkill3, ScanCodeShort.KEY_3},
            {GuildWarsControls.WeaponSkill4, ScanCodeShort.KEY_4},
            {GuildWarsControls.WeaponSkill5, ScanCodeShort.KEY_5},
            {GuildWarsControls.HealingSkill, ScanCodeShort.KEY_6},
            {GuildWarsControls.UtilitySkill1, ScanCodeShort.KEY_7},
            {GuildWarsControls.UtilitySkill2, ScanCodeShort.KEY_8},
            {GuildWarsControls.UtilitySkill3, ScanCodeShort.KEY_9},
            {GuildWarsControls.EliteSkill, ScanCodeShort.KEY_0}
        };

        private static readonly Dictionary<GuildWarsControls, VirtualKeyShort> VirtualKeyShorts = new Dictionary<GuildWarsControls, VirtualKeyShort>
        {
            {GuildWarsControls.WeaponSkill1, VirtualKeyShort.KEY_1},
            {GuildWarsControls.WeaponSkill2, VirtualKeyShort.KEY_2},
            {GuildWarsControls.WeaponSkill3, VirtualKeyShort.KEY_3},
            {GuildWarsControls.WeaponSkill4, VirtualKeyShort.KEY_4},
            {GuildWarsControls.WeaponSkill5, VirtualKeyShort.KEY_5},
            {GuildWarsControls.HealingSkill, VirtualKeyShort.KEY_6},
            {GuildWarsControls.UtilitySkill1, VirtualKeyShort.KEY_7},
            {GuildWarsControls.UtilitySkill2, VirtualKeyShort.KEY_8},
            {GuildWarsControls.UtilitySkill3, VirtualKeyShort.KEY_9},
            {GuildWarsControls.EliteSkill, VirtualKeyShort.KEY_0}
        };

        public Keyboard()
        {
            var mainWindowHandle = Process.GetProcesses()
                .First(
                    p => p.ProcessName.Equals("GW2-64", StringComparison.OrdinalIgnoreCase) ||
                         p.ProcessName.Equals("GW2", StringComparison.OrdinalIgnoreCase)).MainWindowHandle;

            PInvoke.SetForegroundWindow(mainWindowHandle);
        }

        public void Press(GuildWarsControls key)
        {
            var nInputs = new[]
            {
                new Input
                {
                    type = InputType.KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KeybdInput
                        {
                            wScan = ScanCodeShorts[key],
                            wVk = VirtualKeyShorts[key]
                        }
                    }
                }
            };

            PInvoke.SendInput((uint) nInputs.Length, nInputs, Input.Size);
        }

        public void Release(GuildWarsControls key)
        {
            var nInputs = new[]
            {
                new Input
                {
                    type = InputType.KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KeybdInput
                        {
                            wScan = ScanCodeShorts[key],
                            wVk = VirtualKeyShorts[key],
                            dwFlags = KeyEventF.KEYUP
                        }
                    }
                }
            };

            PInvoke.SendInput((uint) nInputs.Length, nInputs, Input.Size);
        }
    }
}