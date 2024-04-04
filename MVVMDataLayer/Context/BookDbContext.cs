using Microsoft.EntityFrameworkCore;
using MVVMEntityLayer;

namespace MVVMDataLayer.Context
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Book> Books { get; set; }
    }
}
