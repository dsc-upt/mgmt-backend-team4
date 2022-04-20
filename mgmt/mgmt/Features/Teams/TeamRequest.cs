using System.ComponentModel.DataAnnotations;

namespace mgmt.Features.Teams;

public class TeamRequest
{
    [Required]
    public string TeamLead { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string GithubLink { get; set; }
}