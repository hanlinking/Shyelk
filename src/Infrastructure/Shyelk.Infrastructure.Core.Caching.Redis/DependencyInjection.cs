using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Shyelk.Infrastructure.Core.Caching.Redis
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, Action<RedisCacheOptions> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services is null");
            }
            if (options == null)
            {
                throw new ArgumentNullException("options is null");
            }
            services.AddScoped<IDistributedCache, RedisCache>();
            services.Configure<RedisCacheOptions>(options);
            return services;
        }
        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services is null");
            }
            if (configure == null)
            {
                throw new ArgumentNullException("configure is null");
            }
            services.AddScoped<IDistributedCache, RedisCache>();
            services.Configure<RedisCacheOptions>(configure);
            return services;
        }
    }
}
