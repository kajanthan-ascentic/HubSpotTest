using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HubSpotTest.Model;
using HubSpotTest.Service.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HubSpotTest.Service.Service
{
    public class HubSpotTokenService : ITokenService
    {
        private HubSpotToken token = new HubSpotToken();
        private readonly IOptions<HubSpotSettings> hotspotSettings;

        public HubSpotTokenService(IOptions<HubSpotSettings> hotspotSettings)
        {
            this.hotspotSettings = hotspotSettings;
        }

        public async Task<string> GetToken(string code)
        {
            if (!this.token.IsValidAndNotExpiring)
            {
                this.token = await this.GetNewAccessToken(code);
            }
            return token.AccessToken;
        }


        private async Task<HubSpotToken> GetNewAccessToken(string code)
        {
           // var token = new HubSpotToken();
            var client = new HttpClient();
            var client_id = this.hotspotSettings.Value.ClientId;
            var client_secret = this.hotspotSettings.Value.ClientSecret;
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");

            var postMessage = new Dictionary<string, string>();
            postMessage.Add("grant_type", "authorization_code");
            postMessage.Add("client_id", client_id);
            postMessage.Add("client_secret", client_secret);
            postMessage.Add("redirect_uri", "http://localhost:5001");
            postMessage.Add("code", code);
            var request = new HttpRequestMessage(HttpMethod.Post, this.hotspotSettings.Value.TokenUrl)
            {
                Content = new FormUrlEncodedContent(postMessage)
            };

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<HubSpotToken>(json);
                token.ExpiresAt = DateTime.UtcNow.AddSeconds(this.token.ExpiresIn);
            }
            else
            {
                throw new ApplicationException("Unable to retrieve access token from Hot spot Token");
            }

            return token;
        }
    }
}
