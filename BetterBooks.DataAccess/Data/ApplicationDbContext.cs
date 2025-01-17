using BetterBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterBooks.DataAccess
{
    public class ApplicationDbContext: DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
