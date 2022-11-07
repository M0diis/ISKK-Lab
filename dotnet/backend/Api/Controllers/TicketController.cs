using Microsoft.AspNetCore.Mvc;
using modkaz.DBs;
using modkaz.Backend.Models.Entity;

namespace modkaz.Backend.Controllers.Post;

/// <summary>
/// <para>Implements restfull API for working with entities</para>
/// <para>Thread safe.</para>
/// </summary>
[ApiController]
[Route("backend/ticket")]
public class TicketController : ControllerBase
{
	/// <summary>
	/// Logger.
	/// </summary>
	private readonly ILogger<TicketController> _logger;

	private readonly MyDatabase _database;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="logger">Logger to use. Injected.</param>
	/// <param name="database">Database context</param>
	public TicketController(ILogger<TicketController> logger, MyDatabase database)
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
	[ProducesResponseType(typeof(List<TicketForListing>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult List()
	{
		_logger.LogInformation("Got request to /backend/review/list");
		
		var tickets = _database.Tickets
			.OrderBy(it => it.id)
			.Select(it => TicketForListing.DatabaseToObject(it))
			.ToList();
		
		var users = _database.Users
			.OrderBy(it => it.id)
			.Select(it => UserBindingModel.DatabaseToObject(it))
			.ToList();
		
		foreach (var ticket in tickets)
		{
			foreach (var user in users.Where(user => ticket.FK_UserID == user.Id))
			{
				ticket.UserName = user.Name;
			}
		}
		
		_logger.LogInformation("Listed {Count} entities", tickets.Count);
		
		return Ok(tickets);
	}
	
	/// <summary>
	/// Loads data for a single entity.
	/// </summary>
	/// <param name="userId">ID of the ticket to load.</param>
	/// <returns>Data of entity loaded.</returns>
	/// <response code="404">If entity with given ID can't be loaded.</response>
	/// <response code="500">On exception.</response>
	[HttpGet("list/by-user")]
	// [Authorize(Roles = "user")]
	[ProducesResponseType(typeof(List<EntityForCU>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult Load(int? userId)
	{
		_logger.LogInformation("Got request to /backend/review/list/by-user with userId={UserId}", userId);
		
		if (userId == null)
		{
			throw new ArgumentException("Argument 'userId' is null.");
		}

		var ticketsByUser = _database.Tickets
			.Where(it => it.fk_userId == userId)
			.Select(it => TicketForListing.DatabaseToObject(it))
			.ToList();

		_logger.LogInformation("Loaded {Count} entities", ticketsByUser.Count);

		return Ok(ticketsByUser);
	}
}
