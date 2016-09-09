using System;
using System.IO;
using System.Threading.Tasks;
using GuildWars2Orchestra.Audio.Emulators;
using GuildWars2Orchestra.GuildWars2.Controls;
using GuildWars2Orchestra.GuildWars2.Instrument;
using GuildWars2Orchestra.Midi;
using GuildWars2Orchestra.MusicBoxNotation.Parsers;
using GuildWars2Orchestra.MusicBoxNotation.Persistance;
using GuildWars2Orchestra.Player;
using GuildWars2Orchestra.Player.Algorithms;

namespace GuildWars2Orchestra
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Task.Run(async () => await MainAsync(args)).Wait();
        }

        private static async Task MainAsync(string[] args)
        {
            var filePath = args.Length == 1 ? args[0] : @"TestData\Guilty Crown - My Dearest.xml";

            var extension = Path.GetExtension(filePath)?.ToLower();

            switch (extension)
            {
                case ".xml":
                    await MusicBoxNotationAsync(filePath);
                    break;
                case ".mid":
                    await MidiAsync(filePath);
                    break;
                default:
                    throw new NotSupportedException(extension);
            }
        }

        private static async Task MusicBoxNotationAsync(string filePath)
        {
            var rawMusicSheet = new XmlMusicSheetReader().LoadFromFile(filePath);

            var musicSheet = new MusicSheetParser(new ChordParser(new NoteParser())).Parse(
                rawMusicSheet.Melody,
                int.Parse(rawMusicSheet.Tempo),
                int.Parse(rawMusicSheet.Meter.Split('/')[0]),
                int.Parse(rawMusicSheet.Meter.Split('/')[1]));

            await Task.Delay(200);

            var algorithm = rawMusicSheet.Algorithm == "favor notes"
                ? new FavorNotesAlgorithm() : (IPlayAlgorithm)new FavorChordsAlgorithm();

            IKeyboard guildWarsKeyboard = new GuildWarsKeyboard();
            //IKeyboard guildWarsKeyboard = new HarpEmulator();

            var musicPlayer = new MusicPlayer(musicSheet, new Harp(guildWarsKeyboard), algorithm);

            await musicPlayer.Play();
        }

        private static async Task MidiAsync(string filePath)
        {
            var musicSheet = new MidiParser().Parse(filePath);

            await Task.Delay(200);

            var musicPlayer = new MusicPlayer(musicSheet, new Harp(new GuildWarsKeyboard()), new FavorChordsAlgorithm());

            await musicPlayer.Play();
        }
    }
}