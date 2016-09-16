using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.GuildWars2.Instrument;

namespace GuildWars2Orchestra.Player.Algorithms
{
    public interface IPlayAlgorithm
    {
        void Play(Harp harp, MetronomeMark metronomeMark, ChordOffset[] melody);
    }
}