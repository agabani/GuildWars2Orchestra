using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using GuildWars2Orchestra.Values;
using ManagedWinapi;
using ManagedWinapi.Windows;

namespace GuildWars2Orchestra
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var metronomeMark = new MetronomeMark(60);

            var process = Process.GetProcesses()
                .FirstOrDefault(
                    p => p.ProcessName.Equals("GW2-64", StringComparison.OrdinalIgnoreCase) ||
                         p.ProcessName.Equals("GW2", StringComparison.OrdinalIgnoreCase));

            if (process == null)
            {
                Console.WriteLine("Guild Wars 2 is not running.");
                Environment.Exit(1);
            }

            var systemWindow = SystemWindow.AllToplevelWindows.First(w => w.HWnd == process.MainWindowHandle);

            SystemWindow.ForegroundWindow = systemWindow;

            while (true)
            {
                WalkFoward(TimeSpan.FromSeconds(1));
                TurnRight(TimeSpan.FromSeconds(1));
                UseSkill2(TimeSpan.FromMilliseconds(100));
            }
        }

        private static void WalkFoward(TimeSpan duration)
        {
            var keyboardKey = new KeyboardKey(Keys.W);
            keyboardKey.Press();
            Thread.Sleep(duration);
            keyboardKey.Release();
        }

        private static void TurnRight(TimeSpan duration)
        {
            var keyboardKey = new KeyboardKey(Keys.D);
            keyboardKey.Press();
            Thread.Sleep(duration);
            keyboardKey.Release();
        }

        private static void UseSkill2(TimeSpan duration)
        {
            var keyboardKey = new KeyboardKey(Keys.D2);
            keyboardKey.Press();
            Thread.Sleep(duration);
            keyboardKey.Release();
        }
    }
}