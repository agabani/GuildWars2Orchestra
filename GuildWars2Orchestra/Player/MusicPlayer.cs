using System.Linq;
using System.Threading;
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

        public void Play()
        {
            var wholeNoteLength = _musicSheet.MetronomeMark.WholeNoteLength;
            var melody = _musicSheet.Melody.ToArray();

            foreach (var strum in melody)
            {
                var chord = strum.Chord;

                foreach (var note in chord.Notes)
                {
                    _harp.PlayNote(note);
                }

                var timeSpan = wholeNoteLength
                    .Multiply(chord.Length.Nominator)
                    .Divide(chord.Length.Denominator)
                    .Divide(2);

                Thread.Sleep(timeSpan);
            }
        }
    }
}