using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.DataAccess;
using Application.DomainModel.Services;
using Application.DomainServices;
using Application.DomainModel.Services.DataAccess;
using Application.DataAccess.Repository;
using Microsoft.Extensions.Hosting;

namespace B_Application.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSignalR();

            services.AddHostedService<SignalRTimedHostedServiceClient>();

            services.AddSingleton<IPriceService, PriceService>();
            services.AddSingleton<IPriceRepository, PriceRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
        }
    }
}
