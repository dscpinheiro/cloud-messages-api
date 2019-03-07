using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages.Api.Models;
using Messages.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Messages.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();

            var connectionString = GetConnectionStringValue("API_DB_CONNECTION");

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ApiDbContext>(options =>
                {
                    options.UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.EnableRetryOnFailure());
                });

            services.AddScoped<IMessageService, MessageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(b => b.AllowAnyMethod().AllowAnyHeader().WithOrigins(Configuration["AllowedHosts"]));
            app.UseMvc();
        }

        private string GetConnectionStringValue(string key)
        {
            var connectionString = System.Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = Configuration.GetConnectionString(key);
            }

            return connectionString;
        }
    }
}
