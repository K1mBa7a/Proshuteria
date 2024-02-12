using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace proshuteria.Data
{
    public class ApplicationDbContext:IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Meat> Meats { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MeatCategory> MeatCategories { get; set; }
         
        
    }
}

