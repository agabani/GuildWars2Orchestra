﻿using System.Threading.Tasks;
using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.Instrument;

namespace GuildWars2Orchestra.Player.Algorithms
{
    public interface IPlayAlgorithm
    {
        Task Play(Harp harp, MetronomeMark metronomeMark, ChordOffset[] melody);
    }
}