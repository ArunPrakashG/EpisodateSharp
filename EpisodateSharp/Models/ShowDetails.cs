using Newtonsoft.Json;
using System.Collections.Immutable;

namespace EpisodateSharp.Models {
	public class ShowDetails {
		[JsonProperty("tvShow")]
		public TvShow Show { get; set; }

		public class TvShow : TelevisionShow {
			[JsonProperty("url")]
			public string Url { get; private set; }

			[JsonProperty("description")]
			public string Description { get; private set; }

			[JsonProperty("description_source")]
			public string DescriptionSource { get; private set; }

			[JsonProperty("runtime")]
			public int Runtime { get; private set; }

			[JsonProperty("youtube_link")]
			public string YoutubeLink { get; private set; }

			[JsonProperty("image_path")]
			public string ImageLink { get; private set; }

			[JsonProperty("rating")]
			public string Rating { get; private set; }

			[JsonProperty("rating_count")]
			public string RatingCount { get; private set; }

			[JsonProperty("countdown")]
			public Episode UpcomingEpisode { get; private set; }

			[JsonProperty("genres")]
			public string[] Genres { get; private set; }

			[JsonProperty("pictures")]
			public string[] PictureUrls { get; private set; }

			[JsonProperty("episodes")]
			public ImmutableArray<Episode> Episodes { get; private set; }
		}

		public class Episode {
			[JsonProperty("season")]
			public int SeasonNumber { get; private set; }

			[JsonProperty("episode")]
			public int EpisodeNumber { get; private set; }

			[JsonProperty("name")]
			public string Name { get; private set; }

			[JsonProperty("air_date")]
			public string AiredOn { get; private set; }
		}
	}
}
