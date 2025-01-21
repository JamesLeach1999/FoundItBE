using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FinderBE.Models;

public class User
{
    [AllowNull]
    public Guid UserId = Guid.NewGuid();

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }

    [AllowNull]
    public string PhoneNumber { get; set; }

    public DateTime AccountCreatedDate  = DateTime.Now;
}
