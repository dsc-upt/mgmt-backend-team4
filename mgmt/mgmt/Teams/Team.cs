using mgmt.Users;

namespace mgmt.Teams;

public class Team : Entity
{
    public User TeamLead { get; set; }
    public string Name { get; set; }
    public string GithubLink { get; set; }
}