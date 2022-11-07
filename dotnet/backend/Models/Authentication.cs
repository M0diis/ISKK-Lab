namespace modkaz.Backend.Models.Authentication;

/// <summary>
/// Response to the successfull login request.
/// </summary>
public class LogInResponse
{
	/// <summary>
	/// User ID.
	/// </summary>
	public int UserId { get; set;}

	/// <summary>
	/// User title.
	/// </summary>
	public string UserTitle { get; set;}

	/// <summary>
	/// JWT for subsequent authentication.
	/// </summary>
	public string Jwt { get; set;}
	
	/// <summary>
	/// Is Admin.
	/// </summary>
	public bool IsAdmin { get; set;}
}