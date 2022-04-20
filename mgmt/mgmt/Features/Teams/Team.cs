using mgmt.Base;
using mgmt.Features.Users;

namespace mgmt.Features.Teams;

public class Team : Entity
{
    public User TeamLead { get; set; }
    public string Name { get; set; }
    public string GithubLink { get; set; }
}