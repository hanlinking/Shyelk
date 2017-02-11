using System;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Shyelk.Infrastructure.Core.Data.EntityFramework;
using Shyelk.Infrastructure.Core.DependencyInjection;
using Shyelk.Infrastructure.Core.Reflection;
using Shyelk.UserCenter.Repository;
using Shyelk.UserCenter.Entity;
namespace Shyelk.UserCenter.WebApi.Configuration
{
    public class RepositoriesActivtor : IServiceManager
    {
        public IServiceCollection RegisterService(IServiceCollection services)
        {
            services=_registerRepository(services);
            services=_registerService(services);
            return services;
        }
        private IServiceCollection _registerRepository(IServiceCollection services)
        {
            bool result=typeof(IRoleRepository).IsAssignableFrom(typeof(RoleRepository));
            var iRepositories = ReflectionTools.GetSubInterface(typeof(IRepository<,>));
            var repositories = ReflectionTools.GetSubTypes(typeof(BaseRepository<,>));
            foreach (var iRepository in iRepositories)
            {
                var repository = repositories.FirstOrDefault(f => iRepository.IsAssignableFrom(f));
                if (repository != null)
                {
                    services.AddTransient(iRepository, repository);
                }
            }
            return services;
        }
        private IServiceCollection _registerService(IServiceCollection services){
            var iservices = ReflectionTools.GetSubInterface(typeof(Shyelk.Infrastructure.Core.Service.IService));
            var sservices = ReflectionTools.GetSubTypes(typeof(Shyelk.Infrastructure.Core.Service.IService));
            foreach (var iservice in iservices)
            {
                var service = sservices.FirstOrDefault(f => iservice.IsAssignableFrom(f));
                if (service != null)
                {
                    services.AddTransient(iservice, service);
                }
            }
            return services;
        }
    }
}