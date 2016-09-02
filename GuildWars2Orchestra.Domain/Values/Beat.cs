namespace GuildWars2Orchestra.Domain.Values
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