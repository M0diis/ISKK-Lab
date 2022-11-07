using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using modkaz.Backend.Models;
using modkaz.Backend.Util;
using modkaz.DBs;


namespace modkaz.Backend.Controllers.Authentication;

[ApiController]
[Route("backend/auth")]
public class AuthenticationController : ControllerBase
{
	/// <summary>
	/// Logger.
	/// </summary>
	private readonly ILogger<AuthenticationController> _logger;
	
	private readonly MyDatabase _database;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="logger">Logger. Injected.</param>
	/// <param name="database">Database context</param>
	public AuthenticationController(ILogger<AuthenticationController> logger, MyDatabase database)
	{
		_logger = logger;
		_database = database;
	}

	/// <summary>
	/// Log the user in. Note that passing plaintext password through unencrypted channel is insecure.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="password">Password.</param>
	/// <returns>User data and JWT token for authorization.</returns>
	/// <response code="400">On authentication failure.</response>
	/// <response code="500">On exception.</response>
	[HttpGet("login")]
	[ProducesResponseType(typeof(LogInResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult LogIn(string username, string password)
	{
		_logger.LogInformation("Got request to /backend/auth/login");
		
		if( username == null )
			throw new ArgumentException("Argument 'username' is null.");

		if( password == null )
			throw new ArgumentException("Argument 'password' is null.");
		
		if( username == "a" && password == "b" )
		{
			// Create JWT token containing user permissions and other info
			var claimsDev = new List<Claim>
			{
				new(ClaimTypes.Role, "user"),
				new("userId", $"{1}")
			};

			var tokenDev = JwtUtil.CreateToken(claimsDev, Config.JwtSecret, TimeSpan.FromHours(8));
			var tokenStringDev = JwtUtil.SerializeToken(tokenDev);
			
			var respDev = new LogInResponse
			{
				UserId = 1,
				UserTitle = $"{username}",
				Jwt = tokenStringDev
			};

			return Ok(respDev);
		}

		var user = _database
			.Users.Where(x => x.name == username && x.password == password)
			.ToList().First();

		if (user == null)
		{
			return BadRequest("Invalid login credentials.");
		}
		
		_logger.LogInformation("User {Username} logged in", user.name);
		
		// Create JWT token containing user permissions and other info
		var claims = new List<Claim>
		{
			new(ClaimTypes.Role, "user"),
			new("userId", $"{user.id}")
		};

		var token = JwtUtil.CreateToken(claims, Config.JwtSecret, TimeSpan.FromHours(8));
		var tokenString = JwtUtil.SerializeToken(token);
		
		var resp = new LogInResponse
		{
			UserId = user.id,
			UserTitle = $"{user.name}",
			IsAdmin = user.admin,
			Jwt = tokenString
		};

		return Ok(resp);
	}

	/// <summary>
	/// Log the user out. This should invalidate current JWT's by advancing some kind of user 
	/// bound counter that is also passed in JWT's and checked in authentication step.
	/// </summary>
	[HttpGet("logout")]
	public void LogOut(string jwt)
	{
		_logger.LogInformation("Got request to /backend/auth/logout with JWT: {Jwt}", jwt);
	}	
}
