using System;
using System.IO;
using System.Reflection;
using BeatPulse;
using Messages.Api.Data;
using Messages.Api.Filters;
using Messages.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Messages.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            HostingEnvironment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = GetConnectionStringValue("API_DB_CONNECTION");

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ApiDbContext>(options =>
                {
                    options.UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.EnableRetryOnFailure());
                });

            services.AddBeatPulse(options => options.AddNpgSql(connectionString));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Messages API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.DocumentFilter<RemoveModelsFilter>();
            });

            services.AddScoped<IMessageService, MessageService>();

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseBeatPulse(options =>
            {
                options.ConfigurePath("hc");
                options.ConfigureDetailedOutput(detailedOutput: true);
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Messages API V1");
                options.RoutePrefix = string.Empty;
            });

            app.UseCors(b => b.AllowAnyMethod().AllowAnyHeader().WithOrigins(Configuration["AllowedHosts"]));
            app.UseMvc();
        }

        private string GetConnectionStringValue(string key)
        {
            var connectionString = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = Configuration.GetConnectionString(key);
            }

            return connectionString;
        }
    }
}
