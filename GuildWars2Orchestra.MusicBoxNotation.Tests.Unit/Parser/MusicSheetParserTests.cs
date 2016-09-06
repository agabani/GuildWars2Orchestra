using System;
using System.Linq;
using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.MusicBoxNotation.Parsers;
using GuildWars2Orchestra.MusicBoxNotation.Tests.Unit.TestData;
using NUnit.Framework;

namespace GuildWars2Orchestra.MusicBoxNotation.Tests.Unit.Parser
{
    [TestFixture]
    internal class MusicSheetParserTests
    {
        private static void AssertChord(ChordOffset chordOffset, Note.Keys expectedNote, Note.Octaves expectedOctave, decimal expectedOffset, Fraction expectedFraction)
        {
            Assert.That(chordOffset.Offest.Value, Is.EqualTo(expectedOffset));
            Assert.That(chordOffset.Chord.Length, Is.EqualTo(expectedFraction));
            var note = chordOffset.Chord.Notes.ElementAt(0);
            Assert.That(note.Key, Is.EqualTo(expectedNote));
            Assert.That(note.Octave, Is.EqualTo(expectedOctave));
        }

        [Test]
        public void it_parses()
        {
            var musicSheetParser = new MusicSheetParser(new ChordParser(new NoteParser()));

            var musicSheet = musicSheetParser.Parse(
                Melodies.FinalFantasyXiii2.AWish.Melody,
                Melodies.FinalFantasyXiii2.AWish.Tempo,
                Melodies.FinalFantasyXiii2.AWish.Nominator,
                Melodies.FinalFantasyXiii2.AWish.Denominator);

            Assert.That(musicSheet.MetronomeMark.Metronome, Is.EqualTo(75));
            Assert.That(musicSheet.MetronomeMark.WholeNoteLength, Is.EqualTo(TimeSpan.FromMilliseconds(400)));

            var melody = musicSheet.Melody.ToArray();

            AssertChord(melody[0], Note.Keys.F, Note.Octaves.Middle, 0.0m, new Fraction(1, 5));
            AssertChord(melody[1], Note.Keys.A, Note.Octaves.Middle, 0.2m, new Fraction(1, 5));
            AssertChord(melody[2], Note.Keys.C, Note.Octaves.High, 0.4m, new Fraction(1, 5));
            AssertChord(melody[3], Note.Keys.E, Note.Octaves.High, 0.6m, new Fraction(1, 5));
            AssertChord(melody[4], Note.Keys.F, Note.Octaves.High, 0.8m, new Fraction(1, 5));
            AssertChord(melody[5], Note.Keys.A, Note.Octaves.High, 1.0m, new Fraction(5, 1));
            AssertChord(melody[6], Note.Keys.D, Note.Octaves.Middle, 6.0m, new Fraction(1, 5));
            AssertChord(melody[7], Note.Keys.F, Note.Octaves.Middle, 6.2m, new Fraction(1, 5));
            AssertChord(melody[8], Note.Keys.A, Note.Octaves.Middle, 6.4m, new Fraction(1, 5));
            AssertChord(melody[9], Note.Keys.C, Note.Octaves.High, 6.6m, new Fraction(1, 5));
            AssertChord(melody[10], Note.Keys.D, Note.Octaves.High, 6.8m, new Fraction(1, 5));
            AssertChord(melody[11], Note.Keys.F, Note.Octaves.High, 7.0m, new Fraction(5, 1));
        }
    }
}