using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace HubSpot.Sync.API.Extentions
{
    public static class LoggerServiceExtensions    {        public static void UseLoggerConfig(this IApplicationBuilder app, ILoggerFactory loggerFactory, ILogger logger)        {            loggerFactory.AddFile("Logs/sync-app-{Date}.txt");            var startTime = DateTimeOffset.UtcNow;            logger.LogInformation("Started at {StartTime} and 0x{Hello:X} is hex of 42", startTime, 42);        }    }
}
