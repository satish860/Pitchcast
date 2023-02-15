using AlterNats;

namespace Pitchcast.DataWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly INatsCommand natsCommand;

        public Worker(ILogger<Worker> logger, INatsCommand natsCommand)
        {
            _logger = logger;
            this.natsCommand = natsCommand;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           var subscribe = await natsCommand.QueueSubscribeAsync<string>("hello", "hello.queue", (message) =>
            {
                _logger.LogInformation("Worker running at: {message}", message);

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