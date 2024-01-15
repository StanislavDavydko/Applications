using System;
using System.Threading.Tasks;

namespace Application.DomainModel.Services
{
    public interface IPriceService
    {
        Task Add(string currencyPair, double price, DateTime timestamp);

        Task<PriceInformation> Get(int id);
    }
}
