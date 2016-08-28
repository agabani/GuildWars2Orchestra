using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Music
{
    public class ScoreNote
    {
        public ScoreNote(Note note, Beat beat)
        {
            Note = note;
            Beat = beat;
        }

        public Beat Beat { get; private set; }
        public Note Note { get; private set; }
    }
}