using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EpisodateSharp {
	internal sealed class Requester : IDisposable {
		private const string BASE_ADDRESS = "https://www.episodate.com/";
		private const int MAX_TRIES = 3;
		private readonly int DELAY_BETWEEN_REQUESTS = 1; // in secs
		private static readonly SemaphoreSlim Sync = new SemaphoreSlim(1, 1);
		private readonly HttpClientHandler ClientHandler;
		private readonly HttpClient Client;

		internal Requester(HttpClientHandler _httpClientHandler, int _delayBetweenRequests = 1) {
			ClientHandler = _httpClientHandler ?? throw new ArgumentNullException(nameof(_httpClientHandler) + " cannot be null!");
			DELAY_BETWEEN_REQUESTS = _delayBetweenRequests;
			Client = new HttpClient(ClientHandler, true);
		}

		internal Requester(IWebProxy _proxy, int _delayBetweenRequests = 1) {
			ClientHandler = new HttpClientHandler() {
				Proxy = _proxy ?? throw new ArgumentNullException(nameof(_proxy) + " cannot be null!"),
				UseProxy = true
			};
			DELAY_BETWEEN_REQUESTS = _delayBetweenRequests;
			Client = new HttpClient(ClientHandler, true);
		}

		internal Requester(int _delayBetweenRequests = 1) {
			ClientHandler = new HttpClientHandler();
			DELAY_BETWEEN_REQUESTS = _delayBetweenRequests;
			Client = new HttpClient(ClientHandler, true);
		}

		internal async Task<T> InternalRequestAsObject<T>(string requestRelativeUrl, int maxTries = MAX_TRIES) {
			if (string.IsNullOrEmpty(requestRelativeUrl) || !ParseAsUri(requestRelativeUrl, out Uri requestUri)) {
				throw new ArgumentNullException(nameof(requestRelativeUrl) + " cannot be null!");
			}

			bool success = false;

			for (int i = 0; i < maxTries; i++) {
				try {
					using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri)) {
						using (HttpResponseMessage response = await ExecuteRequest(async () => await Client.SendAsync(request).ConfigureAwait(false)).ConfigureAwait(false)) {
							if (response.StatusCode == HttpStatusCode.Forbidden) {
								throw new Exception("Request failed.");
							}

							if (!response.IsSuccessStatusCode) {
								continue;
							}

							using (HttpContent responseContent = response.Content) {
								string jsonContent = await responseContent.ReadAsStringAsync().ConfigureAwait(false);

								if (string.IsNullOrEmpty(jsonContent)) {
									continue;
								}

								success = true;
								return JsonConvert.DeserializeObject<T>(jsonContent);
							}
						}
					}
				}
				catch (Exception) {
					success = false;
					continue;
				}
				finally {
					if (!success) {
						await Task.Delay(TimeSpan.FromSeconds(DELAY_BETWEEN_REQUESTS)).ConfigureAwait(false);
					}
				}
			}

			if (!success) {
				throw new Exception($"Request failed after multiple tries. ({maxTries})");
			}

			return default;
		}

		private async Task<T> ExecuteRequest<T>(Func<Task<T>> function) {
			if (function == null) {
				return default;
			}

			await Sync.WaitAsync().ConfigureAwait(false);

			try {
				return await function().ConfigureAwait(false);
			}
			finally {
				await Task.Delay(TimeSpan.FromSeconds(DELAY_BETWEEN_REQUESTS));
				Sync.Release();
			}
		}

		private bool ParseAsUri(string requestRelativeUrl, out Uri requestUri) {
			requestUri = null;

			if (string.IsNullOrEmpty(requestRelativeUrl)) {
				return false;
			}

			try {
				requestUri = new Uri(BASE_ADDRESS + requestRelativeUrl);
				return true;
			}
			catch (Exception) {
				return false;
			}
		}

		/// <summary>
		/// <inheritdoc />
		/// </summary>
		public void Dispose() {
			Client?.Dispose();
			Sync?.Dispose();
		}
	}
}
