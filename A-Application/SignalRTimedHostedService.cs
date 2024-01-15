using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Application.Web.DTO;

namespace A_Application.Web
{
    internal class SignalRTimedHostedService : IHostedService, IDisposable
    {
        private readonly IHubContext<PriceHub> _hub;
        private readonly ILogger _logger;

        public SignalRTimedHostedService(IHubContext<PriceHub> hub, ILogger<SignalRTimedHostedService> logger)
        {
            _hub = hub;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            await DoWork();
        }

        private async Task DoWork()
        {
            _logger.LogInformation("Timed Background Service is working.");
            // send message using _hub

            Random random = new Random();

            while (true)
            {
                await Task.Delay(5000);
                var priceData = new PriceInformationDTO()
                {
                    CurrencyPair = "EUR/USD",
                    Price = random.NextDouble() * 1.0 - 100.0,
                    Timestamp = DateTime.UtcNow
                };

                Console.WriteLine(priceData.Timestamp.ToString());

                await _hub.Clients.All.SendAsync("Prices", priceData);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
