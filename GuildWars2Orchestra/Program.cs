using System.Threading;
using CommandLine;

namespace GuildWars2Orchestra
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<ApplicationOptions>(args)
                .MapResult(
                    options =>
                    {
                        var musicPlayer = MusicPlayerFactory.Create(options);
                        Thread.Sleep(200);
                        musicPlayer.Play();
                        return 0;
                    },
                    errors => 1
                );
        }
    }
}