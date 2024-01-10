using Application.DomainModel;
using Application.DomainModel.Services;
using Application.DomainModel.Services.DataAccess;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Application.DomainServices
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _repository;

        public ApplicationService(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<HttpStatusCode> Add(
            string currencyPair,
            double price,
            DateTime timestamp)
        {
            var priceData = new PriceData
            {
                CurrencyPair = currencyPair,
                Price = price,
                Timestamp = timestamp
            };

            _repository.Add(priceData);
            await _repository.SaveChangesAsync();

           return HttpStatusCode.OK;
        }
    }
}
