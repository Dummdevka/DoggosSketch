using System;
namespace DAL
{
	public interface IApiFetcher
	{
		Task<List<string>> FetchBreeds();

		Task<List<string>> FetchRandomImages();

		Task<List<string>> FetchImagesByBreed(string breed);

		//private Task<T> MakeRequest<T>(string url);
	}
}

