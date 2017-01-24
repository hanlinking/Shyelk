using System;
using System.Reflection;
using System.Linq;
namespace Shyelk.Infrastructure.Core.Reflection
{
    /// <summary>
    /// 反射工具扩展类
    /// </summary>
    public static class ReflectionExtensions
    {
        public static bool IsAssignableFromGenericType(this Type genericType, Type type)
        {
            if (genericType == null)
            {
                throw new NullReferenceException(nameof(genericType));
            }
            if (type == null)
            {
                return false;
            }
            return genericType == type
            || genericType.HasInterfaceThatMapsFromGenericTypeDefinition(type)
            || genericType.MapsFromGenericTypeDefinition(type)
            || genericType.IsAssignableFromGenericType(type.GetTypeInfo().BaseType);
        }
        private static bool HasInterfaceThatMapsFromGenericTypeDefinition(this Type genericType, Type type)
        {
            return type.GetTypeInfo()
                        .GetInterfaces()
                        .Where(t => t.GetTypeInfo().IsGenericType)
                        .Any(t => t.GetGenericTypeDefinition() == genericType);
        }
        private static bool MapsFromGenericTypeDefinition(this Type genericType, Type type)
        {
            var result = genericType.GetTypeInfo().IsGenericTypeDefinition
                        && type.GetTypeInfo().IsGenericType
                        && type.GetGenericTypeDefinition() == genericType;
            return result;
        }
    }
}