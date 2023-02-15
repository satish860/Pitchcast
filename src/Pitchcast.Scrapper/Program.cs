﻿using Microsoft.Extensions.Configuration;
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
                 }).Build();
            using var serviceScope = host.Services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var podcastIndex = provider.GetService(typeof(IPodcastIndex)) as IPodcastIndex;

            List<PodcastDetailsSlim> podcasts = new List<PodcastDetailsSlim>();
            foreach (var item in ScrapperPipeline.GetAllGenre())
            {
                Console.WriteLine($"Got the Genre of {item.Name} with Url {item.Link}");
                var ids = ScrapperPipeline
                    .GetPopularPodcast(item.Link)
                    .Select(p => p.Id).First();
                PodcastIndexSharp.Model.Podcast podcast = await podcastIndex.Podcasts().ByiTunesId(uint.Parse(ids)).ConfigureAwait(false);
                Console.WriteLine($"Podcast by iTunes ID: Podcast \"{podcast.Title}\" was last updated {podcast.LastUpdateTime}");

                var episode = await podcastIndex.Episodes().ByiTunesId(uint.Parse(ids),1000).ConfigureAwait(false);
                Console.ReadKey();


                //foreach (var pagedIds in ids.Page(200))
                //{
                //    var stringSeperatedIds = string.Join(",", pagedIds);

                //    var details = await ScrapperPipeline.GetPodCastDetails(stringSeperatedIds).ConfigureAwait(false);
                //    podcasts.AddRange(details);
                //}
            }

            //List<PodcastEpisode> podcastedpisode = new List<PodcastEpisode>();
            //Console.WriteLine($"Total Podcast in popular category {podcasts.Count}");
            //var count = 0;
            //foreach (var podcast in podcasts)
            //{
            //    Console.WriteLine($"I am in {count} out of {podcasts.Count}");
            //    if (podcast.FeedUrl != null)
            //    {
            //        var episode = await ScrapperPipeline
            //                            .GetPodcastEpisodesAsync(podcast.FeedUrl.OriginalString)
            //                            .ConfigureAwait(false);
            //        podcastedpisode.AddRange(episode);
            //    }

            //    count++;
            //}
            //var JsonList = JsonSerializer.Serialize(podcastedpisode);
            //await File.WriteAllTextAsync("someepisode.json", JsonList).ConfigureAwait(false);
        }



    }
}