using Microsoft.AspNetCore.SignalR;
using Bogus;
using Application.DomainModel;
using System.Threading.Tasks;
using System;

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
                var priceData = new PriceInformation
                {
                    CurrencyPair = "EUR/USD",
                    Price = _faker.Random.Double(1.0, 100.0),
                    Timestamp = DateTime.UtcNow
                };

                await _hubContext.Clients.All.SendAsync("Prices", priceData);
            }
        }
    }

}
