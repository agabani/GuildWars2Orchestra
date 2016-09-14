namespace GuildWars2Orchestra.GuildWars2.Controls
{
    public interface IKeyboard
    {
        void Press(GuildWarsControls key);
        void Release(GuildWarsControls key);
    }
}