using System;
using DAL;
using Microsoft.Extensions.Configuration;

namespace Tests
{
	public class ApiFetcherTest
	{
		public IApiFetcher _apiFetcher {
			get; set;
		}

		public IConfiguration _config;

		public ApiFetcherTest()
		{
			Dictionary<string, string> configuration = new() {
				{ "ApiUrl", "https://dog.ceo/api" }
			};
			_config = new ConfigurationBuilder()
				.AddInMemoryCollection(configuration)
				.Build();
			_apiFetcher = new ApiFetcher(_config);
		}

		[Fact]
		public async Task FetchBreeds_ValidCall() {
			List<string> actual = await _apiFetcher.FetchBreeds();

			Assert.True(actual is not null);
			Assert.True(actual is IEnumerable<string>);
		}

		[Fact]
		public async Task FetchRandomImages_ValidCall() {
			List<string> actual = await _apiFetcher.FetchRandomImages();

			Assert.True(actual is not null);
			Assert.True(actual is IEnumerable<string>);
		}

		[Fact]
		public async Task FetchImagesByBreed_ValidCall() {
			List<string> actual = await _apiFetcher.FetchImagesByBreed("collie");

			Assert.True(actual is not null);
			Assert.True(actual is IEnumerable<string>);
		}
	}
}

