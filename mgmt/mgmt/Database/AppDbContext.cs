using mgmt.Clients;
using mgmt.Users;
using mgmt.Teams;
using mgmt.UserProfiles;
using Microsoft.EntityFrameworkCore;
namespace mgmt.Database;
public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {}

    public DbSet<User> Users { get; set; }
    
    public DbSet<Team> Teams { get; set; }
    
    public DbSet<UserProfile> UserProfiles { get; set; }
    
    public DbSet<Client> Clients { get; set; }
}