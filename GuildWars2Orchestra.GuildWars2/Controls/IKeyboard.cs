namespace GuildWars2Orchestra.GuildWars2.Controls
{
    public interface IKeyboard
    {
        void Press(GuildWarsKeyboard.Controls key);
        void Release(GuildWarsKeyboard.Controls key);
        void PressAndRelease(GuildWarsKeyboard.Controls key);
    }
}