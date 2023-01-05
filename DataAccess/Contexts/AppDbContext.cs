using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ): base(options)
        {

        }

        public DbSet<SlideItems> SlideItems { get; set; } 
    }

}
