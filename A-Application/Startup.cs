using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace A_Application.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            });

            services.AddHostedService<SignalRTimedHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(e =>
            {
                e.MapControllers();
                e.MapHub<PriceHub>("/priceHub");
            });
        }
    }
}
