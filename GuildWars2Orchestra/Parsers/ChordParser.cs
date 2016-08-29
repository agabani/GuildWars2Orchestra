using System.Linq;
using System.Text.RegularExpressions;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Parsers
{
    public class ChordParser
    {
        private static readonly Regex MultipleNotesAndDurationRegex = new Regex(@"\[([ABCDEFGabcdefg',]+)\](\d+)?\/?(\d+)?");
        private static readonly Regex SingleNoteAndDurationRegex = new Regex(@"([ABCDEFGabcdefg',]+)(\d+)?\/?(\d+)?");
        private static readonly Regex NoteRegex = new Regex(@"([ABCDEFGabcdefg][,']?)");
        private readonly NoteParser _noteParser;
        private Fraction _notesPerBeat;

        public ChordParser(NoteParser noteParser)
        {
            _noteParser = noteParser;
        }

        public ChordParser WithNotesPerBeat(Fraction notesPerBeat)
        {
            _notesPerBeat = notesPerBeat;
            return this;
        }

        public Chord Parse(string text)
        {
            var notesAndDuration = IsSingleNote(text)
                ? SingleNoteAndDurationRegex.Match(text)
                : MultipleNotesAndDurationRegex.Match(text);

            var notes = notesAndDuration.Groups[1].Value;
            var nominator = notesAndDuration.Groups[2].Value;
            var denomintor = notesAndDuration.Groups[3].Value;

            var length = ParseFraction(nominator, denomintor)*_notesPerBeat;

            return new Chord(NoteRegex.Matches(notes)
                .Cast<Match>()
                .Select(x => _noteParser.Parse(x.Groups[1].Value)),
                length);
        }

        private static Fraction ParseFraction(string nominator, string denominator)
        {
            return new Fraction(
                string.IsNullOrEmpty(nominator) ? 1 : int.Parse(nominator),
                string.IsNullOrEmpty(denominator) ? 1 : int.Parse(denominator));
        }

        private static bool IsSingleNote(string text)
        {
            return !text.StartsWith("[");
        }
    }
}