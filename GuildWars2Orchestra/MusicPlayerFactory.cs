using System;
using System.IO;
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
    internal static class MusicPlayerFactory
    {
        internal static MusicPlayer Create(ApplicationOptions options)
        {
            return GetMusicSheet(options);
        }

        private static MusicPlayer GetMusicSheet(ApplicationOptions options)
        {
            var extension = Path.GetExtension(options.File)?.ToLower();

            switch (extension)
            {
                case ".xml":
                    return MusicBoxNotationMusicPlayerFactory(options);
                case ".mid":
                    return MidiMusicPlayerFactory(options);
                default:
                    throw new NotSupportedException(extension);
            }
        }

        private static MusicPlayer MusicBoxNotationMusicPlayerFactory(ApplicationOptions filePath)
        {
            var rawMusicSheet = new XmlMusicSheetReader().LoadFromFile(filePath.File);

            var musicSheet = new MusicSheetParser(new ChordParser(new NoteParser())).Parse(
                rawMusicSheet.Melody,
                int.Parse(rawMusicSheet.Tempo),
                int.Parse(rawMusicSheet.Meter.Split('/')[0]),
                int.Parse(rawMusicSheet.Meter.Split('/')[1]));

            var algorithm = rawMusicSheet.Algorithm == "favor notes"
                ? new FavorNotesAlgorithm() : (IPlayAlgorithm) new FavorChordsAlgorithm();

            return new MusicPlayer(
                musicSheet,
                new Harp(GetKeyboard(filePath)),
                algorithm);
        }

        private static MusicPlayer MidiMusicPlayerFactory(ApplicationOptions filePath)
        {
            return new MusicPlayer(
                new MidiParser().Parse(filePath.File),
                new Harp(GetKeyboard(filePath)),
                new FavorChordsAlgorithm());
        }

        private static IKeyboard GetKeyboard(ApplicationOptions options)
        {
            return options.Emulated ? (IKeyboard) new HarpEmulator() : new GuildWarsKeyboard();
        }
    }
}