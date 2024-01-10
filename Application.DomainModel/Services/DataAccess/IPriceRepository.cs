using System.Threading.Tasks;

namespace Application.DomainModel.Services.DataAccess
{
    public interface IPriceRepository
    {
        Task SaveChangesAsync();

        void Add(PriceInformation price);

        Task<PriceInformation> GetPrice(int id);
    }
}
