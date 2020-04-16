using System;
namespace HubSpotTest.Model
{
    public class HubSpotSettings
    {
        public HubSpotSettings()
        {
        }

        public string TokenUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ApiKey { get; set; }
        public string BaseAddress { get; set; }

        public string RedirectUrl { get; set; }
    }
}
