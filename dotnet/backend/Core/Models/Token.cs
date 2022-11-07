namespace Org.Ktu.T120B178.Backend.Models;

public class Token 
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expires { get; set; }
}