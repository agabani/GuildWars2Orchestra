using System.Threading;
using GuildWars2Orchestra.Controls;
using GuildWars2Orchestra.Instrument;
using GuildWars2Orchestra.Parsers;
using GuildWars2Orchestra.Persistance;
using GuildWars2Orchestra.Player;

namespace GuildWars2Orchestra
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var xmlMusicSheetReader = new XmlMusicSheetReader(new MusicSheetParser(new ChordParser(new NoteParser())));

            var musicSheet = xmlMusicSheetReader.LoadFromFile(@"TestData\Guilty Crown - My Dearest.xml");

            var harp = new Harp(new GuildWarsKeyboard());

            Thread.Sleep(200);

            var musicPlayer = new MusicPlayer(musicSheet, harp);

            musicPlayer.Play();
        }
    }
}