using System.IO;
using GuildWars2Orchestra.Midi.Tests.Unit.Properties;
using NUnit.Framework;

namespace GuildWars2Orchestra.Midi.Tests.Unit
{
    [TestFixture]
    internal class MidiParserTests
    {
        private const string TestMidiFilePath = "midi_parser_test.midi";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            using (var memoryStream = new MemoryStream(Resources.Musician_14th_song_d_gray_man))
            {
                using (var fileStream = new FileStream(TestMidiFilePath, FileMode.Create))
                {
                    memoryStream.CopyTo(fileStream);
                }
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            File.Delete(TestMidiFilePath);
        }

        [Test]
        public void it_parses()
        {
            var midiParser = new MidiParser();

            var musicSheet = midiParser.Parse(TestMidiFilePath);

            Assert.That(musicSheet.MetronomeMark.Metronome, Is.EqualTo(120));
            Assert.That(musicSheet.Melody, Is.Not.Null.And.Not.Empty);
        }
    }
}