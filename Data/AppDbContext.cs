using Microsoft.EntityFrameworkCore;

namespace UserSubscriptionWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
