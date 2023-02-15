using AlterNats;

namespace Pitchcast.DataWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                    services.AddNats();
                })
                
                .Build();

            host.Run();
        }
    }
}