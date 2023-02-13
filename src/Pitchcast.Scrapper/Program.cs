namespace Pitchcast.Scrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ScrapperPipeline scrapperPipeline = new ScrapperPipeline();
            foreach (var item in scrapperPipeline.GetAllGenre())
            {
                Console.WriteLine($"Got the Genre of {item.Name} with Url {item.Link}");
                foreach (var podcast in scrapperPipeline.GetPopularPodcast(item.Link))
                {
                    Console.WriteLine($"  Popular Podcast in {item.Name} with {podcast.Id} and {podcast.Name}");
                } 
            } 
        }
    }
}