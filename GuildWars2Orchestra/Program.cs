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
            var musicSheet = new MusicSheetParser(new ChordParser(new NoteParser()))
                .Parse(
                    Melodies.FinalFantasyXiii2.AWish.Melody,
                    Melodies.FinalFantasyXiii2.AWish.Tempo,
                    Melodies.FinalFantasyXiii2.AWish.Nominator,
                    Melodies.FinalFantasyXiii2.AWish.Denominator
                );

            var harp = new Harp(new GuildWarsKeyboard());

            Thread.Sleep(200);

            var musicPlayer = new MusicPlayer(musicSheet, harp);

            musicPlayer.Play();

//            _harp.PlayNote(new Note(Note.Keys.Note1, Note.Octaves.Low));
//            _harp.PlayNote(new Note(Note.Keys.Note2, Note.Octaves.Low));
//            _harp.PlayNote(new Note(Note.Keys.Note3, Note.Octaves.Low));
//            _harp.PlayNote(new Note(Note.Keys.Note4, Note.Octaves.Low));
//            _harp.PlayNote(new Note(Note.Keys.Note5, Note.Octaves.Low));
//            _harp.PlayNote(new Note(Note.Keys.Note6, Note.Octaves.Low));
//            _harp.PlayNote(new Note(Note.Keys.Note7, Note.Octaves.Low));
//            _harp.PlayNote(new Note(Note.Keys.Note8, Note.Octaves.Low));

//            _harp.PlayNote(new Note(Note.Keys.Note1, Note.Octaves.Middle));
//            _harp.PlayNote(new Note(Note.Keys.Note2, Note.Octaves.Middle));
//            _harp.PlayNote(new Note(Note.Keys.Note3, Note.Octaves.Middle));
//            _harp.PlayNote(new Note(Note.Keys.Note4, Note.Octaves.Middle));
//            _harp.PlayNote(new Note(Note.Keys.Note5, Note.Octaves.Middle));
//            _harp.PlayNote(new Note(Note.Keys.Note6, Note.Octaves.Middle));
//            _harp.PlayNote(new Note(Note.Keys.Note7, Note.Octaves.Middle));
//            _harp.PlayNote(new Note(Note.Keys.Note8, Note.Octaves.Middle));

//            _harp.PlayNote(new Note(Note.Keys.Note1, Note.Octaves.High));
//            _harp.PlayNote(new Note(Note.Keys.Note2, Note.Octaves.High));
//            _harp.PlayNote(new Note(Note.Keys.Note3, Note.Octaves.High));
//            _harp.PlayNote(new Note(Note.Keys.Note4, Note.Octaves.High));
//            _harp.PlayNote(new Note(Note.Keys.Note5, Note.Octaves.High));
//            _harp.PlayNote(new Note(Note.Keys.Note6, Note.Octaves.High));
//            _harp.PlayNote(new Note(Note.Keys.Note7, Note.Octaves.High));
//            _harp.PlayNote(new Note(Note.Keys.Note8, Note.Octaves.High));
        }
    }
}