using Microsoft.AspNetCore.Mvc;
using modkaz.Backend.Interfaces.Service;
using modkaz.Backend.Models;

namespace modkaz.Backend.Controllers;

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
	
	/// <summary>
	/// Users service.
	/// </summary>
	private readonly IUsersService _usersService;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="logger">Logger to use. Injected.</param>
	/// <param name="reviewsService">Service for working with reviews</param>
	/// <param name="usersService">Service for working with users</param>
	public ReviewController(ILogger<ReviewController> logger, IReviewsService reviewsService, IUsersService usersService)
	{
		_logger = logger;
		_reviewsService = reviewsService;
		_usersService = usersService;
	}

	/// <summary>
	/// List entities.
	/// </summary>
	/// <returns>A list of entities.</returns>
	/// <response code="500">On exception.</response>
	[HttpGet("list")]
	[ProducesResponseType(typeof(List<ReviewForListing>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> List()
	{
		_logger.LogInformation("Got request to /backend/review/list");
		
		var users = (await _usersService.GetAllAsync())
			.OrderBy(it => it.id)
			.Select(UserForListing.DatabaseToObject)
			.ToList();
		
		var reviews = (await _reviewsService.GetAllAsync())
			.OrderBy(it => it.id)
			.Select(ReviewForListing.DatabaseToObject)
			.ToList();

		foreach (var review in reviews)
		{
			foreach (var user in users.Where(user => review.FK_UserID == user.Id))
			{
				review.UserName = user.Name;
			}
		}
		
		_logger.LogInformation("Listed {Count} review entities", reviews.Count);
		
		return Ok(reviews);
	}
}
