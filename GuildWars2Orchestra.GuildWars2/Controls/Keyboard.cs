using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using GuildWars2Orchestra.GuildWars2.PInvoke;

namespace GuildWars2Orchestra.GuildWars2.Controls
{
    public class Keyboard : IKeyboard
    {
        public Keyboard()
        {
/*
            var process = Process.GetProcesses()
                .First(p => p.ProcessName.Equals("notepad", StringComparison.OrdinalIgnoreCase));
*/

            var process = Process.GetProcesses()
                .First(
                    p => p.ProcessName.Equals("GW2-64", StringComparison.OrdinalIgnoreCase) ||
                         p.ProcessName.Equals("GW2", StringComparison.OrdinalIgnoreCase));

            SetForegroundWindow(process.MainWindowHandle);


/*            var thisProcess = Process.GetCurrentProcess();

          
            /*var process = Process.GetProcesses()
                .FirstOrDefault(
                    p => p.ProcessName.Equals("GW2-64", StringComparison.OrdinalIgnoreCase) ||
                         p.ProcessName.Equals("GW2", StringComparison.OrdinalIgnoreCase));#1#

            var processThreads = thisProcess.Threads
                .OfType<ProcessThread>();

/*            var thisProcessThread = processThreads
                .First(p => p.ThreadState == ThreadState.Running);#1#

            var processThread = _targetProcess.Threads
                .OfType<ProcessThread>()
                .First();

            foreach (var thread in processThreads)
            {
                AttachThreadInput((uint) thread.Id, (uint) processThread.Id, true);
            }*/
        }

        public void Press(GuildWarsKeyboard.Controls key)
        {
            ScanCodeShort scanCodeShort;
            VirtualKeyShort virtualKeyShort;

            scanCodeShort = ScanCodeShort(key, out scanCodeShort, out virtualKeyShort);

            var nInputs = new[]
            {
                new INPUT
                {
                    type = InputType.KEYBOARD, U = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wScan = scanCodeShort, wVk = virtualKeyShort
                        }
                    }
                }
            };

            //AttachInput();
            SendInput((uint) nInputs.Length, nInputs, INPUT.Size);
            //DetachInput();
        }

        public void Release(GuildWarsKeyboard.Controls key)
        {
            ScanCodeShort scanCodeShort;
            VirtualKeyShort virtualKeyShort;

            scanCodeShort = ScanCodeShort(key, out scanCodeShort, out virtualKeyShort);

            var nInputs = new[]
            {
                new INPUT
                {
                    type = InputType.KEYBOARD, U = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wScan = scanCodeShort, wVk = virtualKeyShort, dwFlags = KEYEVENTF.KEYUP
                        }
                    }
                }
            };

            //AttachInput();
            SendInput((uint) nInputs.Length, nInputs, INPUT.Size);
            //DetachInput();
        }

        public void PressAndRelease(GuildWarsKeyboard.Controls key)
        {
            throw new NotImplementedException();
        }

        private void AttachInput()
        {
            var id = Process.GetCurrentProcess().Threads
                .OfType<ProcessThread>()
                .Single(p => p.ThreadState == ThreadState.Running).Id;

            AttachThreadInput((uint) id, (uint) Process.GetProcesses()
                .FirstOrDefault(
                    p => p.ProcessName.Equals("notepad", StringComparison.OrdinalIgnoreCase)).Id, true);
        }

        private void DetachInput()
        {
            var id = Process.GetCurrentProcess().Threads
                .OfType<ProcessThread>()
                .Single(p => p.ThreadState == ThreadState.Running).Id;

            AttachThreadInput((uint) id, (uint) Process.GetProcesses()
                .FirstOrDefault(
                    p => p.ProcessName.Equals("notepad", StringComparison.OrdinalIgnoreCase)).Id, false);
        }

        private static ScanCodeShort ScanCodeShort(GuildWarsKeyboard.Controls key, out ScanCodeShort scanCodeShort, out VirtualKeyShort virtualKeyShort)
        {
            switch (key)
            {
                case GuildWarsKeyboard.Controls.WeaponSkill1:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_1;
                    virtualKeyShort = VirtualKeyShort.KEY_1;
                    break;
                case GuildWarsKeyboard.Controls.WeaponSkill2:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_2;
                    virtualKeyShort = VirtualKeyShort.KEY_2;
                    break;
                case GuildWarsKeyboard.Controls.WeaponSkill3:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_3;
                    virtualKeyShort = VirtualKeyShort.KEY_3;
                    break;
                case GuildWarsKeyboard.Controls.WeaponSkill4:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_4;
                    virtualKeyShort = VirtualKeyShort.KEY_4;
                    break;
                case GuildWarsKeyboard.Controls.WeaponSkill5:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_5;
                    virtualKeyShort = VirtualKeyShort.KEY_5;
                    break;
                case GuildWarsKeyboard.Controls.HealingSkill:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_6;
                    virtualKeyShort = VirtualKeyShort.KEY_6;
                    break;
                case GuildWarsKeyboard.Controls.UtilitySkill1:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_7;
                    virtualKeyShort = VirtualKeyShort.KEY_7;
                    break;
                case GuildWarsKeyboard.Controls.UtilitySkill2:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_8;
                    virtualKeyShort = VirtualKeyShort.KEY_8;
                    break;
                case GuildWarsKeyboard.Controls.UtilitySkill3:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_9;
                    virtualKeyShort = VirtualKeyShort.KEY_9;
                    break;
                case GuildWarsKeyboard.Controls.EliteSkill:
                    scanCodeShort = PInvoke.ScanCodeShort.KEY_0;
                    virtualKeyShort = VirtualKeyShort.KEY_0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
            return scanCodeShort;
        }

        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}