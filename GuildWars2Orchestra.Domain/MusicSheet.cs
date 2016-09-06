using System.Collections.Generic;
using GuildWars2Orchestra.Domain.Values;

namespace GuildWars2Orchestra.Domain
{
    public class MusicSheet
    {
        public MusicSheet(MetronomeMark metronomeMark, IEnumerable<ChordOffset> melody)
        {
            MetronomeMark = metronomeMark;
            Melody = melody;
        }

        public MetronomeMark MetronomeMark { get; }

        public IEnumerable<ChordOffset> Melody { get; }
    }
}