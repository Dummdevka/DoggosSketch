using System;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Services.Caching
{
	public class CachingService : ICachingService
	{
		private readonly IDistributedCache _cache;
		private readonly IConnectionMultiplexer _multiplexer;

		public CachingService(IDistributedCache cache, IConfiguration config)
		{
			_cache = cache;
			_multiplexer = ConnectionMultiplexer.Connect(config.GetConnectionString("Redis"));
		}

		public async Task<T?> GetValueAsync<T>(string key, CancellationToken cancellationToken = default)
			where T : class {
			string value = await _cache.GetStringAsync(key, cancellationToken);

			if (value is null) {
				return null;
			}
			T? result = JsonSerializer.Deserialize<T>(value);
			return result;
		}

		public async Task SetValueAsync<T>(string key, 
			T data,
			TimeSpan? absoluteExpiration = null,
			TimeSpan? unusedExpiration = null, 
			CancellationToken cancellationToken = default) where T : class{
			var options = new DistributedCacheEntryOptions();
			options.AbsoluteExpirationRelativeToNow = absoluteExpiration ?? TimeSpan.FromHours(1);
			options.SlidingExpiration = unusedExpiration;

			string json = JsonSerializer.Serialize(data);

			await _cache.SetStringAsync(key, json, cancellationToken);
		}

		public async Task ClearCacheAsync(CancellationToken cancellationToken = default) {
			var server = _multiplexer.GetServer("localhost", 5002);
			foreach (var key in server.Keys()) {
				await _cache.RemoveAsync(key, cancellationToken);
			}
		}

		public async Task RemoveValuesByKeyPattern(string pattern, CancellationToken cancellationToken = default) {
			var server = _multiplexer.GetServer("localhost", 5002);
			foreach (var key in server.Keys(pattern: pattern+ '*')) {
				await _cache.RemoveAsync(key, cancellationToken);
			}
		}
	}
}

