using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Shyelk.Infrastructure.Core.Reflection;

namespace Shyelk.Infrastructure.Core.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBaseService(this IServiceCollection services)
        {
           IEnumerable<Type> types= ReflectionTools.GetSubTypes<IServiceManager>();
           foreach (var type in types)
           {
              IServiceManager manager=ReflectionTools.CreateInstance(type) as IServiceManager;
             services= manager.RegisterService(services); 
           }
            return services;
        }
        public static IServiceCollection AddBaseService(this IServiceCollection services,IEnumerable<Assembly> assemblies)
        {
            return services;
        }
        public static IServiceCollection AddBaseService(this IServiceCollection services,string assemblyPath,Func<Assembly,bool> expression)
        {
            //List<Assembly> assemblies = new List<Assembly>();
            //IFileProvider provider = new PhysicalFileProvider(pluginsPath);
            //var changeToken = provider.Watch("*/**.dll");
            //changeToken.RegisterChangeCallback((ss) => Console.WriteLine(ss), "ss");
            //var directoryContent = provider.GetDirectoryContents("").Where(c => c.Name.EndsWith("dll"));
            //foreach (var content in directoryContent)
            //{
            //    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(content.PhysicalPath);
            //    builder.AddApplicationPart(assembly);
            //}
           // builder.AddControllersAsServices();
           // builder.AddViewComponentsAsServices();
           // return builder;
            return services;
        }
    }
}
