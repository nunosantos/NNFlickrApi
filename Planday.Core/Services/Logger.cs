
using Planday.Core.Interfaces;
using Serilog;

namespace Planday.Core.Services
{
    public class Logger : IServiceLogger
    {
        /// <summary>
        /// Logs data to the filesystem
        /// </summary>
        /// <param name="data">The data which needs to be persisted</param>
        public void LogToFile(string data)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            Log.Information(data);
        }
    }
}
