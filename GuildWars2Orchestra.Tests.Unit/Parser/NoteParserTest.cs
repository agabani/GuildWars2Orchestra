using GuildWars2Orchestra.Parsers;
using GuildWars2Orchestra.Values;
using NUnit.Framework;

namespace GuildWars2Orchestra.Tests.Unit.Parser
{
    [TestFixture]
    internal class NoteParserTest
    {
        [Test]
        [TestCase("Z,", Note.Keys.None, Note.Octaves.None)]
        [TestCase("C,", Note.Keys.Note1, Note.Octaves.Low)]
        [TestCase("D,", Note.Keys.Note2, Note.Octaves.Low)]
        [TestCase("E,", Note.Keys.Note3, Note.Octaves.Low)]
        [TestCase("F,", Note.Keys.Note4, Note.Octaves.Low)]
        [TestCase("G,", Note.Keys.Note5, Note.Octaves.Low)]
        [TestCase("A,", Note.Keys.Note6, Note.Octaves.Low)]
        [TestCase("B,", Note.Keys.Note7, Note.Octaves.Low)]
        [TestCase("Z", Note.Keys.None, Note.Octaves.None)]
        [TestCase("C", Note.Keys.Note1, Note.Octaves.Middle)]
        [TestCase("D", Note.Keys.Note2, Note.Octaves.Middle)]
        [TestCase("E", Note.Keys.Note3, Note.Octaves.Middle)]
        [TestCase("F", Note.Keys.Note4, Note.Octaves.Middle)]
        [TestCase("G", Note.Keys.Note5, Note.Octaves.Middle)]
        [TestCase("A", Note.Keys.Note6, Note.Octaves.Middle)]
        [TestCase("B", Note.Keys.Note7, Note.Octaves.Middle)]
        [TestCase("z", Note.Keys.None, Note.Octaves.None)]
        [TestCase("c", Note.Keys.Note1, Note.Octaves.High)]
        [TestCase("d", Note.Keys.Note2, Note.Octaves.High)]
        [TestCase("e", Note.Keys.Note3, Note.Octaves.High)]
        [TestCase("f", Note.Keys.Note4, Note.Octaves.High)]
        [TestCase("g", Note.Keys.Note5, Note.Octaves.High)]
        [TestCase("a", Note.Keys.Note6, Note.Octaves.High)]
        [TestCase("b", Note.Keys.Note7, Note.Octaves.High)]
        [TestCase("z'", Note.Keys.None, Note.Octaves.None)]
        [TestCase("c'", Note.Keys.Note8, Note.Octaves.High)]
        public void it_parses_note(string text, Note.Keys key, Note.Octaves octave)
        {
            var noteParser = new NoteParser();

            var note = noteParser.Parse(text);

            Assert.That(note.Key, Is.EqualTo(key));
            Assert.That(note.Octave, Is.EqualTo(octave));
        }
    }
}