using Messages.Api.Data;
using Messages.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MessagesTests.Validation
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseInMemoryDatabase("IntegrationTestsDatabase");
                options.UseInternalServiceProvider(serviceProvider);
            });

            services.AddScoped<IMessageService, MessageService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app) => app.UseMvc();
    }
}