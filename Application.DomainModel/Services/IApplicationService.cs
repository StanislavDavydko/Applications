using System;
using System.Net;
using System.Threading.Tasks;

namespace Application.DomainModel.Services
{
    public interface IApplicationService
    {
        Task<HttpStatusCode> Add(
            string currencyPair,
            double price,
            DateTime timestamp);
    }
}
