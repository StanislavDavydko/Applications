using Application.DomainModel;
using Application.DomainModel.Services.DataAccess;
using System.Threading.Tasks;

namespace Application.DataAccess.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        public ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Add(PriceData price)
        {
            _context.Prices.Add(price);
        }
    }
}
