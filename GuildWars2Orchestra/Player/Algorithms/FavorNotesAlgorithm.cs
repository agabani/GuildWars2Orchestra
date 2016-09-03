using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.Instrument;
using GuildWars2Orchestra.Kernal.Extensions;

namespace GuildWars2Orchestra.Player.Algorithms
{
    public class FavorNotesAlgorithm : IPlayAlgorithm
    {
        public async Task Play(Harp harp, ChordOffset[] melody, Func<Fraction, TimeSpan> timeCalculator, MetronomeMark metronomeMark)
        {
            await PrepareChordsOctave(harp, melody[0].Chord);

            for (var chordIndex = 0; chordIndex < melody.Length; chordIndex++)
            {
                var chord = melody[chordIndex].Chord;

                await PlayChord(harp, chord);

                if (chordIndex < melody.Length - 1)
                {
                    await PrepareChordsOctave(harp, melody[chordIndex + 1].Chord);
                }

                await Task.Delay(timeCalculator(chord.Length));
            }
        }

        private static async Task PrepareChordsOctave(Harp harp, Chord chord)
        {
            await harp.GoToOctave(chord.Notes.First());
        }

        private static async Task PlayChord(Harp harp, Chord chord)
        {
            var notes = chord.Notes.ToArray();

            for (var noteIndex = 0; noteIndex < notes.Length; noteIndex++)
            {
                await harp.PlayNote(notes[noteIndex]);

                if (noteIndex < notes.Length - 1)
                {
                    await PrepareNoteOctave(harp, notes[noteIndex + 1]);
                }
            }
        }

        private static async Task PrepareNoteOctave(Harp harp, Note note)
        {
            await harp.GoToOctave(note);
        }
    }

    public class FavorNotesAlgorithm2 : IPlayAlgorithm
    {
        public async Task Play(Harp harp, ChordOffset[] melody, Func<Fraction, TimeSpan> timeCalculator, MetronomeMark metronomeMark)
        {
            await PrepareChordsOctave(harp, melody[0].Chord);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var strumIndex = 0; strumIndex < melody.Length;)
            {
                var strum = melody[strumIndex];

                if (stopwatch.ElapsedMilliseconds > metronomeMark.WholeNoteLength.Multiply(strum.Offest).TotalMilliseconds)
                {
                    var chord = strum.Chord;

                    await PlayChord(harp, chord);

                    if (strumIndex < melody.Length - 1)
                    {
                        await PrepareChordsOctave(harp, melody[strumIndex + 1].Chord);
                    }

                    strumIndex++;
                }
                else
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(30));
                }
            }

            stopwatch.Stop();
        }

        private static async Task PrepareChordsOctave(Harp harp, Chord chord)
        {
            await harp.GoToOctave(chord.Notes.First());
        }

        private static async Task PlayChord(Harp harp, Chord chord)
        {
            var notes = chord.Notes.ToArray();

            for (var noteIndex = 0; noteIndex < notes.Length; noteIndex++)
            {
                await harp.PlayNote(notes[noteIndex]);

                if (noteIndex < notes.Length - 1)
                {
                    await PrepareNoteOctave(harp, notes[noteIndex + 1]);
                }
            }
        }

        private static async Task PrepareNoteOctave(Harp harp, Note note)
        {
            await harp.GoToOctave(note);
        }
    }
}