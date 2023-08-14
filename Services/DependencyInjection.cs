using Microsoft.Extensions.DependencyInjection;
using Services.Caching;

namespace Services;

public static class DependencyInjection
{
	public static IServiceCollection addServices(this IServiceCollection services) {
		return services.AddScoped<ICachingService, CachingService>();
	}
}

