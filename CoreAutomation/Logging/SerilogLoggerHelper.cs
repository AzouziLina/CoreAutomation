using CoreAutomation.Interfce;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CoreAutomation.Logging
{
    public class SerilogLoggerHelper : ILoggerHelper
    {
        private static SerilogLoggerHelper serilogLoggerHelper;

        public SerilogLoggerHelper() // Start Logging
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
        public static SerilogLoggerHelper GetInstance()
        {
            if (serilogLoggerHelper == null)
                serilogLoggerHelper = new SerilogLoggerHelper();
            return serilogLoggerHelper;
        }
        public void Info(string message)
        {
            Log.Information(message);
        }
        public void Error(string message)
        {
            Log.Error(message);
        }
        public void EndLogging()
        {
            Log.CloseAndFlushAsync();
        }

    }
}
