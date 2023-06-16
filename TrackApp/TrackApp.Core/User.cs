using System.ComponentModel.DataAnnotations;

namespace TrackApp.Core;

public class User
{
    [Key] public int UserId { get; set; }

    [Required, MinLength(3), MaxLength(10)]
    public string FirstName { get; set; }

    [Required, MinLength(3), MaxLength(15)]
    public string LastName { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
}