using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
using System.Linq.Expressions;

namespace Shyelk.Infrastructure.Core.Reflection
{
    public static class ReflectionTools
    {
        private static IEnumerable<Assembly> _assemblies
        {
            get
            {
                return GetAllAssembly();
            }
        }
        ///<summary>
        /// get all Assembly
        ///</summary>
        private static IEnumerable<Assembly> GetAllAssembly()
        {
            return DependencyContext.Default.CompileLibraries
            .Where(lib => !lib.Serviceable && lib.Type != "package").Select(lib => Assembly.Load(new AssemblyName(lib.Name)));
        }
        public static IEnumerable<Type> GetSubInterface(Type type, IEnumerable<Assembly> assemblies)
        {
            TypeInfo typeInfo = type.GetTypeInfo();
            if (!typeInfo.IsInterface)
            {
                throw new ArgumentException(nameof(type) + "must be a interface");
            }
            return assemblies.SelectMany(a =>
             {
                 return a.GetTypes().Where(t =>
                 {
                     if (t == type)
                     {
                         return false;
                     }
                     TypeInfo tInfo = t.GetTypeInfo();
                     if (!tInfo.IsInterface)
                     {
                         return false;
                     }
                     if (typeInfo.IsGenericTypeDefinition)
                     {
                         return type.IsAssignableFromGenericType(t);
                     }
                     return type.IsAssignableFrom(t);
                 });
             });
        }
        public static IEnumerable<Type> GetSubInterface(Type type)
        {
            var assemblies = _assemblies.Where(a =>
            {
                Assembly assembly = type.GetTypeInfo().Assembly;
                return a.FullName == assembly.FullName || a.GetReferencedAssemblies().Any(ra => ra.FullName == assembly.FullName);
            });
            return GetSubInterface(type, assemblies);
        }
        public static IEnumerable<Type> GetSubInterface(Type type, Assembly assembly)
        {
            return GetSubTypes(type, new List<Assembly>() { { assembly } });
        }
        public static IEnumerable<Type> GetSubInterface<T>()
        {
            return GetSubInterface(typeof(T));
        }
        public static IEnumerable<Type> GetSubInterface<T>(Assembly assembly)
        {
            return GetSubInterface(typeof(T), assembly);
        }
        public static IEnumerable<Type> GetSubInterface<T>(IEnumerable<Assembly> assemblies)
        {
            return GetSubInterface(typeof(T), assemblies);
        }
        public static IEnumerable<Type> GetSubTypes(Type type)
        {
            var assemblies = _assemblies.Where(a =>
            {
                Assembly assembly = type.GetTypeInfo().Assembly;
                return a.FullName == assembly.FullName || a.GetReferencedAssemblies().Any(ra => ra.FullName == assembly.FullName);
            });
            return GetSubTypes(type, assemblies);
        }
        public static IEnumerable<Type> GetSubTypes(Type type, Assembly assembly)
        {
            return GetSubTypes(type, new List<Assembly>() { { assembly } });
        }
        public static IEnumerable<Type> GetSubTypes(Type type, IEnumerable<Assembly> assemblies)
        {
            TypeInfo typeInfo = type.GetTypeInfo();
            return assemblies.SelectMany(a =>
             {
                 return a.GetTypes().Where(t =>
                 {
                     if (t == type)
                     {
                         return false;
                     }
                     TypeInfo tInfo = t.GetTypeInfo();
                     if (tInfo.IsAbstract || !tInfo.IsClass || !tInfo.IsPublic)
                     {
                         return false;
                     }
                     if (typeInfo.IsGenericTypeDefinition)
                     {
                         return type.IsAssignableFromGenericType(t);
                     }
                     return type.IsAssignableFrom(t);
                 });
             });
        }
        public static IEnumerable<Type> GetSubTypes<T>()
        {
            return GetSubTypes(typeof(T));
        }
        public static IEnumerable<Type> GetSubTypes<T>(Assembly assembly)
        {
            return GetSubTypes(typeof(T), assembly);
        }
        public static IEnumerable<Type> GetSubTypes<T>(IEnumerable<Assembly> assemblies)
        {
            return GetSubTypes(typeof(T), assemblies);
        }
        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
        public static IEnumerable<Assembly> GetAssemblyFromPath(string path)
        {
            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(AssemblyLoadContext.Default.LoadFromAssemblyPath(path));
            return assemblies;
        }
    }
}
