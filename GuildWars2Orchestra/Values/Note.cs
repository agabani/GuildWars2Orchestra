namespace GuildWars2Orchestra.Values
{
    public class Note
    {
        public enum Keys
        {
            None,
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
            None,
            Low,
            Middle,
            High
        }

        public Note(Keys key, Octaves octave)
        {
            Key = key;
            Octave = octave;
        }

        public Keys Key { get; }
        public Octaves Octave { get; }

        public override string ToString()
        {
            return $"{(Octave == Octaves.High ? "▲" : Octave == Octaves.Low ? "▼" : string.Empty)}{Key}";
        }

        public override bool Equals(object obj)
        {
            return Equals((Note) obj);
        }

        protected bool Equals(Note other)
        {
            return Key == other.Key && Octave == other.Octave;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Key*397) ^ (int) Octave;
            }
        }
    }
}