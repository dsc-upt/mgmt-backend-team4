using System.ComponentModel.DataAnnotations;
using mgmt.Base;
using mgmt.Features.Clients;
using mgmt.Features.Teams;
using mgmt.Features.Users;

namespace mgmt.Features.Projects;

public class Project : Entity
{
    [Required] public string Name { get; set; }
    [Required] public User Manager { get; set; }
    [Required] public string Status { get; set; }
    [Required] public Client Client { get; set; }
    [Required] public ICollection<Team> Teams { get; set; }
}