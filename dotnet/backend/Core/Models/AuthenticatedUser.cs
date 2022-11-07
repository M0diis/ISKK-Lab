using modkaz.Backend.Models.Entity;

namespace Org.Ktu.T120B178.Backend.Models;

public class AuthenticatedUser
{
    public UserBindingModel User { get; set; }
    public Token Token { get; set; }
}