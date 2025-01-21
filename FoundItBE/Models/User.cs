using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FoundItBE.Models;

public class User
{
    [AllowNull]
    public Guid UserId { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }

    [AllowNull]
    public string PhoneNumber { get; set; }

    public DateTime AccountCreatedDate {  get; set; }
}
