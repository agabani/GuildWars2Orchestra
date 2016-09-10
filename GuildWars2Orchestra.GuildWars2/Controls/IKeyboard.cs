namespace GuildWars2Orchestra.GuildWars2.Controls
{
    public interface IKeyboard
    {
        void Press(GuildWarsKeyboard.GuildWarsSkill key);
        void Release(GuildWarsKeyboard.GuildWarsSkill key);
        void PressAndRelease(GuildWarsKeyboard.GuildWarsSkill key);
    }
}