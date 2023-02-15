using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pitchcast.DataWorker
{
    public class Podcast
    {
        public string? Name { get; set; }

        public int Id { get; set; }

        public Uri? ImageUrl { get; set; }

        public string ItunesId { get; set; }

        public string Description { get; set; }

        public int? EpisodeCount { get; set; }

        public IEnumerable<Episode> Episodes { get; set; }
    }
}
