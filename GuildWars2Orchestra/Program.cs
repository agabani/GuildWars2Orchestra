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

            var rawMusicSheet = new XmlMusicSheetReader().LoadFromFile(fileName);

            var musicSheet = new MusicSheetParser(new ChordParser(new NoteParser())).Parse(
                rawMusicSheet.Melody,
                int.Parse(rawMusicSheet.Tempo),
                int.Parse(rawMusicSheet.Meter.Split('/')[0]),
                int.Parse(rawMusicSheet.Meter.Split('/')[1]));

            await Task.Delay(200);

            var algorithm = rawMusicSheet.Algorithm == "favor notes"
                ? new FavorNotesAlgorithm() : (IPlayAlgorithm) new FavorChordsAlgorithm();

            var musicPlayer = new MusicPlayer(musicSheet, new Harp(new GuildWarsKeyboard()), algorithm);

            await musicPlayer.Play();
        }
    }
}