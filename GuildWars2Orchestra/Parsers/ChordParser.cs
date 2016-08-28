using System.Linq;
using System.Text.RegularExpressions;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Parsers
{
    public class ChordParser
    {
        private static readonly Regex ComponentRegex = new Regex(@"\[([ABCDEFGabcdefg',]+)\](\d+)?\/?(\d+)?");
        private static readonly Regex NoteRegex = new Regex(@"([ABCDEFGabcdefg][,']?)");
        private readonly NoteParser _noteParser;

        public ChordParser(NoteParser noteParser)
        {
            _noteParser = noteParser;
        }

        public Note[] Parse(string text)
        {
            var match = ComponentRegex.Match(text);

            var chord = match.Groups[1].Value;
            var nominator = match.Groups[2].Value;
            var denomintor = match.Groups[3].Value;

            var lengthText = string.IsNullOrEmpty(denomintor) ? nominator : $"{nominator}/{denomintor}";

            return NoteRegex.Matches(chord)
                .Cast<Match>()
                .Select(x => _noteParser.Parse(x.Groups[1].Value + lengthText))
                .ToArray();
        }
    }
}