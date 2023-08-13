using System;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
	public static class DependencyInjection
	{
		public static IServiceCollection addDAL(this IServiceCollection services) {
			return services.AddSingleton<IApiFetcher, ApiFetcher>();
		}
	}
}

