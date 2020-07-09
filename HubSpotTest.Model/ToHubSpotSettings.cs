using System;
namespace HubSpotTest.Model
{
    public class ToHubSpotSettings
    {
        public ToHubSpotSettings()
        {
        }

        public string ApiKey { get; set; }

        public string BaseAddress { get; set; }

        public string DeveloperApiKey { get; set; }

        public int RateLimitDelayMiliSeconds { get; set; }
    }
}
