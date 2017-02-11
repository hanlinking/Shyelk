using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shyelk.Infrastructure.Core.Reflection;

namespace Shyelk.Infrastructure.Core.DependencyInjection
{
    public abstract class ServiceManager : IServiceManager
    {
        public abstract IServiceCollection RegisterService(IServiceCollection services);
        protected virtual IServiceCollection RegisterScopedService(IServiceCollection services,Type iType,Type type){
           services= RegisterServices(services,iType,type,ServiceLifetime.Scoped);
           return services;
        }
        protected virtual IServiceCollection RegisterTransientService(IServiceCollection services,Type iType,Type type){
           services= RegisterServices(services,iType,type,ServiceLifetime.Transient);
           return services;
        }
        protected virtual IServiceCollection RegisterSingletonService(IServiceCollection services,Type iType,Type type){
           services= RegisterServices(services,iType,type,ServiceLifetime.Singleton);
           return services;
        }
        protected virtual IServiceCollection RegisterServices(IServiceCollection services,Type iType,Type type,ServiceLifetime lifetime){
            var iSubTypes = ReflectionTools.GetSubInterface(iType);
            var subTypes = ReflectionTools.GetSubTypes(type);
            foreach (var iSubType in iSubTypes)
            {
                var subType = subTypes.FirstOrDefault(f => iSubType.IsAssignableFrom(f));
                if (subType != null)
                {
                    ServiceDescriptor serviceDescriptor=new ServiceDescriptor(iSubType,subType,lifetime);
                    services.Add(serviceDescriptor);
                }
            }
            return services;
        }
    }
}