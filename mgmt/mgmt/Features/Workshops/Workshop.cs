using System.ComponentModel.DataAnnotations;
using mgmt.Base;
using mgmt.Features.Users;

namespace mgmt.Features.Workshops;

public class Workshop : Entity
{
    [Required] public User Trainer { get; set; }
    [Required] public string Topic { get; set; }
    [Required] public string Description { get; set; }
    [Required] public string CoverImage { get; set; }
    [Required] public DateTime DateStart { get; set; }
    [Required] public DateTime DateEnd { get; set; }
    [Required] public int Capacity { get; set; }
    [Required] public string Location { get; set; }
    [Required] public User[] Participants { get; set; }
    [Required] public string Presentation { get; set; }
}