using mgmt.Features.Clients;
using mgmt.Features.Projects;
using mgmt.Features.Teams;
using mgmt.Features.UserProfiles;
using mgmt.Features.Users;
using mgmt.Features.Workshops;
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
    
    public DbSet<Project> Projects { get; set; }
    
    public DbSet<Workshop> Workshops { get; set; }
}