using CommandLine;

namespace GuildWars2Orchestra.CommandLineInterface
{
    internal class Options
    {
        [Value(0, HelpText = "Input .mid file to convert", Required = true)]
        public string InputMidi { get; set; }

        [Value(1, HelpText = "Destination path for .xml", Required = true)]
        public string OutputXml { get; set; }

        [Option('d', "duration", Default = false, HelpText = "Include chord duration")]
        public bool IncludeChordDuration { get; set; }
    }
}