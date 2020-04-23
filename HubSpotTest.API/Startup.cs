using HubSpotTest.API.Extension;
using HubSpotTest.Model;
using HubSpotTest.Service.Interface;
using HubSpotTest.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Resonance;
using Resonance.Models;
using Resonance.Repo;
using Resonance.Repo.Database;
using System;

namespace HubSpotTest.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private ILogger<Startup> Logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<HubSpotSettings>(Configuration.GetSection("HubSpot"));
            services.AddSingleton<IHttpClinentService, HttpClinentService>();
            services.AddSingleton<ITokenService, HubSpotTokenService>();
            services.AddSingleton<IHotSpotApiService, HotSpotApiService>();
            services.AddControllers().AddNewtonsoftJson();

            services.AddSwagger();

            ConfigureRepoServices(services, Configuration); // Using the db-repo's doesn't have any api/web dependencies
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
            ILogger<Startup> logger)
        {
            this.Logger = logger;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseLoggerConfig(loggerFactory, logger);

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCustomSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureRepoServices(IServiceCollection serviceCollection, IConfiguration config)
        {
            // Configure IEventingRepoFactory dependency (reason: the repo that must be used in this app)
            var dbType = config["Resonance:Repo:Database:Type"];
            var useMySql = (dbType == null || dbType.Equals("MySql", StringComparison.OrdinalIgnoreCase)); // Anything else than MySql is considered MsSql
            var commandTimeout = TimeSpan.FromSeconds(int.Parse(config["Resonance:Repo:Database:CommandTimeout"]));

            if (useMySql)
            {
                var connectionString = config.GetConnectionString("Resonance.MySql");
                serviceCollection.AddTransient<IEventingRepoFactory>((p) => new MySqlEventingRepoFactory(connectionString, commandTimeout));
            }
            else // MsSql
            {
                var connectionString = config.GetConnectionString("Resonance.MsSql");
                serviceCollection.AddTransient<IEventingRepoFactory>((p) => new MsSqlEventingRepoFactory(connectionString, commandTimeout));
            }

            // Configure EventPublisher and Consumer
            var maxRetries = int.Parse(config["Resonance:RetryPolicy:MaxRetries"]);
            var initialBackoffMs = double.Parse(config["Resonance:RetryPolicy:InitialBackoffMs"]);
            var incrBackoff = (config["Resonance:RetryPolicy:IncrBackoff"] ?? string.Empty).Equals(Boolean.TrueString, StringComparison.OrdinalIgnoreCase);
            var retryPolicy = new RetryPolicy(maxRetries, TimeSpan.FromMilliseconds(initialBackoffMs), incrBackoff);
            serviceCollection
                .AddSingleton<InvokeOptions>((p) => new InvokeOptions(retryPolicy, (ex, attempt, maxAttempts) =>
                {
                    //var logger = serviceCollection. serviceProvider.GetService<ILogger<InvokeOptions>>();
                    if ( Logger != null)
                        Logger.LogWarning("Error while loading/saving events ({attempt} out of {maxAttempts}), action will be retried: {details}", attempt, maxAttempts, ex.Message);
                }))
                .AddTransient<IEventPublisherAsync>(p => new EventPublisher(p.GetService<IEventingRepoFactory>(), DateTimeProvider.Repository, TimeSpan.FromSeconds(30), p.GetService<InvokeOptions>()))
                .AddTransient<IEventConsumerAsync>(p => new EventConsumer(p.GetService<IEventingRepoFactory>(), TimeSpan.FromSeconds(30), p.GetService<InvokeOptions>()));
        }
    }
}
