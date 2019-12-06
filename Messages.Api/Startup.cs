using Messages.Api.Data;
using Messages.Api.Filters;
using Messages.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace Messages.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            HostingEnvironment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = GetConnectionStringValue("API_DB_CONNECTION");
            if (HostingEnvironment.IsEnvironment("CI") && string.IsNullOrWhiteSpace(connectionString))
            {
                services.AddDbContext<ApiDbContext>(options => options.UseInMemoryDatabase("InMemoryDB"));
            }
            else
            {
                services
                    .AddEntityFrameworkNpgsql()
                    .AddDbContext<ApiDbContext>(options =>
                    {
                        options.UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.EnableRetryOnFailure());
                    });
            }

            services.AddHealthChecks().AddDbContextCheck<ApiDbContext>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddCors();
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Messages API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.DocumentFilter<RemoveModelsFilter>();
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Messages API V1");
                options.RoutePrefix = string.Empty;
            });

            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(b => b.AllowAnyMethod().AllowAnyHeader().WithOrigins(Configuration["AllowedHosts"]));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc");
                endpoints.MapControllers();
            });
        }

        private string GetConnectionStringValue(string key)
        {
            /* Postgres can be run locally in Docker via:
             * docker run --name pg-docker -e POSTGRES_PASSWORD=notthepassword -p 5432:5432 postgres:12-alpine
             */

            var connectionString = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = Configuration.GetConnectionString(key);
            }

            return connectionString;
        }
    }
}
