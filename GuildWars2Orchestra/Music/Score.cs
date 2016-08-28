using System.Collections.Generic;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Music
{
    public class Score
    {
        public Score(MetronomeMark metronomeMark, List<ScoreNote> scoreNotes)
        {
            MetronomeMark = metronomeMark;
            ScoreNotes = scoreNotes;
        }

        public MetronomeMark MetronomeMark { get; private set; }

        public List<ScoreNote> ScoreNotes { get; private set; }
    }
}