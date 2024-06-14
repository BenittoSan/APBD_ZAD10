using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Models;

public class User
{
    [Key] public int IdUser { get; set; }
    [Required] public string Login { get; set; }
    [Required,MaxLength(256)] public string Password { get; set; }
    
    [EmailAddress,Required] public string Email { get; set; }
    [Required] public string Salt { get; set; }
    [Required] public string RefreshToken { get; set; }
    [Required] public DateTime RefreshTokenExp { get; set; }
}