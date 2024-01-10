namespace A_Application.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();  // підключені сервіси SignalR
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(e =>
            {
                e.MapHub<ChatHub>("/chat");     //ChatHub буде обробляти запроси по путі до /chat
            });
        }
    }
}
