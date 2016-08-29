using System;
using GuildWars2Orchestra.Extensions;

namespace GuildWars2Orchestra.Values
{
    public class MetronomeMark
    {
        public MetronomeMark(int metronome, Fraction beatsPerMeasure)
        {
            Metronome = metronome;

            WholeNoteLength = TimeSpan.FromMinutes(1)
                .Divide(metronome*16/beatsPerMeasure.Denominator)
                .Multiply(4);
        }

        public int Metronome { get; }
        public TimeSpan WholeNoteLength { get; }
    }
}