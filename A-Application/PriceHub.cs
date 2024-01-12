using Microsoft.AspNetCore.SignalR;
using Bogus;
using Application.DomainModel;
using System.Threading.Tasks;
using System;
using Application.Web.DTO;

namespace A_Application.Web
{
    public class PriceHub : Hub
    {
        private readonly IHubContext<PriceHub> _hubContext;
        private readonly Faker _faker;

        public PriceHub(IHubContext<PriceHub> hubContext)
        {
            _hubContext = hubContext;
            _faker = new Faker();
        }

        public async Task SendPriceData()
        {
            while (true)
            {
                await Task.Delay(200);
                var priceData = new PriceInformationDTO(new PriceInformation
                {
                    CurrencyPair = "EUR/USD",
                    Price = _faker.Random.Double(1.0, 100.0),
                    Timestamp = DateTime.UtcNow

                });

                Console.WriteLine("A_Application started");

                await _hubContext.Clients.All.SendAsync("priceInformationDTO", priceData);
            }
        }
    }

}
