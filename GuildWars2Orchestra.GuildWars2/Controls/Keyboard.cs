using System;
using System.Diagnostics;
using System.Linq;
using GuildWars2Orchestra.GuildWars2.Extern;

namespace GuildWars2Orchestra.GuildWars2.Controls
{
    public class Keyboard : IKeyboard
    {
        private readonly Process _process;

        public Keyboard()
        {
            switch (2)
            {
                case 1:
                    _process = Process.GetProcesses()
                        .First(p => p.ProcessName.Equals("notepad", StringComparison.OrdinalIgnoreCase));

                    PInvoke.SetForegroundWindow(_process.MainWindowHandle);
                    break;

                case 2:
                    _process = Process.GetProcesses()
                        .First(
                            p => p.ProcessName.Equals("GW2-64", StringComparison.OrdinalIgnoreCase) ||
                                 p.ProcessName.Equals("GW2", StringComparison.OrdinalIgnoreCase));

                    PInvoke.SetForegroundWindow(_process.MainWindowHandle);
                    break;

                case 3:
                    _process = Process.GetProcesses()
                        .First(p => p.ProcessName.Equals("notepad", StringComparison.OrdinalIgnoreCase));
                    break;

                case 4:
                    _process = Process.GetProcesses()
                        .First(
                            p => p.ProcessName.Equals("GW2-64", StringComparison.OrdinalIgnoreCase) ||
                                 p.ProcessName.Equals("GW2", StringComparison.OrdinalIgnoreCase));

                    break;
            }
        }

        public void Press(GuildWarsControls key)
        {
            ScanCodeShort scanCodeShort;
            VirtualKeyShort virtualKeyShort;

            scanCodeShort = ScanCodeShort(key, out scanCodeShort, out virtualKeyShort);

            var nInputs = new[]
            {
                new Input
                {
                    type = InputType.KEYBOARD, U = new InputUnion
                    {
                        ki = new KeybdInput
                        {
                            wScan = scanCodeShort, wVk = virtualKeyShort
                        }
                    }
                }
            };

            AttachInput();
            PInvoke.SendInput((uint) nInputs.Length, nInputs, Input.Size);
            DetachInput();
        }

        public void Release(GuildWarsControls key)
        {
            ScanCodeShort scanCodeShort;
            VirtualKeyShort virtualKeyShort;

            scanCodeShort = ScanCodeShort(key, out scanCodeShort, out virtualKeyShort);

            var nInputs = new[]
            {
                new Input
                {
                    type = InputType.KEYBOARD, U = new InputUnion
                    {
                        ki = new KeybdInput
                        {
                            wScan = scanCodeShort, wVk = virtualKeyShort, dwFlags = KeyEventF.KEYUP
                        }
                    }
                }
            };

            AttachInput();
            PInvoke.SendInput((uint) nInputs.Length, nInputs, Input.Size);
            DetachInput();
        }

        public void PressAndRelease(GuildWarsControls key)
        {
            throw new NotImplementedException();
        }

        private void AttachInput()
        {
            //var attachThreadInput = AttachThreadInput((uint) GetCurrentProcess().Id, (uint) _process.Id, true);
            uint currentThreadId = PInvoke.GetCurrentThreadId();
            uint temp;
            uint foregroundThreadId = PInvoke.GetWindowThreadProcessId(_process.MainWindowHandle, out temp);
            var attachThreadInput = PInvoke.AttachThreadInput(currentThreadId, foregroundThreadId, false);
        }

        private void DetachInput()
        {
            //var attachThreadInput = AttachThreadInput((uint) GetCurrentProcess().Id, (uint) _process.Id, false);
            uint currentThreadId = PInvoke.GetCurrentThreadId();
            uint temp;
            uint foregroundThreadId = PInvoke.GetWindowThreadProcessId(_process.MainWindowHandle, out temp);
            var attachThreadInput = PInvoke.AttachThreadInput(currentThreadId, foregroundThreadId, false);
        }

        private ProcessThread GetCurrentProcess()
        {
            var processThreads = Process.GetCurrentProcess().Threads
                .OfType<ProcessThread>();

            return processThreads
                .Single(p => p.ThreadState == ThreadState.Running);
        }

        private static ScanCodeShort ScanCodeShort(GuildWarsControls key, out ScanCodeShort scanCodeShort, out VirtualKeyShort virtualKeyShort)
        {
            switch (key)
            {
                case GuildWarsControls.WeaponSkill1:
                    scanCodeShort = Extern.ScanCodeShort.KEY_1;
                    virtualKeyShort = VirtualKeyShort.KEY_1;
                    break;
                case GuildWarsControls.WeaponSkill2:
                    scanCodeShort = Extern.ScanCodeShort.KEY_2;
                    virtualKeyShort = VirtualKeyShort.KEY_2;
                    break;
                case GuildWarsControls.WeaponSkill3:
                    scanCodeShort = Extern.ScanCodeShort.KEY_3;
                    virtualKeyShort = VirtualKeyShort.KEY_3;
                    break;
                case GuildWarsControls.WeaponSkill4:
                    scanCodeShort = Extern.ScanCodeShort.KEY_4;
                    virtualKeyShort = VirtualKeyShort.KEY_4;
                    break;
                case GuildWarsControls.WeaponSkill5:
                    scanCodeShort = Extern.ScanCodeShort.KEY_5;
                    virtualKeyShort = VirtualKeyShort.KEY_5;
                    break;
                case GuildWarsControls.HealingSkill:
                    scanCodeShort = Extern.ScanCodeShort.KEY_6;
                    virtualKeyShort = VirtualKeyShort.KEY_6;
                    break;
                case GuildWarsControls.UtilitySkill1:
                    scanCodeShort = Extern.ScanCodeShort.KEY_7;
                    virtualKeyShort = VirtualKeyShort.KEY_7;
                    break;
                case GuildWarsControls.UtilitySkill2:
                    scanCodeShort = Extern.ScanCodeShort.KEY_8;
                    virtualKeyShort = VirtualKeyShort.KEY_8;
                    break;
                case GuildWarsControls.UtilitySkill3:
                    scanCodeShort = Extern.ScanCodeShort.KEY_9;
                    virtualKeyShort = VirtualKeyShort.KEY_9;
                    break;
                case GuildWarsControls.EliteSkill:
                    scanCodeShort = Extern.ScanCodeShort.KEY_0;
                    virtualKeyShort = VirtualKeyShort.KEY_0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
            return scanCodeShort;
        }
    }
}