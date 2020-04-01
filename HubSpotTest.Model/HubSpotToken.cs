using System;
using Newtonsoft.Json;

namespace HubSpotTest.Model
{
    public class HubSpotToken
    {
        public HubSpotToken()
        {
        }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefereshToken { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsValidAndNotExpiring
        {
            get
            {
                return !String.IsNullOrEmpty(this.AccessToken) &&
                  this.ExpiresAt > DateTime.UtcNow.AddSeconds(30);
            }
        }
    }
}
