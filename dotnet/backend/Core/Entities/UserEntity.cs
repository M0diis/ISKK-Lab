namespace modkaz.Backend.Entities;

public class UserEntity
{   
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Admin { get; set; }

    public RefreshToken RefreshToken { get; set; }
}