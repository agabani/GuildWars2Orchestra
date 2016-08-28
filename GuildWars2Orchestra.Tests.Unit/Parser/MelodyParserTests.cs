using System.Linq;
using GuildWars2Orchestra.Parsers;
using GuildWars2Orchestra.Tests.Unit.TestData;
using GuildWars2Orchestra.Values;
using NUnit.Framework;

namespace GuildWars2Orchestra.Tests.Unit.Parser
{
    [TestFixture]
    internal class MelodyParserTests
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
        public void it_parses_melody()
        {
            var notesPerBeat = new Fraction(
                Melodies.FinalFantasyXiii2.AWish.Nominator,
                Melodies.FinalFantasyXiii2.AWish.Denominator);

            var chordParser = new ChordParser(new NoteParser(), notesPerBeat);

            var melodyParser = new MelodyParser(chordParser);

            var melody = melodyParser.Parse(Melodies.FinalFantasyXiii2.AWish.Melody).ToArray();

            AssertChord(melody[0], Note.Keys.Note4, Note.Octaves.Middle, 0m, new Fraction(1, 10));
            AssertChord(melody[1], Note.Keys.Note6, Note.Octaves.Middle, 0.1m, new Fraction(1, 10));
            AssertChord(melody[2], Note.Keys.Note1, Note.Octaves.High, 0.2m, new Fraction(1, 10));
            AssertChord(melody[3], Note.Keys.Note3, Note.Octaves.High, 0.3m, new Fraction(1, 10));
            AssertChord(melody[4], Note.Keys.Note4, Note.Octaves.High, 0.4m, new Fraction(1, 10));
            AssertChord(melody[5], Note.Keys.Note6, Note.Octaves.High, 0.5m, new Fraction(5, 2));
            AssertChord(melody[6], Note.Keys.Note2, Note.Octaves.Middle, 3.0m, new Fraction(1, 10));
            AssertChord(melody[7], Note.Keys.Note4, Note.Octaves.Middle, 3.1m, new Fraction(1, 10));
            AssertChord(melody[8], Note.Keys.Note6, Note.Octaves.Middle, 3.2m, new Fraction(1, 10));
            AssertChord(melody[9], Note.Keys.Note1, Note.Octaves.High, 3.3m, new Fraction(1, 10));
            AssertChord(melody[10], Note.Keys.Note2, Note.Octaves.High, 3.4m, new Fraction(1, 10));
            AssertChord(melody[11], Note.Keys.Note4, Note.Octaves.High, 3.5m, new Fraction(5, 2));
        }
    }
}