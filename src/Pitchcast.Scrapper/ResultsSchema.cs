﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Pitchcast.Scrapper;
//
//    var PodCastDetails = PodCastDetails.FromJson(jsonString);

namespace Pitchcast.Scrapper
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PodCastDetails
    {
        [JsonProperty("resultCount")]
        public long ResultCount { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("artistId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ArtistId { get; set; }

        [JsonProperty("artistIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> ArtistIds { get; set; }

        [JsonProperty("artistName", NullValueHandling = NullValueHandling.Ignore)]
        public string ArtistName { get; set; }

        [JsonProperty("artistViewUrl")]
        public Uri ArtistViewUrl { get; set; }

        [JsonProperty("artworkUrl100", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ArtworkUrl100 { get; set; }

        [JsonProperty("artworkUrl160", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ArtworkUrl160 { get; set; }

        [JsonProperty("artworkUrl30", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ArtworkUrl30 { get; set; }

        [JsonProperty("artworkUrl60")]
        public Uri ArtworkUrl60 { get; set; }

        [JsonProperty("artworkUrl600")]
        public Uri ArtworkUrl600 { get; set; }

        [JsonProperty("closedCaptioning", NullValueHandling = NullValueHandling.Ignore)]
        public string ClosedCaptioning { get; set; }

        [JsonProperty("collectionCensoredName", NullValueHandling = NullValueHandling.Ignore)]
        public string CollectionCensoredName { get; set; }

        [JsonProperty("collectionExplicitness", NullValueHandling = NullValueHandling.Ignore)]
        public string CollectionExplicitness { get; set; }

        [JsonProperty("collectionHdPrice", NullValueHandling = NullValueHandling.Ignore)]
        public long? CollectionHdPrice { get; set; }

        [JsonProperty("collectionId")]
        public long CollectionId { get; set; }

        [JsonProperty("collectionName")]
        public string CollectionName { get; set; }

        [JsonProperty("collectionPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? CollectionPrice { get; set; }

        [JsonProperty("collectionViewUrl")]
        public Uri CollectionViewUrl { get; set; }

        [JsonProperty("contentAdvisoryRating")]
        public string ContentAdvisoryRating { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("episodeContentType", NullValueHandling = NullValueHandling.Ignore)]
        public string EpisodeContentType { get; set; }

        [JsonProperty("episodeFileExtension", NullValueHandling = NullValueHandling.Ignore)]
        public string EpisodeFileExtension { get; set; }

        [JsonProperty("episodeGuid", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? EpisodeGuid { get; set; }

        [JsonProperty("episodeUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri EpisodeUrl { get; set; }

        [JsonProperty("feedUrl")]
        public Uri FeedUrl { get; set; }

        [JsonProperty("genreIds", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(DecodeArrayConverter))]
        public List<long> GenreIds { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("previewUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri PreviewUrl { get; set; }

        [JsonProperty("primaryGenreName", NullValueHandling = NullValueHandling.Ignore)]
        public string PrimaryGenreName { get; set; }

        [JsonProperty("releaseDate")]
        public DateTimeOffset ReleaseDate { get; set; }

        [JsonProperty("shortDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string ShortDescription { get; set; }

        [JsonProperty("trackCensoredName", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackCensoredName { get; set; }

        [JsonProperty("trackCount", NullValueHandling = NullValueHandling.Ignore)]
        public long? TrackCount { get; set; }

        [JsonProperty("trackExplicitness", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackExplicitness { get; set; }

        [JsonProperty("trackId")]
        public long TrackId { get; set; }

        [JsonProperty("trackName")]
        public string TrackName { get; set; }

        [JsonProperty("trackPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? TrackPrice { get; set; }

        [JsonProperty("trackTimeMillis")]
        public long TrackTimeMillis { get; set; }

        [JsonProperty("trackViewUrl")]
        public Uri TrackViewUrl { get; set; }

        [JsonProperty("wrapperType")]
        public string WrapperType { get; set; }
    }

    public partial class PodCastDetails
    {
        public static PodCastDetails FromJson(string json) => JsonConvert.DeserializeObject<PodCastDetails>(json, Pitchcast.Scrapper.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this PodCastDetails self) => JsonConvert.SerializeObject(self, Pitchcast.Scrapper.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class DecodeArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(List<long>);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            reader.Read();
            var value = new List<long>();
            while (reader.TokenType != JsonToken.EndArray)
            {
                var converter = ParseStringConverter.Singleton;
                var arrayItem = (long)converter.ReadJson(reader, typeof(long), null, serializer);
                value.Add(arrayItem);
                reader.Read();
            }
            return value;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (List<long>)untypedValue;
            writer.WriteStartArray();
            foreach (var arrayItem in value)
            {
                var converter = ParseStringConverter.Singleton;
                converter.WriteJson(writer, arrayItem, serializer);
            }
            writer.WriteEndArray();
            return;
        }

        public static readonly DecodeArrayConverter Singleton = new DecodeArrayConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}