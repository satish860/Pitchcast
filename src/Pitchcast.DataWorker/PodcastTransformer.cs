using PodcastIndexSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pitchcast.DataWorker
{
    public class PodcastTransformer 
    {
        private readonly IPodcastIndex podcastIndex;

        public PodcastTransformer(IPodcastIndex podcastIndex)
        {
            this.podcastIndex = podcastIndex;
        }

        public async Task GetPodcastDetails(string id)
        {
            PodcastIndexSharp.Model.Podcast downloadedPodcasts = await podcastIndex.Podcasts().ByiTunesId(uint.Parse(id)).ConfigureAwait(false);
            var episodes = await podcastIndex.Episodes().ByiTunesId(uint.Parse(id), 1000).ConfigureAwait(false);

            IEnumerable<Episode> podcastEpisodes = episodes.Select(p =>
            {
                return new Episode
                {
                    DatePublished = p.DatePublished,
                    Description = p.Description,
                    DownloadLink = p.EnclosureUrl,
                    DownloadMimeType = p.EnclosureType,
                    EpisodeImage = p.FeedImage,
                    EpisodeLength = p.EnclosureLength,
                };
            }).ToList();

            Podcast podcast = new Podcast
            {
                Description = downloadedPodcasts.Description,
                EpisodeCount = downloadedPodcasts.EpisodeCount,
                Id = downloadedPodcasts.Id,
                ImageUrl = downloadedPodcasts.Image,
                Episodes = podcastEpisodes
            };

            // Call database and store the value.
            
        }
    }
}
