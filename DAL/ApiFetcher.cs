using System;
using System.Net;
using System.Text.Json;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DAL
{
	public class ApiFetcher : IApiFetcher
	{
		private readonly IConfiguration _config;
		private ILogger<IApiFetcher> _logger;
		private string baseUrl {
			get {
				return _config.GetSection("ApiUrl").Value;
			}
		}

		public ApiFetcher(IConfiguration config, ILogger<IApiFetcher> logger )
		{
			_config = config;
			_logger = logger;
		}

		public async Task<List<string>> FetchBreeds()
		{
			string endpoint = baseUrl + "/breeds/list/all";

			BreedsResponse data = await MakeRequest<BreedsResponse>(endpoint);
			List<string> breeds = data.message.Select(x => x.Key).ToList();

			return breeds;
			
		}

		public async Task<List<string>> FetchRandomImages()
		{
			string endpoint = baseUrl + "/breeds/image/random/50";

			ImagesResponse data = await MakeRequest<ImagesResponse>(endpoint);
			List<string> images = data.message.ToList();

			return images;
		}

		public async Task<List<string>> FetchImagesByBreed(string breed)
		{
			string endpoint = baseUrl + $"/breed/{breed}/images";

			ImagesResponse data = await MakeRequest<ImagesResponse>(endpoint);
			List<string> images = data.message.ToList();

			return images;
		}

		private async Task<T> MakeRequest<T>(string url) {
			using (HttpClient client = new()) {
				var json = await client.GetStringAsync(url);
				var data = JsonSerializer.Deserialize<T>(json);
				return data;
			}
		}
	}
}

