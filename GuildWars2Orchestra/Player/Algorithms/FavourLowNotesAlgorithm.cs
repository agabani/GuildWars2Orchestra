using System;
using System.Threading.Tasks;
using GuildWars2Orchestra.Instrument;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Player.Algorithms
{
    public class FavourLowNotesAlgorithm : IPlayAlgorithm
    {
        public async Task Play(Harp harp, ChordOffset[] melody, Func<Fraction, TimeSpan> timeCalculator)
        {
            foreach (var strum in melody)
            {
                var chord = strum.Chord;

                foreach (var note in chord.Notes)
                {
                    await harp.GoToOctave(note);
                    await harp.PlayNote(note);
                }

                await Task.Delay(timeCalculator(chord.Length));
            }
        }
    }
}