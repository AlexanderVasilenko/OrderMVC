using System.Data.Entity;

namespace OrderForm.Domain.DB
{
    class OrdDbContext:DbContext
    {
        public DbSet<Entities.Order> Orders { get; set; }
    }
}
