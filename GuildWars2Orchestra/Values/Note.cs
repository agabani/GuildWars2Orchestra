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

        public override bool Equals(object obj)
        {
            return Equals((Note) obj);
        }

        protected bool Equals(Note other)
        {
            return Equals(Length, other.Length) && Key == other.Key && Octave == other.Octave;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Length?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (int) Key;
                hashCode = (hashCode*397) ^ (int) Octave;
                return hashCode;
            }
        }
    }
}