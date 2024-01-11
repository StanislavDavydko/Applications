using Application.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Application.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PriceInformation> Prices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PriceInformation>(b =>
            {
                b.ToTable("PriceInformation");
            });
        }
    }
}
