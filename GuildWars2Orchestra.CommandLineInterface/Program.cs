using CommandLine;

namespace GuildWars2Orchestra.CommandLineInterface
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<Options>(args)
                .MapResult(
                    options =>
                    {
                        new Application(options).Run();
                        return 0;
                    },
                    errors => 1
                );
        }
    }
}