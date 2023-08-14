namespace Services.Caching
{
	public interface ICachingService
	{
		Task<T?> GetValueAsync<T>(string key, CancellationToken cancellationToken = default)
			where T : class;


		Task SetValueAsync<T>(string key,
			T data,
			TimeSpan? absoluteExpiration = null,
			TimeSpan? unusedExpiration = null,
			CancellationToken cancellationToken = default) where T : class;


		Task ClearCacheAsync(CancellationToken cancellationToken = default);


		Task RemoveValuesByKeyPattern(string pattern, CancellationToken cancellationToken = default);

	}
}