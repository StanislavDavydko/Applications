using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.AspNetCore.SignalR.Client;
using Application.DomainModel.Services;
using Application.Web.DTO;

namespace B_Application.Web
{
    internal class SignalRTimedHostedServiceClient : IHostedService, IDisposable
    {
        private readonly HubConnection hubConnection;
        private readonly IPriceService _priceService;
        private readonly ILogger _logger;
        private Timer _timer;

        public SignalRTimedHostedServiceClient(IPriceService priceService, ILogger<SignalRTimedHostedServiceClient> logger)
        {
            _logger = logger;
            _priceService = priceService;

            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5170")
                .Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");

            hubConnection.On<PriceInformationDTO>("Price", (priceInformationDto) =>
            {
                _priceService.Add(
                    priceInformationDto.CurrencyPair,
                    priceInformationDto.Price,
                    priceInformationDto.Timestamp);

                Console.WriteLine("Data is saved");
            });
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
