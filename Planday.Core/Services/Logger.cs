
using Planday.Core.Interfaces;
using Serilog;

namespace Planday.Core.Services
{
    public class Logger : IServiceLogger
    {
        public void LogToFile(string data)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            Log.Information(data);
        }
    }
}
