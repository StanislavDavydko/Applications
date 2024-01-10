using Application.DomainModel;
using Application.DomainModel.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace B_Application.Web
{
    public class PriceClient
    {
        private readonly HubConnection hubConnection;
        private readonly IPriceService _priceService;

        public PriceClient(string hubUrl, IPriceService priceService)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

            _priceService = priceService;

            hubConnection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await hubConnection.StartAsync();
            };
        }

        public async Task ConnectAsync()
        {
            await hubConnection.StartAsync();
        }

        public async Task SubscribeToPriceData()
        {
            hubConnection.On<PriceInformation>("ReceivePriceData", (priceInformation) =>
            {
                _priceService.Add(
                    priceInformation.CurrencyPair, 
                    priceInformation.Price, 
                    priceInformation.Timestamp);
            });

            await hubConnection.SendAsync("SubscribeToPriceData");
        }

        public async Task DisconnectAsync()
        {
            await hubConnection.StopAsync();
        }
    }
}
