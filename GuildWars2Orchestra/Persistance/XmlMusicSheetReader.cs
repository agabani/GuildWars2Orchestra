using System.Linq;
using System.Xml.Linq;
using GuildWars2Orchestra.Music;
using GuildWars2Orchestra.Parsers;

namespace GuildWars2Orchestra.Persistance
{
    public class XmlMusicSheetReader
    {
        private readonly MusicSheetParser _musicSheetParser;

        public XmlMusicSheetReader(MusicSheetParser musicSheetParser)
        {
            _musicSheetParser = musicSheetParser;
        }

        public MusicSheet LoadFromFile(string path)
        {
            var xDocument = XDocument.Load(path);

            var name = xDocument.Elements().Single().Elements("name").Single().Value;
            var tempo = xDocument.Elements().Single().Elements("tempo").Single().Value;
            var melody = xDocument.Elements().Single().Elements("melody").Single().Value;
            var meter = xDocument.Elements().Single().Elements("meter").Single().Value.Split('/');

            var musicSheet = _musicSheetParser.Parse(
                melody,
                int.Parse(tempo),
                int.Parse(meter[0]),
                int.Parse(meter[1]));

            return musicSheet;
        }
    }
}