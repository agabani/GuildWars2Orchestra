using System.Collections.Generic;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Music
{
    public class MusicSheet
    {
        public MusicSheet(MetronomeMark metronomeMark, IEnumerable<ChordOffset> melody)
        {
            MetronomeMark = metronomeMark;
            Melody = melody;
        }

        public MetronomeMark MetronomeMark { get; private set; }

        public IEnumerable<ChordOffset> Melody { get; private set; }
    }
}