using System.Linq;
using System.Xml.Linq;

namespace GuildWars2Orchestra.Persistance
{
    public class XmlMusicSheetReader
    {
        public RawMusicSheet LoadFromFile(string path)
        {
            var xDocument = XDocument.Load(path);

            return new RawMusicSheet(
                xDocument.Elements().Single().Elements("name").SingleOrDefault()?.Value,
                xDocument.Elements().Single().Elements("tempo").SingleOrDefault()?.Value,
                xDocument.Elements().Single().Elements("meter").SingleOrDefault()?.Value,
                xDocument.Elements().Single().Elements("melody").SingleOrDefault()?.Value,
                xDocument.Elements().Single().Elements("algorithm").SingleOrDefault()?.Value
                );
        }
    }
}