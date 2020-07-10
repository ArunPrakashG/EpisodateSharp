using EpisodateSharp.Models;
using System;
using System.Threading.Tasks;

namespace EpisodateSharp.Demo {
	internal class Program {
		public static async Task Main(string[] args) {
			using(EpisodateClient client = new EpisodateClient()) {
				PopularShows popularShows = await client.GetPopularShowsAsync().ConfigureAwait(false);

				if(popularShows == null) {
					Console.WriteLine("Request failed!");
					Console.WriteLine("Check your connection and restart!");
					Console.WriteLine("Press any key to exit...");
					Console.ReadKey();
				}

				for(int i = 0; i < popularShows.ShowsCollection.Length; i++) {
					TelevisionShow show = popularShows.ShowsCollection[i];
					Console.WriteLine(show.Name);
					Console.WriteLine(show.CurrentStatus);
				}

				Console.ReadKey();
			}
		}
	}
}
