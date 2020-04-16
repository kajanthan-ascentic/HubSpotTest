using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace HubSpotTest.API.Extension
{
    public static class LoggerServiceExtensions    {        public static void UseLoggerConfig(this IApplicationBuilder app, ILoggerFactory loggerFactory, ILogger logger)        {            loggerFactory.AddFile("Logs/myapp-{Date}.txt");            var startTime = DateTimeOffset.UtcNow;            logger.LogInformation("Started at {StartTime} and 0x{Hello:X} is hex of 42", startTime, 42);        }    }
}
