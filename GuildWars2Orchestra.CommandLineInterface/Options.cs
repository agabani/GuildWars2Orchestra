using CommandLine;

namespace GuildWars2Orchestra.CommandLineInterface
{
    internal class Options
    {
        [Value(0, HelpText = "Input .mid file to convert", Required = true)]
        internal string InputMidi { get; set; }

        [Value(1, HelpText = "Destination path for .xml", Required = true)]
        internal string OutputXml { get; set; }
    }
}