using System;
using Microsoft.Extensions.Configuration;

namespace DAL
{
	public class ApiFetcher : IApiFetcher
	{
		private readonly IConfiguration _config;

		public ApiFetcher(IConfiguration config)
		{
			_config = config;
		}

		public async Task<List<string>> FetchBreeds()
		{
			string endpoint = "/breeds/list/all";
			throw new NotImplementedException();
		}

		public async Task<List<string>> FetchImages()
		{
			string endpoint = "/breeds/image/random/50";
			throw new NotImplementedException();
		}

		public async Task<List<string>> FetchImagesByBreed(string breed)
		{
			string endpoint = $"/breed/{breed}images";
			throw new NotImplementedException();
		}
	}
}

