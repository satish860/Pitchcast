using AlterNats;
using MongoDB.Driver;
using PodcastIndexSharp;
using System.Reflection;

namespace Pitchcast.DataWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var config = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                    services.AddPodcastIndexSharp(config);
                    services.AddSingleton<IMongoClient, MongoClient>(s =>
                    {
                        var uri = s.GetRequiredService<IConfiguration>()["DBHOST"];
                        return new MongoClient(uri);
                    });
                    services.AddSingleton<IMongoCollection<Podcast>>(s =>
                    {
                        var mongoClient = s.GetRequiredService<IMongoClient>();
                        var DBName = s.GetRequiredService<IConfiguration>()["DBNAME"];
                        var database = mongoClient.GetDatabase(DBName);
                        var collection = database.GetCollection<Podcast>("podcast");
                        return collection;
                    });
                    services.AddSingleton<PodcastTransformer>();
                    services.AddNats();
                })
                
                .Build();

            host.Run();
        }
    }
}