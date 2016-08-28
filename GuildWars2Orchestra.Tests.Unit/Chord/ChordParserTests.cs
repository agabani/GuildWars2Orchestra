using GuildWars2Orchestra.Parsers;
using GuildWars2Orchestra.Values;
using NUnit.Framework;

namespace GuildWars2Orchestra.Tests.Unit.Chord
{
    [TestFixture]
    internal class ChordParserTests
    {
        [Test]
        public void it_parses_notes()
        {
            const string text = "[D,dfa]6";

            var chordParser = new ChordParser(new NoteParser(new Fraction(1, 4)));

            var notes = chordParser.Parse(text);

            var note0 = new Note(Note.Keys.Note2, Note.Octaves.Low, new Fraction(6, 4));
            var note1 = new Note(Note.Keys.Note2, Note.Octaves.High, new Fraction(6, 4));
            var note2 = new Note(Note.Keys.Note4, Note.Octaves.High, new Fraction(6, 4));
            var note3 = new Note(Note.Keys.Note6, Note.Octaves.High, new Fraction(6, 4));

            Assert.That(notes, Contains.Item(note0));
            Assert.That(notes, Contains.Item(note1));
            Assert.That(notes, Contains.Item(note2));
            Assert.That(notes, Contains.Item(note3));
        }
    }
}