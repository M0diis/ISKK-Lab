namespace modkaz.Backend.Entities;

public class RefreshToken
{
    public string Value { get; set; }
    public bool IsBlacklisted { get; set; }
        
    public long UserId { get; set; }
    public UserEntity User { get; set; }
}