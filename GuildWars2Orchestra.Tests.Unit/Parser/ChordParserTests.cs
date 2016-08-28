using System.Linq;
using GuildWars2Orchestra.Parsers;
using GuildWars2Orchestra.Values;
using NUnit.Framework;

namespace GuildWars2Orchestra.Tests.Unit.Parser
{
    [TestFixture]
    internal class ChordParserTests
    {
        [Test]
        [TestCase("c", Note.Keys.Note1, Note.Octaves.High, 1, 4)]
        [TestCase("C,2", Note.Keys.Note1, Note.Octaves.Low, 2, 4)]
        [TestCase("C,4", Note.Keys.Note1, Note.Octaves.Low, 4, 4)]
        [TestCase("C6", Note.Keys.Note1, Note.Octaves.Middle, 6, 4)]
        [TestCase("C/2", Note.Keys.Note1, Note.Octaves.Middle, 1, 8)]
        [TestCase("C/4", Note.Keys.Note1, Note.Octaves.Middle, 1, 16)]
        [TestCase("C/6", Note.Keys.Note1, Note.Octaves.Middle, 1, 24)]
        [TestCase("c'3/2", Note.Keys.Note8, Note.Octaves.High, 3, 8)]
        [TestCase("c'3/4", Note.Keys.Note8, Note.Octaves.High, 3, 16)]
        [TestCase("c'3/6", Note.Keys.Note8, Note.Octaves.High, 3, 24)]
        public void it_parses_duration(string text, Note.Keys key, Note.Octaves octave, int nominator, int denominator)
        {
            var chordParser = new ChordParser(new NoteParser(), new Fraction(1, 4));

            var chord = chordParser.Parse(text);

            var note = chord.Notes.First();

            Assert.That(note.Key, Is.EqualTo(key));
            Assert.That(note.Octave, Is.EqualTo(octave));
            Assert.That(chord.Length.Nominator, Is.EqualTo(nominator));
            Assert.That(chord.Length.Denominator, Is.EqualTo(denominator));
        }

        [Test]
        public void it_parses_notes()
        {
            const string text = "[D,dfa]6";

            var chordParser = new ChordParser(new NoteParser(), new Fraction(1, 4));

            var chord = chordParser.Parse(text);

            var notes = chord.Notes.ToArray();

            Assert.That(chord.Length, Is.EqualTo(new Fraction(6, 4)));

            var note0 = new Note(Note.Keys.Note2, Note.Octaves.Low);
            var note1 = new Note(Note.Keys.Note2, Note.Octaves.High);
            var note2 = new Note(Note.Keys.Note4, Note.Octaves.High);
            var note3 = new Note(Note.Keys.Note6, Note.Octaves.High);

            Assert.That(notes, Contains.Item(note0));
            Assert.That(notes, Contains.Item(note1));
            Assert.That(notes, Contains.Item(note2));
            Assert.That(notes, Contains.Item(note3));
        }
    }
}