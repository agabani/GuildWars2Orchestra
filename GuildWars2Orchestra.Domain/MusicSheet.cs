using System.Collections.Generic;
using GuildWars2Orchestra.Domain.Values;

namespace GuildWars2Orchestra.Domain
{
    public class MusicSheet
    {
        public MusicSheet(MetronomeMark metronomeMark, IEnumerable<ChordOffset> melody, Fraction beatsPerMeasure)
        {
            MetronomeMark = metronomeMark;
            Melody = melody;
            BeatsPerMeasure = beatsPerMeasure;
        }

        public MetronomeMark MetronomeMark { get; private set; }

        public IEnumerable<ChordOffset> Melody { get; private set; }
        public Fraction BeatsPerMeasure { get; private set; }
    }
}