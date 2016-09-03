using System;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2Orchestra.Domain;
using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.Instrument;
using GuildWars2Orchestra.Kernal.Extensions;
using GuildWars2Orchestra.Player.Algorithms;

namespace GuildWars2Orchestra.Player
{
    public class MusicPlayer
    {
        private readonly IPlayAlgorithm _algorithm;
        private readonly Harp _harp;
        private readonly MusicSheet _musicSheet;

        public MusicPlayer(MusicSheet musicSheet, Harp harp, IPlayAlgorithm algorithm)
        {
            _musicSheet = musicSheet;
            _harp = harp;
            _algorithm = algorithm;
        }

        public async Task Play()
        {
            Func<Fraction, TimeSpan> timeCalculation = fraction =>
                _musicSheet.MetronomeMark.WholeNoteLength
                    .Multiply(fraction.Nominator)
                    .Divide(fraction.Denominator);

            await _algorithm.Play(_harp, _musicSheet.Melody.ToArray(), timeCalculation, _musicSheet.MetronomeMark);
        }
    }
}