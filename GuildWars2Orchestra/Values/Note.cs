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

        public Note(Keys key, Octaves octave, Fraction length)
        {
            Length = length;
            Key = key;
            Octave = octave;
        }

        public Fraction Length { get; }

        public Keys Key { get; }
        public Octaves Octave { get; }

        public override string ToString()
        {
            return $"{(Octave == Octaves.High ? "▲" : Octave == Octaves.Low ? "▼" : string.Empty)}{Key} {Length}";
        }
    }
}