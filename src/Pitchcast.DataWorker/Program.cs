using AlterNats;
using PodcastIndexSharp;
using System.Reflection;

namespace Pitchcast.DataWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                    services.AddPodcastIndexSharp(config);
                    services.AddSingleton<PodcastTransformer>();
                    services.AddNats();
                })
                
                .Build();

            host.Run();
        }
    }
}