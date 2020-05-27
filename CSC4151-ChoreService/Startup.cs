using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Clients;
using Common.Repositories;
using Common.Settings;
using CSC4151_ChoreService.ASB;
using CSC4151_ChoreService.Handlers;
using CSC4151_ChoreService.Pusher;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PusherServer;

namespace CSC4151_ChoreService
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
            // Settings
            var sqlSettings = new SqlSettings();
            Configuration.Bind("SQL", sqlSettings);
            services.AddSingleton<SqlSettings>(sqlSettings);

            var settings = new Settings();
            Configuration.Bind("Configuration", settings);
            services.AddSingleton<Settings>(settings);

            var pusherSettings = new PusherSettings();
            Configuration.Bind("Pusher", pusherSettings);
            services.AddSingleton<PusherSettings>(pusherSettings);

            // Repositories
            services.AddSingleton<IChoreRepository, ChoreRepository>();

            // Clients
            services.AddSingleton<SqlClient>();

            // ServiceBus
            services.AddSingleton<ServiceBusClient>();
            services.AddHostedService<EndpointInitializer>();

            // Message Handlers
            services.AddTransient<CreateChoreHandler>();
            services.AddTransient<DeleteChoreHandler>();

            //Pusher
            services.AddSingleton<LazyPusher>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
