using GuildWars2Orchestra.Parsers;
using GuildWars2Orchestra.Values;
using NUnit.Framework;

namespace GuildWars2Orchestra.Tests.Unit.Chord
{
    [TestFixture]
    internal class NoteParserTest
    {
        [Test]
        [TestCase("C,", Note.Keys.Note1, Note.Octaves.Low, 1, 4)]
        [TestCase("D,", Note.Keys.Note2, Note.Octaves.Low, 1, 4)]
        [TestCase("E,", Note.Keys.Note3, Note.Octaves.Low, 1, 4)]
        [TestCase("F,", Note.Keys.Note4, Note.Octaves.Low, 1, 4)]
        [TestCase("G,", Note.Keys.Note5, Note.Octaves.Low, 1, 4)]
        [TestCase("A,", Note.Keys.Note6, Note.Octaves.Low, 1, 4)]
        [TestCase("B,", Note.Keys.Note7, Note.Octaves.Low, 1, 4)]
        [TestCase("C", Note.Keys.Note1, Note.Octaves.Middle, 1, 4)]
        [TestCase("D", Note.Keys.Note2, Note.Octaves.Middle, 1, 4)]
        [TestCase("E", Note.Keys.Note3, Note.Octaves.Middle, 1, 4)]
        [TestCase("F", Note.Keys.Note4, Note.Octaves.Middle, 1, 4)]
        [TestCase("G", Note.Keys.Note5, Note.Octaves.Middle, 1, 4)]
        [TestCase("A", Note.Keys.Note6, Note.Octaves.Middle, 1, 4)]
        [TestCase("B", Note.Keys.Note7, Note.Octaves.Middle, 1, 4)]
        [TestCase("c", Note.Keys.Note1, Note.Octaves.High, 1, 4)]
        [TestCase("d", Note.Keys.Note2, Note.Octaves.High, 1, 4)]
        [TestCase("e", Note.Keys.Note3, Note.Octaves.High, 1, 4)]
        [TestCase("f", Note.Keys.Note4, Note.Octaves.High, 1, 4)]
        [TestCase("g", Note.Keys.Note5, Note.Octaves.High, 1, 4)]
        [TestCase("a", Note.Keys.Note6, Note.Octaves.High, 1, 4)]
        [TestCase("b", Note.Keys.Note7, Note.Octaves.High, 1, 4)]
        [TestCase("c'", Note.Keys.Note8, Note.Octaves.High, 1, 4)]
        public void it_parses_note(string text, Note.Keys key, Note.Octaves octave, int nominator, int denominator)
        {
            var noteParser = new NoteParser(new Fraction(1, 4));

            var note = noteParser.Parse(text);

            Assert.That(note.Key, Is.EqualTo(key));
            Assert.That(note.Octave, Is.EqualTo(octave));
            Assert.That(note.Length.Nominator, Is.EqualTo(nominator));
            Assert.That(note.Length.Denominator, Is.EqualTo(denominator));
        }

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
            var noteParser = new NoteParser(new Fraction(1, 4));

            var note = noteParser.Parse(text);

            Assert.That(note.Key, Is.EqualTo(key));
            Assert.That(note.Octave, Is.EqualTo(octave));
            Assert.That(note.Length.Nominator, Is.EqualTo(nominator));
            Assert.That(note.Length.Denominator, Is.EqualTo(denominator));
        }
    }
}