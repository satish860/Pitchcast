namespace Pitchcast.Scrapper
{
    public record PodcastDetailsSlim
    {
        public string Name { get; init; }

        public Uri FeedUrl { get; init; }

        public string Description { get; init; }

        public List<string> GenreList { get; init; }
    }
}
