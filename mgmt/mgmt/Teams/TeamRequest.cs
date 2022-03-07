using System.ComponentModel.DataAnnotations;
using mgmt.Users;

namespace mgmt.Teams;

public class TeamRequest
{
    [Required]
    public string TeamLead { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string GithubLink { get; set; }
}