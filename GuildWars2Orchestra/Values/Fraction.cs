namespace GuildWars2Orchestra.Values
{
    public class Fraction
    {
        public Fraction(int nominator, int denominator)
        {
            Nominator = nominator;
            Denominator = denominator;
        }

        public int Nominator { get; }

        public int Denominator { get; }

        public override string ToString()
        {
            return $"{Nominator}/{Denominator}";
        }
    }
}