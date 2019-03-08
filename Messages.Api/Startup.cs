using Messages.Api.Models;
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
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            var connectionString = GetConnectionStringValue("API_DB_CONNECTION");

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ApiDbContext>(options =>
                {
                    options.UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.EnableRetryOnFailure());
                });

            services.AddScoped<IMessageService, MessageService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Messages API",
                    Version = "v1",
                    Description = "REST API for managing messages",
                    Contact = new Contact
                    {
                        Name = "Daniel S. Pinheiro",
                        Url = "https://github.com/dscpinheiro"
                    }
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(b => b.AllowAnyMethod().AllowAnyHeader().WithOrigins(Configuration["AllowedHosts"]));

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Messages API V1");
                options.RoutePrefix = string.Empty;
            });

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
