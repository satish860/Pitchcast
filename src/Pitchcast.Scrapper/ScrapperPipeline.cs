using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Pitchcast.Scrapper
{
    public class ScrapperPipeline
    {
        public static IEnumerable<Genre> GetAllGenre()
        {
            const string seedUrl = "https://podcasts.apple.com/us/genre/podcasts/id26?mt=2";
            var web = new HtmlWeb();
            var doc = web.Load(seedUrl);
            var listofLinks = doc.QuerySelectorAll(".top-level-genre");
            var genreList = listofLinks.Select(p =>
            {
                return new Genre
                {
                    Name = p.InnerHtml,
                    Link = p.Attributes["href"].Value,
                };
            }).ToList();
            return genreList;
        }

        public static IEnumerable<Podcast> GetPopularPodcast(string url)
        {
            var web = new HtmlWeb();
            var indivdualGenre = web.Load(url);
            List<Podcast> podcasts = new ();
            foreach (var node in indivdualGenre.QuerySelectorAll("div > div"))
            {
                if (node.Id.Equals("selectedcontent", StringComparison.InvariantCultureIgnoreCase))
                {
                    var links = node.QuerySelectorAll("a");
                    podcasts = links.Select(p =>
                    {
                        return new Podcast
                        {
                            Name = p.InnerHtml,
                            Url = p.Attributes["href"].Value,
                            Id = GetPodcastId(p.Attributes["href"].Value),

                        };
                    }).ToList();
                }
            }
            return podcasts;
        }

        public static async Task<IEnumerable<PodcastDetailsSlim>> GetPodCastDetails(string ids)
        {
            var HttpClient = new HttpClient();
            var response = await HttpClient.GetAsync($"https://itunes.apple.com/lookup?id={ids}&country=US&media=podcast").ConfigureAwait(false);
            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            PodCastDetails details = PodCastDetails.FromJson(stringResponse);
            return details.Results.Select(p =>
             {
                 return new PodcastDetailsSlim
                 {
                     Name = p.CollectionName,
                     FeedUrl = p.FeedUrl,
                     GenreList = p.Genres,
                 };
             });
        }



        private static string GetPodcastId(string url)
        {
            var idfragment = url.Split("/")
                .Last()
                .Replace("id", "", StringComparison.Ordinal);
            return idfragment;
        }
    }
}
