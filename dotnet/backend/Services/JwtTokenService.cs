using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using modkaz.Backend.Entities;
using modkaz.Backend.Interfaces;
using modkaz.Backend.Models.Entity;
using Org.Ktu.T120B178.Backend.Models;

namespace modkaz.Backend.Services;

    public class JwtTokenService : ITokenService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public JwtTokenService(IOptions<AuthenticationSettings> authenticationSettings, IRefreshTokenRepository refreshTokenRepository)
        {
            this._authenticationSettings = authenticationSettings.Value;
            this._refreshTokenRepository = refreshTokenRepository;
            this._tokenHandler = new JwtSecurityTokenHandler();
        }        

        public async Task<bool> ValidateAccessTokenAsync(string accessToken)
        {
            var canReadAccessToken = this._tokenHandler.CanReadToken(accessToken);

            if (!canReadAccessToken) 
            {
                return false;
            }

            // Not sure if this takes expiration into account?
            SecurityToken validatedToken; 
            var key = Encoding.ASCII.GetBytes(this._authenticationSettings.Secret);
            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var user = this._tokenHandler.ValidateToken(accessToken, parameters, out validatedToken);

            return validatedToken != null;
        }

        public async Task<bool> ValidateRefreshTokenAsync(string refreshToken, UserDetails userDetails)
        {
            var refreshTokenValue = this._refreshTokenRepository
                .FindByCondition(e => !e.IsBlacklisted && e.Value == refreshToken && e.UserId == userDetails.Id)
                .Select(e => e.Value)
                .SingleOrDefault();
            
            return refreshTokenValue != null;
        }

        public async Task<Token> GenerateTokenAsync(UserBindingModel userDetails)
        {
            var key = Encoding.ASCII.GetBytes(this._authenticationSettings.Secret);
            var expires = DateTime.UtcNow.AddDays(7);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userDetails.Id.ToString()),
                    new Claim(ClaimTypes.Name, userDetails.Name.ToString()),
                    new Claim(ClaimTypes.Email, userDetails.Email.ToString()),
                    new Claim(ClaimTypes.Role, userDetails.Role.ToString())
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = this._tokenHandler.CreateToken(tokenDescriptor);

            var refreshTokenValue = this._refreshTokenRepository
                .FindByCondition(e => !e.IsBlacklisted && e.User.Id == userDetails.Id)
                .Select(e => e.Value)
                .SingleOrDefault();

            if (refreshTokenValue == null)
            {
                refreshTokenValue = this.GenerateRefreshToken();
                var refreshToken = new RefreshToken {
                    Value = refreshTokenValue,
                    UserId = userDetails.Id
                };

                this._refreshTokenRepository.Create(refreshToken);
            } 

            return new Token { 
                AccessToken = this._tokenHandler.WriteToken(accessToken),
                RefreshToken = refreshTokenValue,
                Expires = expires
            };
        }

        public async Task<UserBindingModel> DecodeAccessTokenAsync(string accessToken)
        {
            var tokenCanBeRead = this._tokenHandler.CanReadToken(accessToken);
            
            if (!tokenCanBeRead)
            {
                return null;
            }

            SecurityToken validatedToken; 
            
            var key = Encoding.ASCII.GetBytes(this._authenticationSettings.Secret);
            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var user = this._tokenHandler.ValidateToken(accessToken, parameters, out validatedToken);

            return ToUserDetails(user);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private UserBindingModel ToUserDetails(ClaimsPrincipal principal)
        {
            var id = Convert.ToInt32(principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var username = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var role = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            return new UserBindingModel 
            {
                Id = id,
                Name = username,
                Admin = role,
                Email = email
            };
        }
    
    }