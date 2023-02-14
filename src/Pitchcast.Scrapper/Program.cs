using System.Text.Json;

namespace Pitchcast.Scrapper
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            List<PodcastDetailsSlim> podcasts = new List<PodcastDetailsSlim>();
            foreach (var item in ScrapperPipeline.GetAllGenre())
            {
                Console.WriteLine($"Got the Genre of {item.Name} with Url {item.Link}");
                var ids = ScrapperPipeline
                    .GetPopularPodcast(item.Link)
                    .Select(p => p.Id);

                foreach (var pagedIds in ids.Page(200))
                {
                    var stringSeperatedIds = string.Join(",", pagedIds);

                    var details = await ScrapperPipeline.GetPodCastDetails(stringSeperatedIds).ConfigureAwait(false);
                    podcasts.AddRange(details);
                }
            }

            var JsonList = JsonSerializer.Serialize(podcasts);
            await File.WriteAllTextAsync("some.json", JsonList).ConfigureAwait(false);
        }
    }
}