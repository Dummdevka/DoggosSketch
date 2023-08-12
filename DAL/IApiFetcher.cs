using System;
namespace DAL
{
	public interface IApiFetcher
	{
		Task<List<string>> FetchBreeds();

		Task<List<string>> FetchImages();

		Task<List<string>> FetchImagesByBreed(string breed);
	}
}

