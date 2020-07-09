using System;
namespace HubSpotTest.Model
{
    public class HubSpotSettings
    {
        public string TokenUrl { get; set; }        public string ClientId { get; set; }        public string ClientSecret { get; set; }        public string ApiKey { get; set; }        public string BaseAddress { get; set; }        public string RedirectUrl { get; set; }        public string AppId { get; set; }        public string DeveloperApiKey { get; set; }        public int RateLimitDelayMiliSeconds { get; set; }
    }
}
