using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using modkaz.Backend.Interfaces;
using modkaz.Backend.Interfaces.Service;
using modkaz.Backend.Models;

namespace modkaz.Backend.Controllers;

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

	/// <summary>
	/// Reviews service.
	/// </summary>
	private readonly ITicketsService _ticketsService;
	
	/// <summary>
	/// Users service.
	/// </summary>
	private readonly IUsersService _usersService;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="logger">Logger to use. Injected.</param>
	/// <param name="ticketsService">Tickets service to use. Injected.</param>
	/// <param name="usersService">Users service to use. Injected.</param>
	public TicketController(ILogger<TicketController> logger, ITicketsService ticketsService, IUsersService usersService)
	{
		_logger = logger;
		_ticketsService = ticketsService;
		_usersService = usersService;
	}

	/// <summary>
	/// List entities.
	/// </summary>
	/// <returns>A list of entities.</returns>
	/// <response code="500">On exception.</response>
	[HttpGet("list")]
	[ProducesResponseType(typeof(List<TicketForListing>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> List()
	{
		_logger.LogInformation("Got request to /backend/review/list");
		
		var tickets = (await _ticketsService.GetAllAsync())
			.OrderBy(it => it.id)
			.Select(TicketForListing.DatabaseToObject)
			.ToList();
		
		var users = (await _usersService.GetAllAsync())
			.OrderBy(it => it.id)
			.Select(UserForListing.DatabaseToObject)
			.ToList();
		
		foreach (var ticket in tickets)
		{
			foreach (var user in users.Where(user => ticket.FK_UserID == user.Id))
			{
				ticket.UserName = user.Name;
			}
		}
		
		_logger.LogInformation("Listed {Count} ticket entities", tickets.Count);
		
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
	[Authorize(Roles = "user")]
	[ProducesResponseType(typeof(List<TicketForListing>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Load(int? userId)
	{
		_logger.LogInformation("Got request to /backend/review/list/by-user with userId={UserId}", userId);
		
		if (userId == null)
		{
			throw new ArgumentException("Argument 'userId' is null.");
		}

		var ticketsByUser = (await _ticketsService.GetTicketsByUserIdAsync(userId.Value))
			.Select(TicketForListing.DatabaseToObject)
			.ToList();

		_logger.LogInformation("Loaded {Count} ticket entities", ticketsByUser.Count);

		return Ok(ticketsByUser);
	}
}
