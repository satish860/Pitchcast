namespace Pitchcast.Scrapper
{
    public class PodcastDetailsSlim
    {
        public string Name { get; set; }

        public Uri FeedUrl { get; set; }

        public string Description { get; set; }

        public List<string> GenreList { get; set; }
    }
}
