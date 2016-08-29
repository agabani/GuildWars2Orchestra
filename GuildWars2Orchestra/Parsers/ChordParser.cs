﻿using System.Linq;
using System.Text.RegularExpressions;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Parsers
{
    public class ChordParser
    {
        private static readonly Regex NotesAndDurationRegex = new Regex(@"\[?([ABCDEFGabcdefg',]+)\]?(\d+)?\/?(\d+)?");
        private static readonly Regex NoteRegex = new Regex(@"([ABCDEFGabcdefg][,']?)");
        private readonly NoteParser _noteParser;

        public ChordParser(NoteParser noteParser)
        {
            _noteParser = noteParser;
        }

        public Chord Parse(string text)
        {
            var notesAndDuration = NotesAndDurationRegex.Match(text);

            var notes = notesAndDuration.Groups[1].Value;
            var nominator = notesAndDuration.Groups[2].Value;
            var denomintor = notesAndDuration.Groups[3].Value;

            var length = ParseFraction(nominator, denomintor);

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
    }
}