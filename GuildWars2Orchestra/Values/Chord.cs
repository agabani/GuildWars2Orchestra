using System.Collections.Generic;

namespace GuildWars2Orchestra.Values
{
    public class Chord
    {
        public Chord(IEnumerable<Note> notes, Fraction length)
        {
            Length = length;
            Notes = notes;
        }

        public Fraction Length { get; }

        public IEnumerable<Note> Notes { get; }

        public override string ToString()
        {
            return $"{string.Join(":", Notes)} {Length}";
        }
    }
}