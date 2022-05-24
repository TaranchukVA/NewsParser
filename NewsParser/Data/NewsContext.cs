using Microsoft.EntityFrameworkCore;
using NewsParser.Data.Tables;

namespace NewsParser.Data
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<NewsStorage> News { get; set; }

             
    }
}
