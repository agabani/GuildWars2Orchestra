using System.Threading.Tasks;
using CommandLine;

namespace GuildWars2Orchestra
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Task.Run(async () => await MainAsync(args)).Wait();
        }

        private static async Task MainAsync(string[] args)
        {
            await Parser.Default
                .ParseArguments<ApplicationOptions>(args)
                .MapResult(
                    async options =>
                    {
                        var musicPlayer = MusicPlayerFactory.Create(options);
                        await Task.Delay(200);
                        await musicPlayer.Play();
                    },
                    errors => Task.FromResult(1)
                );
        }
    }
}