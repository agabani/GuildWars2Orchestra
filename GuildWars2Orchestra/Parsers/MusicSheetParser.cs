using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GuildWars2Orchestra.Music;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Parsers
{
    public class MusicSheetParser
    {
        private static readonly Regex NonWhitespace = new Regex(@"[^\s]+");
        private readonly ChordParser _chordParser;

        public MusicSheetParser(ChordParser chordParser)
        {
            _chordParser = chordParser;
        }

        public MusicSheet Parse(string text, int metronome, int nominator, int denominator)
        {
            return new MusicSheet(new MetronomeMark(metronome, new Fraction(nominator, denominator)), ParseMelody(text));
        }

        private IEnumerable<ChordOffset> ParseMelody(string text)
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