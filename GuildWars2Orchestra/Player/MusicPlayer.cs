using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2Orchestra.Extensions;
using GuildWars2Orchestra.Instrument;
using GuildWars2Orchestra.Music;

namespace GuildWars2Orchestra.Player
{
    public class MusicPlayer
    {
        private readonly Harp _harp;
        private readonly MusicSheet _musicSheet;

        public MusicPlayer(MusicSheet musicSheet, Harp harp)
        {
            _musicSheet = musicSheet;
            _harp = harp;
        }

        public async Task Play()
        {
            var wholeNoteLength = _musicSheet.MetronomeMark.WholeNoteLength;
            var melody = _musicSheet.Melody.ToArray();

            foreach (var strum in melody)
            {
                var chord = strum.Chord;

                foreach (var note in chord.Notes)
                {
                    await _harp.PlayNote(note);
                }

                var timeSpan = wholeNoteLength
                    .Multiply(chord.Length.Nominator)
                    .Divide(chord.Length.Denominator);

                await Task.Delay(timeSpan);
            }
        }
    }
}