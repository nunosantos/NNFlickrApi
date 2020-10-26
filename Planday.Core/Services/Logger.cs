
using Planday.Core.Interfaces;
using Serilog;
using System.IO;

namespace Planday.Core.Services
{
    public class Logger : IServiceLogger
    {
        /// <summary>
        /// Logs data to the filesystem
        /// </summary>
        /// <param name="data">The data which needs to be persisted</param>
        public void LogToFile(string data, string path)
        {
            
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File(path + "/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            Log.Information(data);
        }
    }
}
