using Microsoft.AspNetCore.Mvc;
using modkaz.DBs;
using modkaz.Backend.Models.Entity;

namespace modkaz.Backend.Controllers.Post;

/// <summary>
/// <para>Implements restfull API for working with entities</para>
/// <para>Thread safe.</para>
/// </summary>
[ApiController]
[Route("backend/review")]
public class ReviewController : ControllerBase
{
	/// <summary>
	/// Logger.
	/// </summary>
	private readonly ILogger<ReviewController> _logger;

	private readonly MyDatabase _database;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="logger">Logger to use. Injected.</param>
	/// <param name="database">Database context</param>
	public ReviewController(ILogger<ReviewController> logger, MyDatabase database)
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
	[ProducesResponseType(typeof(List<ReviewForListing>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult List()
	{
		_logger.LogInformation("Got request to /backend/review/list");
		
		var reviews = _database.Reviews
			.OrderBy(it => it.id)
			.Select(it => ReviewForListing.DatabaseToObject(it))
			.ToList();
		
		var users = _database.Users
			.OrderBy(it => it.id)
			.Select(it => UserBindingModel.DatabaseToObject(it))
			.ToList();
		
		foreach (var review in reviews)
		{
			foreach (var user in users.Where(user => review.FK_UserID == user.Id))
			{
				review.UserName = user.Name;
			}
		}
		
		_logger.LogInformation("Listed {Count} entities", reviews.Count);
		
		return Ok(reviews);
	}
}
