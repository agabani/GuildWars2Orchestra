using GuildWars2Orchestra.Domain;
using GuildWars2Orchestra.Domain.Values;
using NAudio.Midi;

namespace GuildWars2Orchestra.Midi
{
    public class MidiParser
    {
        private static readonly Fraction BeatsPerMeasure = new Fraction(1, 4);

        public MusicSheet Parse(MidiFile midi)
        {
            return new MusicSheet(ParseMetronomeMark(midi), null);
        }

        private static MetronomeMark ParseMetronomeMark(MidiFile midi)
        {
            return new MetronomeMark(midi.DeltaTicksPerQuarterNote/4, BeatsPerMeasure);
        }
    }
}