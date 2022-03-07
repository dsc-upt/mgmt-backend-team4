using System.ComponentModel.DataAnnotations;
using mgmt.Users;

namespace mgmt.Clients;

public class ClientRequest
{
    [Required] public string Name { get; set; }
    
    [Required] public string ContactPerson { get; set; }
    
    [Required] [EmailAddress] public string Email { get; set; }
    
    [Required] [Phone] public string Phone { get; set; }
}