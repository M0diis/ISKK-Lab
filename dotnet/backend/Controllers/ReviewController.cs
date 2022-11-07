using Microsoft.AspNetCore.Mvc;
using modkaz.Backend.Interfaces;
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
	
	/// <summary>
	/// Reviews service.
	/// </summary>
	private readonly IReviewsService _reviewsService;

	private readonly MyDatabase _database;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="logger">Logger to use. Injected.</param>
	/// <param name="database">Database context</param>
	/// <param name="reviewsService">Service for working with reviews</param>
	public ReviewController(ILogger<ReviewController> logger, MyDatabase database, IReviewsService reviewsService)
	{
		_logger = logger;
		_database = database;
		_reviewsService = reviewsService;
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
	public async Task<IActionResult> List()
	{
		_logger.LogInformation("Got request to /backend/review/list");
		
		var users = _database.Users
			.OrderBy(it => it.id)
			.Select(it => UserForListing.DatabaseToObject(it))
			.ToList();
		
		var reviews = (await _reviewsService.GetReviewsAsync())
			.OrderBy(it => it.id)
			.Select(it => ReviewForListing.DatabaseToObject(it))
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
