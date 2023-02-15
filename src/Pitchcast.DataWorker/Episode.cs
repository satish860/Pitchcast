using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pitchcast.DataWorker
{
    public class Episode
    {
        public string Title { get; set; }

        public DateTime? DatePublished { get; set; }

        public string Description { get; set; }

        public long EpisodeLength { get; set; }

        public int SizeToDownload { get; set; }

        public Uri DownloadLink { get; set; }

        public string DownloadMimeType { get; set; }

        public Uri EpisodeImage { get; set; }
    }
}
