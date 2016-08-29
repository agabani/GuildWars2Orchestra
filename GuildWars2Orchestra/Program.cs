using System;
using System.Threading;
using GuildWars2Orchestra.Controls;
using GuildWars2Orchestra.Parsers;
using GuildWars2Orchestra.TestData;

namespace GuildWars2Orchestra
{
    internal class Program
    {
        private static GuildWarsKeyboard _keyboard;

        private static void Main(string[] args)
        {
            var musicSheet = new MusicSheetParser(new ChordParser(new NoteParser())).Parse(
                Melodies.FinalFantasyXiii2.AWish.Melody,
                Melodies.FinalFantasyXiii2.AWish.Tempo,
                Melodies.FinalFantasyXiii2.AWish.Nominator,
                Melodies.FinalFantasyXiii2.AWish.Denominator);


            _keyboard = new GuildWarsKeyboard();

            PressNote("1");
            PressNote("2");
            PressNote("3");
            PressNote("4");
            PressNote("5");
            PressNote("6");
            PressNote("7");
            PressNote("8");
        }

        private static void PressNote(string note)
        {
            _keyboard.Press(note);
            Thread.Sleep(TimeSpan.FromMilliseconds(30));
            _keyboard.Release(note);
        }
    }
}