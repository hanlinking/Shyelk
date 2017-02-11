using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shyelk.Infrastructure.Core.Data.EntityFramework;
using Shyelk.Infrastructure.Core.DependencyInjection;
using Shyelk.Infrastructure.Core.Reflection;

namespace Shyelk.UserCenter.WebApi.Configuration
{
    public class RepositoriesActivtor : IServiceManager
    {
        public IServiceCollection RegisterService(IServiceCollection services)
        {
           var iRepositories= ReflectionTools.GetSubInterface(typeof(IRepository<,>));
           var repositories=ReflectionTools.GetSubTypes(typeof(BaseRepository<,>));
           
           return services;
        }
    }
}