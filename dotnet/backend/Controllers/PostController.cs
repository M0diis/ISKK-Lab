using Microsoft.AspNetCore.Mvc;
using modkaz.DBs;
using modkaz.Backend.Models.Entity;

namespace modkaz.Backend.Controllers.Post;

/// <summary>
/// <para>Implements restfull API for working with entities</para>
/// <para>Thread safe.</para>
/// </summary>
[ApiController]
[Route("backend/post")]
public class PostController : ControllerBase
{
	/// <summary>
	/// Logger.
	/// </summary>
	private readonly ILogger<PostController> _logger;

	private readonly MyDatabase _database;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="logger">Logger to use. Injected.</param>
	/// <param name="database">Database context</param>
	public PostController(ILogger<PostController> logger, MyDatabase database)
	{
		_logger = logger;
		_database = database;
	}

	/// <summary>
	/// List entities.
	/// </summary>
	/// <returns>A list of entities.</returns>
	/// <response code="500">On exception.</response>
	[HttpGet("list")]
	// [Authorize(Roles = "user")]
	[ProducesResponseType(typeof(List<PostForListing>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult List()
	{
		_logger.LogInformation("Got request to /backend/post/list");

		var posts = _database.Posts
			.OrderBy(it => it.id)
			.Select(it => PostForListing.DatabaseToObject(it))
			.ToList();
		
		var users = _database.Users
			.OrderBy(it => it.id)
			.Select(it => UserForListing.DatabaseToObject(it))
			.ToList();
		
		foreach (var post in posts)
		{
			foreach (var user in users.Where(user => post.FK_UserID == user.Id))
			{
				post.UserName = user.Name;
			}
		}
		
		_logger.LogInformation("Listed {Count} entities", posts.Count);
		
		return Ok(posts);
	}
}
