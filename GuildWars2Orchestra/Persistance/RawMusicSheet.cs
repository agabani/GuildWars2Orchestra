namespace GuildWars2Orchestra.Persistance
{
    public class RawMusicSheet
    {
        public RawMusicSheet(string name, string tempo, string meter, string melody, string algorithm)
        {
            Name = name;
            Tempo = tempo;
            Melody = melody;
            Algorithm = algorithm;
            Meter = meter;
        }

        public string Name { get; private set; }
        public string Tempo { get; private set; }
        public string Melody { get; private set; }
        public string Meter { get; private set; }
        public string Algorithm { get; private set; }
    }
}