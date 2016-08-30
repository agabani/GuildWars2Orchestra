using System;
using System.Threading.Tasks;
using GuildWars2Orchestra.Instrument;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Player.Algorithms
{
    public interface IPlayAlgorithm
    {
        Task Play(Harp harp, ChordOffset[] melody, Func<Fraction, TimeSpan> timeCalculator);
    }
}