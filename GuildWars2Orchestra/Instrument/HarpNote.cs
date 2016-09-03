namespace GuildWars2Orchestra.Instrument
{
    public class HarpNote
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

        public HarpNote(Keys key, Octaves octave)
        {
            Key = key;
            Octave = octave;
        }

        public Keys Key { get; }
        public Octaves Octave { get; }

        public override bool Equals(object obj)
        {
            return Equals((HarpNote) obj);
        }

        protected bool Equals(HarpNote other)
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