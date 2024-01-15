using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.DataAccess;
using Application.DomainModel.Services;
using Application.DomainServices;
using Application.DomainModel.Services.DataAccess;
using Application.DataAccess.Repository;

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

            services.AddControllers();

            services.AddSignalR();

            services.AddHostedService<SignalRTimedHostedServiceClient>();

            services.AddScoped<IPriceService, PriceService>();
            services.AddScoped<IPriceRepository, PriceRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });
        }
    }
}
