using System;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2Orchestra.Instrument;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Player.Algorithms
{
    public class FavourHighNotesAlgorithm : IPlayAlgorithm
    {
        public async Task Play(Harp harp, ChordOffset[] melody, Func<Fraction, TimeSpan> timeCalculator)
        {
            await harp.PrepareNote(melody[0].Chord.Notes.First());

            for (var chordIndex = 0; chordIndex < melody.Length; chordIndex++)
            {
                var chord = melody[chordIndex].Chord;
                var notes = chord.Notes.ToArray();

                for (var noteIndex = 0; noteIndex < notes.Length; noteIndex++)
                {
                    await harp.PlayNote(notes[noteIndex]);

                    if (noteIndex < notes.Length - 1)
                    {
                        await harp.PrepareNote(notes[noteIndex + 1]);
                    }
                }

                if (chordIndex < melody.Length - 1)
                {
                    await harp.PrepareNote(melody[chordIndex + 1].Chord.Notes.First());
                }

                await Task.Delay(timeCalculator(chord.Length));
            }
        }
    }
}