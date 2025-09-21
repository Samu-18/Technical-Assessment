using Microsoft.EntityFrameworkCore;
namespace TaskAPI.Controllers
{
    public class AppContext
    {
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Task> Tasks => Set<Task>();
    }
}
