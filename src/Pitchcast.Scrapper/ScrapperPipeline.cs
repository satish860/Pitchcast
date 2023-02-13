using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pitchcast.Scrapper
{
    public class ScrapperPipeline
    {
        public List<Genre> GetAllGenre()
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
                    Link = p.Attributes["href"].Value
                };
            }).ToList();
            return genreList;
        }

        public List<Podcast> GetPopularPodcast(string url)
        {
            var web = new HtmlWeb();
            var indivdualGenre = web.Load(url);
            List<Podcast> podcasts = new List<Podcast>();
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
                            Id = GetPodcastId(p.Attributes["href"].Value)

                        };
                    }).ToList();
                }
            }
            return podcasts;
        }

        

        public string GetPodcastId(string url)
        {
            var idfragment = url.Split("/").Last().Replace("id", "");
            return idfragment;
        }
    }


    public class Podcast
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public string Url { get; set; }
    }


   

    public class Genre
    {
        public string Name { get; set; }

        public string Link { get; set; }

        public long Id { get; set; }
    }
}
