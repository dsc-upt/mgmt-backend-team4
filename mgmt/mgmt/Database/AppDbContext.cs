using mgmt.Users;
using mgmt.Teams;
using Microsoft.EntityFrameworkCore;
namespace mgmt.Database;
public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {}

    public DbSet<User> Users { get; set; }
    
    public DbSet<Team> Teams { get; set; }
}