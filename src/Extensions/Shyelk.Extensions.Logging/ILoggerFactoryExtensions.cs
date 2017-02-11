using Microsoft.Extensions.Logging;

namespace Shyelk.Extensions.Logging
{
    public static class ILoggerFactoryExtensions
    {
        public static ILoggerFactory AddFileSystem(this ILoggerFactory factory)
        {
             factory.AddProvider(new FileSystemLoggerProvider());
             return factory;
        }
    }
}