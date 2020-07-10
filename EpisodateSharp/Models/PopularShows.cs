using Newtonsoft.Json;

namespace EpisodateSharp.Models {
	public class PopularShows {
		[JsonProperty("total")]
		public string TotalShowsFound { get; set; }

		[JsonProperty("page")]
		public int CurrentPageNumber { get; set; }

		[JsonProperty("pages")]
		public int TotalPages { get; set; }

		[JsonProperty("tv_shows")]
		public TelevisionShow[] ShowsCollection { get; set; }
	}
}
