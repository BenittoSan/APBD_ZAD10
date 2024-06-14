using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Models.Authorization;

public class RegisterRequest
{
    [EmailAddress]
    public string Email { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}