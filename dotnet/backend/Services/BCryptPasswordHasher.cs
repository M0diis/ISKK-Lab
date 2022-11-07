using modkaz.Backend.Interfaces;

namespace modkaz.Backend.Services;

internal class BCryptPasswordHasher : IPasswordHasher
{
    public async Task<string> HashAsync(string value)
    {
        var hashed = BCrypt.Net.BCrypt.HashPassword(value, GetRandomSalt());
        return BCrypt.Net.BCrypt.HashPassword(value, GetRandomSalt());
    }

    public async Task<bool> ValidateHashAsync(string raw, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(raw, hash);
    }

    private static string GetRandomSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt(12);
    }
}