using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.Parsers;
using NUnit.Framework;

namespace GuildWars2Orchestra.Tests.Unit.Parser
{
    [TestFixture]
    internal class NoteParserTest
    {
        [Test]
        [TestCase("Z,", Note.Keys.None, Note.Octaves.None)]
        [TestCase("C,", Note.Keys.C, Note.Octaves.Low)]
        [TestCase("D,", Note.Keys.D, Note.Octaves.Low)]
        [TestCase("E,", Note.Keys.E, Note.Octaves.Low)]
        [TestCase("F,", Note.Keys.F, Note.Octaves.Low)]
        [TestCase("G,", Note.Keys.G, Note.Octaves.Low)]
        [TestCase("A,", Note.Keys.A, Note.Octaves.Low)]
        [TestCase("B,", Note.Keys.B, Note.Octaves.Low)]
        [TestCase("Z", Note.Keys.None, Note.Octaves.None)]
        [TestCase("C", Note.Keys.C, Note.Octaves.Middle)]
        [TestCase("D", Note.Keys.D, Note.Octaves.Middle)]
        [TestCase("E", Note.Keys.E, Note.Octaves.Middle)]
        [TestCase("F", Note.Keys.F, Note.Octaves.Middle)]
        [TestCase("G", Note.Keys.G, Note.Octaves.Middle)]
        [TestCase("A", Note.Keys.A, Note.Octaves.Middle)]
        [TestCase("B", Note.Keys.B, Note.Octaves.Middle)]
        [TestCase("z", Note.Keys.None, Note.Octaves.None)]
        [TestCase("c", Note.Keys.C, Note.Octaves.High)]
        [TestCase("d", Note.Keys.D, Note.Octaves.High)]
        [TestCase("e", Note.Keys.E, Note.Octaves.High)]
        [TestCase("f", Note.Keys.F, Note.Octaves.High)]
        [TestCase("g", Note.Keys.G, Note.Octaves.High)]
        [TestCase("a", Note.Keys.A, Note.Octaves.High)]
        [TestCase("b", Note.Keys.B, Note.Octaves.High)]
        [TestCase("z'", Note.Keys.None, Note.Octaves.None)]
        [TestCase("c'", Note.Keys.C, Note.Octaves.Highest)]
        public void it_parses_note(string text, Note.Keys key, Note.Octaves octave)
        {
            var noteParser = new NoteParser();

            var note = noteParser.Parse(text);

            Assert.That(note.Key, Is.EqualTo(key));
            Assert.That(note.Octave, Is.EqualTo(octave));
        }
    }
}