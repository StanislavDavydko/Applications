using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.AspNetCore.SignalR.Client;
using Application.DomainModel.Services;
using Application.Web.DTO;
using Microsoft.Extensions.DependencyInjection;

namespace B_Application.Web
{
    internal class SignalRTimedHostedServiceClient : IHostedService, IDisposable
    {
        private readonly HubConnection hubConnection;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public SignalRTimedHostedServiceClient(
            IServiceProvider serviceProvider,
            ILogger<SignalRTimedHostedServiceClient> logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:8080/priceHub")
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            await DoWork();
        }

        private async Task DoWork()
        {
            _logger.LogInformation("Timed Background Service is working.");

            hubConnection.On<PriceInformationDTO>("Prices", async (priceInformationDto) =>
            {
                var priceService = _serviceProvider.GetRequiredService<IPriceService>();
                await priceService.Add(
                    priceInformationDto.CurrencyPair,
                    priceInformationDto.Price,
                    priceInformationDto.Timestamp);

                Console.WriteLine(priceInformationDto.Timestamp.ToString());
            });

            
            await hubConnection.StartAsync();
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
