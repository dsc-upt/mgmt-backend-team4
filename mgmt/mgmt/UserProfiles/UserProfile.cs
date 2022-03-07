using System.ComponentModel.DataAnnotations;
using mgmt.Teams;
using mgmt.Users;

namespace mgmt.UserProfiles;

public class UserProfile : Entity
{
    
     [Required] public User User { get; set; }

     [Required] public ICollection<Team> Teams { get; set; }

     [Required] public string FacebookLink { get; set; }

     [Required] [Phone] public string Phone { get; set; }
     
     [Required] public DateOnly Birthday { get; set; }
     
     [Required] public string  Picture { get; set; }

}