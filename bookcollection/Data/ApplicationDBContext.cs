using bookcollection.Models;
using Microsoft.EntityFrameworkCore;

namespace bookcollection.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
