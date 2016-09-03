using GuildWars2Orchestra.Domain.Values;
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
            var midiFile = new MidiFile(@"C:/Users/speechless/Desktop/Musician_14th_song_d.gray_man.mid");

            var midiParser = new MidiParser();

            var musicSheet = midiParser.Parse(midiFile);

            Assert.That(musicSheet.MetronomeMark.Metronome, Is.EqualTo(120));
        }
    }
}