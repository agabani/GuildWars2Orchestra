namespace GuildWars2Orchestra.Values
{
    public class Note
    {
        public enum Keys
        {
            Note1,
            Note2,
            Note3,
            Note4,
            Note5,
            Note6,
            Note7,
            Note8
        }

        public enum Octaves
        {
            Low,
            Middle,
            High
        }

        public Note(Keys key, Octaves octave, int nominator, int denominator)
        {
            Denominator = denominator;
            Nominator = nominator;
            Key = key;
            Octave = octave;
        }

        public int Nominator { get; }
        public int Denominator { get; }

        public Keys Key { get; }
        public Octaves Octave { get; }

        public override string ToString()
        {
            return $"{(Octave == Octaves.High ? "▲" : Octave == Octaves.Low ? "▼" : string.Empty)}{Key} {Nominator}/{Denominator}";
        }
    }
}