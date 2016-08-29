using System;
using System.Threading;
using GuildWars2Orchestra.Controls;
using GuildWars2Orchestra.Instrument;
using GuildWars2Orchestra.Parsers;
using GuildWars2Orchestra.Player;
using GuildWars2Orchestra.TestData;

namespace GuildWars2Orchestra
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string melody;
            int tempo;
            int nomintor;
            int denomintor;

            switch (7)
            {
                case 1:
                    melody = Melodies.FinalFantasyXiii2.AWish.Melody;
                    tempo = Melodies.FinalFantasyXiii2.AWish.Tempo;
                    nomintor = Melodies.FinalFantasyXiii2.AWish.Nominator;
                    denomintor = Melodies.FinalFantasyXiii2.AWish.Denominator;
                    break;

                case 2:
                    melody = Melodies.FinalFantasyVii.Theme.Melody;
                    tempo = Melodies.FinalFantasyVii.Theme.Tempo;
                    nomintor = Melodies.FinalFantasyVii.Theme.Nominator;
                    denomintor = Melodies.FinalFantasyVii.Theme.Denominator;
                    break;

                case 3:
                    melody = Melodies.FinalFantasy.Prelude.Melody;
                    tempo = Melodies.FinalFantasy.Prelude.Tempo;
                    nomintor = Melodies.FinalFantasy.Prelude.Nominator;
                    denomintor = Melodies.FinalFantasy.Prelude.Denominator;
                    break;

                case 4:
                    melody = Melodies.FinalFantasyVi.TerrasTheme.Melody;
                    tempo = Melodies.FinalFantasyVi.TerrasTheme.Tempo;
                    nomintor = Melodies.FinalFantasyVi.TerrasTheme.Nominator;
                    denomintor = Melodies.FinalFantasyVi.TerrasTheme.Denominator;
                    break;

                case 5:
                    melody = Melodies.FinalFantasyVii.GoldSaucer.Melody;
                    tempo = Melodies.FinalFantasyVii.GoldSaucer.Tempo;
                    nomintor = Melodies.FinalFantasyVii.GoldSaucer.Nominator;
                    denomintor = Melodies.FinalFantasyVii.GoldSaucer.Denominator;
                    break;

                case 6:
                    melody = Melodies.FinalFantasyVii.TheGreatWarrior.Melody;
                    tempo = Melodies.FinalFantasyVii.TheGreatWarrior.Tempo;
                    nomintor = Melodies.FinalFantasyVii.TheGreatWarrior.Nominator;
                    denomintor = Melodies.FinalFantasyVii.TheGreatWarrior.Denominator;
                    break;

                case 7:
                    melody = Melodies.Pokemon.PokemonCenterTheme.Melody;
                    tempo = Melodies.Pokemon.PokemonCenterTheme.Tempo;
                    nomintor = Melodies.Pokemon.PokemonCenterTheme.Nominator;
                    denomintor = Melodies.Pokemon.PokemonCenterTheme.Denominator;
                    break;

                default:
                    throw new ApplicationException();
            }

            var musicSheet = new MusicSheetParser(new ChordParser(new NoteParser()))
                .Parse(melody, tempo, nomintor, denomintor);

            var harp = new Harp(new GuildWarsKeyboard());

            Thread.Sleep(200);

            var musicPlayer = new MusicPlayer(musicSheet, harp);

            musicPlayer.Play();
        }
    }
}