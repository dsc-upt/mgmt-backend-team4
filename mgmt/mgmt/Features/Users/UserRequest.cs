using System.ComponentModel.DataAnnotations;

namespace mgmt.Features.Users;

public class UserRequest
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public List<string> Roles { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}