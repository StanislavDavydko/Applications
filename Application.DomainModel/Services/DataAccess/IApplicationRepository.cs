using System.Threading.Tasks;

namespace Application.DomainModel.Services.DataAccess
{
    public interface IApplicationRepository
    {
        Task SaveChangesAsync();

        void Add(PriceData price);
    }
}
