﻿using System;
using System.Diagnostics;
using System.Threading;
using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.GuildWars2.Instrument;
using GuildWars2Orchestra.Kernal.Extensions;

namespace GuildWars2Orchestra.Player.Algorithms
{
    public class FavorChordsAlgorithm : IPlayAlgorithm
    {
        public void Play(Harp harp, MetronomeMark metronomeMark, ChordOffset[] melody)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var strumIndex = 0; strumIndex < melody.Length;)
            {
                var strum = melody[strumIndex];

                if (stopwatch.ElapsedMilliseconds > metronomeMark.WholeNoteLength.Multiply(strum.Offest).TotalMilliseconds)
                {
                    var chord = strum.Chord;

                    foreach (var note in chord.Notes)
                    {
                        harp.GoToOctave(note);
                        harp.PlayNote(note);
                    }

                    strumIndex++;
                }
                else
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(1));
                }
            }

            stopwatch.Stop();
        }
    }
}