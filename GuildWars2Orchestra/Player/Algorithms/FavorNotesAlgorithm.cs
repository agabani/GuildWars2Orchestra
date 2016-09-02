using System;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.Instrument;

namespace GuildWars2Orchestra.Player.Algorithms
{
    public class FavorNotesAlgorithm : IPlayAlgorithm
    {
        public async Task Play(Harp harp, ChordOffset[] melody, Func<Fraction, TimeSpan> timeCalculator)
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
}