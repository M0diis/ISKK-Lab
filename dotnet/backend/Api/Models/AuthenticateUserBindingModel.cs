using System.ComponentModel.DataAnnotations;

namespace Org.Ktu.T120B178.Backend.Api.Models;

public class AuthenticateUserBindingModel
{
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } 
}