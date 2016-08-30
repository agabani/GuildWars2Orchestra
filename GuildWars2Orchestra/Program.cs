using System.Threading.Tasks;
using GuildWars2Orchestra.Controls;
using GuildWars2Orchestra.Instrument;
using GuildWars2Orchestra.Parsers;
using GuildWars2Orchestra.Persistance;
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
            var fileName = args.Length == 1 ? args[0] : @"TestData\Guilty Crown - My Dearest.xml";

            var xmlMusicSheetReader = new XmlMusicSheetReader(new MusicSheetParser(new ChordParser(new NoteParser())));
            var musicSheet = xmlMusicSheetReader.LoadFromFile(fileName);

            var harp = new Harp(new GuildWarsKeyboard());

            await Task.Delay(200);

            var musicPlayer = new MusicPlayer(musicSheet, harp, new FavourLowNotesAlgorithm());

            await musicPlayer.Play();
        }
    }
}