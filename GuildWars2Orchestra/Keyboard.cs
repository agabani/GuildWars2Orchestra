using System.Collections.Generic;
using System.Windows.Forms;
using ManagedWinapi;

namespace GuildWars2Orchestra
{
    public class Keyboard : IKeyboard
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

        public void Press(string key)
        {
            KeyboardKey[key].Press();
        }

        public void Release(string key)
        {
            KeyboardKey[key].Release();
        }
    }
}