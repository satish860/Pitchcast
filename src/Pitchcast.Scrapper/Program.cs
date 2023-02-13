using System.Text.Json;

namespace Pitchcast.Scrapper
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ScrapperPipeline scrapperPipeline = new ScrapperPipeline();
            List<PodcastDetailsSlim> podcasts = new List<PodcastDetailsSlim>();
            foreach (var item in scrapperPipeline.GetAllGenre())
            {
                Console.WriteLine($"Got the Genre of {item.Name} with Url {item.Link}");
                var ids = scrapperPipeline
                    .GetPopularPodcast(item.Link)
                    .Select(p => p.Id);

                foreach (var pagedIds in ids.Page(200))
                {
                    var stringSeperatedIds = string.Join(",", pagedIds);

                    var details = await scrapperPipeline.GetPodCastDetails(stringSeperatedIds);
                    podcasts.AddRange(details);
                }
            }

            var JsonList = JsonSerializer.Serialize(podcasts);
            await File.WriteAllTextAsync("some.json", JsonList);
        }
    }
}