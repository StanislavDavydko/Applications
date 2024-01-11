using Application.DomainModel.Services;
using Application.Web.DTO;
using Microsoft.AspNetCore.SignalR.Client;
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

            hubConnection.StartAsync();

            SubscribeToPriceData();

            _priceService = priceService;
        }

        public async Task ConnectAsync()
        {
            await hubConnection.StartAsync();
        }

        public async Task SubscribeToPriceData()
        {
            hubConnection.On<PriceInformationDTO>("Pricest", (priceInformationDto) =>
            {
                _priceService.Add(
                    priceInformationDto.CurrencyPair,
                    priceInformationDto.Price,
                    priceInformationDto.Timestamp);                
            });
        }

        public async Task DisconnectAsync()
        {
            await hubConnection.StopAsync();
        }
    }
}
