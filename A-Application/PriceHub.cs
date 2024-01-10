using Microsoft.AspNetCore.SignalR;
using Application.DomainModel;

namespace A_Application.Web
{
    public class PriceHub : Hub
    {
        private readonly IHubContext<PriceHub> _hubContext;

        public PriceHub(IHubContext<PriceHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendPriceData(string currencyPair, double price)
        {
            while (true)
            {
                var priceData = new PriceData
                {
                    CurrencyPair = currencyPair,
                    Price = price,
                    Timestamp = DateTime.UtcNow
                };

                await _hubContext.Clients.All.SendAsync("ReceivePriceData", priceData);

                // Wait for 200 milliseconds before sending the next data
                await Task.Delay(200);
            }
        }
    }

}
