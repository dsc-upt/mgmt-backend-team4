using System.ComponentModel.DataAnnotations;
using mgmt.Base;

namespace mgmt.Features.Users;

public class User : Entity
{
    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public List<string> Roles { get; set; }

}