using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using GuildWars2Orchestra.GuildWars2.PInvoke;

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

                    SetForegroundWindow(_process.MainWindowHandle);
                    break;

                case 2:
                    _process = Process.GetProcesses()
                        .First(
                            p => p.ProcessName.Equals("GW2-64", StringComparison.OrdinalIgnoreCase) ||
                                 p.ProcessName.Equals("GW2", StringComparison.OrdinalIgnoreCase));

                    SetForegroundWindow(_process.MainWindowHandle);
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
            SendInput((uint) nInputs.Length, nInputs, Input.Size);
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
            SendInput((uint) nInputs.Length, nInputs, Input.Size);
            DetachInput();
        }

        public void PressAndRelease(GuildWarsControls key)
        {
            throw new NotImplementedException();
        }

        private void AttachInput()
        {
            //var attachThreadInput = AttachThreadInput((uint) GetCurrentProcess().Id, (uint) _process.Id, true);
            uint currentThreadId = GetCurrentThreadId();
            uint temp;
            uint foregroundThreadId = GetWindowThreadProcessId(_process.MainWindowHandle, out temp);
            var attachThreadInput = AttachThreadInput(currentThreadId, foregroundThreadId, false);
        }

        private void DetachInput()
        {
            //var attachThreadInput = AttachThreadInput((uint) GetCurrentProcess().Id, (uint) _process.Id, false);
            uint currentThreadId = GetCurrentThreadId();
            uint temp;
            uint foregroundThreadId = GetWindowThreadProcessId(_process.MainWindowHandle, out temp);
            var attachThreadInput = AttachThreadInput(currentThreadId, foregroundThreadId, false);
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
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_1;
                    virtualKeyShort = VirtualKeyShort.KEY_1;
                    break;
                case GuildWarsControls.WeaponSkill2:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_2;
                    virtualKeyShort = VirtualKeyShort.KEY_2;
                    break;
                case GuildWarsControls.WeaponSkill3:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_3;
                    virtualKeyShort = VirtualKeyShort.KEY_3;
                    break;
                case GuildWarsControls.WeaponSkill4:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_4;
                    virtualKeyShort = VirtualKeyShort.KEY_4;
                    break;
                case GuildWarsControls.WeaponSkill5:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_5;
                    virtualKeyShort = VirtualKeyShort.KEY_5;
                    break;
                case GuildWarsControls.HealingSkill:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_6;
                    virtualKeyShort = VirtualKeyShort.KEY_6;
                    break;
                case GuildWarsControls.UtilitySkill1:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_7;
                    virtualKeyShort = VirtualKeyShort.KEY_7;
                    break;
                case GuildWarsControls.UtilitySkill2:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_8;
                    virtualKeyShort = VirtualKeyShort.KEY_8;
                    break;
                case GuildWarsControls.UtilitySkill3:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_9;
                    virtualKeyShort = VirtualKeyShort.KEY_9;
                    break;
                case GuildWarsControls.EliteSkill:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_0;
                    virtualKeyShort = VirtualKeyShort.KEY_0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
            return scanCodeShort;
        }

        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] Input[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    }
}