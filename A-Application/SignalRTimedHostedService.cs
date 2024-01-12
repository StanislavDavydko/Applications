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
        private Timer _timer;

        public SignalRTimedHostedService(IHubContext<PriceHub> hub, ILogger<SignalRTimedHostedService> logger)
        {
            _hub = hub;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(100));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");
            // send message using _hub

            Random random = new Random();

            while (true)
            {
                Task.Delay(5000);
                var priceData = new PriceInformationDTO()
                {
                    CurrencyPair = "EUR/USD",
                    Price = random.NextDouble() * 1.0 - 100.0,
                    Timestamp = DateTime.UtcNow
                };

                Console.WriteLine("A_Application sends data price data");

                _hub.Clients.All.SendAsync("Price", priceData);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
