using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.GuildWars2.Instrument;
using GuildWars2Orchestra.Kernal.Extensions;

namespace GuildWars2Orchestra.Player.Algorithms
{
    public class FavorNotesAlgorithm : IPlayAlgorithm
    {
        public void Play(Harp harp, MetronomeMark metronomeMark, ChordOffset[] melody)
        {
            PrepareChordsOctave(harp, melody[0].Chord);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var strumIndex = 0; strumIndex < melody.Length;)
            {
                var strum = melody[strumIndex];

                if (stopwatch.ElapsedMilliseconds > metronomeMark.WholeNoteLength.Multiply(strum.Offest).TotalMilliseconds)
                {
                    var chord = strum.Chord;

                    PlayChord(harp, chord);

                    if (strumIndex < melody.Length - 1)
                    {
                        PrepareChordsOctave(harp, melody[strumIndex + 1].Chord);
                    }

                    strumIndex++;
                }
                else
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(30));
                }
            }

            stopwatch.Stop();
        }

        private static void PrepareChordsOctave(Harp harp, Chord chord)
        {
            harp.GoToOctave(chord.Notes.First());
        }

        private static void PlayChord(Harp harp, Chord chord)
        {
            var notes = chord.Notes.ToArray();

            for (var noteIndex = 0; noteIndex < notes.Length; noteIndex++)
            {
                harp.PlayNote(notes[noteIndex]);

                if (noteIndex < notes.Length - 1)
                {
                    PrepareNoteOctave(harp, notes[noteIndex + 1]);
                }
            }
        }

        private static void PrepareNoteOctave(Harp harp, Note note)
        {
            harp.GoToOctave(note);
        }
    }
}