using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Collections.Generic;
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
    }

    public class Genre
    {
        public string Name { get; set; }

        public string Link { get; set; }

        public long Id { get; set; }
    }
}
