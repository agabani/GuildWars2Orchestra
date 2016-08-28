using GuildWars2Orchestra.Extensions;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Controls
{
    public class NotePlayer
    {
        private readonly MetronomeMark _metronomeMark;

        public NotePlayer(MetronomeMark metronomeMark)
        {
            _metronomeMark = metronomeMark;
        }

        public void PlayNote(int nominator, int denominator)
        {
            var timeSpan = _metronomeMark
                .WholeNoteLength
                .Multiply(nominator)
                .Divide(denominator);
        }
    }
}