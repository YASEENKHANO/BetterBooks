using BetterBooksWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterBooksWeb.Data
{
    public class ApplicationDbContext: DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        
        }


        public DbSet<Category> Categories { get; set; }

    }
}
