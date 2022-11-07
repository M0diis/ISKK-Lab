using Org.Ktu.T120B178.Backend.Models;

namespace modkaz.Backend.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticatedUser> AuthenticateAsync(Credentials credentials);
    Task<AuthenticatedUser> RefreshAsync(Token token);
}