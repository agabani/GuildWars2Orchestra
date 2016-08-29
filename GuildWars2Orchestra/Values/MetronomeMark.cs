using System;
using GuildWars2Orchestra.Extensions;

namespace GuildWars2Orchestra.Values
{
    public class MetronomeMark
    {
        public MetronomeMark(int metronome, Fraction beatsPerMeasure)
        {
            Metronome = metronome;

            WholeNoteLength = TimeSpan
                .FromMinutes(1)
                .Divide(metronome)
                .Multiply(beatsPerMeasure.Denominator)
                .Divide(beatsPerMeasure.Nominator);
        }

        public int Metronome { get; }
        public TimeSpan WholeNoteLength { get; }
    }
}