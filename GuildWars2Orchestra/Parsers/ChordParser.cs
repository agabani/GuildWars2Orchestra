using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Parsers
{
    public class ChordParser
    {
        private Fraction _notesPerBeat;

        public ChordParser(Fraction notesPerBeat)
        {
            _notesPerBeat = notesPerBeat;
        }

        public Note[] Parse(string text)
        {
            return new[]
            {
                new Note(Note.Keys.Note2, Note.Octaves.Low, new Fraction(1, 4)),
                new Note(Note.Keys.Note2, Note.Octaves.High, new Fraction(1, 4)),
                new Note(Note.Keys.Note4, Note.Octaves.High, new Fraction(1, 4)),
                new Note(Note.Keys.Note6, Note.Octaves.High, new Fraction(1, 4))
            };
        }
    }
}