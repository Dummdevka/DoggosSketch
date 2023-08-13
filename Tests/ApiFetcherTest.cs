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

		public ApiFetcherTest(IConfiguration config)
		{
			_apiFetcher = new ApiFetcher(config);
		}

		[Fact]
		public void FetchBreeds_ValidCall() {
			var actual = _apiFetcher.FetchBreeds();

			Assert.True(actual is IList<string>);
		}
	}
}

