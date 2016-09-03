using NAudio.Midi;
using NUnit.Framework;

namespace GuildWars2Orchestra.Midi.Tests.Unit
{
    [TestFixture]
    internal class MidiParserTests
    {
        [Test]
        public void it_parses()
        {
            var midiPath = @"C:/Users/speechless/Desktop/Musician_14th_song_d.gray_man.mid";
            //var midiFile = new MidiFile(midiPath);

            var midiParser = new MidiParser();

            var musicSheet = midiParser.Parse(midiPath);

            Assert.That(musicSheet.MetronomeMark.Metronome, Is.EqualTo(120));
            Assert.That(musicSheet.Melody, Is.Not.Null.And.Not.Empty);
        }
    }
}