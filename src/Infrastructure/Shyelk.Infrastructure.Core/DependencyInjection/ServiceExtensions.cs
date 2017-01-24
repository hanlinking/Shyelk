using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Shyelk.Infrastructure.Core.Reflection;

namespace Shyelk.Infrastructure.Core.DependencyInjection
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 添加基本服务
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"></see></param>
        /// <returns><see cref="IServiceCollection"></see></returns>
        public static IServiceCollection AddBaseService(this IServiceCollection services)
        {
            IEnumerable<Type> types = ReflectionTools.GetSubTypes<IServiceManager>();
            return RegisterService(services, types);
        }
        public static IServiceCollection AddBaseService(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            IEnumerable<Type> types = ReflectionTools.GetSubTypes<IServiceManager>(assemblies);
            return RegisterService(services, types);
        }
        public static IServiceCollection AddBaseService(this IServiceCollection services, string assemblyPath)
        {
            return services.AddBaseService(ReflectionTools.GetAssemblyFromPath(assemblyPath));
        }
        private static IServiceCollection RegisterService(IServiceCollection services, IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                IServiceManager manager = ReflectionTools.CreateInstance(type) as IServiceManager;
                services = manager.RegisterService(services);
            }
            return services;
        }
    }
}
