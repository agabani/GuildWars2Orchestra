using System.Linq;
using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.Parsers;
using NUnit.Framework;

namespace GuildWars2Orchestra.Tests.Unit.Parser
{
    [TestFixture]
    internal class ChordParserTests
    {
        [Test]
        [TestCase("c", Note.Keys.C, Note.Octaves.High, 1, 1)]
        [TestCase("C,2", Note.Keys.C, Note.Octaves.Low, 2, 1)]
        [TestCase("C,4", Note.Keys.C, Note.Octaves.Low, 4, 1)]
        [TestCase("C6", Note.Keys.C, Note.Octaves.Middle, 6, 1)]
        [TestCase("C/2", Note.Keys.C, Note.Octaves.Middle, 1, 2)]
        [TestCase("C/4", Note.Keys.C, Note.Octaves.Middle, 1, 4)]
        [TestCase("C/6", Note.Keys.C, Note.Octaves.Middle, 1, 6)]
        [TestCase("c'3/2", Note.Keys.C, Note.Octaves.Highest, 3, 2)]
        [TestCase("c'3/4", Note.Keys.C, Note.Octaves.Highest, 3, 4)]
        [TestCase("c'3/6", Note.Keys.C, Note.Octaves.Highest, 3, 6)]
        public void it_parses_duration(string text, Note.Keys key, Note.Octaves octave, int nominator, int denominator)
        {
            var chordParser = new ChordParser(new NoteParser());

            var chord = chordParser.Parse(text);

            var note = chord.Notes.First();

            Assert.That(note.Key, Is.EqualTo(key));
            Assert.That(note.Octave, Is.EqualTo(octave));
            Assert.That(chord.Length.Nominator, Is.EqualTo(nominator));
            Assert.That(chord.Length.Denominator, Is.EqualTo(denominator));
        }

        [Test]
        [TestCase("[D,dfa]6")]
        [TestCase("[D,dfa6]")]
        public void it_parses_notes(string text)
        {
            var chordParser = new ChordParser(new NoteParser());

            var chord = chordParser.Parse(text);

            var notes = chord.Notes.ToArray();

            Assert.That(chord.Length, Is.EqualTo(new Fraction(6, 1)));

            var note0 = new Note(Note.Keys.D, Note.Octaves.Low);
            var note1 = new Note(Note.Keys.D, Note.Octaves.High);
            var note2 = new Note(Note.Keys.F, Note.Octaves.High);
            var note3 = new Note(Note.Keys.A, Note.Octaves.High);

            Assert.That(notes, Contains.Item(note0));
            Assert.That(notes, Contains.Item(note1));
            Assert.That(notes, Contains.Item(note2));
            Assert.That(notes, Contains.Item(note3));
        }
    }
}