using MDP3.Tables;
using Microsoft.EntityFrameworkCore;

namespace MDP3.AppDbContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Producer> Producers { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;


    }
}
