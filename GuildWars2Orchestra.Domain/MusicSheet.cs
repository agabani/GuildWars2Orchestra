using System.Collections.Generic;
using GuildWars2Orchestra.Domain.Values;

namespace GuildWars2Orchestra.Domain
{
    public class MusicSheet
    {
        public MusicSheet(string title, MetronomeMark metronomeMark, IEnumerable<ChordOffset> melody)
        {
            MetronomeMark = metronomeMark;
            Melody = melody;
            Title = title;
        }

        public string Title { get; }

        public MetronomeMark MetronomeMark { get; }

        public IEnumerable<ChordOffset> Melody { get; }
    }
}