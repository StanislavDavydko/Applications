using Application.DomainModel.Services;
using Application.Web.DTO;
using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace B_Application.Web
{
    public class PriceClient
    {
        private readonly HubConnection hubConnection;
        private readonly IPriceService _priceService;

        public PriceClient(IPriceService priceService)
        {
            _priceService = priceService;

            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5170")
                .Build();

            ConnectAsync();

            SubscribeToPriceData();

            DisconnectAsync();
        }

        public void ConnectAsync()
        {
            hubConnection.StartAsync();

            Console.WriteLine("Connection is done");
        }

        public void SubscribeToPriceData()
        {
            hubConnection.On<PriceInformationDTO>("Price", (priceInformationDto) =>
            {
                _priceService.Add(
                    priceInformationDto.CurrencyPair,
                    priceInformationDto.Price,
                    priceInformationDto.Timestamp);

                Console.WriteLine("Data is saved");
            });
        }

        public void DisconnectAsync()
        {
            hubConnection.StopAsync();

            Console.WriteLine("Disconnection is done");
        }
    }
}
