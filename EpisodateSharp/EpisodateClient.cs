using EpisodateSharp.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EpisodateSharp {
	/// <summary>
	/// Client used to unteract with https://www.episodate.com/api
	/// </summary>
	public sealed class EpisodateClient : IDisposable {
		private readonly Requester Requester;

		/// <summary>
		/// Constructor to pass <see cref="HttpClientHandler"/> instance to the internal <see cref="Requester"/> Instance.
		/// </summary>
		/// <param name="_httpClientHandler">The <see cref="HttpClientHandler"/></param>
		/// <param name="_delayBetweenRequests">The delay between each internal request, in seconds.</param>
		public EpisodateClient(HttpClientHandler _httpClientHandler, int _delayBetweenRequests = 1) => Requester = new Requester(_httpClientHandler, _delayBetweenRequests);

		/// <summary>
		/// Constructor to use <see cref="IWebProxy"/> for internal requests.
		/// </summary>
		/// <param name="_proxy">The <see cref="IWebProxy"/></param>
		/// <param name="_delayBetweenRequests">The delay between each internal request, in seconds.</param>
		public EpisodateClient(IWebProxy _proxy, int _delayBetweenRequests = 1) => Requester = new Requester(_proxy, _delayBetweenRequests);

		/// <summary>
		/// Default constructor to initialize <see cref="Requester"/> with default configuration.
		/// </summary>		
		/// <param name="_delayBetweenRequests">The delay between each internal request, in seconds.</param>
		public EpisodateClient(int _delayBetweenRequests = 1) => Requester = new Requester(_delayBetweenRequests);

		/// <summary>
		/// Gets all popular TV shows.
		/// </summary>
		/// <param name="pageNumberToFetch">The page to fetch.</param>
		/// <returns><see cref="PopularShows"/></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task<PopularShows> GetPopularShowsAsync(int pageNumberToFetch = 1) {
			pageNumberToFetch = pageNumberToFetch <= 0 ? 1 : pageNumberToFetch;
			string requestRelativeUrl = "api/most-popular?page=" + pageNumberToFetch;
			return await Requester.InternalRequestAsObject<PopularShows>(requestRelativeUrl).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets a TV show details.
		/// </summary>
		/// <param name="showName">The name of the show to get the details of.</param>
		/// <returns><see cref="ShowDetails"/></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task<ShowDetails> GetShowDetailsAsync(string showName) {
			if (string.IsNullOrEmpty(showName)) {
				throw new ArgumentNullException(nameof(showName) + " cannot be null!");
			}

			string requestRelativeUrl = "api/show-details?q=" + showName;
			return await Requester.InternalRequestAsObject<ShowDetails>(requestRelativeUrl).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets a TV show details.
		/// </summary>
		/// <param name="showId">The id of the TV show to get the details of.</param>
		/// <returns><see cref="ShowDetails"/></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task<ShowDetails> GetShowDetailsAsync(int showId) {
			if(showId <= 0) {
				throw new ArgumentOutOfRangeException(nameof(showId) + " is invalid!");
			}

			string requestRelativeUrl = $"api/show-details?q={showId}";
			return await Requester.InternalRequestAsObject<ShowDetails>(requestRelativeUrl).ConfigureAwait(false);
		}

		/// <summary>
		/// Search for a show based on query.
		/// </summary>
		/// <param name="searchQuery">The search query. (show-name, id...)</param>
		/// <param name="pageNumberToFetch">The page to fetch.</param>
		/// <returns><see cref="ShowDetails"/></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task<SearchResult> SearchShowAsync(string searchQuery, int pageNumberToFetch = 1) {
			pageNumberToFetch = pageNumberToFetch <= 0 ? 1 : pageNumberToFetch;
			string requestRelativeUrl = $"api/search?q={searchQuery}&page={pageNumberToFetch}";
			return await Requester.InternalRequestAsObject<SearchResult>(requestRelativeUrl).ConfigureAwait(false);
		}

		/// <summary>
		/// <inheritdoc />
		/// </summary>
		public void Dispose() => Requester?.Dispose();
	}
}
