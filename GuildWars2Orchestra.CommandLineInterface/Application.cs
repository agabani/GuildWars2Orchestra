using GuildWars2Orchestra.Midi;
using GuildWars2Orchestra.MusicBoxNotation.Persistance;
using GuildWars2Orchestra.MusicBoxNotation.Serializer;

namespace GuildWars2Orchestra.CommandLineInterface
{
    internal class Application
    {
        private readonly Options _options;

        internal Application(Options options)
        {
            _options = options;
        }

        internal void Run()
        {
            var musicSheet = new MidiParser().Parse(_options.InputMidi);

            var rawMusicSheet = new MusicSheetSerializer(new ChordOffsetSerializer(new ChordSerializer(new NoteSerializer(), _options.IncludeChordDuration))).Serialize(musicSheet);

            new XmlMusicSheetWriter().SaveToFile(rawMusicSheet, _options.OutputXml);
        }
    }
}