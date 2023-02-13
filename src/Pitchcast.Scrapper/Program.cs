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
            } 
        }
    }
}