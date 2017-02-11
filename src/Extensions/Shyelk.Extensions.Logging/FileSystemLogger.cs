using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Shyelk.Extensions.Logging
{
    internal class FileSystemLogger : ILogger
    {
        private readonly string _basePath;
        private static string _simpleFormat="LogLevel:{0},Message:{1},Trace:{2}";
        public FileSystemLogger()
        {
            _basePath=Path.Combine(Directory.GetCurrentDirectory(),"Log");
            if (!Directory.Exists(_basePath))
            {
               Directory.CreateDirectory(_basePath);
            }
        }
        public void Log(LogLevel level, string message, Exception exception = null)
        {
            string trace=exception?.StackTrace;
            string loglevel=Enum.GetName(typeof(LogLevel),level);
            string writeString=string.Format(_simpleFormat,loglevel,message,trace);
            string debugpath=Path.Combine(_basePath,"Debug");
            if (!Directory.Exists(debugpath))
            {
                Directory.CreateDirectory(debugpath);
            }
            string logfile=DateTime.Now.ToString("yyyy-MM-dd");
            string filepath=Path.Combine(debugpath,logfile);
            if (!File.Exists(filepath))
            {
                using(var fs=File.Create(filepath))
                {
                    byte[] content=Encoding.UTF8.GetBytes(writeString);
                    fs.Write(content,0,content.Length);
                }
            }else
            {
                using(var fs=File.AppendText(filepath))
                {
                    fs.WriteLine(writeString);
                }
            }
        }

        public void Log<LogInfo>(LogLevel level, string message, Exception exception = null)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}