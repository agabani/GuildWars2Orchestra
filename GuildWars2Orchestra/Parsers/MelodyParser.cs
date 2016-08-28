using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Parsers
{
    public class MelodyParser
    {
        private static readonly Regex NonWhitespace = new Regex(@"[^\s]+");
        private readonly ChordParser _chordParser;

        public MelodyParser(ChordParser chordParser)
        {
            _chordParser = chordParser;
        }

        public IEnumerable<ChordOffset> Parse(string text)
        {
            var beatCounter = 0m;

            return NonWhitespace.Matches(text)
                .Cast<Match>()
                .Select(x =>
                {
                    var chord = _chordParser.Parse(x.Value);
                    var chordOffset = new ChordOffset(chord, new Beat(beatCounter));
                    beatCounter += chord.Length;
                    return chordOffset;
                });
        }
    }
}