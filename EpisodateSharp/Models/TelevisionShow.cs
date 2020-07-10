using Newtonsoft.Json;

namespace EpisodateSharp.Models {
	public class TelevisionShow {
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("permalink")]
		public string PermaLink { get; set; }

		[JsonProperty("start_date")]
		public string FirstAiredDate { get; set; }

		[JsonProperty("end_date")]
		public string LastAiredDate { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("network")]
		public string Network { get; set; }

		[JsonProperty("status")]
		public string CurrentStatus { get; set; }

		[JsonProperty("image_thumbnail_path")]
		public string ImageThumbnailUrl { get; set; }
	}
}
