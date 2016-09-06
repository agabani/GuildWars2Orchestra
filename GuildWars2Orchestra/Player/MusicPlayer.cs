using System.Linq;
using System.Threading.Tasks;
using GuildWars2Orchestra.Domain;
using GuildWars2Orchestra.GuildWars2.Instrument;
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
            await _algorithm.Play(_harp, _musicSheet.MetronomeMark, _musicSheet.Melody.ToArray());
        }
    }
}