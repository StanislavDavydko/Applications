using Microsoft.AspNetCore.SignalR;
using Bogus;
using System.Threading.Tasks;
using System;
using Application.Web.DTO;

namespace A_Application.Web
{
    public class PriceHub : Hub
    {
        private readonly Faker _faker;

        public PriceHub()
        {
            _faker = new Faker();
        }

        public async Task SendPriceData()
        {
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

                await Clients.All.SendAsync("Prices", priceData);
            }
        }
    }
}
