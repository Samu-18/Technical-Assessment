using Microsoft.EntityFrameworkCore;

namespace TaskAPI.Models;

public class AppContextDb : DbContext
{
    public AppContextDb(DbContextOptions<AppContextDb> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Task> Tasks => Set<Task>();
}
