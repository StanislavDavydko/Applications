using Application.DomainModel;
using Application.DomainModel.Services;
using Application.DomainModel.Services.DataAccess;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Application.DomainServices
{
    public class PriceService : IPriceService
    {
        private readonly IPriceRepository _repository;

        public PriceService(IPriceRepository repository)
        {
            _repository = repository;
        }

        public async Task<HttpStatusCode> Add(
            string currencyPair,
            double price,
            DateTime timestamp)
        {
            var priceData = new PriceInformation
            {
                CurrencyPair = currencyPair,
                Price = price,
                Timestamp = timestamp
            };

            _repository.Add(priceData);
            await _repository.SaveChangesAsync();

           return HttpStatusCode.OK;
        }

        public async Task<PriceInformation> Get(int id)
        {
            var price = await _repository.GetPrice(id);

            return price;
        }
    }
}
