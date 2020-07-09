using HubSpot.Sync.API.Extentions;
using HubSpot.Sync.API.Interface;
using HubSpot.Sync.API.Model.Settings;
using HubSpot.Sync.API.Service;
using HubSpot.Sync.Service;
using HubSpotTest.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HubSpot.Sync.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<HubSpotSettings>(Configuration.GetSection("HubSpot"));            services.Configure<ToHubSpotSettings>(Configuration.GetSection("ToHubSpot"));            services.Configure<SyncPropertyMapperSettings>(Configuration.GetSection("HubspotAwisSyncProperties"));

            services.AddServiceExtentionServices(Configuration);
            services.AddScoped<IContactHubspotService, ContactHubspotService>();
            services.AddScoped<ICompanyHubspotService, CompanyHubspotService>();
            services.AddScoped<IDealHubspotService, DealHubspotService>();
            services.AddScoped<IAssociationHubspotService, AssociationHubspotService>();


            
            services.AddSwagger();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseLoggerConfig(loggerFactory, logger);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCustomSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
