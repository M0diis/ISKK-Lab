using modkaz.Backend.Interfaces;
using modkaz.Backend.Models.Entity;
using Org.Ktu.T120B178.Backend.Models;

namespace modkaz.Backend.Services;

public class AuthenticationService : IAuthenticationService
{   
    private readonly ITokenService _tokenService;
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _hasher;

    public AuthenticationService(
        IPasswordHasher hasher, ITokenService tokenService, IUsersRepository usersRepository)
    {
        _hasher = hasher;
        _tokenService = tokenService;
        _usersRepository = usersRepository;
    }

    public async Task<AuthenticatedUser> AuthenticateAsync(Credentials credentials)
    {
        var user = _usersRepository
            .FindByCondition(u => u.Username == credentials.Username)
            .SingleOrDefault();

        if (user == null)
        {
            return null;
        }
        
        var valid = await _hasher.ValidateHashAsync(credentials.Password, user.Password);

        if (valid == null)
        {
            return null;
        }

        var userDetails = new UserBindingModel 
        {
            Id = user.Id,
            Name = user.Username,
            Email = user.Email,
            Admin = user.Admin
        };

        var token = await _tokenService.GenerateTokenAsync(userDetails);
       
        return new AuthenticatedUser
        {
            User = userDetails,
            Token = token
        };
    }

    public async Task<AuthenticatedUser> RefreshAsync(Token token)
    {
        var isAccessTokenValid = await _tokenService.ValidateAccessTokenAsync(token.AccessToken);

        if (!isAccessTokenValid)
        {
            return null;
        }

        var userDetails = await _tokenService.DecodeAccessTokenAsync(token.AccessToken);

        if (userDetails == null)
        {
            return null;
        }

        var isRefreshTokenValid = await _tokenService.ValidateRefreshTokenAsync(token.RefreshToken, userDetails);

        if (!isRefreshTokenValid)
        {
            return null;
        }

        var refreshedToken = await _tokenService.GenerateTokenAsync(userDetails);

        return new AuthenticatedUser
        {
            User = userDetails,
            Token = refreshedToken
        };
    }
}