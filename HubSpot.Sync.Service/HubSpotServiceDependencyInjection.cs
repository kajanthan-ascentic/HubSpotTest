using System;
using HubSpot.Sync.Service.HttpClientData;
using HubSpot.Sync.Service.Interface;
using HubSpot.Sync.Service.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HubSpot.Sync.Service
{

    public static class HubSpotServiceDependencyInjection    {        public static IServiceCollection AddServiceExtentionServices(this IServiceCollection services, IConfiguration configuration)        {

            services.AddScoped<IHttpClientService, HttpClientService>();            services.AddScoped<IToHubspotHttpClientService, ToHubspotHttpClientService>();            services.AddScoped<IHubspotHttpClientService, HubspotHttpClientService>();            services.AddScoped<IContactService, ContactService>();            services.AddScoped<ICompanyService, CompanyService>();            services.AddScoped<IDealService, DealService>();            return services;        }    }
}
