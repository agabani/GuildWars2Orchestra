﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2Orchestra.Extensions;
using GuildWars2Orchestra.Instrument;
using GuildWars2Orchestra.Music;
using GuildWars2Orchestra.Values;

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

            await _harp.PrepareNote(melody[0].Chord.Notes.First());

            for (int chordIndex = 0; chordIndex < melody.Length; chordIndex++)
            {
                var chord = melody[chordIndex].Chord;
                var notes = chord.Notes.ToArray();

                for (int noteIndex = 0; noteIndex < notes.Length; noteIndex++)
                {
                    await _harp.PlayNote(notes[noteIndex]);

                    if (noteIndex < notes.Length - 1)
                    {
                        await _harp.PrepareNote(notes[noteIndex + 1]);
                    }
                }

                if (chordIndex < melody.Length - 1)
                {
                    await _harp.PrepareNote(melody[chordIndex + 1].Chord.Notes.First());
                }

                var timeSpan = wholeNoteLength
                    .Multiply(chord.Length.Nominator)
                    .Divide(chord.Length.Denominator);

                await Task.Delay(timeSpan);
            }

            /*foreach (var strum in melody)
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
            }*/
        }
    }
}