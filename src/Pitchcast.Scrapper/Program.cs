using AlterNats;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PodcastIndexSharp;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks.Dataflow;

namespace Pitchcast.Scrapper
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();
            var host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices(s =>
                 {
                     s.AddPodcastIndexSharp(config);
                     s.AddNats();
                 }).Build();
            using var serviceScope = host.Services.CreateAsyncScope();
            var provider = serviceScope.ServiceProvider;

            var podcastIndex = provider.GetService(typeof(IPodcastIndex)) as IPodcastIndex;
            var Publisher = provider.GetService<INatsCommand>();

            List<PodcastDetailsSlim> podcasts = new List<PodcastDetailsSlim>();
            foreach (var item in ScrapperPipeline.GetAllGenre())
            {
                Console.WriteLine($"Got the Genre of {item.Name} with Url {item.Link}");
                var ids = ScrapperPipeline
                    .GetPopularPodcast(item.Link)
                    .Select(p => p.Id);
                Console.WriteLine($"There are {ids.Count()} of Podcast");
                var i = 0;
                foreach (var id in ids)
                {
                    i++;
                    await Publisher.PublishAsync("hello", id).ConfigureAwait(false);
                    Console.WriteLine($"I am in {i} in of the total {ids.Count()}");
                    Thread.Sleep(500);
                }
            }

          
        }



    }
}