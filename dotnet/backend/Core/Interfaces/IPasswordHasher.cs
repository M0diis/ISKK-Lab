namespace modkaz.Backend.Interfaces;

public interface IPasswordHasher
{
    Task<string> HashAsync(string value);
    Task<bool> ValidateHashAsync(string raw, string hash);
}