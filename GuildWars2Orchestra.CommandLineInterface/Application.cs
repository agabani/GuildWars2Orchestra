using GuildWars2Orchestra.Domain;
using GuildWars2Orchestra.Midi;
using GuildWars2Orchestra.MusicBoxNotation.Parsers;
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
            MusicSheet musicSheet = new MidiParser().Parse(_options.InputMidi);

            RawMusicSheet rawMusicSheet = new MusicSheetSerializer(new ChordOffsetSerializer(new ChordSerializer(new NoteSerializer()))).Serialize(musicSheet);
        }
    }
}