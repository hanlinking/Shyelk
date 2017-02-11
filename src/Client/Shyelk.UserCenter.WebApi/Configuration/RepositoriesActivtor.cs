using Microsoft.Extensions.DependencyInjection;
using Shyelk.Infrastructure.Core.Data.EntityFramework;
using Shyelk.Infrastructure.Core.DependencyInjection;
namespace Shyelk.UserCenter.WebApi.Configuration
{
    public class RepositoriesActivtor : ServiceManager
    {
        public override IServiceCollection RegisterService(IServiceCollection services)
        {
            services=RegisterScopedService(services,typeof(IRepository<,>),typeof(BaseRepository<,>));
            services=RegisterScopedService(services,typeof(Shyelk.Infrastructure.Core.Service.IService),typeof(Shyelk.Infrastructure.Core.Service.IService));
            return services;
        }
    }
}