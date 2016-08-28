using GuildWars2Orchestra.Parsers;
using GuildWars2Orchestra.Values;
using NUnit.Framework;

namespace GuildWars2Orchestra.Tests.Unit
{
    public class NoteParserTest
    {
        [Test]
        [TestCase("C,", Note.Keys.Note1, Note.Octaves.Low, 1, 1)]
        [TestCase("D,", Note.Keys.Note2, Note.Octaves.Low, 1, 1)]
        [TestCase("E,", Note.Keys.Note3, Note.Octaves.Low, 1, 1)]
        [TestCase("F,", Note.Keys.Note4, Note.Octaves.Low, 1, 1)]
        [TestCase("G,", Note.Keys.Note5, Note.Octaves.Low, 1, 1)]
        [TestCase("A,", Note.Keys.Note6, Note.Octaves.Low, 1, 1)]
        [TestCase("B,", Note.Keys.Note7, Note.Octaves.Low, 1, 1)]

        [TestCase("C", Note.Keys.Note1, Note.Octaves.Middle, 1, 1)]
        [TestCase("D", Note.Keys.Note2, Note.Octaves.Middle, 1, 1)]
        [TestCase("E", Note.Keys.Note3, Note.Octaves.Middle, 1, 1)]
        [TestCase("F", Note.Keys.Note4, Note.Octaves.Middle, 1, 1)]
        [TestCase("G", Note.Keys.Note5, Note.Octaves.Middle, 1, 1)]
        [TestCase("A", Note.Keys.Note6, Note.Octaves.Middle, 1, 1)]
        [TestCase("B", Note.Keys.Note7, Note.Octaves.Middle, 1, 1)]

        [TestCase("c", Note.Keys.Note1, Note.Octaves.High, 1, 1)]
        [TestCase("d", Note.Keys.Note2, Note.Octaves.High, 1, 1)]
        [TestCase("e", Note.Keys.Note3, Note.Octaves.High, 1, 1)]
        [TestCase("f", Note.Keys.Note4, Note.Octaves.High, 1, 1)]
        [TestCase("g", Note.Keys.Note5, Note.Octaves.High, 1, 1)]
        [TestCase("a", Note.Keys.Note6, Note.Octaves.High, 1, 1)]
        [TestCase("b", Note.Keys.Note7, Note.Octaves.High, 1, 1)]
        [TestCase("c'", Note.Keys.Note8, Note.Octaves.High, 1, 1)]
        public void it_parses_note(string text, Note.Keys key, Note.Octaves octave, int nominator, int denominator)
        {
            var noteParser = new NoteParser();

            var note = noteParser.Parse(text);

            Assert.That(note.Key, Is.EqualTo(key));
            Assert.That(note.Octave, Is.EqualTo(octave));
            Assert.That(note.Nominator, Is.EqualTo(nominator));
            Assert.That(note.Denominator, Is.EqualTo(denominator));
        }
    }
}

/* ==== NOTE MAPPING ====

    C,	D,	E,	F,	G,	A,	B,
    ▼1	▼2	▼3	▼4	▼5	▼6	▼7

    C	D	E	F	G	A	B
    1	2	3	4	5	6	7

    c	d	e	f	g	a	b	c'
    ▲1	▲2	▲3	▲4	▲5	▲6	▲7	▲8
*/