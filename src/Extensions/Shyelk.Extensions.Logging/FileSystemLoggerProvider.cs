using System;
using Microsoft.Extensions.Logging;

namespace Shyelk.Extensions.Logging
{
    public class FileSystemLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new FileSystemLogger();
        }

        public void Dispose()
        {
            
        }
    }
}