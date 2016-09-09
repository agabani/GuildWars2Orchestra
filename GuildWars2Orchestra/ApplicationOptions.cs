using CommandLine;

namespace GuildWars2Orchestra
{
    internal class ApplicationOptions
    {
        [Option('e', "emulated", Default = false, HelpText = "Play music using an emulated music player.")]
        public bool Emulated { get; set; }

        [Option(Default = @"TestData\Guilty Crown - My Dearest.xml")]
        public string File { get; set; }
    }
}