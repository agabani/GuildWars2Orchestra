using CommandLine;

namespace GuildWars2Orchestra
{
    internal class ApplicationOptions
    {
        [Option('e', "emulated",
            Default = false,
            HelpText = "Play music using an emulated music player.",
            Required = false)]
        public bool Emulated { get; set; }

        [Value(0,
            Default = @"TestData\Guilty Crown - My Dearest.xml",
            HelpText = @"Location of .xml file or .mid file to be played.")]
        public string File { get; set; }
    }
}