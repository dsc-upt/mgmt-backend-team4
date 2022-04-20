using System.ComponentModel.DataAnnotations;
using mgmt.Base;
using mgmt.Features.Users;

namespace mgmt.Features.Clients;

public class Client : Entity
{
    [Required] public string Name { get; set; }
    
    [Required] public User ContactPerson { get; set; }
    
    [Required] [EmailAddress] public string Email { get; set; }
    
    [Required] [Phone] public string Phone { get; set; }
}