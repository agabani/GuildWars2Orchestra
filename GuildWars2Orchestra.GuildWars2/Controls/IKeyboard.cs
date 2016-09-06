namespace GuildWars2Orchestra.GuildWars2.Controls
{
    public interface IKeyboard
    {
        void Press(string key);
        void Release(string key);
        void PressAndRelease(string key);
    }
}