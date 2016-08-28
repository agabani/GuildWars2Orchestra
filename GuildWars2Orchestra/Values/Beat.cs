namespace GuildWars2Orchestra.Values
{
    public class Beat
    {
        public Beat(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; private set; }
    }
}