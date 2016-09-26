using System.IO;
using System.Xml.Linq;

namespace GuildWars2Orchestra.MusicBoxNotation.Persistance
{
    public class XmlMusicSheetWriter
    {
        public void SaveToFile(RawMusicSheet musicSheet, string path)
        {
            var xDocument = new XDocument(
                new XElement("song",
                    new XElement("name", musicSheet.Name),
                    new XElement("instrument", "harp"),
                    new XElement("tempo", musicSheet.Tempo),
                    new XElement("meter", musicSheet.Meter),
                    new XElement("algorithm", "favor notes"),
                    new XElement("melody", musicSheet.Melody))
                );

            using (var fileStream = File.Open(path, FileMode.Create))
            {
                xDocument.Save(fileStream);
            }
        }
    }
}