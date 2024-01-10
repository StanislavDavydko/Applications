using Application.DomainModel;
using Application.DomainModel.Services.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.DataAccess.Repository
{
    public class PriceRepository : IPriceRepository
    {
        public ApplicationDbContext _context;

        public PriceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Add(PriceInformation price)
        {
            _context.Prices.Add(price);
        }

        public async Task<PriceInformation> GetPrice(int id)
        {
            return await _context.Prices
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
