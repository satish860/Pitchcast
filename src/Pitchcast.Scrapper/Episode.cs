using CodeHollow.FeedReader.Feeds.Itunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pitchcast.Scrapper
{
    public class Episode
    {

        //
        // Summary:
        //     The itunes:author element
        public string Author { get; }

        //
        // Summary:
        //     The itunes:block element
        public bool Block { get; }

        //
        // Summary:
        //     The itunes:image element
        public ItunesImage Image { get; }

        //
        // Summary:
        //     The itunes:duration element
        public TimeSpan? Duration { get; }

        //
        // Summary:
        //     The itunes:explicit element
        public bool Explicit { get; }

        //
        // Summary:
        //     The itunes:isClosedCaptioned element
        public bool IsClosedCaptioned { get; }

        //
        // Summary:
        //     The itunes:order element
        public int Order { get; }

        //
        // Summary:
        //     The itunes:subtitle element
        public string Subtitle { get; }

        //
        // Summary:
        //     The itunes:summary element
        public string Summary { get; }

        /// <summary>
        /// The Itunes:Title element.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Media Url
        /// </summary>
        public string MediaUrl { get; set; }

        //
        // Summary:
        //     Initializes a new instance of the CodeHollow.FeedReader.Feeds.Itunes.ItunesItem
        //     class.
        //
        // Parameters:
        //   itemElement:
        public Episode(XElement itemElement)
        {
            Author = itemElement.GetValue("itunes", "author");
            Block = itemElement.GetValue("itunes", "block").EqualsIgnoreCase("yes");
            XElement element = itemElement.GetElement("itunes", "image");
            if (element != null)
            {
                Image = new ItunesImage(element);
            }

            string value = itemElement.GetValue("itunes", "duration");
            Duration = ParseDuration(value);
            string value2 = itemElement.GetValue("itunes", "explicit");
            Explicit = value2.EqualsIgnoreCase("yes", "explicit", "true");
            IsClosedCaptioned = itemElement.GetValue("itunes", "isClosedCaptioned").EqualsIgnoreCase("yes");
            if (int.TryParse(itemElement.GetValue("itunes", "order"), out var result))
            {
                Order = result;
            }

            Subtitle = itemElement.GetValue("itunes", "subtitle");
            Title = itemElement.GetValue("itunes", "title");
            Summary = itemElement.GetValue("itunes", "summary");
            XElement audioUrl = itemElement.GetElement("enclosure");
            if (element != null)
            {
                MediaUrl = audioUrl.GetAttributeValue("url");
            }
            //MediaUrl = itemElement.GetAttribute("enclosure",);
        }

       

        private static TimeSpan? ParseDuration(string duration)
        {
            if (string.IsNullOrWhiteSpace(duration))
            {
                return null;
            }

            string[] array = duration.Split(':');
            if (array.Length == 1 && long.TryParse(array[0], out var result))
            {
                return TimeSpan.FromSeconds(result);
            }

            if (array.Length == 2 && int.TryParse(array[0], out var result2) && int.TryParse(array[1], out var result3))
            {
                return new TimeSpan(0, result2, result3);
            }

            if (array.Length == 3 && int.TryParse(array[0], out var result4) && int.TryParse(array[1], out var result5) && int.TryParse(array[2], out var result6))
            {
                return new TimeSpan(result4, result5, result6);
            }

            return null;
        }
    }
}

