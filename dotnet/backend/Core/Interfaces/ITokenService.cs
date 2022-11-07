using modkaz.Backend.Models.Entity;
using Org.Ktu.T120B178.Backend.Models;

namespace modkaz.Backend.Interfaces;

public interface ITokenService
{
    Task<bool> ValidateAccessTokenAsync(string accessToken);
    Task<bool> ValidateRefreshTokenAsync(string refreshToken, UserBindingModel userDetails);
    Task<Token> GenerateTokenAsync(UserBindingModel userDetails);
    Task<UserBindingModel> DecodeAccessTokenAsync(string accessToken);
}