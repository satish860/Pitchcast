using AlterNats;

namespace Pitchcast.DataWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly INatsCommand natsCommand;
        private readonly PodcastTransformer podcastTransformer;

        public Worker(ILogger<Worker> logger, INatsCommand natsCommand, PodcastTransformer podcastTransformer)
        {
            _logger = logger;
            this.natsCommand = natsCommand;
            this.podcastTransformer = podcastTransformer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           var subscribe = await natsCommand.QueueSubscribeAsync<string>("hello", "hello.queue", async (message) =>
            {
                _logger.LogInformation("Got the Podcast with id {message}", message);
                await podcastTransformer.GetPodcastDetails(message);
            }).ConfigureAwait(false);
           
            while (!stoppingToken.IsCancellationRequested)
            {
                // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //await Task.Delay(1000, stoppingToken);
            }
            subscribe.Dispose();
        }
    }
}